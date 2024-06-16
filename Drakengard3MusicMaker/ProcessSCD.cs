using Drakengard3MusicMaker.Support;
using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    internal class ProcessSCD
    {
        public static string OutMp3File { get; set; }
        public static decimal Mp3SampleRate { get; set; }
        public static decimal Mp3ChannelCount { get; set; }
        public static decimal Mp3LoopStart { get; set; }
        public static decimal Mp3LoopEnd { get; set; }
        public static bool CustomVolumeButtonChecked { get; set; }
        public static int VolumeSliderValue { get; set; }

        public static void ConvertAudio(string inScdFile)
        {
            using (var inScdStream = new FileStream(inScdFile, FileMode.Open, FileAccess.Read))
            {
                using (var inScdReader = new BinaryReader(inScdStream))
                {
                    inScdReader.BaseStream.Position = 0;
                    var getHeader = inScdReader.ReadChars(8);
                    var scdHeader = string.Join("", getHeader).Replace("\0", "");

                    if (scdHeader == "SEDBSSCF")
                    {
                        var outScdFile = Path.Combine(Path.GetDirectoryName(inScdFile), $"New_{Path.GetFileName(inScdFile)}");
                        SharedMethods.IfFileExistDel(outScdFile);

                        uint audioInfoPos = 0;
                        uint audioStart = 0;
                        long mp3FileSize = 0;
                        uint volumeToSet = 0;
                        uint outScdAudioStart = 0;

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

                            using (var mp3Stream = new FileStream(OutMp3File, FileMode.Open, FileAccess.Read))
                            {
                                outScdStream.Seek(outScdAudioStart, SeekOrigin.Begin);
                                mp3Stream.Seek(0, SeekOrigin.Begin);
                                mp3Stream.CopyTo(outScdStream);

                                SharedVariables.OutSCDsize = outScdStream.Length;

                                if (SharedVariables.OutSCDsize % 4 != 0)
                                {
                                    var remainder = SharedVariables.OutSCDsize % 4;
                                    var increaseBytes = 4 - remainder;
                                    var newSize = SharedVariables.OutSCDsize + increaseBytes;
                                    var padNulls = newSize - SharedVariables.OutSCDsize;

                                    outScdStream.Seek(SharedVariables.OutSCDsize, SeekOrigin.Begin);
                                    outScdStream.PadNull(padNulls);

                                    SharedVariables.OutSCDsize = outScdStream.Length;
                                }

                                mp3FileSize = mp3Stream.Length;

                                inScdReader.BaseStream.Position = 296;
                                volumeToSet = inScdReader.ReadBytesUInt32(true);

                                if (CustomVolumeButtonChecked)
                                {
                                    switch (VolumeSliderValue)
                                    {
                                        case 0:
                                            volumeToSet = 0;
                                            break;
                                        case 1:
                                            volumeToSet = 1041865114;
                                            break;
                                        case 2:
                                            volumeToSet = 1051931443;
                                            break;
                                        case 3:
                                            volumeToSet = 1057803469;
                                            break;
                                        case 4:
                                            volumeToSet = 1061158912;
                                            break;
                                        case 5:
                                            volumeToSet = 1064514355;
                                            break;
                                        case 6:
                                            volumeToSet = 1065353216;
                                            break;
                                    }
                                }
                            }
                        }

                        using (var outScdWriter = new BinaryWriter(File.Open(outScdFile, FileMode.Open, FileAccess.Write)))
                        {
                            outScdWriter.BaseStream.Position = 20;
                            outScdWriter.WriteBytesUInt32((uint)SharedVariables.OutSCDsize, true);

                            outScdWriter.BaseStream.Position = 296;
                            outScdWriter.WriteBytesUInt32(volumeToSet, true);

                            outScdWriter.BaseStream.Position = audioInfoPos;
                            outScdWriter.WriteBytesUInt32((uint)mp3FileSize, true);

                            outScdWriter.BaseStream.Position = audioInfoPos + 4;
                            outScdWriter.WriteBytesUInt32((uint)Mp3ChannelCount, true);

                            outScdWriter.BaseStream.Position = audioInfoPos + 8;
                            outScdWriter.WriteBytesUInt32((uint)Mp3SampleRate, true);

                            outScdWriter.BaseStream.Position = audioInfoPos + 16;
                            outScdWriter.WriteBytesUInt32((uint)Mp3LoopStart, true);

                            outScdWriter.BaseStream.Position = audioInfoPos + 20;
                            outScdWriter.WriteBytesUInt32((uint)Mp3LoopEnd, true);
                        }
                    }
                    else
                    {
                        SharedVariables.IsSCDvalid = false;
                        SharedMethods.AppMsgBox("Unable to detect SCD header.\nSelect a valid .XXX audio file to replace", "Missing Header", MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}