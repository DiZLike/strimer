using strimer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streamer.cs
{
    internal class Radio
    {
		private Player? _player;
		private Playlist? _playlist;
		private Thread? _playlist_thread;
		private bool _radio_stop = false;

		public Radio()
		{
			_player = new Player();
			_playlist_thread = new Thread(StartPlaylist);
			_playlist_thread.Name = "Playlist";
			_playlist_thread.IsBackground = false;
			_playlist_thread.Start();
		}
		private void StartPlaylist()
		{
			Helper.Log("Load playlist");
			string file = Helper.GetParam("radio.playlist");
			string track_time = String.Empty;
			_playlist = new(file);

			while(!_radio_stop)
			{
				if (_player.IsStoped)
				{
					Helper.Log("Load track");
					_player.StreamFree();
					_player.SetTitle("ewewe", "159");
					_player.PlayAudio(_playlist.GetRandomTrack());
					track_time = _player.GetTrackTime();
					string mes = $"Playing: Track: {_playlist.Current + 1} \\ {_playlist.Count}; Time: {track_time}";
					Helper.Log(mes);
					Console.WriteLine();
				}
				string track_pos = _player.GetTrackPosition();
				string console_message = $"Playing: Track: {_playlist.Current + 1} \\ {_playlist.Count}; Time: {track_pos} \\ {track_time}";
				Console.Write($"\r{console_message}");

				Thread.Sleep(1000);
			}

		}
		
	}
}