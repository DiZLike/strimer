using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.EncOpus;
using Un4seen.Bass.AddOn.Opus;

namespace strimer.cs.encoders
{
    internal class Enc
    {
        public string exe = "exe";
        public static string current_enc = String.Empty;
        public string prefix_exe = String.Empty;
        public int bitrate_ind = 0;
        public int bitrate_mode_ind = 0;
        public int content_type_ind = 0;
        public int complexity = 0;
        public int framesize_ind = 0;

        public string bitrate = String.Empty;
        public string bitrate_mode = String.Empty;
        public string content_type = String.Empty;
        public string framesize = String.Empty;

        public int enc_handle = 0;
        public string content = String.Empty;
        public Mixer? mixer;

        public string artist = String.Empty;
        public string title = String.Empty;

        public void SetExe()
        {
            if (current_enc == "opus")
            {
                if (App.Arc == "X64")
                    exe = Path.Combine(App.app_dir, @"encs\opus\win64\opusenc");
                else if (App.Arc == "X86")
					exe = Path.Combine(App.app_dir, @"encs\opus\win32\opusenc");
                else if (App.Arc == "ARM" || App.Arc == "ARM64")
					exe = Path.Combine(App.app_dir, @"opusenc");
			}
        }
		public void SetTitle(string artist, string title)
		{
			this.artist = artist;
			this.title = title;
			if (current_enc == "opus")
            {
                bool ok = BassEnc_Opus.BASS_Encode_OPUS_NewStream(enc_handle, $"--artist \"{artist}\" --title \"{title}\"" +
                    $" --bitrate {bitrate} --{bitrate_mode} --{content_type} --comp {complexity}" +
                    $" --framesize {framesize}", BASSEncode.BASS_ENCODE_OPUS_RESET);
            }
		}
	}
}
