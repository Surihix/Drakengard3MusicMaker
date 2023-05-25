using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker.AppClasses
{
    public class ProcessScd
    {
        public static void ConvertAudio(string inScdFileVar, string outScdDirVar, string outScdNameVar, string mp3Dir,
            string mp3Name, decimal mp3SampleRate, decimal mp3ChannelCount, decimal mp3LoopStart, decimal mp3LoopEnd,
            bool customVolButtonCheckedVar, int volSliderVal, ref long outScdFileSize, ref bool scdCheck)
        {
            using (FileStream inScd = new FileStream(inScdFileVar, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader inScdReader = new BinaryReader(inScd))
                {
                    inScdReader.BaseStream.Position = 0;
                    var getHeader = inScdReader.ReadChars(8);
                    string scdHeader = string.Join("", getHeader).Replace("\0", "");

                    if (scdHeader.Contains("SEDBSSCF"))
                    {
                        MainForm.ExistFileDel(outScdDirVar + "\\" + "New_" + outScdNameVar);

                        using (FileStream outScd = new FileStream(outScdDirVar + "\\" + "New_" + outScdNameVar, FileMode.Append, FileAccess.Write))
                        {
                            inScd.Seek(0, SeekOrigin.Begin);
                            MainForm.BEReader(inScdReader, 112, out uint audioInfoPos);
                            MainForm.BEReader(inScdReader, audioInfoPos + 24, out uint audioStart);

                            inScd.Seek(audioInfoPos + 32 + audioStart, SeekOrigin.Begin);
                            var outScdAudioStart = (uint)inScd.Position;

                            inScd.Seek(0, SeekOrigin.Begin);
                            byte[] outScdDataBuffer = new byte[outScdAudioStart];
                            var bytesToOutScd = inScd.Read(outScdDataBuffer, 0, outScdDataBuffer.Length);
                            outScd.Write(outScdDataBuffer, 0, bytesToOutScd);

                            using (FileStream mp3File = new FileStream(mp3Dir + "\\" + mp3Name, FileMode.Open, FileAccess.Read))
                            {
                                outScd.Seek(outScdAudioStart, SeekOrigin.Begin);
                                mp3File.Seek(0, SeekOrigin.Begin);
                                mp3File.CopyTo(outScd);

                                outScdFileSize = outScd.Length;

                                if (outScdFileSize % 4 != 0)
                                {
                                    var Remainder = outScdFileSize % 4;
                                    var IncreaseBytes = 4 - Remainder;
                                    var NewSize = outScdFileSize + IncreaseBytes;
                                    var PadNulls = NewSize - outScdFileSize;

                                    outScd.Seek(outScdFileSize, SeekOrigin.Begin);
                                    for (int pad = 0; pad < PadNulls; pad++)
                                    {
                                        outScd.WriteByte(0);
                                    }
                                    outScdFileSize = outScd.Length;
                                }


                                var mp3FileSize = mp3File.Length;

                                MainForm.BEReader(inScdReader, 296, out uint Vol);

                                if (customVolButtonCheckedVar.Equals(true))
                                {
                                    switch (volSliderVal)
                                    {
                                        case 0:
                                            Vol = 0;
                                            break;
                                        case 1:
                                            Vol = 1041865114;
                                            break;
                                        case 2:
                                            Vol = 1051931443;
                                            break;
                                        case 3:
                                            Vol = 1057803469;
                                            break;
                                        case 4:
                                            Vol = 1061158912;
                                            break;
                                        case 5:
                                            Vol = 1064514355;
                                            break;
                                        case 6:
                                            Vol = 1065353216;
                                            break;
                                    }
                                }

                                using (BinaryWriter outScdWriter = new BinaryWriter(outScd))
                                {
                                    MainForm.BEWriter(outScdWriter, 20, (uint)outScdFileSize);
                                    MainForm.BEWriter(outScdWriter, 296, Vol);

                                    MainForm.BEWriter(outScdWriter, audioInfoPos, (uint)mp3FileSize);
                                    MainForm.BEWriter(outScdWriter, audioInfoPos + 4, (uint)mp3ChannelCount);
                                    MainForm.BEWriter(outScdWriter, audioInfoPos + 8, (uint)mp3SampleRate);
                                    MainForm.BEWriter(outScdWriter, audioInfoPos + 16, (uint)mp3LoopStart);
                                    MainForm.BEWriter(outScdWriter, audioInfoPos + 20, (uint)mp3LoopEnd);
                                }
                            }
                        }
                    }
                    else
                    {
                        scdCheck = false;
                        MainForm.AppMsgBox("Unable to detect SCD header.\nSelect a valid .XXX audio file to replace", "Missing Header", MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}