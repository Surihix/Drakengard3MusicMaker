using Drakengard3MusicMaker.Support;
using System;
using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            if (!File.Exists("AppHelp.txt"))
            {
                SharedMethods.AppMsgBox("The 'AppHelp.txt' file is missing.\nPlease ensure that this file is present next to the app to use the Help option.", "Warning", MessageBoxIcon.Warning);
            }

            OgVolRadioButton.Checked = true;
            SampleRateNumUpDown.Value = 0;
        }


        private void Mp3BrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("MP3 Audio File (*.mp3)", out OpenFileDialog mp3PathSelect, "|*.mp3");

            if (mp3PathSelect.ShowDialog() == DialogResult.OK)
            {
                var mp3filePath = mp3PathSelect.FileName;
                var mp3TxtBoxText = Path.GetFullPath($"{mp3filePath}");
                Mp3PathTxtBox.Text = mp3TxtBoxText;
            }
        }


        private void XXXBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("XXX Audio File (*.XXX)", out OpenFileDialog xxxPathSelect, "|*.XXX");

            if (xxxPathSelect.ShowDialog() == DialogResult.OK)
            {
                var xxxFilePath = xxxPathSelect.FileName;
                var xxxTxtBoxText = Path.GetFullPath($"{xxxFilePath}");
                XXXPathTxtBox.Text = xxxTxtBoxText;
            }
        }


        private void PS3TOCBrowseBtn_Click(object sender, EventArgs e)
        {
            OFDInitializer("PS3TOC Text file", out OpenFileDialog tocPath_select, "|PS3TOC.TXT");

            if (tocPath_select.ShowDialog() == DialogResult.OK)
            {
                var tocFilePath = tocPath_select.FileName;
                var tocTxtBoxText = Path.GetFullPath($"{tocFilePath}");
                PS3TOCPathTxtBox.Text = tocTxtBoxText;
            }
        }


        private void LoadFromMp3Btn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFromMp3Btn.Text = "Loading....";
                EnableDisableControls(false);

                SampleRateNumUpDown.Value = 0;
                ChannelCountNumUpDown.Value = 1;

                var isLoadOk = File.Exists($"{Mp3PathTxtBox.Text}");

                if (isLoadOk)
                {
                    var mp3Settings = ProcessMp3.GetMp3Info(Mp3PathTxtBox.Text);

                    if (mp3Settings != null)
                    {
                        SampleRateNumUpDown.Value = mp3Settings.SampleRate;
                        ChannelCountNumUpDown.Value = mp3Settings.ChannelCount;
                    }

                    LoadFromMp3Btn.Text = "Load from mp3";
                    EnableDisableControls(true);
                }
                else
                {
                    SharedMethods.AppMsgBox("Mp3 file path isn't set correctly or the file does not exist.\nPlease set the correct filepath for the mp3 file before trying this option.", "Error", MessageBoxIcon.Error);

                    LoadFromMp3Btn.Text = "Load from mp3";
                    EnableDisableControls(true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Handled")
                {
                    SharedMethods.AppMsgBox("" + ex, "Error", MessageBoxIcon.Error);
                }

                LoadFromMp3Btn.Text = "Load from mp3";
                EnableDisableControls(true);
            }
        }


        private void ConvertAudioBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ConvertAudioBtn.Text = "Converting....";
                EnableDisableControls(false);

                var isConvertOk = File.Exists($"{Mp3PathTxtBox.Text}") && File.Exists($"{XXXPathTxtBox.Text}") && File.Exists($"{PS3TOCPathTxtBox.Text}");

                if (isConvertOk)
                {
                    var appSettings = new AppSettings()
                    {
                        OutMp3File = Mp3PathTxtBox.Text,
                        Mp3SampleRate = SampleRateNumUpDown.Value,
                        Mp3ChannelCount = ChannelCountNumUpDown.Value,
                        Mp3LoopStart = LoopStartNumUpDown.Value,
                        Mp3LoopEnd = LoopEndNumUpDown.Value,
                        CustomVolumeButtonChecked = CustomVolRadioButton.Checked,
                        VolumeSliderValue = VolSlider.Value
                    };

                    ProcessSCD.ConvertAudio(XXXPathTxtBox.Text, appSettings);
                    ProcessTOC.SingleModeEdit(PS3TOCPathTxtBox.Text, Path.GetFileName(XXXPathTxtBox.Text));

                    ConvertAudioBtn.Text = "Convert Audio";
                    EnableDisableControls(true);
                }
                else
                {
                    SharedMethods.AppMsgBox("One or more file paths aren't set correctly or the files themselves does not exist.\nPlease set the correct filepaths for all of the files before trying to convert them.", "Error", MessageBoxIcon.Error);

                    ConvertAudioBtn.Text = "Convert Audio";
                    EnableDisableControls(true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Handled")
                {
                    SharedMethods.AppMsgBox("" + ex, "Error", MessageBoxIcon.Error);
                }

                ConvertAudioBtn.Text = "Convert Audio";
                EnableDisableControls(true);
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


        private void OgVolRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            VolSlider.Enabled = false;
        }
        private void CustomVolRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            VolSlider.Enabled = true;
        }


        private void EnableDisableControls(bool isEnabled)
        {
            Mp3PathTxtBox.Enabled = isEnabled;
            Mp3BrowseBtn.Enabled = isEnabled;
            XXXPathTxtBox.Enabled = isEnabled;
            XXXBrowseBtn.Enabled = isEnabled;
            PS3TOCPathTxtBox.Enabled = isEnabled;
            PS3TOCBrowseBtn.Enabled = isEnabled;
            LoadFromMp3Btn.Enabled = isEnabled;
            SampleRateNumUpDown.Enabled = isEnabled;
            ChannelCountNumUpDown.Enabled = isEnabled;
            ConvertAudioBtn.Enabled = isEnabled;
            OgVolRadioButton.Enabled = isEnabled;
            CustomVolRadioButton.Enabled = isEnabled;
            if (CustomVolRadioButton.Checked)
            {
                VolSlider.Enabled = isEnabled;
            }
            LoopStartNumUpDown.Enabled = isEnabled;
            LoopEndNumUpDown.Enabled = isEnabled;
            AboutLinkLabel.Enabled = isEnabled;
            HelpLinkLabel.Enabled = isEnabled;
        }


        private void AboutlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var aboutBox = new AboutWindow();
            aboutBox.ShowDialog();
        }


        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("AppHelp.txt"))
            {
                try
                {
                    System.Diagnostics.Process.Start("AppHelp.txt");
                }
                catch (Exception ex)
                {
                    SharedMethods.AppMsgBox("Error: " + ex, "Error", MessageBoxIcon.Error);
                }
            }
            else
            {
                SharedMethods.AppMsgBox("Unable to locate the help text file.\nPlease ensure that this text file is present next to the app before using this option.", "Error", MessageBoxIcon.Error);
            }
        }
    }
}