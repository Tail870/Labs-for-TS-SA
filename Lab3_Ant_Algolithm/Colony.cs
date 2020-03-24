using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab3_Ant_Algolithm
{
    public class Colony
    {

        public static int SCALE = 5;
        public static double SCALE_NUM = 1;
        public static double EPS;
        public static int MAX_VERTEXES = 27;
        public static int MAX_ANTS = 30;
        public static int MAX_DISTANCE = 200;
        public static double MAX_TOURS;
        public static double MAX_TIME;
        public static double best;
        public static int bestIndex;
        public static List<double> allEdgesValues;
        public static bool isBestChanged;
        public static String bestPath;

        public static double ALPHA = 1.0;
        public static double BETA = 5.0;
        public static double RHO = 0.5;   /* Интенсивность / Испарение */
        public static double Q = 100;
        public static double INIT_PHEROMONE;

        private List<Ant> ants;
        private List<Vertex> vertexes;
        private AntEdge[,] edges;
        private string[] names = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public Colony(int amountOfVertex, int amountOfAnts)
        {
            SCALE_NUM = 1;
            for (int i = 0; i < SCALE; i++)
                SCALE_NUM *= 10;
            EPS = (1.0 / SCALE_NUM) * 10;
            isBestChanged = false;
            amountOfVertex = Math.Min(amountOfVertex, MAX_VERTEXES);
            amountOfAnts = Math.Min(amountOfAnts, MAX_ANTS);
            MAX_TOURS = MAX_ANTS * MAX_DISTANCE;
            best = MAX_TOURS;
            MAX_TIME = MAX_VERTEXES * MAX_TOURS;
            edges = new AntEdge[amountOfVertex, amountOfVertex];
            vertexes = new List<Vertex>();
            Random rand = new Random();
            for (int i = 0; i < amountOfVertex; i++)
                vertexes.Add(new Vertex((int)(rand.NextDouble() * MAX_DISTANCE), (int)(rand.NextDouble() * MAX_DISTANCE), names[i]));
            INIT_PHEROMONE = (1.0 / vertexes.Count);
            for (int from = 0; from < amountOfVertex; from++)
            {
                for (int to = 0; to < amountOfVertex; to++)
                {
                    if (from == to)
                        edges[from, to] = null;
                    else
                        if (edges[from, to] == null || edges[from, to].PathLength == 0)
                    {
                        int xd = Math.Abs(vertexes[from].X - vertexes[to].X);
                        int yd = Math.Abs(vertexes[from].Y - vertexes[to].Y);
                        edges[from, to] = new AntEdge(Math.Round(Math.Pow(xd * xd + yd * yd, (1.0 / 2.0)) * SCALE_NUM) / SCALE_NUM, INIT_PHEROMONE);
                        edges[to, from] = new AntEdge(edges[from, to]);
                    }
                }
            }
            ants = new List<Ant>();
            int vertexIndex = 0;
            for (int i = 0; i < amountOfAnts; i++)
            {
                if (vertexIndex >= vertexes.Count) vertexIndex = 0;
                ants.Add(new Ant(vertexIndex++));
            }
            showVertexesMatrix();
        }

        public int SimulateColony()
        {
            int moving = 0;
            for (int antNum = 0; antNum < ants.Count; antNum++)
                /* Убедиться, что муравью есть куда идти */
                if (ants[antNum].TabuList.Count < vertexes.Count)
                {
                    int nextVertex = ChooseNextCity(antNum);
                    ants[antNum].addTabuEdge(edges[ants[antNum].CurVertex, nextVertex], nextVertex);
                    /* Обработка окончания путешествия (из последнего города в первый */
                    if (ants[antNum].TabuList.Count == vertexes.Count)
                        ants[antNum].addDistanceToSum(edges[ants[antNum].getLastTabuListElem(), ants[antNum].TabuList[0]].PathLength);
                    moving++;
                }
            return moving;
        }

        public void restartAnts()
        {

            int to = 0;
            for (int antNum = 0; antNum < ants.Count; antNum++)
            {

                if (ants[antNum].SumOfPathLength < best)
                {
                    best = ants[antNum].SumOfPathLength;
                    bestIndex = antNum;
                    isBestChanged = true;
                    bestPath = ants[antNum].getPath(vertexes);
                    allEdgesValues = new List<double>(ants[antNum].AllEdgesValues);
                }
                ants[antNum].clear();
                ants[antNum].initAnt(to++);
                if (to >= vertexes.Count)
                    to = 0;
            }
        }


        public double antProduct(int from, int to)
        { return Math.Pow(edges[from, to].AmountOfPheromones, ALPHA) * Math.Pow(1.0 / edges[from, to].PathLength, BETA); }

        public int ChooseNextCity(int ant)
        {
            int from, to;
            double denom = 0.0;
            /* Выбрать следующий город */
            from = ants[ant].CurVertex;
            /* Расчет знаменателя */
            for (to = 0; to < vertexes.Count; to++)
                if (!ants[ant].TabuList.Contains(to))
                    denom += antProduct(from, to);
            if (denom == 0.0)
                throw new Exception("DENOM MUST BE LARGER THAN 0.0");
            do
            {
                double p;
                to++;
                if (to >= vertexes.Count)
                    to = 0;
                if (!ants[ant].TabuList.Contains(to))
                {
                    p = antProduct(from, to) / denom;
                    Random rand = new Random();
                    if (rand.NextDouble() < p)
                        break;
                }
            } while (true);
            return to;
        }

        public void UpdateTrails()
        {
            int from, to;
            /* Испарение фермента */
            for (from = 0; from < vertexes.Count; from++)
            {
                for (to = 0; to < vertexes.Count; to++)
                {
                    if (to != from)
                    {
                        edges[from, to].multiplyPheromones((1.0 - RHO));
                        if (edges[from, to].AmountOfPheromones <= EPS)
                            edges[from, to].AmountOfPheromones = INIT_PHEROMONE;
                    }
                }
            }
            /* Нанесение нового фермента */
            /* Для пути каждого муравья */
            foreach (Ant ant in ants)
            {
                /* Обновляем каждый шаг пути */
                for (int i = 0; i < vertexes.Count; i++)
                {

                    if (i < vertexes.Count - 1)
                    {
                        from = ant.TabuList[i];
                        to = ant.TabuList[i + 1];
                    }
                    else
                    {
                        from = ant.TabuList[i];
                        to = ant.TabuList[0];
                    }

                    //формула 2.2 из методички Q/L(t)
                    edges[from, to].addPheromones((Q / ant.SumOfPathLength));
                    edges[to, from].AmountOfPheromones = edges[from, to].AmountOfPheromones;

                }
            }
            foreach (AntEdge edge in edges)
                if (edge != null)
                    edge.multiplyPheromones(RHO);
        }

        public void showVertexesMatrix()
        {
            int maxLength = 3;
            for (int i = 0; i < vertexes.Count; i++)
                for (int j = 0; j < vertexes.Count; j++)
                    if (edges[i, j] != null && (edges[i, j].PathLength).ToString().Length > maxLength)
                        maxLength = String.Format("{0}" + SCALE, edges[i, j].PathLength).Length;
            maxLength++;
            Console.WriteLine("Матрица путей (NaP - Not a Path (не путь)):\n");
            StringBuilder titleRow = new StringBuilder("  ");
            StringBuilder subTitleRow = new StringBuilder("\n  ");
            foreach (Vertex v in vertexes)
            {
                int localLength = maxLength - v.Name.Length;
                int localLengthL = localLength / 2;
                int localLengthR = (localLength - localLengthL);
                for (int k = 0; k < localLengthL + 1; k++)
                    titleRow.Append(" ");
                titleRow.Append(String.Format("{0}", v.Name));
                for (int k = 0; k < localLengthR; k++)
                    titleRow.Append(" ");
                titleRow.Append("");
                for (int k = 0; k < maxLength + 1; k++)
                    subTitleRow.Append("");
            }
            titleRow.Append(subTitleRow);
            Console.WriteLine(titleRow);
            for (int i = 0; i < vertexes.Count; i++)
            {
                titleRow = new StringBuilder(vertexes[i].Name + " ");
                subTitleRow = new StringBuilder("  ");
                for (int j = 0; j < vertexes.Count; j++)
                {
                    string value;
                    if (j != i)
                        value = String.Format("{0, " + SCALE + "}", edges[i, j].PathLength);
                    else
                        value = "NaP";
                    int localLength = maxLength - value.Length;
                    int localLengthL = localLength / 2;
                    int localLengthR = (localLength - localLengthL);
                    for (int k = 0; k < localLengthL; k++)
                        titleRow.Append(" ");
                    titleRow.Append(" ").Append(value);
                    for (int k = 0; k < localLengthR; k++)
                        titleRow.Append(" ");
                    for (int k = 0; k < maxLength + 1; k++)
                        subTitleRow.Append(" ");
                }
                titleRow.Append("\n").Append(subTitleRow);
                Console.WriteLine(titleRow);
            }
            Console.WriteLine("\n");
        }
    }
}