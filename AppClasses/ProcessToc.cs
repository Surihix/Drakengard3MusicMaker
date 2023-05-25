using System.IO;
using System.Windows.Forms;
using System;
using System.Linq;

namespace Drakengard3MusicMaker.AppClasses
{
    internal class ProcessToc
    {
        public static void EditText(string tocDir, string tocName, string outScdNameVar, long outScdFileSize)
        {
            var totalEntries = File.ReadLines(tocDir + "\\" + tocName).Count();
            using (FileStream inTocTxtFile = new FileStream(tocDir + "\\" + tocName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader tocReader = new StreamReader(inTocTxtFile))
                {
                    MainForm.ExistFileDel(tocDir + "\\" + "New_" + tocName);

                    using (FileStream outTocTxtFile = new FileStream(tocDir + "\\" + "New_" + tocName, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter tocWriter = new StreamWriter(outTocTxtFile))
                        {
                            var locatedMarker = "";
                            for (int i = 0; i < totalEntries; i++)
                            {
                                var parsedLine = tocReader.ReadLine();
                                string[] fileInfo = parsedLine.Split(' ');
                                var fileSize = Convert.ToUInt32(fileInfo[0]);
                                var size2 = Convert.ToUInt32(fileInfo[1]);
                                var filePath = fileInfo[2];
                                uint size3 = Convert.ToUInt32(fileInfo[3]);

                                if (filePath.EndsWith(outScdNameVar, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    locatedMarker = "Found";
                                    fileSize = (uint)outScdFileSize;
                                }

                                var currentLine = fileSize + " " + size2 + " " + filePath + " " + size3;
                                tocWriter.WriteLine(currentLine);
                            }

                            if (locatedMarker.Equals(""))
                            {
                                MainForm.AppMsgBox("Generated TOC file is not updated.\nThe XXX file to be replaced is missing its info in the TOC file. the game might crash after it finishes playing this audio file.", "Warning", MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MainForm.AppMsgBox("Generated new music and TOC files.\nThe 'New_' files can be added to your game's sound folder", "Sucess", MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }
    }
}