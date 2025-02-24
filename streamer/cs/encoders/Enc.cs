﻿using streamer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
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
		public Mixer Mixer { get => _mixer; set => _mixer = value; }
		public int Bitrate { get => bitrate; set => bitrate = value; }
		public int Encoder_handle { get => encoder_handle; set => encoder_handle = value; }
		public string Content { get => content; set => content = value; }

		private protected BaseEncoder _encoder = null!;
		private protected Mixer _mixer = null!;

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
			// только opus
			string text = $"--artist \"{artist}\" --title \"{title}\"";
			string options = $"{exe} --bitrate {bitrate} --{bitrate_mode} --{content_type} --comp{complexity} --framesize {framesize} {text} - -";
			bool ok = BassEnc_Opus.BASS_Encode_OPUS_NewStream(encoder_handle, options, BASSEncode.BASS_ENCODE_FP_16BIT);
		}
		public bool GetEncoderStatus()
		{
			BASSActive status = BassEnc.BASS_Encode_IsActive(encoder_handle);
			Helper.Log($"Encoder status: {status.ToString()}");
			if (status == BASSActive.BASS_ACTIVE_PLAYING)
				return true;
			else return false;
		}
		public int NEW2_StartEncode()
		{
			// только opus
			string options = $"{exe} --bitrate {bitrate} --{bitrate_mode} --{content_type} --comp{complexity} --framesize {framesize} - -";
			encoder_handle = BassEnc_Opus.BASS_Encode_OPUS_Start(_mixer.main_mixer_handle, options, BASSEncode.BASS_ENCODE_FP_16BIT, null, IntPtr.Zero);
			return encoder_handle;
		}
		public void RestartEncode()
		{
			int handle = NEW2_StartEncode();
			Helper.Log($"Restart encoder handle: {handle.ToString()}");
		}
	}
}
