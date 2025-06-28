using Drakengard3MusicMaker.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    internal class ProcessMp3
    {
        public static Mp3Settings GetMp3Info(string mp3File, bool isBatchMode)
        {
            var mp3Settings = new Mp3Settings();

            string errorMsg;

            if (isBatchMode)
            {
                errorMsg = $"Failed to read '{Path.GetFileName(mp3File)}' file.\nPlease convert the file in Single mode by specifying the sample rate and channel count manually.";
            }
            else
            {
                errorMsg = "Failed to read mp3 file.\nPlease specify the sample rate and channel count manually.";
            }

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
                    var mpegVersion = string.Empty;

                    if (MPEGVersionsDict.ContainsKey(keyValRead))
                    {
                        mpegVersion = MPEGVersionsDict[keyValRead];
                    }
                    else
                    {
                        if (isBatchMode)
                        {
                            SharedMethods.AppMsgBox(errorMsg, "Error", MessageBoxIcon.Error);
                            return null;
                        }
                        else
                        {
                            SharedMethods.ErrorStop(errorMsg);
                        }
                    }

                    keyValRead = Convert.ToInt32(mp3FrameHeaderBits.Substring(20, 2), 2);
                    mp3Settings.SampleRate = decimal.One;

                    if (SampleRateDict.ContainsKey((keyValRead, mpegVersion)))
                    {
                        mp3Settings.SampleRate = SampleRateDict[(keyValRead, mpegVersion)];
                    }
                    else
                    {
                        SharedMethods.ErrorStop("Failed to read mp3 file.\nPlease specify the sample rate and channel count manually.");
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
                    if (isBatchMode)
                    {
                        SharedMethods.AppMsgBox(errorMsg, "Error", MessageBoxIcon.Error);
                        return null;
                    }
                    else
                    {
                        SharedMethods.ErrorStop(errorMsg);
                    }
                }
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