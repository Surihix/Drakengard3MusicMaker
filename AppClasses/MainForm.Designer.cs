namespace Drakengard3MusicMaker.AppClasses
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Mp3PathTxtBox = new System.Windows.Forms.TextBox();
            this.XXXPathTxtBox = new System.Windows.Forms.TextBox();
            this.PS3TOCPathTxtBox = new System.Windows.Forms.TextBox();
            this.Mp3BrowseBtn = new System.Windows.Forms.Button();
            this.XXXBrowseBtn = new System.Windows.Forms.Button();
            this.PS3TOCBrowseBtn = new System.Windows.Forms.Button();
            this.mp3SettingsGrpBox = new System.Windows.Forms.GroupBox();
            this.ChannelCountLabel = new System.Windows.Forms.Label();
            this.SampleRateLabel = new System.Windows.Forms.Label();
            this.ChannelCountNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.SampleRateNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.GameAudioSettingsGrpBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomVolRadioButton = new System.Windows.Forms.RadioButton();
            this.OgVolRadioButton = new System.Windows.Forms.RadioButton();
            this.VolSlider = new System.Windows.Forms.TrackBar();
            this.ExperimentalGrpBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoopStartLabel = new System.Windows.Forms.Label();
            this.LoopEndNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.LoopStartNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.ConvertAudiobtn = new System.Windows.Forms.Button();
            this.Mp3PathLabel = new System.Windows.Forms.Label();
            this.XXXPathLabel = new System.Windows.Forms.Label();
            this.PS3TOCPathLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ConvertAudioBtnToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SampleRateToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ChannelCountToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.VolFromFileToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.CustomVolToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoopStartToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoopEndToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.HelpLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AboutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AppImgPictureBox = new System.Windows.Forms.PictureBox();
            this.mp3SettingsGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelCountNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRateNumUpDown)).BeginInit();
            this.GameAudioSettingsGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolSlider)).BeginInit();
            this.ExperimentalGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoopEndNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopStartNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AppImgPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Mp3PathTxtBox
            // 
            this.Mp3PathTxtBox.Location = new System.Drawing.Point(12, 192);
            this.Mp3PathTxtBox.Name = "Mp3PathTxtBox";
            this.Mp3PathTxtBox.Size = new System.Drawing.Size(488, 20);
            this.Mp3PathTxtBox.TabIndex = 1;
            this.Mp3PathTxtBox.TextChanged += new System.EventHandler(this.Mp3PathTxtBox_TextChanged);
            // 
            // XXXPathTxtBox
            // 
            this.XXXPathTxtBox.Location = new System.Drawing.Point(11, 242);
            this.XXXPathTxtBox.Name = "XXXPathTxtBox";
            this.XXXPathTxtBox.Size = new System.Drawing.Size(488, 20);
            this.XXXPathTxtBox.TabIndex = 2;
            this.XXXPathTxtBox.TextChanged += new System.EventHandler(this.XXXPathTxtBox_TextChanged);
            // 
            // PS3TOCPathTxtBox
            // 
            this.PS3TOCPathTxtBox.Location = new System.Drawing.Point(11, 292);
            this.PS3TOCPathTxtBox.Name = "PS3TOCPathTxtBox";
            this.PS3TOCPathTxtBox.Size = new System.Drawing.Size(488, 20);
            this.PS3TOCPathTxtBox.TabIndex = 3;
            this.PS3TOCPathTxtBox.TextChanged += new System.EventHandler(this.PS3TOCPathTxtBox_TextChanged);
            // 
            // Mp3BrowseBtn
            // 
            this.Mp3BrowseBtn.Location = new System.Drawing.Point(506, 191);
            this.Mp3BrowseBtn.Name = "Mp3BrowseBtn";
            this.Mp3BrowseBtn.Size = new System.Drawing.Size(62, 23);
            this.Mp3BrowseBtn.TabIndex = 4;
            this.Mp3BrowseBtn.Text = "Browse...";
            this.Mp3BrowseBtn.UseVisualStyleBackColor = true;
            this.Mp3BrowseBtn.Click += new System.EventHandler(this.Mp3BrowseBtn_Click);
            // 
            // XXXBrowseBtn
            // 
            this.XXXBrowseBtn.Location = new System.Drawing.Point(506, 241);
            this.XXXBrowseBtn.Name = "XXXBrowseBtn";
            this.XXXBrowseBtn.Size = new System.Drawing.Size(62, 23);
            this.XXXBrowseBtn.TabIndex = 5;
            this.XXXBrowseBtn.Text = "Browse...";
            this.XXXBrowseBtn.UseVisualStyleBackColor = true;
            this.XXXBrowseBtn.Click += new System.EventHandler(this.XXXBrowseBtn_Click);
            // 
            // PS3TOCBrowseBtn
            // 
            this.PS3TOCBrowseBtn.Location = new System.Drawing.Point(506, 291);
            this.PS3TOCBrowseBtn.Name = "PS3TOCBrowseBtn";
            this.PS3TOCBrowseBtn.Size = new System.Drawing.Size(62, 23);
            this.PS3TOCBrowseBtn.TabIndex = 6;
            this.PS3TOCBrowseBtn.Text = "Browse...";
            this.PS3TOCBrowseBtn.UseVisualStyleBackColor = true;
            this.PS3TOCBrowseBtn.Click += new System.EventHandler(this.PS3TOCBrowseBtn_Click);
            // 
            // mp3SettingsGrpBox
            // 
            this.mp3SettingsGrpBox.Controls.Add(this.ChannelCountLabel);
            this.mp3SettingsGrpBox.Controls.Add(this.SampleRateLabel);
            this.mp3SettingsGrpBox.Controls.Add(this.ChannelCountNumUpDown);
            this.mp3SettingsGrpBox.Controls.Add(this.SampleRateNumUpDown);
            this.mp3SettingsGrpBox.Location = new System.Drawing.Point(12, 364);
            this.mp3SettingsGrpBox.Name = "mp3SettingsGrpBox";
            this.mp3SettingsGrpBox.Size = new System.Drawing.Size(261, 99);
            this.mp3SettingsGrpBox.TabIndex = 7;
            this.mp3SettingsGrpBox.TabStop = false;
            this.mp3SettingsGrpBox.Text = "mp3 Settings :";
            // 
            // ChannelCountLabel
            // 
            this.ChannelCountLabel.AutoSize = true;
            this.ChannelCountLabel.Location = new System.Drawing.Point(145, 29);
            this.ChannelCountLabel.Name = "ChannelCountLabel";
            this.ChannelCountLabel.Size = new System.Drawing.Size(80, 13);
            this.ChannelCountLabel.TabIndex = 3;
            this.ChannelCountLabel.Text = "ChannelCount :";
            // 
            // SampleRateLabel
            // 
            this.SampleRateLabel.AutoSize = true;
            this.SampleRateLabel.Location = new System.Drawing.Point(31, 29);
            this.SampleRateLabel.Name = "SampleRateLabel";
            this.SampleRateLabel.Size = new System.Drawing.Size(71, 13);
            this.SampleRateLabel.TabIndex = 2;
            this.SampleRateLabel.Text = "SampleRate :";
            // 
            // ChannelCountNumUpDown
            // 
            this.ChannelCountNumUpDown.Location = new System.Drawing.Point(148, 45);
            this.ChannelCountNumUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ChannelCountNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChannelCountNumUpDown.Name = "ChannelCountNumUpDown";
            this.ChannelCountNumUpDown.Size = new System.Drawing.Size(72, 20);
            this.ChannelCountNumUpDown.TabIndex = 1;
            this.ChannelCountToolTip.SetToolTip(this.ChannelCountNumUpDown, "Set the number of channels present in the mp3 file.");
            this.ChannelCountNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SampleRateNumUpDown
            // 
            this.SampleRateNumUpDown.Location = new System.Drawing.Point(34, 45);
            this.SampleRateNumUpDown.Maximum = new decimal(new int[] {
            48000,
            0,
            0,
            0});
            this.SampleRateNumUpDown.Name = "SampleRateNumUpDown";
            this.SampleRateNumUpDown.Size = new System.Drawing.Size(72, 20);
            this.SampleRateNumUpDown.TabIndex = 0;
            this.SampleRateToolTip.SetToolTip(this.SampleRateNumUpDown, "Set the sample rate of the mp3 file.");
            // 
            // GameAudioSettingsGrpBox
            // 
            this.GameAudioSettingsGrpBox.Controls.Add(this.label2);
            this.GameAudioSettingsGrpBox.Controls.Add(this.CustomVolRadioButton);
            this.GameAudioSettingsGrpBox.Controls.Add(this.OgVolRadioButton);
            this.GameAudioSettingsGrpBox.Controls.Add(this.VolSlider);
            this.GameAudioSettingsGrpBox.Controls.Add(this.ExperimentalGrpBox);
            this.GameAudioSettingsGrpBox.Location = new System.Drawing.Point(12, 481);
            this.GameAudioSettingsGrpBox.Name = "GameAudioSettingsGrpBox";
            this.GameAudioSettingsGrpBox.Size = new System.Drawing.Size(555, 129);
            this.GameAudioSettingsGrpBox.TabIndex = 8;
            this.GameAudioSettingsGrpBox.TabStop = false;
            this.GameAudioSettingsGrpBox.Text = "Game Audio Settings :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "0     1      2     3     4      5     6";
            // 
            // CustomVolRadioButton
            // 
            this.CustomVolRadioButton.AutoSize = true;
            this.CustomVolRadioButton.Location = new System.Drawing.Point(48, 46);
            this.CustomVolRadioButton.Name = "CustomVolRadioButton";
            this.CustomVolRadioButton.Size = new System.Drawing.Size(117, 17);
            this.CustomVolRadioButton.TabIndex = 3;
            this.CustomVolRadioButton.TabStop = true;
            this.CustomVolRadioButton.Text = "Set Custom Volume";
            this.CustomVolToolTip.SetToolTip(this.CustomVolRadioButton, "Set a custom volume level with the slider below.");
            this.CustomVolRadioButton.UseVisualStyleBackColor = true;
            this.CustomVolRadioButton.CheckedChanged += new System.EventHandler(this.CustomVolRadioButton_CheckedChanged);
            // 
            // OgVolRadioButton
            // 
            this.OgVolRadioButton.AutoSize = true;
            this.OgVolRadioButton.Location = new System.Drawing.Point(48, 22);
            this.OgVolRadioButton.Name = "OgVolRadioButton";
            this.OgVolRadioButton.Size = new System.Drawing.Size(121, 17);
            this.OgVolRadioButton.TabIndex = 2;
            this.OgVolRadioButton.TabStop = true;
            this.OgVolRadioButton.Text = "Use Volume from file";
            this.VolFromFileToolTip.SetToolTip(this.OgVolRadioButton, "Set the volume level from the XXX file that you are replacing.");
            this.OgVolRadioButton.UseVisualStyleBackColor = true;
            this.OgVolRadioButton.CheckedChanged += new System.EventHandler(this.OgVolRadioButton_CheckedChanged);
            // 
            // VolSlider
            // 
            this.VolSlider.LargeChange = 1;
            this.VolSlider.Location = new System.Drawing.Point(34, 71);
            this.VolSlider.Maximum = 6;
            this.VolSlider.Name = "VolSlider";
            this.VolSlider.Size = new System.Drawing.Size(161, 45);
            this.VolSlider.TabIndex = 1;
            // 
            // ExperimentalGrpBox
            // 
            this.ExperimentalGrpBox.Controls.Add(this.label1);
            this.ExperimentalGrpBox.Controls.Add(this.LoopStartLabel);
            this.ExperimentalGrpBox.Controls.Add(this.LoopEndNumUpDown);
            this.ExperimentalGrpBox.Controls.Add(this.LoopStartNumUpDown);
            this.ExperimentalGrpBox.Location = new System.Drawing.Point(305, 19);
            this.ExperimentalGrpBox.Name = "ExperimentalGrpBox";
            this.ExperimentalGrpBox.Size = new System.Drawing.Size(235, 88);
            this.ExperimentalGrpBox.TabIndex = 0;
            this.ExperimentalGrpBox.TabStop = false;
            this.ExperimentalGrpBox.Text = "Experimental :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "LoopEnd :";
            // 
            // LoopStartLabel
            // 
            this.LoopStartLabel.AutoSize = true;
            this.LoopStartLabel.Location = new System.Drawing.Point(21, 26);
            this.LoopStartLabel.Name = "LoopStartLabel";
            this.LoopStartLabel.Size = new System.Drawing.Size(59, 13);
            this.LoopStartLabel.TabIndex = 2;
            this.LoopStartLabel.Text = "LoopStart :";
            // 
            // LoopEndNumUpDown
            // 
            this.LoopEndNumUpDown.Location = new System.Drawing.Point(134, 42);
            this.LoopEndNumUpDown.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.LoopEndNumUpDown.Name = "LoopEndNumUpDown";
            this.LoopEndNumUpDown.Size = new System.Drawing.Size(76, 20);
            this.LoopEndNumUpDown.TabIndex = 1;
            this.LoopEndToolTip.SetToolTip(this.LoopEndNumUpDown, "Set the loop end value in bytes.");
            // 
            // LoopStartNumUpDown
            // 
            this.LoopStartNumUpDown.Location = new System.Drawing.Point(24, 42);
            this.LoopStartNumUpDown.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.LoopStartNumUpDown.Name = "LoopStartNumUpDown";
            this.LoopStartNumUpDown.Size = new System.Drawing.Size(76, 20);
            this.LoopStartNumUpDown.TabIndex = 0;
            this.LoopStartToolTip.SetToolTip(this.LoopStartNumUpDown, "Set the loop start value in bytes.");
            // 
            // ConvertAudiobtn
            // 
            this.ConvertAudiobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConvertAudiobtn.Location = new System.Drawing.Point(396, 396);
            this.ConvertAudiobtn.Name = "ConvertAudiobtn";
            this.ConvertAudiobtn.Size = new System.Drawing.Size(115, 42);
            this.ConvertAudiobtn.TabIndex = 9;
            this.ConvertAudiobtn.Text = "Convert Audio";
            this.ConvertAudioBtnToolTip.SetToolTip(this.ConvertAudiobtn, "Convert the selected mp3 file to XXX audio file.");
            this.ConvertAudiobtn.UseVisualStyleBackColor = true;
            this.ConvertAudiobtn.Click += new System.EventHandler(this.ConvertAudiobtn_Click);
            // 
            // Mp3PathLabel
            // 
            this.Mp3PathLabel.AutoSize = true;
            this.Mp3PathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mp3PathLabel.Location = new System.Drawing.Point(12, 174);
            this.Mp3PathLabel.Name = "Mp3PathLabel";
            this.Mp3PathLabel.Size = new System.Drawing.Size(149, 15);
            this.Mp3PathLabel.TabIndex = 10;
            this.Mp3PathLabel.Text = "Select mp3 file to Encode:";
            // 
            // XXXPathLabel
            // 
            this.XXXPathLabel.AutoSize = true;
            this.XXXPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XXXPathLabel.Location = new System.Drawing.Point(12, 224);
            this.XXXPathLabel.Name = "XXXPathLabel";
            this.XXXPathLabel.Size = new System.Drawing.Size(181, 15);
            this.XXXPathLabel.TabIndex = 11;
            this.XXXPathLabel.Text = "Select XXX audio file to replace:";
            // 
            // PS3TOCPathLabel
            // 
            this.PS3TOCPathLabel.AutoSize = true;
            this.PS3TOCPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PS3TOCPathLabel.Location = new System.Drawing.Point(12, 274);
            this.PS3TOCPathLabel.Name = "PS3TOCPathLabel";
            this.PS3TOCPathLabel.Size = new System.Drawing.Size(113, 15);
            this.PS3TOCPathLabel.TabIndex = 12;
            this.PS3TOCPathLabel.Text = "Select PS3TOC file:";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(278, 639);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(29, 15);
            this.VersionLabel.TabIndex = 13;
            this.VersionLabel.Text = "v1.2";
            // 
            // HelpLinkLabel
            // 
            this.HelpLinkLabel.AutoSize = true;
            this.HelpLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLinkLabel.Location = new System.Drawing.Point(533, 639);
            this.HelpLinkLabel.Name = "HelpLinkLabel";
            this.HelpLinkLabel.Size = new System.Drawing.Size(36, 16);
            this.HelpLinkLabel.TabIndex = 14;
            this.HelpLinkLabel.TabStop = true;
            this.HelpLinkLabel.Text = "Help";
            this.HelpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkLabel_LinkClicked);
            // 
            // AboutLinkLabel
            // 
            this.AboutLinkLabel.AutoSize = true;
            this.AboutLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutLinkLabel.Location = new System.Drawing.Point(10, 639);
            this.AboutLinkLabel.Name = "AboutLinkLabel";
            this.AboutLinkLabel.Size = new System.Drawing.Size(42, 16);
            this.AboutLinkLabel.TabIndex = 15;
            this.AboutLinkLabel.TabStop = true;
            this.AboutLinkLabel.Text = "About";
            this.AboutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutlinkLabel_LinkClicked);
            // 
            // AppImgPictureBox
            // 
            this.AppImgPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AppImgPictureBox.BackgroundImage")));
            this.AppImgPictureBox.Location = new System.Drawing.Point(13, 13);
            this.AppImgPictureBox.Name = "AppImgPictureBox";
            this.AppImgPictureBox.Size = new System.Drawing.Size(556, 140);
            this.AppImgPictureBox.TabIndex = 0;
            this.AppImgPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 661);
            this.Controls.Add(this.AboutLinkLabel);
            this.Controls.Add(this.HelpLinkLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.PS3TOCPathLabel);
            this.Controls.Add(this.XXXPathLabel);
            this.Controls.Add(this.Mp3PathLabel);
            this.Controls.Add(this.ConvertAudiobtn);
            this.Controls.Add(this.GameAudioSettingsGrpBox);
            this.Controls.Add(this.mp3SettingsGrpBox);
            this.Controls.Add(this.PS3TOCBrowseBtn);
            this.Controls.Add(this.XXXBrowseBtn);
            this.Controls.Add(this.Mp3BrowseBtn);
            this.Controls.Add(this.PS3TOCPathTxtBox);
            this.Controls.Add(this.XXXPathTxtBox);
            this.Controls.Add(this.Mp3PathTxtBox);
            this.Controls.Add(this.AppImgPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Drakengard 3 Music Maker";
            this.mp3SettingsGrpBox.ResumeLayout(false);
            this.mp3SettingsGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelCountNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SampleRateNumUpDown)).EndInit();
            this.GameAudioSettingsGrpBox.ResumeLayout(false);
            this.GameAudioSettingsGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VolSlider)).EndInit();
            this.ExperimentalGrpBox.ResumeLayout(false);
            this.ExperimentalGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoopEndNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopStartNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AppImgPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AppImgPictureBox;
        private System.Windows.Forms.TextBox Mp3PathTxtBox;
        private System.Windows.Forms.TextBox XXXPathTxtBox;
        private System.Windows.Forms.TextBox PS3TOCPathTxtBox;
        private System.Windows.Forms.Button Mp3BrowseBtn;
        private System.Windows.Forms.Button XXXBrowseBtn;
        private System.Windows.Forms.Button PS3TOCBrowseBtn;
        private System.Windows.Forms.GroupBox mp3SettingsGrpBox;
        private System.Windows.Forms.GroupBox GameAudioSettingsGrpBox;
        private System.Windows.Forms.Button ConvertAudiobtn;
        private System.Windows.Forms.GroupBox ExperimentalGrpBox;
        private System.Windows.Forms.NumericUpDown ChannelCountNumUpDown;
        private System.Windows.Forms.NumericUpDown SampleRateNumUpDown;
        private System.Windows.Forms.TrackBar VolSlider;
        private System.Windows.Forms.NumericUpDown LoopEndNumUpDown;
        private System.Windows.Forms.NumericUpDown LoopStartNumUpDown;
        private System.Windows.Forms.RadioButton CustomVolRadioButton;
        private System.Windows.Forms.RadioButton OgVolRadioButton;
        private System.Windows.Forms.Label ChannelCountLabel;
        private System.Windows.Forms.Label SampleRateLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LoopStartLabel;
        private System.Windows.Forms.Label Mp3PathLabel;
        private System.Windows.Forms.Label XXXPathLabel;
        private System.Windows.Forms.Label PS3TOCPathLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ConvertAudioBtnToolTip;
        private System.Windows.Forms.ToolTip ChannelCountToolTip;
        private System.Windows.Forms.ToolTip SampleRateToolTip;
        private System.Windows.Forms.ToolTip CustomVolToolTip;
        private System.Windows.Forms.ToolTip VolFromFileToolTip;
        private System.Windows.Forms.ToolTip LoopEndToolTip;
        private System.Windows.Forms.ToolTip LoopStartToolTip;
        private System.Windows.Forms.LinkLabel HelpLinkLabel;
        private System.Windows.Forms.LinkLabel AboutLinkLabel;
    }
}

