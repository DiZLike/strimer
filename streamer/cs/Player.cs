using strimer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass.AddOn.Tags;

namespace streamer.cs
{
    internal class Player
    {
        public IceCast Ice => _ice;
        public int Listeners { get => _ice.Listeners; }
		public int PeakListeners { get => _ice.PeakListeners; }

		private readonly List<int> frequency = new()
        {
            48000,
            44100,
            32000,
            22050,
            11025,
            8000
        };
        private readonly int dev;
		private readonly int sample_rate = 44100;
        private readonly IceCast _ice = null!;
        private readonly Mixer _mixer = null!;
        private readonly ReplayGain _gain = null!;
        private int _stream = 0;
        private bool _use_replace_gain = false;
		private bool _use_custom_gain = false;
		public bool IsPlaying { get { return Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING; } }
		public bool IsStoped { get { return Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_STOPPED; } }
        public Player()
        {
            if (!App.is_configured)
            {
                dev = SetAudioDevice();
                sample_rate = SetFrequency();
            }
            else
            {
                dev = Helper.GetParam("device.device").ToInt();
				sample_rate = Helper.GetParam("device.frequency").ToInt();
			}
            App.is_error = !Bass.BASS_Init(dev, sample_rate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            BassFx.BASS_FX_GetVersion();
            App.IsError();
            Helper.Println("is_init");
            Console.WriteLine();
            _use_replace_gain = Helper.ToBoolFromWord(Helper.GetParam("radio.use_replay_gain"));
			_use_custom_gain = Helper.ToBoolFromWord(Helper.GetParam("radio.use_custom_gain"));
			Plugins plugins = new();
			_ice = new(sample_rate);
            _gain = new(_use_replace_gain, _use_custom_gain, _ice.Mixer.main_mixer_handle);
        }
        private int SetAudioDevice()
        {
            Helper.Println("sel_dev");
            int dev_count = Bass.BASS_GetDeviceCount();
            int dev = -1;
            for (int i = 0; i < dev_count; i++)
            {
                Console.WriteLine(i + ": " + Bass.BASS_GetDeviceInfo(i));
            }
            do
            {
                dev = Helper.InputIntln();
                if (dev >= dev_count)
                {
                    Helper.Println("no_dev");
                } else break;
            } while (true);
            Helper.SetParam("device.device", dev.ToString()); // сохранение устройства
            Console.WriteLine();
            return dev;
        }
        private int SetFrequency()
        {
            int freq = 44100;
            Helper.Println("sel_freq");
            for (int i = 0; i < frequency.Count; i++)
            {
                Console.WriteLine(i + ": " + frequency[i]);
            }
            do
            {
                freq = Helper.InputIntln();
                if (freq >= frequency.Count)
                {
                    Helper.Println("no_freq");
                }
                else break;
            } while (true);
            if (freq == -1)
            {
                Helper.SetParam("device.frequency", frequency[1].ToString()); // сохранение частоты
                return frequency[1];
            }
            Helper.SetParam("device.frequency", frequency[freq].ToString()); // сохранение частоты
            return frequency[freq];
        }
        private bool CheckAudioFile(string file)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("No file!");
                Helper.Log("No file!");
                return false!;
            }
            _stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            if (_stream == 0)
            {
                Helper.Log($"Stream ERROR! File: {Path.GetFileName(file)}");
                Console.WriteLine($"Stream ERROR! Message: {App.GetErrorMessage()}");
                Helper.Log($"Stream ERROR! Message: {App.GetErrorMessage()}");
                return false!;
            }
            return true;
        }
        private void CheckEncodeStatus()
        {
            bool encoder_status = _ice.Encoder.GetEncoderStatus();
            if (!encoder_status)
                _ice.Encoder.RestartEncode();
        }
        public TAG_INFO PlayAudio(string file)
        {
            _ice.RemoveStream(_stream);
            StreamFree();
            Helper.Log($"Load track: {Path.GetFileName(file)}");
            if (!CheckAudioFile(file))
                return null!;
            CheckEncodeStatus();
            Helper.Log($"Track stream: {_stream}");
            TAG_INFO tag_info = GetTags(file);
            _gain.SetGain(tag_info);
            _gain.ApplyGain();
            _ice.AddStream(_stream);
            return tag_info;
		}
        private TAG_INFO GetTags(string file)
        {
            TAG_INFO tag_info = new TAG_INFO();
            tag_info = BassTags.BASS_TAG_GetFromFile(file);
            if (tag_info.artist.Length == 0)
                tag_info.artist = _ice.StreamName;
            if (tag_info.title.Length == 0)
            {
                string new_title = Path.GetFileNameWithoutExtension(file);
                tag_info.title = new_title;
            }
            return tag_info;
        }
        public string GetTrackPosition()
        {
            long raw_pos = Bass.BASS_ChannelGetPosition(_stream);
            double sec = Bass.BASS_ChannelBytes2Seconds(_stream, raw_pos);
            DateTime time = new DateTime();
            time = time.AddSeconds(sec);
            return time.ToString("mm:ss");
        }
        public string GetTrackTime()
        {
            long raw_time = Bass.BASS_ChannelGetLength(_stream);
            double sec = Bass.BASS_ChannelBytes2Seconds(_stream, raw_time);
			DateTime time = new DateTime();
			time = time.AddSeconds(sec);
			return time.ToString("mm:ss");
		}
        public void SetTitle(string artist, string title)
        {
            _ice.SetTitle(artist, title);
        }
        public void StreamFree()
        {
            if (_stream != 0)
            {
				App.is_error = !Bass.BASS_StreamFree(_stream);
				App.IsError();
			}
        }
        private void Test(string file)
        {
            var tag = Bass.BASS_ChannelGetTagsOGG(_stream);
        }
	}
}