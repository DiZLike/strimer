using streamer.cs;
using strimer;
using System.Reflection;
using System.Runtime.InteropServices;
using Un4seen;
using Un4seen.Bass;

namespace streamer
{
    internal class Program
    {
		public static readonly Assembly Reference = typeof(Program).Assembly;
        public static readonly Version? Version = Reference.GetName().Version;
		private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Version: {Version.Major}.{Version.Minor}.{Version.Build}");
				CheckOS();
				App.is_configured = Helper.GetParam("app.configured").ToBoolFromWord();
				Helper.Log("Is configured: " + App.is_configured.ToString());
				if (!App.is_configured)
					ReplaceLib();
				CheckArgs(args);

				Radio radio = new();
			}
            catch (Exception ex)
            {
				Helper.Log("Error: " + ex.Message);
			}
        }
        private static void CheckArgs(string[] args)
        {

        }
        private static void CheckOS()
        {
            Helper.Println("det_os_arc");
            bool is_win = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            Architecture arc = RuntimeInformation.ProcessArchitecture;
            if (is_win)
            {
                App.OS = "Windows";
                App.pref_lib = String.Empty;
                App.dot_lib = ".dll";
            }
            else
            {
                App.OS = "Linux";
                App.pref_lib = "lib";
                App.dot_lib = ".so";
            }
            App.Arc = arc.ToString();

            Helper.Prints("os");
            Console.WriteLine(App.OS);
            Helper.Prints("arc1");
            Console.WriteLine(App.Arc);
            Console.WriteLine();
        }
        private static void ReplaceLib()
        {
            Helper.Println("rep_l");
            App.current_os_folder = App.os_sel_folder[App.OS.ToLower() + App.Arc.ToLower()];
            string folder = App.current_os_folder;
            string dotlib = App.dot_lib;

            string[] libs = Directory.GetFiles(folder);
            foreach (string lib in libs)
            {
                Helper.Prints("repl_l");
                Console.Write($"{Path.GetFileName(lib)} ... ");
                try
                {
                    File.Copy(lib, Path.Combine(App.app_dir, Path.GetFileName(lib)), true);
                    Console.WriteLine("ok");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("fail!");
                    Console.WriteLine($"\t{ex.Message}");
                }
            }
            Console.WriteLine();
        }
    }
}