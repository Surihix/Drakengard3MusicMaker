using Drakengard3MusicMaker.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    internal class ProcessMp3
    {
        public static Dictionary<string, Mp3Settings> GetMp3InfoBatch(string mp3Dir)
        {
            var mp3SettingsDict = new Dictionary<string, Mp3Settings>();

            var mp3Files = Directory.GetFiles(mp3Dir, "*.mp3", SearchOption.TopDirectoryOnly);

            string mp3TxtFile;

            foreach (var mp3File in mp3Files)
            {
                mp3TxtFile = Path.Combine(mp3Dir, Path.GetFileNameWithoutExtension(mp3File) + ".txt");

                if (File.Exists(mp3TxtFile))
                {
                    var mp3SettingsTxt = new Mp3Settings();
                    var hasReadData = ReadMp3TxtFile(mp3TxtFile, ref mp3SettingsTxt);

                    if (hasReadData)
                    {
                        mp3SettingsDict.Add(mp3File, mp3SettingsTxt);
                        continue;
                    }
                }

                var mp3Settings = ReadMp3Header(mp3File);

                if (mp3Settings == null)
                {
                    SharedMethods.AppMsgBox($"Failed to read '{Path.GetFileName(mp3File)}' file.\nPlease convert the file in Single mode by specifying the sample rate and channel count manually.", "Error", MessageBoxIcon.Error);
                }
                else
                {
                    mp3SettingsDict.Add(mp3File, mp3Settings);
                }
            }

            return mp3SettingsDict;
        }


        public static Mp3Settings GetMp3Info(string mp3File)
        {
            var mp3Settings = ReadMp3Header(mp3File);

            if (mp3Settings == null)
            {
                SharedMethods.AppMsgBox("Failed to read mp3 file.\nPlease specify the sample rate and channel count manually.", "Error", MessageBoxIcon.Error);
                return null;
            }
            else
            {
                return mp3Settings;
            }
        }


        private static bool ReadMp3TxtFile(string mp3TxtFile, ref Mp3Settings mp3SettingsTxt)
        {
            try
            {
                var lineData = File.ReadAllLines(mp3TxtFile);

                if (lineData.Length > 1)
                {
                    if (decimal.TryParse(lineData[0], out decimal sampleRate) == false)
                    {
                        throw new Exception();
                    }

                    if (decimal.TryParse(lineData[1], out decimal channelCount) == false)
                    {
                        throw new Exception();
                    }

                    mp3SettingsTxt.SampleRate = sampleRate;
                    mp3SettingsTxt.ChannelCount = channelCount;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        private static Mp3Settings ReadMp3Header(string mp3File)
        {
            var mp3Settings = new Mp3Settings();

            try
            {
                using (var mp3Reader = new BinaryReader(File.Open(mp3File, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    if (mp3Reader.ReadBytesString(3, false) == "ID3")
                    {
                        _ = mp3Reader.BaseStream.Position += 3;
                        var sizeBytes = mp3Reader.ReadBytes(4);

                        var sizeBits = string.Empty;

                        for (int i = 0; i < 4; i++)
                        {
                            sizeBits += Convert.ToString(sizeBytes[i], 2).PadLeft(8, '0').Substring(1);
                        }

                        _ = mp3Reader.BaseStream.Position += Convert.ToUInt32(sizeBits, 2);
                    }
                    else
                    {
                        _ = mp3Reader.BaseStream.Position = 0;
                    }

                    if (mp3Reader.ReadByte() == 255)
                    {
                        _ = mp3Reader.BaseStream.Position -= 1;
                        var mp3FrameHeaderBytes = mp3Reader.ReadBytes(4);

                        var mp3FrameHeaderBits = string.Empty;

                        for (int i = 0; i < 4; i++)
                        {
                            mp3FrameHeaderBits += Convert.ToString(mp3FrameHeaderBytes[i], 2).PadLeft(8, '0');
                        }

                        var keyValRead = Convert.ToInt32(mp3FrameHeaderBits.Substring(10, 2), 2);

                        string mpegVersion;

                        if (MPEGVersionsDict.ContainsKey(keyValRead))
                        {
                            mpegVersion = MPEGVersionsDict[keyValRead];
                        }
                        else
                        {
                            return null;
                        }

                        keyValRead = Convert.ToInt32(mp3FrameHeaderBits.Substring(20, 2), 2);
                        mp3Settings.SampleRate = decimal.One;

                        if (SampleRateDict.ContainsKey((keyValRead, mpegVersion)))
                        {
                            mp3Settings.SampleRate = SampleRateDict[(keyValRead, mpegVersion)];
                        }
                        else
                        {
                            return null;
                        }

                        keyValRead = Convert.ToInt32(mp3FrameHeaderBits.Substring(24, 2), 2);

                        if (keyValRead == 3)
                        {
                            mp3Settings.ChannelCount = 1;
                        }
                        else
                        {
                            mp3Settings.ChannelCount = 2;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }

            return mp3Settings;
        }


        private static readonly Dictionary<int, string> MPEGVersionsDict = new Dictionary<int, string>()
        {
            { 0, "v2.5" },
            { 1, "reserved" },
            { 2, "v2" },
            { 3, "v1" }
        };


        private static readonly Dictionary<(int, string), int> SampleRateDict = new Dictionary<(int, string), int>()
        {
            { (0, "v1"), 44100 },
            { (0, "v2"), 22050 },
            { (0, "v2.5"), 11025 },

            { (1, "v1"), 48000 },
            { (1, "v2"), 24000 },
            { (1, "v2.5"), 12000 },

            { (2, "v1"), 32000 },
            { (2, "v2"), 16000 },
            { (2, "v2.5"), 8000 },

            { (3, "v1"), 0 },
            { (3, "v2"), 0 },
            { (3, "v2.5"), 0 }
        };
    }
}