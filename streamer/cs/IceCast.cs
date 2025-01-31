using strimer.cs;
using strimer.cs.encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

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
            SetEncoder();
            Helper.SaveParam("strimer");
        }
        private void SetServer()
        {
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
        }
        private void SetEncoder()
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

            if (_encoder_ind == 0) // Opus
                _encoder = new EncOpus(_mixer);
        }
    }
}