using System;
using System.Collections.Generic;

namespace Lab4_Bee_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int runNum = 0;
            //Максимальное количество итераций алгоритмы
            int maxRuns = 1;
            //Максимальное количество итераций алгоритмы
            int maxIter = 10000;
            // Через такое количество итераций без нахождения лучшего решения уменьшим область поиска
            int maxFuncCounter = 10;

            //Начальное значение целевой функции
            double bestFunc = double.MinValue;

            //Количество итераций без улучшения целевой функции
            int funcCounter = 0;
            List<double> koefList = SphereBee.getRangeKoef();
            for (runNum = 0; runNum < maxRuns; runNum++)
            {
                Hive hive = new Hive(300, 30, 10, 5, 15, SphereBee.getStartRange());

                for (int i = 0; i < maxIter; i++)
                {
                    hive.nextStep();
                    if (hive.getBestFitness() > bestFunc)
                    {
                        bestFunc = hive.getBestFitness();
                        funcCounter = 0;
                        Console.WriteLine("\n*** Iteration " + runNum + 1 + "/" + i);
                        Console.WriteLine("Best position: " + hive.getBestPosition().ToString());
                        Console.WriteLine("Best fitness: " + hive.getBestFitness().ToString());
                    }
                    else
                    {
                        funcCounter++;
                        if (funcCounter == maxFuncCounter)
                        {
                            List<double> newRangeList = new List<double>();
                            for (int k = 0; k < hive.getRangeList().Count; k++)
                                newRangeList.Add(hive.getRangeList()[k] * koefList[k]);
                            hive.setRangeList(newRangeList);
                            funcCounter = 0;
                            Console.WriteLine("\n*** Iteration " + (runNum + 1) + "/" + i + " (new range)");
                            Console.WriteLine("New range: " + string.Join(", ", hive.getRangeList()));
                            Console.WriteLine("Best position: " + hive.getBestPosition().toString());
                            Console.WriteLine("Best fitness: " + hive.getBestFitness());
                        }
                    }
                }
                Console.Write("\nBEST FITNESS = " + bestFunc + "\n");
            }
        }
    }
}
