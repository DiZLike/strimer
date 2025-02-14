using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using streamer.cs;
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
            int stream = Bass.BASS_StreamCreateFile("C:\\Users\\Evgeny\\Desktop\\pls\\Axiom Verge Cover1.opus", 0, 0, BASSFlag.BASS_STREAM_DECODE);
            //int stream = Bass.BASS_StreamCreateFile("C:\\Users\\Evgeny\\Desktop\\pls\\Electronic Super Joy _ Groove City - Ginseng.mp3", 0, 0, BASSFlag.BASS_STREAM_DECODE);
            //var v = Bass.BASS_ChannelGetLevel

            short[] buffer = new short[1024 * 3];
            int bytesRead;

            List<short> pcmData = new List<short>();

            // читаем дату
            while ((bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length)) > 0)
            {
                pcmData.AddRange(buffer);

            }


            
            double[] amplitude = ConvertPcmToAmplitude(pcmData.ToArray());

            // смотрим максимум
            double max = -120;
            double sum = 0;
            for (int i = 0; i < amplitude.Length; i++)
            {
                if (amplitude[i] > max)
                    max = amplitude[i];
                sum += amplitude[i];
            }

            double rms = Math.Sqrt(sum / amplitude.Length);
            double rms_db = ConvertAmplitudeToDecibels(rms) * -1;
            double replayGain = 20 * Math.Log10(rms_db);
            //double replayGain = 20 * Math.Log10(rms);
        }
        static double[] ConvertPcmToAmplitude(short[] pcmData)
        {
            // Максимальное значение для 16-битных PCM-данных
            const int maxPcmValue = 32768;

            // Массив для хранения значений в децибелах
            double[] amplitudes = new double[pcmData.Length];

            for (int i = 0; i < pcmData.Length; i++)
            {
                // Нормализация к диапазону [-1.0, 1.0]
                double normalizedValue = pcmData[i] / (double)maxPcmValue;

                // Амплитуда — это абсолютное значение
                double amplitude = Math.Abs(normalizedValue);

                amplitudes[i] = amplitude;
            }

            return amplitudes;
        }
        static double ConvertAmplitudeToDecibels(double amplitude)
        {
            double decibels = -120;
            if (amplitude > 0)
            {
                decibels = 20 * Math.Log10(amplitude);
            }
            else
            {
                // Если амплитуда равна нулю, устанавливаем минимальное значение (например, -∞ или -120 дБ)
                decibels = -120; // или -120
            }
            return decibels;
        }
    }
}
