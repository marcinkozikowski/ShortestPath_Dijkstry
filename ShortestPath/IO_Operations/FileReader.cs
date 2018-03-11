using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.IO_Operations
{
    class FileReader
    {
        string path;
        string[] empty = new string[] { "Empty", " ", "file" };
        public FileReader(string filePath)
        {
            path = filePath;
        }
        public string[] ReadAllLines()
        {
            if (checkFile())
            {
                // Open the file to read from.
                string[] readText = File.ReadAllLines(path);
                return readText;
            }
            else
            {
                return empty;
            }
        }
        private bool checkFile()
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
