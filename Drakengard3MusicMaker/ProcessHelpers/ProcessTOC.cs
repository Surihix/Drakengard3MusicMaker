using Drakengard3MusicMaker.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Drakengard3MusicMaker.ProcessHelpers
{
    internal class ProcessTOC
    {
        public static bool SingleModeUpdate(string tocFile, string inScdFile)
        {
            var totalEntries = File.ReadLines(tocFile).Count();
            var outSCDname = Path.GetFileName(inScdFile);
            var hasUpdatedTOC = false;

            var newTocFile = Path.Combine(Path.GetDirectoryName(tocFile), $"New_{Path.GetFileName(tocFile)}");
            SharedMethods.IfFileExistDel(newTocFile);

            using (StreamReader tocReader = new StreamReader(tocFile))
            {
                using (FileStream outTocTxtStream = new FileStream(newTocFile, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter tocWriter = new StreamWriter(outTocTxtStream))
                    {
                        for (int i = 0; i < totalEntries; i++)
                        {
                            var parsedLine = tocReader.ReadLine();
                            var fileInfo = parsedLine.Split(' ');
                            var fileSize = Convert.ToUInt32(fileInfo[0]);
                            var size2 = Convert.ToUInt32(fileInfo[1]);
                            var filePath = fileInfo[2];
                            uint size3 = Convert.ToUInt32(fileInfo[3]);

                            if (filePath.EndsWith(outSCDname, StringComparison.CurrentCultureIgnoreCase))
                            {
                                hasUpdatedTOC = true;
                                fileSize = (uint)new FileInfo(inScdFile).Length;
                            }

                            var currentLine = fileSize + " " + size2 + " " + filePath + " " + size3;
                            tocWriter.WriteLine(currentLine);
                        }
                    }
                }
            }

            if (hasUpdatedTOC)
            {
                SharedMethods.IfFileExistDel(tocFile + ".old");
                File.Move(tocFile, tocFile + ".old");
                File.Move(newTocFile, tocFile);

                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool BatchModeUpdate(string tocFile, Dictionary<string, string> procSCDfileDict)
        {
            var totalEntries = File.ReadLines(tocFile).Count();

            var newTocFile = Path.Combine(Path.GetDirectoryName(tocFile), $"New_{Path.GetFileName(tocFile)}");
            SharedMethods.IfFileExistDel(newTocFile);
            var hasUpdatedTOC = false;

            using (StreamReader tocReader = new StreamReader(tocFile))
            {
                using (FileStream outTocTxtStream = new FileStream(newTocFile, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter tocWriter = new StreamWriter(outTocTxtStream))
                    {
                        for (int i = 0; i < totalEntries; i++)
                        {
                            var parsedLine = tocReader.ReadLine();
                            var fileInfo = parsedLine.Split(' ');
                            var fileSize = Convert.ToUInt32(fileInfo[0]);
                            var size2 = Convert.ToUInt32(fileInfo[1]);
                            var filePath = fileInfo[2];
                            var fileName = Path.GetFileName(filePath);
                            uint size3 = Convert.ToUInt32(fileInfo[3]);

                            if (procSCDfileDict.ContainsKey(fileName.ToUpper()))
                            {
                                fileSize = (uint)new FileInfo(procSCDfileDict[fileName.ToUpper()]).Length;

                                if (!hasUpdatedTOC)
                                {
                                    hasUpdatedTOC = true;
                                }
                            }

                            var currentLine = fileSize + " " + size2 + " " + filePath + " " + size3;
                            tocWriter.WriteLine(currentLine);
                        }
                    }
                }
            }

            if (hasUpdatedTOC)
            {
                SharedMethods.IfFileExistDel(tocFile + ".old");
                File.Move(tocFile, tocFile + ".old");
                File.Move(newTocFile, tocFile);

                return true;
            }
            else
            {
                SharedMethods.IfFileExistDel(newTocFile);
                return false;
            }
        }
    }
}