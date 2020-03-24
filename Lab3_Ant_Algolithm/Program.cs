using System;

namespace Lab3_Ant_Algolithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №3 - \"Муравьиный алгоритм\"\n" +
                "ИКБО-13-17, Шатилов Анатолий Александрович\n");
            int curTime = 0;
            Colony Colony = new Colony(11, 30);
            //Один цикл времени будет равен amountOfVertex
            while (curTime++ < Colony.MAX_TIME * 2)
            {
                if (Colony.SimulateColony() == 0)
                {
                    Colony.UpdateTrails();
                    if (curTime != Colony.MAX_TIME * 2)
                        Colony.restartAnts();
                    if (Colony.isBestChanged)
                        Console.WriteLine(String.Format("Итерация: " + curTime + "; Текущий лучший путь: {0, " + Colony.SCALE + "}", Colony.best));
                    Colony.isBestChanged = false;
                }
            }
            Console.WriteLine(String.Format("\nЛУЧШЕЕ = {0," + Colony.SCALE + "}" + "\n", Colony.best));
            Console.WriteLine(String.Format("ИНДЕКС ЛУЧШЕГО: {0}", Colony.bestIndex));
            Console.WriteLine(String.Format("ЛУЧШИЙ ПУТЬ: {0}", Colony.bestPath));
            Console.WriteLine("\n");
        }
    }
}
