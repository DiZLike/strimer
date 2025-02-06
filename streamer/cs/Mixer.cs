using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;

namespace strimer.cs
{
    internal class Mixer
    {
        public int main_mixer_handle = 0;
        public Mixer(int frequency)
        {
            main_mixer_handle = BassMix.BASS_Mixer_StreamCreate(frequency, 2, BASSFlag.BASS_MIXER_NONSTOP);
        }
        public void AddStream(int stream)
        {
            App.is_error = !BassMix.BASS_Mixer_StreamAddChannel(main_mixer_handle, stream, 0);
            App.IsError();
        }
        public void RemoveStream(int stream)
        {
			App.is_error = !BassMix.BASS_Mixer_ChannelRemove(stream);
			App.IsError();
		}
    }
}
