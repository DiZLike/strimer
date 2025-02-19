using streamer.cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass.AddOn.Tags;

namespace strimer.cs
{
    public class ReplayGain
    {
        private bool _use_replace_gain = false;
		private bool _use_custom_gain = false;
        private TAG_INFO _tag_info = null!;
        private int _mixer = 0;
        private int _fx_gain_handle = 0;
        private int _fx_limiter_handle = 0;
        private BASS_BFX_COMPRESSOR2 _gain;
        private BASS_BFX_COMPRESSOR2 _limiter;
        public ReplayGain(bool use_replace_gain, bool use_custom_gain, int mixer)
        {
            _use_replace_gain = use_replace_gain;
            _mixer = mixer;
            CreateGainControl();
            CreateLimiter();
        }
        public void SetGain(TAG_INFO tag_info)
        {
            _tag_info = tag_info;
        }
        public void ApplyGain()
        {
            _gain.fAttack = 0.01f;
            _gain.fRatio = 100;
            _gain.fThreshold = 0;
            _gain.fRelease = 250;
            _gain.fGain = _tag_info.replaygain_track_gain;
            Bass.BASS_FXSetParameters(_fx_gain_handle, _gain);
            string error = App.GetErrorMessage();

            //ApplyLimiter();
        }
        public void ApplyLimiter()
        {
            _limiter.fAttack = 0.01f;
            _limiter.fRelease = 200;
            _limiter.fGain = 5;
            _limiter.fRatio = 100;
            _limiter.fThreshold = -15;
            Bass.BASS_FXSetParameters(_fx_limiter_handle, _limiter);

            string error = App.GetErrorMessage();
        }
        private void CreateGainControl()
        {
            _fx_gain_handle = Bass.BASS_ChannelSetFX(_mixer, BASSFXType.BASS_FX_BFX_COMPRESSOR2, 2);
            _gain = new();
        }
        private void CreateLimiter()
        {
            _fx_limiter_handle = Bass.BASS_ChannelSetFX(_mixer, BASSFXType.BASS_FX_BFX_COMPRESSOR2, 1);
            _limiter = new();
        }
    }
}
