using Drakengard3MusicMaker.Support;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Drakengard3MusicMaker
{
    internal class ProcessTOC
    {
        public static void EditText(string tocFile, string outSCDname)
        {
            var totalEntries = File.ReadLines(tocFile).Count();

            using (StreamReader tocReader = new StreamReader(tocFile))
            {
                var newTocFile = Path.Combine(Path.GetDirectoryName(tocFile), $"New_{Path.GetFileName(tocFile)}");
                SharedMethods.IfFileExistDel(newTocFile);

                using (FileStream outTocTxtStream = new FileStream(newTocFile, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter tocWriter = new StreamWriter(outTocTxtStream))
                    {

                        var locatedName = false;
                        for (int i = 0; i < totalEntries; i++)
                        {
                            var parsedLine = tocReader.ReadLine();
                            string[] fileInfo = parsedLine.Split(' ');
                            var fileSize = Convert.ToUInt32(fileInfo[0]);
                            var size2 = Convert.ToUInt32(fileInfo[1]);
                            var filePath = fileInfo[2];
                            uint size3 = Convert.ToUInt32(fileInfo[3]);

                            if (filePath.EndsWith(outSCDname, StringComparison.CurrentCultureIgnoreCase))
                            {
                                locatedName = true;
                                fileSize = (uint)SharedVariables.OutSCDsize;
                            }

                            var currentLine = fileSize + " " + size2 + " " + filePath + " " + size3;
                            tocWriter.WriteLine(currentLine);
                        }

                        if (locatedName)
                        {
                            SharedMethods.AppMsgBox("Generated new music and TOC files.\nThe 'New_' files can be renamed and be added in the game's sound folder", "Sucess", MessageBoxIcon.Information);
                        }
                        else
                        {
                            SharedMethods.AppMsgBox("Generated TOC file is not updated.\nThe XXX file to be replaced is missing its info in the TOC file. the game might crash after it finishes playing this audio file.", "Warning", MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
    }
}