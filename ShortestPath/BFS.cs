using ShortestPath.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class BFS
    {
        public const int INF = Dijkstry.INF;
        public const int NONODESBEFORE = -1;
        public const int NOVISITED = -2;
        public const int VISITED = 1;
        private int nodeNumber = 0;
        public long[,] Graph { get; set; }
        public int Distance { get; set; }
        public ArrayList[] list;
        private int StartPoint;
        public int[,] PathHelp { get; set; }    //o row for nodes, 1 row for distance, 2 row for before nodes,3 row vistted or not visited
        public ArrayList[] Predecessors;
        private Queue<int> BfsQueue;
        public ArrayList PossiblePaths;
        List<int> pathPoints;

        public BFS(long[,] graph,int startPoint,ArrayList[] _list)
        {
            Graph = graph;
            list = _list;
            StartPoint = startPoint;
            nodeNumber = _list.Length;
            PathHelp = new int[4,nodeNumber];
            BfsQueue = new Queue<int>();
            Predecessors = new ArrayList[nodeNumber];
            for (int i = 0; i < Predecessors.Length; i++)
            {
                Predecessors[i] = new ArrayList();
            }
            PossiblePaths = new ArrayList();
            pathPoints = new List<int>();
        }

        public void getBFSPath()
        {
            int currentNode = -1;
            BfsQueue.Clear();
            setEmptyHelpTable(PathHelp);
            PathHelp[3, (StartPoint-1)] = VISITED;  //Start point was visited
            PathHelp[1, (StartPoint-1)] = 0;        //Distance from start point to start point equals 0
            BfsQueue.Enqueue((StartPoint-1));       //Add start point to queue
                while (BfsQueue.Count()>0)       //Until queu is not empty
                {
                    currentNode = BfsQueue.Dequeue();   //take first node from queu
                if (currentNode > -1)
                {
                    for (int i = 0; i < list[currentNode].Count; i++)
                    {
                        Node n = list[currentNode][i] as Node;
                            if ((PathHelp[3, n.Index] == NOVISITED))
                            {
                                BfsQueue.Enqueue(n.Index);
                                PathHelp[3, n.Index] = VISITED;
                                PathHelp[1, n.Index] = PathHelp[1, currentNode] + 1;
                                PathHelp[2, n.Index] = currentNode;
                                Predecessors[n.Index].Add(currentNode);
                            }
                            else if(PathHelp[1, n.Index]==(PathHelp[1,currentNode]-1))  //Jezeli mozna dojsc z tym samym kosztem to dodaj nastepnego poprzednika
                            {
                                if (!Predecessors[currentNode].Contains(n.Index))
                                {
                                    Predecessors[currentNode].Add(n.Index);
                                }
                            }

                    }
                }
                }
        }

        public List<int> getBFSPathToPoint(int dest)
        {
            int beforePoint = -1;
            int distance = 0;
            int destenationPoint = dest - 1;
            List<int> pathPoint = new List<int>();
            pathPoint.Add(destenationPoint);

            while(beforePoint!=(StartPoint-1))
            {
                beforePoint = PathHelp[2, destenationPoint];
                pathPoint.Add(beforePoint);
                distance = distance + getPathCost(beforePoint, destenationPoint);
                destenationPoint = beforePoint;
            }
            Distance = distance;
            return pathPoint;
        }

        public void getBFSPathToPointR(int dst,List<int> currentPath)
        {
            currentPath.Add(dst);
            if (StartPoint - 1 == dst)
            {
                PossiblePaths.Add(currentPath);
                return;
            }
            else if (Predecessors[dst].Count > 1)
            {
                for (int i = 0; i < Predecessors[dst].Count; i++)
                {
                        List<int> nowa = new List<int>();
                        nowa.AddRange(currentPath);
                        getBFSPathToPointR((int)Predecessors[dst][i], nowa);
                }
            }
            else
            {
                List<int> nowa = new List<int>();
                nowa.AddRange(currentPath);
                getBFSPathToPointR((int)Predecessors[dst][0], nowa);
            }
        }

        public List<int> getShortestBFSPath(int dst)
        {
            int shortestWay = int.MaxValue;
            int distance = 0;
            List<int> shortestPath = new List<int>();
            List<int> currentPath = new List<int>();
            ArrayList paths = PossiblePaths;
            for(int i=0;i<paths.Count;i++)
            {
                currentPath = paths[i] as List<int>;
                distance = getPathCost(currentPath);
                if(distance<shortestWay)
                {
                    shortestWay = distance;
                    shortestPath = currentPath;
                }
            }

            return shortestPath;
        }

        private void setEmptyHelpTable(int[,] helpTable)
        {
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<nodeNumber;j++)
                {
                    if (i == 0)
                    {
                        helpTable[i, j] = j;
                    }
                    else if(i==1)
                    {
                        helpTable[i, j] = INF;
                    }
                    else if(i==2)
                    {
                        helpTable[i, j] = NONODESBEFORE;
                    }
                    else if(i==3)
                    {
                        helpTable[i, j] = NOVISITED;
                    }
                }
            }
        }

        public int getPathCost(List<int> path)
        {
            int cost = 0;
            int[] pathArray = path.ToArray();
            int currentPoint = pathArray[0];
            int nextPoint = pathArray[1];
            for(int i=0;i<pathArray.Length-1;i++)
            {
                nextPoint = pathArray[i+1];
                cost = cost + getPathCost(currentPoint, nextPoint);
                currentPoint = nextPoint;
            }
            return cost;
        }

        private int getNodesNumber(int[,] Graph)
        {
            double count = Convert.ToDouble(Graph.Length);
            return Convert.ToInt32(Math.Sqrt(count));
        }

        private int getPathCost(int src, int dst)
        {
            if (src == dst)
            {
                return 0;
            }
            ArrayList nodes = list[src];
            foreach (Node n in nodes)
            {
                if (n.Index == dst)
                {
                    return n.Cost;
                }
            }
            return INF;
        }
    }
}
