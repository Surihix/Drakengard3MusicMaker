using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker.Support
{
    internal class SharedMethods
    {
        public static void ErrorStop(string errorMsg)
        {
            MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw new System.Exception("Handled");
        }


        public static void IfFileExistDel(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}