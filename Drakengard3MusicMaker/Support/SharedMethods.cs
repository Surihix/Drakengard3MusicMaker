using System.IO;
using System.Windows.Forms;

namespace Drakengard3MusicMaker.Support
{
    internal class SharedMethods
    {
        public static void AppMsgBox(string msgTxt, string msgHeader, MessageBoxIcon msgType)
        {
            MessageBox.Show(msgTxt, msgHeader, MessageBoxButtons.OK, msgType);
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