using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace streamer.cs
{
    internal static class Helper
    {
        private static string[]? messages;
        private static string[]? config;
        private static Dictionary<string, string[]> configuration = new();
        static Helper()
        {
            LoadMessages();
            LoadConfig();
            configuration.Add("strimer", config);
        }
        public static string GetParam(string key)
        {
            for (int i = 0; i < config?.Length; i++)
            {
                var text = config[i];
                var sub = text.Substrings(key + "=", ";");
                if (sub.Length > 0)
                    return sub.First();
            }
            return key;
        }
        public static bool SetParam(string key, string value)
        {
            for(int i = 0;i < config?.Length;i++)
            {
                if (config[i].Contains(key))
                {
                    config[i] = $"\t{key}={value};";
                    return true;
                }
            }
            return false;
        }
        public static string GetText(string key)
        {
            for (int i = 0; i < messages?.Length; i++)
            {
                var text = messages[i];
                var sub = text.Substrings(key + "=", ";");
                if (sub.Length > 0)
                    return sub.First();
            }
            return key;

        }
        public static void SaveParam(string conf)
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", $"{conf}.conf");
            File.WriteAllLines(file, configuration[conf]);
        }
        public static void Print(string par)
        {
            Console.Write(GetText(par) + " ");
        }
        public static void Prints(string par)
        {
            Console.Write(GetText(par) + " ");
        }
        public static void Println(string par)
        {
            Console.WriteLine(GetText(par));
        }
        public static int InputIntln()
        {
            while (true)
            {
                Helper.Print("input");
                string? s = Console.ReadLine();
                if (s == String.Empty)
                    return -1;
                bool isNumber = s.All(char.IsDigit);
                if (isNumber)
                    return int.Parse(s);
            }
        }
        public static string Inputln()
        {
            while (true)
            {
                Helper.Print("input");
                string? s = Console.ReadLine();
                if (s != null)
                    return s;
            }
        }
        private static void LoadMessages()
        {
            messages = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "messages.conf"));
        }
        private static void LoadConfig()
        {
            config = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "strimer.conf"));
        }
		public static void IsConfigured()
		{
			Helper.GetParam("app.configured");
		}
        public static void Log(string text)
        {
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), text + "\n");
        }

		// ---------------------------------------------------
		public static string[] Substrings(this string str, string left, string right,
            int startIndex, StringComparison comparsion = StringComparison.Ordinal)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new string[0];
            }

            #region Проверка параметров

            if (left == null)
            {
                throw new Exception();
            }

            if (left.Length == 0)
            {
                throw new Exception();
            }

            if (right == null)
            {
                throw new Exception();
            }

            if (right.Length == 0)
            {
                throw new Exception();
            }

            if (startIndex < 0)
            {
                throw new Exception();
            }

            if (startIndex >= str.Length)
            {
                throw new Exception();
            }

            #endregion

            int currentStartIndex = startIndex;
            List<string> strings = new();

            while (true)
            {
                // Ищем начало позиции левой подстроки.
                int leftPosBegin = str.IndexOf(left, currentStartIndex, comparsion);

                if (leftPosBegin == -1)
                {
                    break;
                }

                // Вычисляем конец позиции левой подстроки.
                int leftPosEnd = leftPosBegin + left.Length;

                // Ищем начало позиции правой строки.
                int rightPos = str.IndexOf(right, leftPosEnd, comparsion);

                if (rightPos == -1)
                {
                    break;
                }

                // Вычисляем длину найденной подстроки.
                int length = rightPos - leftPosEnd;

                strings.Add(str.Substring(leftPosEnd, length));

                // Вычисляем конец позиции правой подстроки.
                currentStartIndex = rightPos + right.Length;
            }

            return strings.ToArray();
        }
        public static string[] Substrings(this string str, string left, string right,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            return str.Substrings(left, right, 0, comparsion);
        }
		public static int ToInt(this string value)
		{
			return int.Parse(value);
		}
        public static bool ToBoolFromWord(this string value)
        {
            if (value == "yes")
                return true;
            else
                return false;
        }
	}
}