using System;
using System.Buffers.Binary;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            OgVolRadioButton.Checked = true;
            SampleRateNumUpDown.Value = 44100;

            DisableTools();
        }


        private void DisableTools()
        {
            SampleRateNumUpDown.Enabled = false;
            ChannelCountNumUpDown.Enabled = false;
            ConvertAudiobtn.Enabled = false;
            OgVolRadioButton.Enabled = false;
            CustomVolRadioButton.Enabled = false;
            VolSlider.Enabled = false;
            LoopStartNumUpDown.Enabled = false;
            LoopEndNumUpDown.Enabled = false;
        }


        private void EnableTools(string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (!string.IsNullOrEmpty(Mp3PathTxtBox.Text)
                && !string.IsNullOrEmpty(XXXPathTxtBox.Text)
                && !string.IsNullOrEmpty(PS3TOCPathTxtBox.Text))
            {
                if (File.Exists($"{Mp3PathTxtBox.Text}")
                    && File.Exists($"{XXXPathTxtBox.Text}")
                    && File.Exists($"{PS3TOCPathTxtBox.Text}"))
                {
                    SampleRateNumUpDown.Enabled = true;
                    ChannelCountNumUpDown.Enabled = true;
                    ConvertAudiobtn.Enabled = true;
                    OgVolRadioButton.Enabled = true;
                    CustomVolRadioButton.Enabled = true;
                    LoopStartNumUpDown.Enabled = true;
                    LoopEndNumUpDown.Enabled = true;

                    if (CustomVolRadioButton.Checked.Equals(true))
                    {
                        VolSlider.Enabled = true;
                    }
                }
                else
                {
                    DisableTools();
                }
            }
        }


        private void OFDInitializer(out string OFDFile, string OFDDesc, out OpenFileDialog OFDVarName, string OFDFilter)
        {
            OFDFile = OFDDesc;
            OFDVarName = new OpenFileDialog();
            OFDVarName.Filter = OFDFile + OFDFilter;
            OFDVarName.RestoreDirectory = true;
        }


        private void PathNameAssigns(out string OFDFileDir, TextBox BoxName, out string OFDFileName)
        {
            OFDFileDir = Path.GetDirectoryName($"{BoxName.Text}");
            OFDFileName = Path.GetFileName($"{BoxName.Text}");
        }


        private void BEReader(BinaryReader ReaderName, uint ReaderPosValue, out byte[] GetVarName, out uint VarName)
        {
            ReaderName.BaseStream.Position = ReaderPosValue;
            GetVarName = ReaderName.ReadBytes((int)ReaderName.BaseStream.Length);
            VarName = BinaryPrimitives.ReadUInt32BigEndian(GetVarName.AsSpan());
        }


        private void ExistFileDel(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }


        private void BEWriter(BinaryWriter WriterName, uint WriterPosValue, out byte[] AdjustVarName, uint VarNameToAdjust)
        {
            WriterName.BaseStream.Position = WriterPosValue;
            AdjustVarName = new byte[4];
            WriterName.BaseStream.Position = WriterPosValue;
            BinaryPrimitives.WriteUInt32BigEndian(AdjustVarName, (uint)VarNameToAdjust);
            WriterName.Write(AdjustVarName);
        }


        private void AppMsgBox(string Msg, string MsgHeader, MessageBoxIcon MsgType)
        {
            MessageBox.Show(Msg, MsgHeader, MessageBoxButtons.OK, MsgType);
        }
        

        private void Mp3BrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer(out string Mp3File, "MP3 Audio File", out OpenFileDialog Mp3path_select, $"|{"*.mp3"}");

            if (Mp3path_select.ShowDialog() == DialogResult.OK)
            {
                string Mp3filePath = Mp3path_select.FileName;
                string Mp3TxtBoxText = Path.GetFullPath($"{Mp3filePath}");
                Mp3PathTxtBox.Text = Mp3TxtBoxText;

                EnableTools($"{Mp3PathTxtBox.Text}");
            }
        }
        private void Mp3PathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools($"{Mp3PathTxtBox.Text}");
        }


        private void XXXBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer(out string XXXFile, "XXX Audio File", out OpenFileDialog XXXpath_select, $"|{"*.XXX"}");

            if (XXXpath_select.ShowDialog() == DialogResult.OK)
            {
                string XXXfilePath = XXXpath_select.FileName;
                string XXXTxtBoxText = Path.GetFullPath($"{XXXfilePath}");
                XXXPathTxtBox.Text = XXXTxtBoxText;

                EnableTools($"{XXXPathTxtBox.Text}");
            }
        }
        private void XXXPathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools($"{XXXPathTxtBox.Text}");
        }


        private void PS3TOCBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer(out string TOCFile, "PS3TOC Text file", out OpenFileDialog TOCpath_select, $"|{"*.txt"}");

            if (TOCpath_select.ShowDialog() == DialogResult.OK)
            {
                string TOCfilePath = TOCpath_select.FileName;
                string TOCTxtBoxText = Path.GetFullPath($"{TOCfilePath}");
                PS3TOCPathTxtBox.Text = TOCTxtBoxText;

                EnableTools($"{PS3TOCPathTxtBox.Text}");
            }
        }
        private void PS3TOCPathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools($"{PS3TOCPathTxtBox.Text}");
        }


        private void ConvertAudiobtn_Click(object sender, EventArgs e)
        {
            PathNameAssigns(out string Mp3Dir, Mp3PathTxtBox, out string Mp3Name);
            PathNameAssigns(out string OutSCDDir, XXXPathTxtBox, out string OutSCDName);
            PathNameAssigns(out string TOCDir, PS3TOCPathTxtBox, out string TOCName);

            using (FileStream InSCD = new FileStream($"{XXXPathTxtBox.Text}", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader InSCDReader = new BinaryReader(InSCD))
                {
                    InSCDReader.BaseStream.Position = 0;
                    var GetHeader = InSCDReader.ReadChars(8);
                    string Header = string.Join("", GetHeader).Replace("\0", "");

                    if (Header.Contains("SEDBSSCF"))
                    {
                        ExistFileDel(OutSCDDir + "\\" + "New_" + OutSCDName);

                        using (FileStream OutSCD = new FileStream(OutSCDDir + "\\" + "New_" + OutSCDName, FileMode.Append,
                            FileAccess.Write))
                        {
                            InSCD.Seek(0, SeekOrigin.Begin);
                            InSCD.CopyTo(OutSCD);

                            BEReader(InSCDReader, 568, out byte[] GetAudioStart, out uint AudioStart);

                            OutSCD.Seek(576, SeekOrigin.Begin);
                            OutSCD.Seek(AudioStart, SeekOrigin.Current);

                            var GetAudioStartPos = OutSCD.Position;
                            uint AudioStartPos = Convert.ToUInt32(GetAudioStartPos);

                            long OutSCDsize = OutSCD.Length;
                            long NewOutSCDsize = OutSCDsize - AudioStartPos;
                            OutSCD.SetLength(Math.Max(0, OutSCD.Length - NewOutSCDsize));

                            using (FileStream Mp3File = new FileStream(Mp3Dir + "\\" + Mp3Name, FileMode.Open, FileAccess.Read))
                            {
                                OutSCD.Seek(AudioStartPos, SeekOrigin.Begin);
                                Mp3File.Seek(0, SeekOrigin.Begin);
                                Mp3File.CopyTo(OutSCD);

                                var OutSCDFileSize = OutSCD.Length;
                                var Mp3FileSize = Mp3File.Length;
                                var Mp3SampleRate = SampleRateNumUpDown.Value;
                                var Mp3ChannelCount = ChannelCountNumUpDown.Value;
                                var Mp3LoopStart = LoopStartNumUpDown.Value;
                                var Mp3LoopEnd = LoopEndNumUpDown.Value;

                                BEReader(InSCDReader, 296, out byte[] GetOGVol, out uint Vol);

                                if (CustomVolRadioButton.Checked.Equals(true))
                                {
                                    switch (VolSlider.Value)
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

                                using (BinaryWriter OutSCDWriter = new BinaryWriter(OutSCD))
                                {
                                    BEWriter(OutSCDWriter, 20, out byte[] AdjustSCDFileSize, (uint)OutSCDFileSize);
                                    BEWriter(OutSCDWriter, 296, out byte[] AdjustVol, (uint)Vol);
                                    BEWriter(OutSCDWriter, 544, out byte[] AdjustMp3FileSize, (uint)Mp3FileSize);
                                    BEWriter(OutSCDWriter, 548, out byte[] AdjustMp3ChannelCount, (uint)Mp3ChannelCount);
                                    BEWriter(OutSCDWriter, 552, out byte[] AdjustMp3SampleRate, (uint)Mp3SampleRate);
                                    BEWriter(OutSCDWriter, 560, out byte[] AdjustMp3LoopStart, (uint)Mp3LoopStart);
                                    BEWriter(OutSCDWriter, 564, out byte[] AdjustMp3LoopEnd, (uint)Mp3LoopEnd);

                                    var TotalEntries = File.ReadLines(TOCDir + "\\" + TOCName).Count();
                                    using (FileStream InTOCTxtFile = new FileStream(TOCDir + "\\" + TOCName, FileMode.Open,
                                        FileAccess.Read))
                                    {
                                        using (StreamReader TOCReader = new StreamReader(InTOCTxtFile))
                                        {
                                            ExistFileDel(TOCDir + "\\" + "New_" + TOCName);

                                            using (FileStream OutTOCTxtFile = new FileStream(TOCDir + "\\" + "New_" + TOCName,
                                                FileMode.Append, FileAccess.Write))
                                            {
                                                using (StreamWriter TOCWriter = new StreamWriter(OutTOCTxtFile))
                                                {
                                                    var LocatedMarker = "";
                                                    for (int i = 0; i < TotalEntries; i++)
                                                    {
                                                        string Parsed = TOCReader.ReadLine();
                                                        string[] FileInfo = Parsed.Split(' ');
                                                        uint FileSize = Convert.ToUInt32(FileInfo[0]);
                                                        uint Size_2 = Convert.ToUInt32(FileInfo[1]);
                                                        string FilePath = Convert.ToString(FileInfo[2]);
                                                        uint Size_3 = Convert.ToUInt32(FileInfo[3]);

                                                        if (FilePath.EndsWith(OutSCDName,
                                                            StringComparison.CurrentCultureIgnoreCase))
                                                        {
                                                            LocatedMarker = "Found";
                                                            FileSize = (uint)OutSCDFileSize;
                                                        }

                                                        string currentLine = FileSize + " " + Size_2 + " " + FilePath + " "
                                                            + Size_3;
                                                        TOCWriter.WriteLine(currentLine);
                                                    }

                                                    if (LocatedMarker.Equals(""))
                                                    {
                                                        AppMsgBox("Generated TOC file is not updated " +
                                                            "\n The XXX file to be replaced is missing its info in the " +
                                                            "TOC file. the game might crash after it finishes playing this " +
                                                            "audio file.", "Warning",
                                                            MessageBoxIcon.Warning);
                                                    }
                                                    else
                                                    {
                                                        AppMsgBox("Generated new music and TOC files \n " +
                                                            "The 'New_' files can be added to your game's " +
                                                            "sound folder", "Sucess",
                                                            MessageBoxIcon.Information);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        AppMsgBox("Unable to detect SCD header" +
                            "\n Select a valid .XXX audio file to replace", "Missing Header", MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void OgVolRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            VolSlider.Enabled = false;
        }
        private void CustomVolRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            VolSlider.Enabled = true;
        }


        private void AboutlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var AboutBox = new AboutWindow();
            System.Media.SystemSounds.Asterisk.Play();
            AboutBox.ShowDialog();
        }

        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var HelpBox = new HelpWindow();
            System.Media.SystemSounds.Asterisk.Play();
            HelpBox.ShowDialog();
        }
    }
}