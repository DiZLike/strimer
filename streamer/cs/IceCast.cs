using strimer.cs;
using strimer.cs.encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace streamer.cs
{
    internal class IceCast
    {
        private string? _server;
        private string? _port;
        private string? _stream_link;
        private string? _stream_name;
        private string? _stream_genre;
        private string? _username;
        private string? _password;
        private int _encoder_ind;

        private Enc? _encoder;
        private Mixer? _mixer;

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

            App.is_error = !BassEnc.BASS_Encode_CastInit(_encoder.enc_handle, $"http://{_server}:{_port}/{_stream_link}",
                $"{_username}:{_password}", _encoder.content, _stream_name, null, _stream_genre, null, null,
                _encoder.bitrate.ToInt(), BASSEncodeCast.BASS_ENCODE_CAST_PUT);
            App.IsError();
			Helper.Println("ice_successfully");
			Console.WriteLine($"Server: http://{_server}:{_port}/{_stream_link}");
			App.is_error = !Bass.BASS_ChannelPlay(_mixer.main_mixer_handle, true);
			App.IsError();
			Helper.Println("b_started");
			//Console.WriteLine();
		}
		public void AddStream(int stream)
		{
			_mixer.AddStream(stream);
		}
		public void RemoveStream(int stream)
		{
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
    }
}