using System;
using System.Collections.Generic;
using System.IO;

namespace SimulatorTool
{
    public class Helpers
    {
        public static int[] ReadIntArrayFromFile(string FilePath)
        {
            var separator = new char();
            if (Path.GetExtension(FilePath).ToLower() == ".tsv")
            {
                separator = '\t';
            }
            else if (Path.GetExtension(FilePath).ToLower() == ".csv")
            {
                separator = ',';
            }
            else if (Path.GetExtension(FilePath).ToLower() == ".txt")
            {
                separator = char.Parse(Environment.NewLine);
            }
            else
            {
                separator = ' ';
            }
            var content = new List<int>();
            try
            {
                foreach(var part in File.ReadAllText(Path.GetFullPath(FilePath)).Split(separator))
                {
                    content.Add(Int32.Parse(part.Trim()));
                }
            }
            catch
            {
                throw new FileLoadException("Cannot parse file contents!");
            }
            return content.ToArray();
        }
    }
}
