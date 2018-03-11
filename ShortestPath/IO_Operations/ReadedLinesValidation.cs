using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.IO_Operations
{
    class ReadedLinesValidation
    {
        string[] readedLines;

        public ReadedLinesValidation(string[] lines)
        {
            readedLines = lines;
        }

        public int[] getCitiesAndPathNumber()
        {
            int[] cityPaths = new int[2];
            string[] cityPath = readedLines[0].Split(' ');

                cityPaths[0] = int.Parse(cityPath[0]);
                cityPaths[1] = int.Parse(cityPath[1]);

            return cityPaths;
        }

        public int[] getSourceAndDestanationPoints()
        {
            int[] points = new int[2];
            string[] betweenPoints = readedLines[readedLines.Count()-1].Split(' ');
            points[0] = int.Parse(betweenPoints[0]);
            points[1] = int.Parse(betweenPoints[1]);

            return points;
        }

        public int[,] getGraphPaths(int citiesCount)
        {
            int[,] graph = new int[citiesCount,citiesCount];
            int source = 0;
            int dest = 0;
            int pathLength = 0;

            graph = writeEmptyGraph(graph);

            for (int i=1;i<readedLines.Count()-1;i++)
            {
                string line = readedLines[i];
                string[] lineSplit = line.Split(' ');

                source = int.Parse(lineSplit[0]);
                dest = int.Parse(lineSplit[1]);
                pathLength = int.Parse(lineSplit[2]);

                graph[source-1, dest-1] = pathLength;
                graph[dest-1, source-1] = pathLength;
            }
            return graph;
        }
        private int[,] writeEmptyGraph(int[,] graph)
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
