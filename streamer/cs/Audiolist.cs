using streamer.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strimer.cs
{
	internal class Audiolist
	{
		private string _file_playlist = String.Empty;
		private string _raw_playlist = String.Empty;
		private string[] _playlist = null!;
		private int _current_track = 0;
		private List<string> _playback_history = new();
		private bool _save_history = false;
		public int Count { get { return _playlist.Length; } }
		public int Current { get { return _current_track; } }
		public Audiolist(string file_tracklist)
		{
			this._file_playlist = file_tracklist;
			_save_history = Helper.ToBoolFromWord(Helper.GetParam("radio.save_playlist_history"));
			if (_save_history)
				LoadHistoryFromFile();
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
			int index = Random.Shared.Next(0, available_tracks.Count);
			_playback_history.Add(available_tracks[index]);
			_current_track = _playlist.ToList().IndexOf(available_tracks[index]);
			if (_save_history)
				SaveHistoryToFile();
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
				Environment.Exit(0xFF);
			}
		}
		private void SaveHistoryToFile()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "history.pls");
			File.WriteAllLines(file, _playback_history.ToArray());
		}
		private void LoadHistoryFromFile()
		{
			string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "history.pls");
			if (!File.Exists(file))
				return;
			string[] hist = File.ReadAllLines(file);
			foreach (var item in hist)
			{
				if (item == String.Empty)
					continue;
				_playback_history.Add(item);
			}
			string msg = $"History loaded";
			Console.WriteLine(msg);
			Helper.Log(msg);
		}
	}
}