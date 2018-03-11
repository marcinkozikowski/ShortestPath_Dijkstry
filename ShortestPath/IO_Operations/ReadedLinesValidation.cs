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
    }
}
