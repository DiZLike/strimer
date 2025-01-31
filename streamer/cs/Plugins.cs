using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace streamer.cs
{
    internal class Plugins
    {
        public List<int> bass_plug_handles = new List<int>();
        public Plugins()
        {
            Helper.Println("plu_l");
            bass_plug_handles.Add(Bass.BASS_PluginLoad(Path.Combine(App.app_dir, App.pref_lib + "bass_aac" + App.dot_lib)));
            bass_plug_handles.Add(Bass.BASS_PluginLoad(Path.Combine(App.app_dir, App.pref_lib + "bassopus" + App.dot_lib)));
            Console.WriteLine("ok");
            Console.WriteLine();
        }
    }
}
