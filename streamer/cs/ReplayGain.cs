using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streamer.cs;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass;

namespace strimer.cs
{
    public class ReplayGain
    {
        private bool _use_replace_gain = false;
        private TAG_INFO _tag_info;
        private int _stream = 0;
        public ReplayGain(bool use_replace_gain, TAG_INFO tag_info, int stream)
        {
            _use_replace_gain = use_replace_gain;
            _tag_info = tag_info;
            _stream = stream;
        }
        public void ApplyGainToVolume()
        {
            if (_use_replace_gain)
            {
                float gain = _tag_info.replaygain_track_gain;
                float volume = (float)Math.Pow(10, gain / 20);
                bool ok = Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, volume);
                string msg = $"Replay Gain: {gain}; Volume set: {volume}";
                Console.WriteLine(msg);
                Helper.Log(msg);
            }
        }
    }
}
