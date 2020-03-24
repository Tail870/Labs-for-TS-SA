using System;
using System.Collections.Generic;

namespace Lab4_Bee_Algorithm
{
    public class SphereBee : Bee
    {
        private static int count = 4; //Количество координат

        public SphereBee()
        {
            for (int i = 0; i < count; i++)
            {
                minVal.Add(-150.0);
                maxVal.Add(150.0);
                position.setCoordinate(i, Point.generateCoordinate(minVal[i], maxVal[i]));
            }
            calcFitness();
        }


        public override void calcFitness()
        {
            if (position.getCoordinates().Count <= 0)
                throw new Exception("Пространство должен быть, как минимум, одномерным");
            fitness = 0.0;
            foreach (double val in position.getCoordinates())
                fitness -= val * val;
        }

        public static List<double> getStartRange()
        {
            List<double> startRange = new List<double>();
            for (int i = 0; i < count; i++)
                startRange.Add(150.0);
            return startRange;
        }

        public static List<double> getRangeKoef()
        {
            List<double> rangeKoef = new List<double>();
            for (int i = 0; i < count; i++)
                rangeKoef.Add(0.98);
            return rangeKoef;
        }

        public string ToString()
        {
            return "SphereBee{" +
                    "fitness=" + fitness +
                    '}';
        }
    }

}
