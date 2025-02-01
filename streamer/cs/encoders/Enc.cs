using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
