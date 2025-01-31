using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.EncOpus;

namespace streamer.cs
{
    internal class Player
    {
        private readonly List<int> frequency = new()
        {
            48000,
            44100,
            32000,
            22050,
            11025,
            8000
        };
        
        private readonly int sample_rate = 44100;
        private readonly IceCast? ice;
        public Player()
        {
            int dev = SetAudioDevice();
            sample_rate = SetFrequency();
            App.is_error = !Bass.BASS_Init(dev, sample_rate, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            App.IsError();
            Helper.Println("is_init");
            Console.WriteLine();

            ice = new(sample_rate);
            //Plugins plugins = new();

            //BassEnc_Opus.BASS_Encode_OPUS_Start(111, "-", Un4seen.Bass.AddOn.Enc.BASSEncode.BASS_ENCODE_FP_16BIT, null, IntPtr.Zero);
            //BassAa
            //Un4seen.Bass.AddOn.Enc.BassEnc.BASS_Encode_CastInit
        }
        private int SetAudioDevice()
        {
            Helper.Println("sel_dev");
            int dev_count = Bass.BASS_GetDeviceCount();
            int dev = -1;
            for (int i = 0; i < dev_count; i++)
            {
                Console.WriteLine(i + ": " + Bass.BASS_GetDeviceInfo(i));
            }
            do
            {
                dev = Helper.InputIntln();
                if (dev >= dev_count)
                {
                    Helper.Println("no_dev");
                } else break;
            } while (true);
            Helper.SetParam("device.device", dev.ToString()); // сохранение устройства
            Console.WriteLine();
            return dev;
        }
        private int SetFrequency()
        {
            int freq = 44100;
            Helper.Println("sel_freq");
            for (int i = 0; i < frequency.Count; i++)
            {
                Console.WriteLine(i + ": " + frequency[i]);
            }
            do
            {
                freq = Helper.InputIntln();
                if (freq >= frequency.Count)
                {
                    Helper.Println("no_freq");
                }
                else break;
            } while (true);
            if (freq == -1)
            {
                Helper.SetParam("device.frequency", frequency[1].ToString()); // сохранение частоты
                return frequency[1];
            }
            Helper.SetParam("device.frequency", frequency[freq].ToString()); // сохранение частоты
            return frequency[freq];
        }
    }
}
