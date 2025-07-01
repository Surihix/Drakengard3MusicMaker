using System;
using System.IO;

namespace Drakengard3MusicMaker.Support
{
    internal class TxtDeserializer
    {
        public static void DeserializeData(string txtFile, ref Mp3Settings mp3SettingsFromTxt)
        {
            var lineData = File.ReadAllLines(txtFile);

            if (lineData.Length > typeof(Mp3Settings).GetFields().Length)
            {
                if (decimal.TryParse(lineData[0], out decimal sampleRate) == false)
                {
                    throw new Exception();
                }

                if (decimal.TryParse(lineData[1], out decimal channelCount) == false)
                {
                    throw new Exception();
                }

                if (int.TryParse(lineData[2], out int volume) == false)
                {
                    throw new Exception();
                }

                if (decimal.TryParse(lineData[3], out decimal loopStart) == false)
                {
                    throw new Exception();
                }

                if (decimal.TryParse(lineData[4], out decimal loopEnd) == false)
                {
                    throw new Exception();
                }

                mp3SettingsFromTxt.SampleRate = sampleRate;
                mp3SettingsFromTxt.ChannelCount = channelCount;
                mp3SettingsFromTxt.Volume = volume;
                mp3SettingsFromTxt.LoopStart = loopStart;
                mp3SettingsFromTxt.LoopEnd = loopEnd;
            }
        }
    }
}