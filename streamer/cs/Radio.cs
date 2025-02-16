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
		private MySrv _mysrv = null!;
        private Player _player = null!;
		private Audiolist _playlist = null!;
		private bool _radio_stop = false;
		public Radio()
		{
			_player = new ();
			_mysrv = new();
			StartPlaylist();
		}
        private void StartPlaylist()
		{
			int debug = 0;

			Helper.Log("Load playlist");
			string file = Helper.GetParam("radio.playlist");
			string track_time = String.Empty;
			_playlist = new(file);

			TAG_INFO tags = new TAG_INFO();
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
					string cons = $"Listeners: {_player.Listeners}\\{_player.PeakListeners}";
					string log = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Listeners: {_player.Listeners}\\{_player.PeakListeners}]";
					_mysrv.Add_History(_playlist.Current + 1, tags.artist, tags.title, Path.GetFileName(audio_file));
					Console.WriteLine();
					Console.WriteLine(cons);
					Helper.Log(log);
				}

				string track_pos = _player.GetTrackPosition();
				string console_message = $"Playing: {tags.artist} - {tags.title} [{_playlist.Current + 1}\\{_playlist.Count}; Time: {track_pos}\\{track_time}]";
				Console.Write($"\r\t\t\t\t\t\t\t\t\t\t\t\t");
				Console.Write($"\r{console_message}");

				if (debug > 60)
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