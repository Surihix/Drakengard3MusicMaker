using System;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    public partial class HelpWindow : Form
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void HelpOKbutton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}