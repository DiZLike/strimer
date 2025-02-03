namespace list_creater
{
    internal class Program
    {
        private static string[] _new_tracks = null!;
        private static List<string> _tracks = new();
        static void Main(string[] args)
        {
            //args = new string[] { @"E:\Desktop\test_dir", "/home/ftpuser/Radio/main/",  @"E:\Desktop\test.pls" };
            ScanFolder(args[0]);
            AddTo(args[2], args[1]);
            Save(args[2]);
            Console.ReadKey();
        }
        private static void ScanFolder(string folder)
        {
            _new_tracks = Directory.GetFiles(folder);
        }
        private static void AddTo(string list, string target_folder)
        {
            _tracks = File.ReadAllLines(list).ToList();
            //List<string> play = new List<string>();
            foreach (var item in _new_tracks)
            {
                string file = Path.GetFileName(item);
                string format_file = $"track={target_folder}{file}?;";
                _tracks.Add(format_file);
            }
        }
        private static void Save(string list)
        {
            File.WriteAllLines(list, _tracks.ToArray());
        }
    }
}
