using System;
using System.Windows.Forms;

namespace Drakengard3MusicMaker.AppClasses
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutOKbutton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
