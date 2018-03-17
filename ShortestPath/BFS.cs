using System;
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
        public int[,] Graph { get; set; }
        private int StartPoint;
        public int[,] PathHelp { get; set; }    //o row for nodes, 1 row for distance, 2 row for before nodes,3 row vistted or not visited
        private Queue<int> BfsQueue;

        public BFS(int[,] graph,int startPoint)
        {
            Graph = graph;
            StartPoint = startPoint;
            nodeNumber = getNodesNumber(graph);
            PathHelp = new int[4, nodeNumber];
            BfsQueue = new Queue<int>();
        }

        public void getBFSPath()
        {
            BfsQueue.Clear();
            setEmptyHelpTable(PathHelp);
            PathHelp[3, (StartPoint-1)] = VISITED;  //Start point was visited
            PathHelp[1, (StartPoint-1)] = 0;        //Distance from start point to start point equals 0
            BfsQueue.Enqueue((StartPoint-1));       //Add start point to queue
            int currentNode = -1;
                while (BfsQueue.Count()>0)       //Until queu is not empty
                {
                    currentNode = BfsQueue.Dequeue();   //take first node from queu
                if (currentNode > -1)
                {
                    for (int i = 0; i < nodeNumber; i++)
                    {
                        if ((Graph[currentNode, i] != INF))
                        {
                            if ((PathHelp[3, i] == NOVISITED))
                            {
                                BfsQueue.Enqueue(i);
                                PathHelp[3, i] = VISITED;
                                PathHelp[1, i] = PathHelp[1, currentNode]+1;
                                PathHelp[2, i] = currentNode;
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
                distance = distance + Graph[beforePoint, destenationPoint];
                destenationPoint = beforePoint;
            }
            return pathPoint;
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

        private int getNodesNumber(int[,] Graph)
        {
            double count = Convert.ToDouble(Graph.Length);
            return Convert.ToInt32(Math.Sqrt(count));
        }
    }
}
