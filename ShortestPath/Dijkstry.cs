using ShortestPath.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Dijkstry
    {
        public const int NONODESBEFORE = -1;
        public const int NOVISITED = -2;
        public const int VISITED = 1;
        public const int INF = 1000000;
        public int[,] Graph { get; set; }
        private int[] Path;
        int[] dist;
        int[] prev;
        int[] visited;
        private Queue<int> distaneQueue;
        ArrayList[] list;

        public Dijkstry(ArrayList[] _list)
        {
            list = _list;
            int size = list.Length;
            dist = new int[size];
            prev = new int[size];
            visited = new int[size];
        }
        public int[] GetPath(int SRC, int DEST)
        {
            int graphSize = list.Count();
            int smallestNode = 0;

            for (int i = 0; i < graphSize; i++)     //Inicjalizacja pustych tabele odleglosci, poprzednikow oraz nazw wierzcholkow
            {
                dist[i] = INF;
                prev[i] = NONODESBEFORE;
                visited[i] = NOVISITED;
            }

            dist[SRC] = 0;                          //odleglosc od punktu poczatkowego =0
            while(isAnyUnvisited(visited))
            {
                int i = 0;
                smallestNode = getSmallestNodeNoVisited();
                if (smallestNode==INF ||smallestNode==DEST)
                {
                    break;
                }
                visited[smallestNode] = VISITED;
                foreach(Node n in list[smallestNode])
                {
                    if(dist[n.Index]>(dist[smallestNode] + getPathCost(smallestNode, n.Index)))   
                    {
                        dist[n.Index] = dist[smallestNode] + getPathCost(smallestNode, n.Index);
                        prev[n.Index] = smallestNode;
                    }
                }
            }
            Path = ReconstructPath(prev, SRC, DEST);
            return Path;
        }
        
        private bool isAnyUnvisited(int[] list)
        {
            for(int i=0;i<list.Count();i++)
            {
                if(list[i]==NOVISITED)
                {
                    return true;
                }
            }
            return false;
        }

        private int getSmallestNodeNoVisited()
        {
            int node=INF;
            int distance = INF;
            for(int i=0;i<dist.Length;i++)
            {
                if(visited[i]==NOVISITED)
                {
                    if(dist[i]<=distance)
                    {
                        distance = dist[i];
                        node = i;
                    }
                }
            }
            return node;
        }

        public int getPathDistance()
        {
            int distance = 0;
            int currentNode = 0;
            int nextNode = 0;
            distaneQueue = new Queue<int>();
            for(int i=0;i<Path.Length;i++)      //add all path nodes to queue
            {
                distaneQueue.Enqueue(Path[i]);
            }
            currentNode = distaneQueue.Dequeue();
            while (distaneQueue.Count()>0)
            {
                nextNode = distaneQueue.Dequeue();
                distance = distance + getPathCost(currentNode, nextNode);
                currentNode = nextNode;
            }
            return distance;
        }

        private int getPathCost(int src, int dst)
        {
            if(src==dst)
            {
                return 0;
            }
            ArrayList nodes = list[src];
            for(int i=0;i<nodes.Count;i++)
            {
                Node n = nodes[i] as Node;
                if(n.Index==dst)
                {
                    return n.Cost;
                }
            }
            return INF;
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
