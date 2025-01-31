using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.EncOpus;

namespace strimer.cs.encoders
{
    internal class EncOpus : Enc
    {
        private Mixer? _mixer;
        private readonly List<string> _bitrates = new()
        {
            "16", "24", "32", "40", "48", "56", "64", "80", "96", "128", "160", "192", "256", "320"
        };
        private readonly List<string> _bitrate_modes = new()
        {
            "vbr", "cvbr", "hard-cbr"
        };
        private readonly List<string> _content_types = new()
        {
            "music", "speech"
        };
        private readonly List<string> _frame_sizes = new()
        {
            "2.5", "5", "10", "20", "40", "60"
        };

        private int _enoder_handle = 0;
        public EncOpus(Mixer mixer)
        {
            this._mixer = mixer;
            exe = "opusenc";
            SetBitrate();
            SetBitrateMode();
            SetContentType();
            SetComplexity();
            SetFramesize();
            StartEncoding();
        }
        private void SetBitrate()
        {
            // битрейт
            Helper.Println("i_bitrate");
            for (int i = 0; i < _bitrates.Count; i++)
                Console.WriteLine(i + ": " + _bitrates[i]);
            do
            {
                bitrate_ind = Helper.InputIntln();
                if (bitrate_ind >= _bitrates.Count)
                    Helper.Println("no_bitrate");
                else if (bitrate_ind < 0)
                {
                    bitrate_ind = 9;
                    break;
                }
                else break;
            } while (true);
            bitrate = _bitrates[bitrate_ind];
            Helper.SetParam("opus.bitrate", _bitrates[bitrate_ind]);
        }
        private void SetBitrateMode()
        {
            // режим
            Helper.Println("i_bitrate_mode");
            for (int i = 0; i < _bitrate_modes.Count; i++)
                Console.WriteLine(i + ": " + _bitrate_modes[i]);
            do
            {
                bitrate_mode_ind = Helper.InputIntln();
                if (bitrate_mode_ind >= _bitrate_modes.Count)
                    Helper.Println("no_bitrate_mode");
                else if (bitrate_mode_ind < 0)
                {
                    bitrate_mode_ind = 0;
                    break;
                }
                else break;
            } while (true);
            bitrate_mode = _bitrate_modes[bitrate_mode_ind];
            Helper.SetParam("opus.bitrate_mode", _bitrate_modes[bitrate_mode_ind]);
        }
        private void SetContentType()
        {
            // тип контента
            Helper.Println("i_content_type");
            for (int i = 0; i < _content_types.Count; i++)
                Console.WriteLine(i + ": " + _content_types[i]);
            do
            {
                content_type_ind = Helper.InputIntln();
                if (content_type_ind >= _content_types.Count)
                    Helper.Println("no_content_type");
                else if (content_type_ind < 0)
                {
                    content_type_ind = 0;
                    break;
                }
                else break;
            } while (true);
            content_type = _content_types[content_type_ind];
            Helper.SetParam("opus.content_type", _content_types[content_type_ind]);
        }
        private void SetComplexity()
        {
            // сложность
            Helper.Println("i_complexity");
            do
            {
                complexity = Helper.InputIntln();
                if (complexity > 10)
                    Helper.Println("no_complexity");
                else if (complexity < 0)
                {
                    complexity = 10;
                    break;
                }
                else break;
            } while (true);
            Helper.SetParam("opus.complexity", complexity.ToString());
        }
        private void SetFramesize()
        {
            // размер кадра
            Helper.Println("i_framesize");
            for (int i = 0; i < _frame_sizes.Count; i++)
                Console.WriteLine(i + ": " + _frame_sizes[i]);
            do
            {
                framesize_ind = Helper.InputIntln();
                if (framesize_ind >= _frame_sizes.Count)
                    Helper.Println("no_framesize");
                else if (framesize_ind < 0)
                {
                    framesize_ind = 3;
                    break;
                }
                else break;
            } while (true);
            framesize = _frame_sizes[framesize_ind];
            Helper.SetParam("opus.framesize", _frame_sizes[framesize_ind]);
        }

        public void StartEncoding()
        {
            string options = $"{exe} --bitrate {bitrate} --{bitrate_mode} --{content_type} --comp {complexity} --framesize {framesize} - -";
            BassEnc_Opus.BASS_Encode_OPUS_Start(111, options, BASSEncode.BASS_ENCODE_FP_16BIT, null, IntPtr.Zero);
        }

    }
}
