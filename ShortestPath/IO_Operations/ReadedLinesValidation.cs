using ShortestPath.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.IO_Operations
{
    class ReadedLinesValidation
    {
        StreamReader fileStream;
        private int SRC = 0;
        private int DST = 0;

        public ReadedLinesValidation(StreamReader _fileStream)
        {
            fileStream = _fileStream;
        }

        public int[] getCitiesAndPathNumber()
        {
            int[] cityPaths = new int[2];
            string[] cityPath = fileStream.ReadLine().Split(' ');

                cityPaths[0] = int.Parse(cityPath[0]);
                cityPaths[1] = int.Parse(cityPath[1]);

            return cityPaths;
        }

        private void setSourceAndDestanationPoints(string line)
        {
            int[] points = new int[2];
            string[] betweenPoints = line.Split(' ');
            SRC = int.Parse(betweenPoints[0]);
            DST = int.Parse(betweenPoints[1]);
        }

        public int[] getSourceAndDestanationPoints()
        {
            string[] betweenPoints = getLastLine().Split(' ');
            SRC = int.Parse(betweenPoints[0]);
            DST = int.Parse(betweenPoints[1]);
            int[] srcdst = new int[2] { SRC, DST };
            return srcdst;
        }

        public long[,] getGraphPaths(int citiesCount, int paths)
        {
            //StreamReader reader = new StreamReader(path);
            long[,] graph = new long[citiesCount,citiesCount];
            int i = 0;
            string[] lineSplit;
            int source = 0;
            int dest = 0;
            int pathLength = 0;
            string line = "";


            graph = writeEmptyGraph(graph);

            while ((line = fileStream.ReadLine()) != null)
            {
                i++;
                if (i == paths)
                {
                    setSourceAndDestanationPoints(line);
                    break;
                }
                else
                {
                    lineSplit = line.Split(' ');
                    source = int.Parse(lineSplit[0]);
                    dest = int.Parse(lineSplit[1]);
                    pathLength = int.Parse(lineSplit[2]);

                    if (pathLength < 0)
                    {
                        throw new Exception("Distance can`t be less than 0");
                    }
                    else
                    {
                        graph[source - 1, dest - 1] = pathLength;
                        graph[dest - 1, source - 1] = pathLength;
                    }
                }
              
               
            }
            return graph;
        }

        private string getLastLine()
        {
            string m = "";
            StreamReader r = fileStream;
            while (r.EndOfStream == false)
            {
                m = r.ReadLine();
            }
            Console.WriteLine("{0}\n", m);
            r.Close();
            return m;
        }

        public ArrayList[] getGraphPaths(int citiesNumber,int pathsCount,ArrayList[] list)
        {
            string[] lineSplit;
            int source = 0;
            int dest = 0;
            int count = 0;
            int pathLength = 0;

            for (int i = 0; i < pathsCount; i++)
            {
                lineSplit = fileStream.ReadLine().Split(' ');
                source = int.Parse(lineSplit[0]);
                dest = int.Parse(lineSplit[1]);
                pathLength = int.Parse(lineSplit[2]);
                Node n = new Node((dest - 1), pathLength);
                Node n1 = new Graph.Node((source - 1), pathLength);
                if (pathLength < 0)
                {
                    throw new Exception("Distance can`t be less than 0");
                }
                else
                {
                    list[source - 1].Add(n);
                    list[dest - 1].Add(n1);
                    count++;
                }
            }
            return list;
        }

        private long[,] writeEmptyGraph(long[,] graph)
        {
            int INF = Dijkstry.INF;
            double count = Convert.ToDouble(graph.Length);
            int size = Convert.ToInt32(Math.Sqrt(count));
            for(int i=0;i<size;i++)
            {
                for(int j=0;j<size;j++)
                {
                    graph[i, j] = INF;
                }
            }
            return graph;
        }
    }
}
