using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using streamer.cs;
using TagLib.Mpeg;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace strimer
{
    public class Test
    {
        public Test()
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            Bass.BASS_PluginLoad(Path.Combine(App.app_dir, App.pref_lib + "bassopus" + App.dot_lib));
            int stream = Bass.BASS_StreamCreateFile(@"C:\Users\Evgeny\Desktop\pls\Axiom Verge Cover1.opus", 0, 0, BASSFlag.BASS_STREAM_DECODE);

			int sample_rate = 44100;
			int channels = 2;
			short[] buffer = new short[sample_rate * channels];

            List<short> pcmData = new List<short>();

			double target = -23.0;

			int samplesRead;


            while ((samplesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length)) > 0)
				pcmData.AddRange(buffer);
			double integtatedLoadness = CalculateIntegratedLoudness(pcmData.ToArray(), sample_rate);
			double replay_gain = target - integtatedLoadness;
		}
		private double CalculateIntegratedLoudness(short[] samples16, int sampleRate)
		{
			float[] samples32 = To32bit(samples16);

			int blockSize = sampleRate * 400 / 1000;
			int numBlock = samples32.Length / blockSize;
			double sumLoudness = 0;
			for (int i = 0; i < numBlock; i++)
			{
				float[] block = new ArraySegment<float>(samples32, i * blockSize, blockSize).ToArray();
				double rms = CalculateRMS(block);
				double momentaryLoudness = -0.691 + 20 * Math.Log10(rms);
				sumLoudness += Math.Pow(10, momentaryLoudness / 10);
			}
			double integratedLoudness = -0.691 + 10 * Math.Log10(sumLoudness / numBlock);
			return integratedLoudness;
		}
		private double CalculateRMS(float[] samples)
		{
			double sumOfSquares = 0;
			for (int i = 0; i < samples.Length; i++)
				sumOfSquares += samples[i] * samples[i];
			return Math.Sqrt(sumOfSquares / samples.Length);
		}
		private float[] To32bit(short[] samples)
		{
			float[] floatSamples = new float[samples.Length];
			for (int i = 0; i < samples.Length; i++)
				floatSamples[i] = samples[i] / 32768.0f;
			return floatSamples;
		}
	}
}
