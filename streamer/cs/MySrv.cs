using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using streamer.cs;

namespace strimer.cs
{
    public class MySrv
    {
        private bool _enable = false;

        private string _srv_url = String.Empty;
        private string _port = String.Empty;
        private string _request_type = String.Empty;
        private string _key = String.Empty;
        private string _key_var = String.Empty;

        private string _add_song_info_page = String.Empty;
        private string _add_song_info_number_var = String.Empty;
        private string _add_song_info_title_var = String.Empty;
        private string _add_song_info_artist_var = String.Empty;
        public MySrv()
        {
            LoadConf();
        }
        private void LoadConf()
        {
            _enable = Helper.ToBoolFromWord(Helper.GetParam("mysrv.enable"));
            if (!_enable)
                return;

            _srv_url = Helper.GetParam("mysrv.server");
            _port = Helper.GetParam("mysrv.port");
            _request_type = Helper.GetParam("mysrv.request_type");
            _key = Helper.GetParam("mysrv.key");
            _key_var = Helper.GetParam("mysrv.key_var");
            _add_song_info_page = Helper.GetParam("mysrv.add_song_info_page");
            _add_song_info_number_var = Helper.GetParam("mysrv.add_song_info_number_var");
            _add_song_info_title_var = Helper.GetParam("mysrv.add_song_info_title_var");
            _add_song_info_artist_var = Helper.GetParam("mysrv.add_song_info_artist_var");

        }
        public void Add_History(int number, string artist, string title)
        {
            SendData(_add_song_info_page, $"key={_key}",
                $"{_add_song_info_number_var}={number}", 
                $"{_add_song_info_artist_var}={artist}", 
                $"{_add_song_info_title_var}={title}");
        }
        private void SendData(string page, string key, params string[] par)
        {
            if (!_enable)
                return;

            HttpClient client = new();
            string url = $"{_srv_url}:{_port}/{page}?{key}";
            foreach (var param in par)
                url += $"&{param}";

            HttpResponseMessage response = UseGET(client, url);
            if (response.IsSuccessStatusCode)
            {
                string response_data = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"MySRV: {response_data}");
            }
            else
                Console.WriteLine($"MySRV: {response.StatusCode}");
        }
        private HttpResponseMessage UseGET(HttpClient client, string url)
        {
            return client.GetAsync(url).Result;
        }
        private void UsePOST()
        {

        }
    }
}
