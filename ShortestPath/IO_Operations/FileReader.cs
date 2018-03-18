using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.IO_Operations
{
    class FileReaderWriter
    {
        string path;
        string[] empty = new string[] { "Empty", " ", "file" };
        public FileReaderWriter(string filePath)
        {
            path = filePath;
        }

        public StreamReader ReadAllLines()
        {
            if (checkFile())
            {
                StreamReader fileStream = new StreamReader(path);
                return fileStream;
            }
            else
            {
                return null;
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

        public void WriteToFile(int[,] graph, int _SRC, int _DST,string pathFile,ArrayList[] list)
        {
            string bfsS = "";
            string dijkstryS = "";

            int INF = Dijkstry.INF;
            int SRC = _SRC;
            int DEST = _DST;
            var dijkstra = new Dijkstry(list);
            int[] path = dijkstra.GetPath(SRC - 1, DEST - 1);

            string pathDi = "";
            string costDi = "";
            for (int i = 0; i < path.Length; i++)
            {
                pathDi = pathDi + (path[i] + 1) + " ";
            }
            costDi += dijkstra.getPathDistance();

            dijkstryS += costDi + Environment.NewLine + pathDi+Environment.NewLine;


            var bfs = new BFS(graph, _SRC,list);
            bfs.getBFSPath();
            List<int> pathBFS = new List<int>();
            pathBFS = bfs.getBFSPathToPoint(DEST);
            bfsS = bfsS + (pathBFS.Count() - 1)+Environment.NewLine;
            string pathS = "";
            pathBFS.Reverse();
            foreach (int a in pathBFS)
            {
                pathS = pathS +(a + 1)+" ";
            }
            bfsS += pathS;

            string all = dijkstryS + Environment.NewLine + bfsS;

            File.WriteAllText(pathFile, all);
        }
    }
}
