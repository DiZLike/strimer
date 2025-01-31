using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Mix;

namespace strimer.cs
{
    internal class Mixer
    {
        public int main_mixer_handle = 0;
        public Mixer(int frequency)
        {
            main_mixer_handle = BassMix.BASS_Mixer_StreamCreate(frequency, 2, Un4seen.Bass.BASSFlag.BASS_MIXER_NONSTOP);
        }
        public void AddStream(int stream)
        {
            BassMix.BASS_Mixer_StreamAddChannel(stream, main_mixer_handle, 0);
        }
    }
}
