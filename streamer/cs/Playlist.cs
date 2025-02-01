using streamer.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strimer.cs
{
	internal class Playlist
	{
		private string _file_playlist = String.Empty;
		private string _raw_playlist = String.Empty;
		private string[]? _playlist;
		private int _current_track = 0;
		private List<string> _playback_history = new();
		private Random? _random;

		public int Count { get { return _playlist.Length; } }
		public int Current { get { return _current_track; } }

		public Playlist(string file_tracklist)
		{
			_random = new Random((int)DateTime.Now.Ticks);
			this._file_playlist = file_tracklist;
			CheckPlaylistFile(file_tracklist);
			LoadPlaylist();
		}
		public string GetRandomTrack()
		{
			var available_tracks = _playlist.Except(_playback_history).ToList();
			if (available_tracks.Count == 0)
			{
				available_tracks = _playlist.ToList();
				_playback_history.Clear();
			}
			int index = _random.Next(0, available_tracks.Count);
			_playback_history.Add(available_tracks[index]);
			_current_track = _playlist.ToList().IndexOf(available_tracks[index]);
			return available_tracks[index];
		}
		private void CheckPlaylistFile(string file_tracklist)
		{
			if(!File.Exists(file_tracklist))
			{
				Helper.Println("no_playlist");
				Helper.Log("No playlist");
				Console.WriteLine($"List: {file_tracklist}");
				Console.ReadKey();
				Environment.Exit(0xFF);
			}
		}
		private void LoadPlaylist()
		{
			_raw_playlist = File.ReadAllText(_file_playlist);
			_playlist = _raw_playlist.Substrings("track=", "?;");
			if (_playlist.Length == 0)
			{
				Helper.Println("er_playlist");
				Console.ReadKey();
				Environment.Exit(0xFF);
			}
		}
	}
}
