using FrostPlayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrostPlayer.Managers
{
    public class ConfigManager
    {
        private readonly string _configPath;

        public ConfigManager()
        {
            _configPath = Path.Combine(Application.StartupPath, "config.json");
        }
        public AppConfig LoadConfig()
        {
            var config = new AppConfig();
            try
            {
                if (File.Exists(_configPath))
                {
                    string json = File.ReadAllText(_configPath);
                    config = JsonConvert.DeserializeObject<AppConfig>(json);
                }
            }
            catch (Exception ex)
            {

            }
            return config;
        }
        public void SaveConfig(AppConfig config)
        {
            try
            {
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(_configPath, json);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
