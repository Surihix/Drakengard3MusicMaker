namespace Drakengard3MusicMaker
{
    partial class AboutWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.AboutOKbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AboutPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AboutOKbutton
            // 
            this.AboutOKbutton.Location = new System.Drawing.Point(86, 164);
            this.AboutOKbutton.Name = "AboutOKbutton";
            this.AboutOKbutton.Size = new System.Drawing.Size(75, 23);
            this.AboutOKbutton.TabIndex = 1;
            this.AboutOKbutton.Text = "OK";
            this.AboutOKbutton.UseVisualStyleBackColor = true;
            this.AboutOKbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "App by Surihix";
            // 
            // AboutPictureBox
            // 
            this.AboutPictureBox.BackgroundImage = global::Drakengard3MusicMaker.Properties.Resources.about_img;
            this.AboutPictureBox.Location = new System.Drawing.Point(67, 14);
            this.AboutPictureBox.Name = "AboutPictureBox";
            this.AboutPictureBox.Size = new System.Drawing.Size(109, 109);
            this.AboutPictureBox.TabIndex = 3;
            this.AboutPictureBox.TabStop = false;
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 197);
            this.Controls.Add(this.AboutPictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AboutOKbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AboutOKbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox AboutPictureBox;
    }
}