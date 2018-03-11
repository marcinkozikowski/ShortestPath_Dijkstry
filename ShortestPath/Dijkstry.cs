using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Dijkstry
    {
        public const int INF = 1000000;
        public int[,] Graph { get; set; }

        public Dijkstry(int[,] graph)
        {
            Graph = graph;
        }

        public int[] GetPath(int SRC, int DEST)
        {
            int graphSize = Graph.GetLength(0);
            int[] dist = new int[graphSize];
            int[] prev = new int[graphSize];
            int[] nodes = new int[graphSize];

            for (int i = 0; i < dist.Length; i++)
            {
                dist[i] = prev[i] = INF;
                nodes[i] = i;
            }

            dist[SRC] = 0;
            do
            {
                int smallest = nodes[0];
                int smallestIndex = 0;
                for (int i = 1; i < graphSize; i++)
                {
                    if (dist[nodes[i]] < dist[smallest])
                    {
                        smallest = nodes[i];
                        smallestIndex = i;
                    }
                }
                graphSize--;
                nodes[smallestIndex] = nodes[graphSize];

                if (dist[smallest] == INF || smallest == DEST)
                    break;

                for (int i = 0; i < graphSize; i++)
                {
                    int v = nodes[i];
                    int newDist = dist[smallest] + Graph[smallest, v];
                    if (newDist < dist[v])
                    {
                        dist[v] = newDist;
                        prev[v] = smallest;
                    }
                }
            } while (graphSize > 0);
            return ReconstructPath(prev, SRC, DEST);
        }

        public int[] ReconstructPath(int[] prev, int SRC, int DEST)
        {
            int[] ret = new int[prev.Length];
            int currentNode = 0;
            ret[currentNode] = DEST;
            while (ret[currentNode] != INF && ret[currentNode] != SRC)
            {
                ret[currentNode + 1] = prev[ret[currentNode]];
                currentNode++;
            }
            if (ret[currentNode] != SRC)
                return null;
            int[] reversed = new int[currentNode + 1];
            for (int i = currentNode; i >= 0; i--)
                reversed[currentNode - i] = ret[i];
            return reversed;
        }

    }
}
