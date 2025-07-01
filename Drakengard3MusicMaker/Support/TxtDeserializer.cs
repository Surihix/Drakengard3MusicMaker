using System;
using System.IO;

namespace Drakengard3MusicMaker.Support
{
    internal class TxtDeserializer
    {
        public static TxtSettings DeserializeData(string txtFile)
        {
            var txtSettings = new TxtSettings();
            var lineData = File.ReadAllLines(txtFile);

            if (lineData.Length > typeof(TxtSettings).GetFields().Length)
            {
                if (decimal.TryParse(lineData[0], out decimal sampleRate) == false)
                {
                    throw new Exception();
                }

                if (decimal.TryParse(lineData[1], out decimal channelCount) == false)
                {
                    throw new Exception();
                }

                if (decimal.TryParse(lineData[2], out decimal volume) == false)
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

                txtSettings.SampleRate = sampleRate;
                txtSettings.ChannelCount = channelCount;
                txtSettings.Volume = volume;
                txtSettings.LoopStart = loopStart;
                txtSettings.LoopEnd = loopEnd;
            }

            return txtSettings;
        }
    }
}