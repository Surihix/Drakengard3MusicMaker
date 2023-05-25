using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker.AppClasses
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (!File.Exists("AppHelp.txt"))
            {
                AppMsgBox("The 'AppHelp.txt' file is missing.\nPlease ensure that this file is present next to the app to use the Help option.", "Warning", MessageBoxIcon.Warning);
            }

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


        private void EnableTools()
        {
            if (!string.IsNullOrEmpty(Mp3PathTxtBox.Text) && !string.IsNullOrEmpty(XXXPathTxtBox.Text) && !string.IsNullOrEmpty(PS3TOCPathTxtBox.Text))
            {
                if (File.Exists($"{Mp3PathTxtBox.Text}") && File.Exists($"{XXXPathTxtBox.Text}") && File.Exists($"{PS3TOCPathTxtBox.Text}"))
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


        private void OFDInitializer(string ofdDesc, out OpenFileDialog ofdVar, string ofdFilter)
        {
            ofdVar = new OpenFileDialog
            {
                Filter = ofdDesc + ofdFilter,
                RestoreDirectory = true
            };
        }


        private void PathNameAssigns(out string ofdFileDir, TextBox txtBoxVar, out string ofdFileName)
        {
            ofdFileDir = Path.GetDirectoryName($"{txtBoxVar.Text}");
            ofdFileName = Path.GetFileName($"{txtBoxVar.Text}");
        }


        public static void BEReader(BinaryReader readerNameVar, uint readerPos, out uint valueVar)
        {
            readerNameVar.BaseStream.Position = readerPos;
            var getByteValue = readerNameVar.ReadBytes((int)readerNameVar.BaseStream.Length);
            valueVar = BinaryPrimitives.ReadUInt32BigEndian(getByteValue.AsSpan());
        }


        public static void ExistFileDel(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }


        public static void BEWriter(BinaryWriter writerNameVar, uint writerPos, uint valueToAdjust)
        {
            writerNameVar.BaseStream.Position = writerPos;
            var adjustByteValue = new byte[4];
            writerNameVar.BaseStream.Position = writerPos;
            BinaryPrimitives.WriteUInt32BigEndian(adjustByteValue, valueToAdjust);
            writerNameVar.Write(adjustByteValue);
        }


        public static void AppMsgBox(string msgTxt, string msgHeader, MessageBoxIcon msgType)
        {
            MessageBox.Show(msgTxt, msgHeader, MessageBoxButtons.OK, msgType);
        }


        private void Mp3BrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("MP3 Audio File (*.mp3)", out OpenFileDialog mp3PathSelect, $"|{"*.mp3"}");

            if (mp3PathSelect.ShowDialog() == DialogResult.OK)
            {
                var mp3filePath = mp3PathSelect.FileName;
                var mp3TxtBoxText = Path.GetFullPath($"{mp3filePath}");
                Mp3PathTxtBox.Text = mp3TxtBoxText;

                EnableTools();
            }
        }
        private void Mp3PathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools();
        }


        private void XXXBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("XXX Audio File (*.XXX)", out OpenFileDialog xxxPathSelect, $"|{"*.XXX"}");

            if (xxxPathSelect.ShowDialog() == DialogResult.OK)
            {
                var xxxFilePath = xxxPathSelect.FileName;
                var xxxTxtBoxText = Path.GetFullPath($"{xxxFilePath}");
                XXXPathTxtBox.Text = xxxTxtBoxText;

                EnableTools();
            }
        }
        private void XXXPathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools();
        }


        private void PS3TOCBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("PS3TOC Text file", out OpenFileDialog tocPath_select, $"|{"PS3TOC.TXT"}");

            if (tocPath_select.ShowDialog() == DialogResult.OK)
            {
                var tocFilePath = tocPath_select.FileName;
                var tocTxtBoxText = Path.GetFullPath($"{tocFilePath}");
                PS3TOCPathTxtBox.Text = tocTxtBoxText;

                EnableTools();
            }
        }
        private void PS3TOCPathTxtBox_TextChanged(object sender, EventArgs e)
        {
            EnableTools();
        }


        private void ConvertAudiobtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists($"{Mp3PathTxtBox.Text}") && File.Exists($"{XXXPathTxtBox.Text}") && File.Exists($"{PS3TOCPathTxtBox.Text}"))
                {
                    PathNameAssigns(out string mp3Dir, Mp3PathTxtBox, out string mp3Name);
                    PathNameAssigns(out string outScdDir, XXXPathTxtBox, out string outScdName);
                    PathNameAssigns(out string tocDir, PS3TOCPathTxtBox, out string tocName);

                    var customVolBtnChecked = false;
                    if (CustomVolRadioButton.Checked.Equals(true))
                    {
                        customVolBtnChecked = true;
                    }

                    long outScdFileSize = 0;
                    var scdCheck = true;

                    ProcessScd.ConvertAudio($"{XXXPathTxtBox.Text}", outScdDir, outScdName, mp3Dir, mp3Name, SampleRateNumUpDown.Value,
                        ChannelCountNumUpDown.Value, LoopStartNumUpDown.Value, LoopEndNumUpDown.Value, customVolBtnChecked,
                        VolSlider.Value, ref outScdFileSize, ref scdCheck);

                    if (scdCheck.Equals(true))
                    {
                        ProcessToc.EditText(tocDir, tocName, outScdName, outScdFileSize);
                    }
                }
                else
                {
                    AppMsgBox("One or more file paths aren't set correctly or the files themselves does not exist.\nPlease set the correct filepaths for all of the files before trying to convert them.", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                AppMsgBox("" + ex, "Error", MessageBoxIcon.Error);
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
            var aboutBox = new AboutWindow();
            System.Media.SystemSounds.Asterisk.Play();
            aboutBox.ShowDialog();
        }


        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("AppHelp.txt"))
            {
                try
                {
                    Process.Start("AppHelp.txt");
                }
                catch (Exception ex)
                {
                    AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                }
            }
            else
            {
                AppMsgBox("Unable to locate the help text file.\nPlease ensure that this text file is present next to the app before using this option.", "Error", MessageBoxIcon.Error);
            }
        }
    }
}