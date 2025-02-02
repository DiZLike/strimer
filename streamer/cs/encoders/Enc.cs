using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.EncOpus;
using Un4seen.Bass.AddOn.Opus;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace strimer.cs.encoders
{
    internal class Enc
    {
		public BaseEncoder Encoder { get => _encoder; set => _encoder = value; }
		public Mixer? Mixer { get => _mixer; set => _mixer = value; }
		public int Bitrate { get => bitrate; set => bitrate = value; }
		public int Encoder_handle { get => encoder_handle; set => encoder_handle = value; }
		public string Content { get => content; set => content = value; }

		private protected BaseEncoder _encoder;
		private protected Mixer? _mixer;

		private protected string exe = "exe";
		private protected string current_enc = String.Empty;
		private protected string prefix_exe = String.Empty;

		private protected int bitrate_ind = 0;
		private protected int bitrate_mode_ind = 0;
		private protected int content_type_ind = 0;

		private protected int bitrate = 0;
		private protected int complexity = 0;
		private protected int framesize_ind = 0;

		private protected string bitrate_mode = String.Empty;
		private protected string content_type = String.Empty;
		private protected string framesize = String.Empty;
		private protected string content = String.Empty;

		private protected int encoder_handle = 0;

		public void SetExe()
        {
            if (current_enc == "opus")
            {
                if (App.Arc.ToLower() == "x64")
                    exe = Path.Combine(App.app_dir, @"encs\opus\win64\opusenc.exe");
                else if (App.Arc.ToLower() == "x86")
					exe = Path.Combine(App.app_dir, @"encs\opus\win32\opusenc.exe");
                else if (App.Arc.ToLower() == "arm" || App.Arc == "arm64")
					exe = Path.Combine(App.app_dir, @"opusenc");
			}
        }
		public void SetTitle(string artist, string title)
		{
			string text = $"--artist \"{artist}\" --title \"{title}\"";
			string options = $"{exe} --bitrate {bitrate} --{bitrate_mode} --{content_type} --comp{complexity} --framesize {framesize} {text} - -";
			bool ok = BassEnc_Opus.BASS_Encode_OPUS_NewStream(encoder_handle, options, BASSEncode.BASS_ENCODE_FP_16BIT);
		}
	}
}
