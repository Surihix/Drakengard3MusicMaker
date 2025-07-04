using Drakengard3MusicMaker.Support;
using System.IO;

namespace Drakengard3MusicMaker.ProcessHelpers
{
    internal class ProcessSCD
    {
        public static bool ConvertAudio(string inScdFile, AppSettings appSettings)
        {
            var outScdFile = Path.Combine(Path.GetDirectoryName(inScdFile), $"New_{Path.GetFileName(inScdFile)}");
            SharedMethods.IfFileExistDel(outScdFile);

            using (var inScdStream = new FileStream(inScdFile, FileMode.Open, FileAccess.Read))
            {
                using (var inScdReader = new BinaryReader(inScdStream))
                {
                    inScdReader.BaseStream.Position = 0;
                    var scdHeader = inScdReader.ReadBytesString(8, false);

                    if (scdHeader != "SEDBSSCF")
                    {
                        if (appSettings.IsSingleMode)
                        {
                            SharedMethods.ErrorStop($"Unable to detect SCD header in '{Path.GetFileName(inScdFile)}'.\nSelect a valid .XXX audio file to replace.");
                        }

                        return false;
                    }

                    uint audioInfoPos = 0;
                    uint audioStart = 0;
                    long mp3FileSize = 0;
                    float volumeToSet = 0;
                    uint outScdAudioStart = 0;
                    var outSCDsize = uint.MinValue;

                    using (var outScdStream = new FileStream(outScdFile, FileMode.Append, FileAccess.Write))
                    {
                        inScdStream.Seek(0, SeekOrigin.Begin);

                        inScdReader.BaseStream.Position = 112;
                        audioInfoPos = inScdReader.ReadBytesUInt32(true);

                        inScdReader.BaseStream.Position = audioInfoPos + 24;
                        audioStart = inScdReader.ReadBytesUInt32(true);

                        inScdStream.Seek(audioInfoPos + 32 + audioStart, SeekOrigin.Begin);
                        outScdAudioStart = (uint)inScdStream.Position;

                        inScdStream.Seek(0, SeekOrigin.Begin);
                        inScdStream.CopyStreamTo(outScdStream, outScdAudioStart, false);

                        using (var mp3Stream = new FileStream(appSettings.OutMp3File, FileMode.Open, FileAccess.Read))
                        {
                            outScdStream.Seek(outScdAudioStart, SeekOrigin.Begin);
                            mp3Stream.Seek(0, SeekOrigin.Begin);
                            mp3Stream.CopyTo(outScdStream);

                            outSCDsize = (uint)outScdStream.Length;

                            if (outSCDsize % 4 != 0)
                            {
                                var remainder = outSCDsize % 4;
                                var increaseBytes = 4 - remainder;
                                var newSize = outSCDsize + increaseBytes;
                                var padNulls = newSize - outSCDsize;

                                outScdStream.Seek(outSCDsize, SeekOrigin.Begin);
                                outScdStream.PadNull(padNulls);

                                outSCDsize = (uint)outScdStream.Length;
                            }

                            mp3FileSize = mp3Stream.Length;

                            inScdReader.BaseStream.Position = 296;
                            volumeToSet = inScdReader.ReadBytesFloat(true);

                            if (appSettings.CustomVolumeButtonChecked)
                            {
                                switch (appSettings.VolumeSliderValue)
                                {
                                    case 0:
                                        volumeToSet = 0;
                                        break;
                                    case 1:
                                        volumeToSet = (float)0.15;
                                        break;
                                    case 2:
                                        volumeToSet = (float)0.35;
                                        break;
                                    case 3:
                                        volumeToSet = (float)0.55;
                                        break;
                                    case 4:
                                        volumeToSet = (float)0.75;
                                        break;
                                    case 5:
                                        volumeToSet = (float)0.95;
                                        break;
                                    case 6:
                                        volumeToSet = 1;
                                        break;
                                }
                            }
                        }
                    }

                    using (var outScdWriter = new BinaryWriter(File.Open(outScdFile, FileMode.Open, FileAccess.Write)))
                    {
                        outScdWriter.BaseStream.Position = 20;
                        outScdWriter.WriteBytesUInt32(outSCDsize, true);

                        outScdWriter.BaseStream.Position = 296;
                        outScdWriter.WriteBytesFloat(volumeToSet, true);

                        outScdWriter.BaseStream.Position = audioInfoPos;
                        outScdWriter.WriteBytesUInt32((uint)mp3FileSize, true);

                        outScdWriter.BaseStream.Position = audioInfoPos + 4;
                        outScdWriter.WriteBytesUInt32((uint)appSettings.Mp3ChannelCount, true);

                        outScdWriter.BaseStream.Position = audioInfoPos + 8;
                        outScdWriter.WriteBytesUInt32((uint)appSettings.Mp3SampleRate, true);

                        outScdWriter.BaseStream.Position = audioInfoPos + 16;
                        outScdWriter.WriteBytesUInt32((uint)appSettings.Mp3LoopStart, true);

                        outScdWriter.BaseStream.Position = audioInfoPos + 20;
                        outScdWriter.WriteBytesUInt32((uint)appSettings.Mp3LoopEnd, true);
                    }
                }
            }

            SharedMethods.IfFileExistDel(inScdFile + ".old");
            File.Move(inScdFile, inScdFile + ".old");
            File.Move(outScdFile, inScdFile);

            return true;
        }
    }
}