using strimer.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Mpeg;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace streamer.cs
{
    internal class Radio
    {
		private readonly MySrv _mysrv = null!;
        private readonly Player _player = null!;
		private Audiolist _playlist = null!;
		private readonly bool _radio_stop = false;
		private string track_time = String.Empty;
		public Radio()
		{
			_player = new ();
			_mysrv = new();
			StartPlaylist();
		}
		private void LoadPlaylist()
		{
			Helper.Log("Load playlist");
			string file = Helper.GetParam("radio.playlist");
			_playlist = new(file);
		}
		private void ShowInfo(TAG_INFO tags, string audio_file)
		{
			string cons = $"Listeners: {_player.Listeners}\\{_player.PeakListeners}";
			string log = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Listeners: {_player.Listeners}\\{_player.PeakListeners}]";
			_mysrv.Add_History(_playlist.Current + 1, tags.artist, tags.title, Path.GetFileName(audio_file));
			Console.WriteLine();
			Console.WriteLine(cons);
			Helper.Log(log);
		}
		private string ShowTimeInfo(TAG_INFO tags)
		{
			string track_pos = _player.GetTrackPosition();
			string console_message = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Time: {track_pos}\\{track_time}]";
			Console.Write($"\r\t\t\t\t\t\t\t\t\t\t\t\t");
			Console.Write($"\r{console_message}");
			return console_message;
		}
        private void StartPlaylist()
		{
			int debug = 0;

			LoadPlaylist();
			TAG_INFO tags = new();
			while (!_radio_stop)
			{
				if (_player.IsStoped)
				{
					bool cast_error = _player.Ice.NEW2_Cast_Init();
                    Console.WriteLine($"Cast error: {cast_error}");
					string audio_file = _playlist.GetRandomTrack();
                    tags = _player.PlayAudio(audio_file);
					if (tags == null)
                        continue;
					_player.SetTitle(tags.artist, tags.title);
					track_time = _player.GetTrackTime();
					ShowInfo(tags, audio_file);
				}
				string console_message = ShowTimeInfo(tags);
				
				if (debug > 180)
				{
                    Helper.Log($"Info: {console_message}");
                    debug = 0;
				}
				debug++;
				Thread.Sleep(1000);
			}
            Console.WriteLine("End WHILE");
        }
	}
}