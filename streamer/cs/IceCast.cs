using strimer.cs;
using strimer.cs.encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.Misc;

namespace streamer.cs
{
    internal class IceCast
    {
		public string? StreamName { get => _stream_name; set => _stream_name = value; }
		public int Listeners { get => GetListeners(); }
		public int PeakListeners { get => GetPeakListeners(); }
        public Enc Encoder { get => _encoder; set => _encoder = value; }
        private string? _server;
        private string? _port;
        private string? _stream_link;
        private string? _stream_name;
        private string? _stream_genre;
        private string? _username;
        private string? _password;
        private int _encoder_ind;

		private Enc _encoder = null!;
		private Mixer _mixer = null!;

        private List<string> _encoders = new()
        {
            "Opus"
        };

        private readonly int freq;
        public IceCast(int freq)
        {
            this.freq = freq;
            SetServer();
            _mixer = new(this.freq);
            SetEncoder();
            Helper.SetParam("app.configured", "yes");
            Helper.SaveParam("strimer");
			Helper.Println("b_started");
			App.IsError();
		}
		public bool NEW2_Cast_Init()
		{
			bool cast_error = false;
            string url = $"http://{_server}:{_port}/{_stream_link}";
			App.is_error = cast_error = !BassEnc.BASS_Encode_CastInit(_encoder.Encoder_handle, url, $"{_username}:{_password}",
				_encoder.Content, _stream_name, null, _stream_genre, null, null, _encoder.Bitrate,
				BASSEncodeCast.BASS_ENCODE_CAST_PUT);
			Console.WriteLine($"Cast status: {App.GetErrorMessage()}");
			Helper.Log($"Cast status: {App.GetErrorMessage()}");
            App.is_error = !Bass.BASS_ChannelPlay(_mixer.main_mixer_handle, true);
            Helper.Println("ice_successfully");
            Console.WriteLine($"Server: http://{_server}:{_port}/{_stream_link}");
			return cast_error;
        }
        public void AddStream(int stream)
		{
			_mixer.AddStream(stream);
		}
		public void RemoveStream(int stream)
		{
			if (stream != 0)
				_mixer.RemoveStream(stream);
		}
        private void SetServer()
        {
            if (!App.is_configured)
            {
				#region input
				Helper.Println("i_server");
				_server = Helper.Inputln();
				if (_server == String.Empty)
					_server = "localhost";
				Helper.SetParam("icecast.server", _server);

				Helper.Println("i_port");
				_port = Helper.Inputln();
				if (_port == String.Empty)
					_port = "8000";
				Helper.SetParam("icecast.port", _port);

				Helper.Println("i_stream_link");
				_stream_link = Helper.Inputln();
				if (_stream_link == String.Empty)
					_stream_link = "live";
				Helper.SetParam("icecast.link", _stream_link);

				Helper.Println("i_stream_name");
				_stream_name = Helper.Inputln();
				if (_stream_name == String.Empty)
					_stream_name = "stream";
				Helper.SetParam("icecast.name", _stream_name);

				Helper.Println("i_stream_genre");
				_stream_genre = Helper.Inputln();
				if (_stream_genre == String.Empty)
					_stream_genre = "Rock";
				Helper.SetParam("icecast.genre", _stream_genre);

				Helper.Println("i_username");
				_username = Helper.Inputln();
				if (_username == String.Empty)
					_username = "source";
				Helper.SetParam("icecast.username", _username);

				Helper.Println("i_password");
				_password = Helper.Inputln();
				if (_password == String.Empty)
					_password = "hackme";
				Helper.SetParam("icecast.password", _password);
				#endregion
			}
			else
            {
                _server = Helper.GetParam("icecast.server");
				_port = Helper.GetParam("icecast.port");
				_stream_link = Helper.GetParam("icecast.link");
				_stream_name = Helper.GetParam("icecast.name");
				_stream_genre = Helper.GetParam("icecast.genre");
				_username = Helper.GetParam("icecast.username");
				_password = Helper.GetParam("icecast.password");
			}
        }
        private void SetEncoder()
        {
            if (!App.is_configured)
            {
				Helper.Println("i_encoder");
				for (int i = 0; i < _encoders.Count; i++)
					Console.WriteLine(i + ": " + _encoders[i]);
				do
				{
					_encoder_ind = Helper.InputIntln();
					if (_encoder_ind >= _encoders.Count)
						Helper.Println("no_enc");
					else if (_encoder_ind < 0)
					{
						_encoder_ind = 0;
						break;
					}
					else break;
				} while (true);
				Helper.SetParam("icecast.encoder", _encoder_ind.ToString());
			}
			else
				_encoder_ind = Helper.GetParam("icecast.encoder").ToInt();
            if (_encoder_ind == 0) // Opus
                _encoder = new EncOpus(_mixer);
        }
		private string GetStatus()
		{
			return BassEnc.BASS_Encode_CastGetStats(_encoder.Encoder_handle, BASSEncodeStats.BASS_ENCODE_STATS_ICESERV, _password);
		}
		private int GetListeners()
		{
			string raw_status = GetStatus();
			string left = $"<source mount=\"/{_stream_link}\">";
			string right = "</source>";

			string[] ar_source = raw_status.Substrings(left, right);
			if (ar_source.Length > 0)
			{
				left = "<listeners>";
				right = "</listeners>";
				string[] ar_listeners = ar_source.First().Substrings(left, right);
				if (ar_listeners.Length > 0)
					return ar_listeners.First().ToInt();
				else return 0;
			}
			else return 0;
		}
		private int GetPeakListeners()
		{
			string raw_status = GetStatus();
			string left = $"<source mount=\"/{_stream_link}\">";
			string right = "</source>";

			string[] ar_source = raw_status.Substrings(left, right);
			if (ar_source.Length > 0)
			{
				left = "<listener_peak>";
				right = "</listener_peak>";
				string[] ar_listeners = ar_source.First().Substrings(left, right);
				if (ar_listeners.Length > 0)
					return ar_listeners.First().ToInt();
				else return 0;
			}
			else return 0;
		}
		public void SetTitle(string artist, string title)
		{
			_encoder.SetTitle(artist, title);
		}
    }
}