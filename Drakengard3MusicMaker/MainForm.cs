using Drakengard3MusicMaker.ProcessHelpers;
using Drakengard3MusicMaker.Support;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
                MessageBox.Show("The 'AppHelp.txt' file is missing.\nPlease ensure that this file is present next to the app to use the Help option.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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


        private void Mp3BrowseDirBtn_Click(object sender, EventArgs e)
        {
            OFLDInitializer("Select a folder that has mp3 files", out VistaFolderBrowserDialog ofldVar);

            if (ofldVar.ShowDialog() == DialogResult.OK)
            {
                Mp3DirTxtBox.Text = ofldVar.SelectedPath;
            }
        }


        private void XXXBrowseDirBtn_Click(object sender, EventArgs e)
        {
            OFLDInitializer("Select a folder that has XXX files", out VistaFolderBrowserDialog ofldVar);

            if (ofldVar.ShowDialog() == DialogResult.OK)
            {
                XXXDirTxtBox.Text = ofldVar.SelectedPath;
            }
        }


        private void PS3TOCBrowseBtn2_Click(object sender, EventArgs e)
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
            LoadFromMp3Btn.Text = "Loading....";
            EnableDisableControls(false);

            try
            {
                SampleRateNumUpDown.Value = 0;
                ChannelCountNumUpDown.Value = 1;

                var isLoadReady = File.Exists($"{Mp3PathTxtBox.Text}");

                if (isLoadReady)
                {
                    var mp3Settings = ProcessMp3.GetMp3Info(Mp3PathTxtBox.Text);

                    if (mp3Settings == null)
                    {
                        MessageBox.Show("Failed to read mp3 file.\nPlease specify the sample rate and channel count manually.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SampleRateNumUpDown.Value = mp3Settings.SampleRate;
                        ChannelCountNumUpDown.Value = mp3Settings.ChannelCount;
                    }
                }
                else
                {
                    SharedMethods.ErrorStop("Mp3 file path isn't set correctly or the file does not exist.\nPlease set the correct filepath for the mp3 file before trying this option.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Handled")
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LoadFromMp3Btn.Text = "Load from mp3";
            EnableDisableControls(true);
        }


        private void ConvertAudioBtn_Click(object sender, EventArgs e)
        {
            ConvertAudioBtn.Text = "Converting....";
            EnableDisableControls(false);

            try
            {
                var isConvertReady = File.Exists($"{Mp3PathTxtBox.Text}") && File.Exists($"{XXXPathTxtBox.Text}") && File.Exists($"{PS3TOCPathTxtBox.Text}");

                if (isConvertReady)
                {
                    var appSettings = new AppSettings()
                    {
                        OutMp3File = Mp3PathTxtBox.Text,
                        Mp3SampleRate = SampleRateNumUpDown.Value,
                        Mp3ChannelCount = ChannelCountNumUpDown.Value,
                        Mp3LoopStart = LoopStartNumUpDown.Value,
                        Mp3LoopEnd = LoopEndNumUpDown.Value,
                        CustomVolumeButtonChecked = CustomVolRadioButton.Checked,
                        VolumeSliderValue = VolSlider.Value,
                        IsSingleMode = true
                    };

                    var hasConvertedAudio = ProcessSCD.ConvertAudio(XXXPathTxtBox.Text, appSettings);

                    if (hasConvertedAudio)
                    {
                        var hasUpdatedTOC = ProcessTOC.SingleModeUpdate(PS3TOCPathTxtBox.Text, XXXPathTxtBox.Text);

                        if (hasUpdatedTOC)
                        {
                            MessageBox.Show("Replaced music data in XXX file and updated TOC file.\nThe XXX and the TOC files prior to the conversion process, have been renamed to '.old' files", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("TOC file was not updated as the XXX file's entry is missing in the TOC file.\nThe game might crash after it finishes playing this audio file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    SharedMethods.ErrorStop("One or more file paths aren't set correctly or the files themselves does not exist.\nPlease set the correct filepaths for all of the files before trying to convert them.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Handled")
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ConvertAudioBtn.Text = "Convert Audio";
            EnableDisableControls(true);
        }


        private void ConvertFilesBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Mp3DirTxtBox.Text) && !string.IsNullOrEmpty(XXXDirTxtBox.Text) && !string.IsNullOrEmpty(PS3TOCPathTxtBox2.Text))
            {
                ConvertFilesBtn.Text = "Converting....";
                EnableDisableControls(false);

                var mp3Dir = Mp3DirTxtBox.Text;
                var xxxDir = XXXDirTxtBox.Text;
                var tocFile = PS3TOCPathTxtBox2.Text;

                Task.Run(() =>
                {
                    try
                    {
                        var isBatchConvertReady = Directory.Exists(mp3Dir) && Directory.Exists(xxxDir) && File.Exists(tocFile);

                        if (isBatchConvertReady)
                        {
                            var mp3InfoDict = ProcessMp3.GetMp3InfoBatch(mp3Dir);
                            var procSCDfileDict = new Dictionary<string, string>();

                            string scdFile;

                            foreach (var mp3 in mp3InfoDict)
                            {
                                scdFile = Path.Combine(xxxDir, Path.GetFileNameWithoutExtension(mp3.Key) + ".XXX");

                                if (File.Exists(scdFile))
                                {
                                    var appSettings = new AppSettings()
                                    {
                                        OutMp3File = mp3.Key,
                                        Mp3SampleRate = mp3.Value.SampleRate,
                                        Mp3ChannelCount = mp3.Value.ChannelCount,
                                        Mp3LoopStart = mp3.Value.LoopStart == -1 ? 0 : mp3.Value.LoopStart,
                                        Mp3LoopEnd = mp3.Value.LoopEnd == -1 ? 0 : mp3.Value.LoopEnd,
                                        CustomVolumeButtonChecked = mp3.Value.Volume != -1,
                                        VolumeSliderValue = mp3.Value.Volume,
                                        IsSingleMode = false
                                    };

                                    var hasConvertedAudio = ProcessSCD.ConvertAudio(scdFile, appSettings);

                                    if (hasConvertedAudio)
                                    {
                                        var scdName = Path.GetFileName(scdFile);
                                        procSCDfileDict.Add(scdName, scdFile);
                                    }
                                }
                            }

                            ProcessTOC.BatchModeUpdate(tocFile, procSCDfileDict);

                            MessageBox.Show("Replaced music data in valid XXX files and updated TOC file.\nThe XXX and the TOC files prior to the conversion process, have been renamed to '.old' files", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            SharedMethods.ErrorStop("One or more paths aren't set correctly or the paths themselves does not exist.\nPlease set the correct paths for all of the files before trying to convert them.");
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "Handled")
                        {
                            MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    finally
                    {
                        BeginInvoke(new Action(() => ConvertFilesBtn.Text = "Convert Files"));
                        BeginInvoke(new Action(() => EnableDisableControls(true)));
                    }
                });
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

        private void OFLDInitializer(string ofldDesc, out VistaFolderBrowserDialog ofldVar)
        {
            ofldVar = new VistaFolderBrowserDialog
            {
                Description = ofldDesc,
                UseDescriptionForTitle = true
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
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Unable to locate the help text file.\nPlease ensure that this text file is present next to the app before using this option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}