using strimer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace streamer.cs
{
    internal class Radio
    {
		private Player _player = null!;
		private Audiolist _playlist = null!;
		private bool _radio_stop = false;

		public Radio()
		{
			_player = new Player();
			StartPlaylist();
		}
		private void StartPlaylist()
		{
			Helper.Log("Load playlist");
			string file = Helper.GetParam("radio.playlist");
			string track_time = String.Empty;
			_playlist = new(file);

			TAG_INFO tags = new TAG_INFO();
			while (!_radio_stop)
			{
				if (_player.IsStoped)
				{
					Helper.Log("Load track");
					_player.StreamFree();
					tags = _player.PlayAudio(_playlist.GetRandomTrack());
					_player.SetTitle(tags.artist, tags.title);
					track_time = _player.GetTrackTime();
					string cons = $"Listeners: {_player.Listeners}\\{_player.PeakListeners}";
					string log = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Listeners: {_player.Listeners}\\{_player.PeakListeners}]";
					Console.WriteLine();
					Console.WriteLine(cons);
					Helper.Log(log);
				}
				string track_pos = _player.GetTrackPosition();
				string console_message = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Time: {track_pos}\\{track_time}]";
				Console.Write($"\r\t\t\t\t\t\t\t\t\t\t\t\t");
				Console.Write($"\r{console_message}");

				Thread.Sleep(1000);
			}

		}
	}
}