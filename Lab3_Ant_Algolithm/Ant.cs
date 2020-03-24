using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_Ant_Algolithm
{
    public class Ant
    {
        public List<int> TabuList;
        public List<double> AllEdgesValues;
        public double SumOfPathLength;
        public double recentPathLength;
        public int CurVertex;

        public Ant(int startPosition)
        { initAnt(startPosition); }

        public void addTabuEdge(AntEdge path, int edge)
        {
            if (!TabuList.Contains(edge))
            {
                TabuList.Add(edge);
                SumOfPathLength += path.PathLength;
                recentPathLength = path.PathLength;
                AllEdgesValues.Add(recentPathLength);
                CurVertex = edge;
            }
        }

        public int getLastTabuListElem()
        { return TabuList[TabuList.Count - 1]; }

        public void addDistanceToSum(double distance)
        {
            SumOfPathLength += distance;
            AllEdgesValues.Add(distance);
        }

        public void initAnt(int startPos)
        {
            TabuList = new List<int>();
            AllEdgesValues = new List<double>();
            TabuList.Add(startPos);
            SumOfPathLength = 0.0;
            CurVertex = startPos;
        }

        public void deleteTabuEdge(AntEdge path, int edge)
        {
            if (TabuList.Contains(edge))
                SumOfPathLength -= path.PathLength;
            TabuList.Remove(edge);
        }

        public void clear()
        {
            SumOfPathLength = 0.0;
            TabuList.Clear();
            TabuList = new List<int>();
            AllEdgesValues.Clear();
            recentPathLength = 0;
            CurVertex = -1;
        }

        public bool isEnd(int amountOfVertexes)
        { return TabuList.Count == amountOfVertexes; }

        public String getPath(List<Vertex> vertexes)
        {
            if (TabuList.Count == 0)
                return "НИ ОДНОГО УЗЛА НЕ ПОСЕЩЕНО";
            if (TabuList.Count == 1)
                return "НЕ БЫЛО СОВЕРШЕННО ПУТЕШЕСТВИЙ.\nНачальный узел: " + vertexes[TabuList[0]];
            StringBuilder path = new StringBuilder();
            for (int i = 0; i < TabuList.Count; i++)
                path.Append(vertexes[TabuList[i]].Name).Append(" >> ");
            path.Append(vertexes[TabuList[0]].Name);
            return path.ToString();
        }

        public static String getPathByTabu(List<Vertex> vertexes, List<int> tabuList)
        {
            if (tabuList.Count == 0)
                return "НИ ОДНОГО УЗЛА НЕ ПОСЕЩЕНО";
            if (tabuList.Count == 1)
                return "НЕ БЫЛО СОВЕРШЕННО ПУТЕШЕСТВИЙ.\nНачальный узел: " + vertexes[tabuList[0]];
            StringBuilder path = new StringBuilder();
            for (int i = 0; i < tabuList.Count; i++)
            {
                path.Append(vertexes[tabuList[i]].Name).Append(" >> ");
            }
            path.Append(vertexes[tabuList[0]].Name);
            return path.ToString();
        }

        public double getPathLength(AntEdge[][] edges)
        {
            if (TabuList.Count <= 1)
                return 0;
            else
            {
                double sum = 0;
                for (int i = 0; i < TabuList.Count - 1; i++)
                    sum += edges[TabuList[i]][TabuList[i + 1]].PathLength;
                return sum;
            }
        }
    }
}