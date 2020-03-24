using System;
using System.Collections.Generic;

namespace Lab4_Bee_Algorithm
{
    public class Bee
    {

        protected List<double> minVal;
        protected List<double> maxVal;
        protected PointXD position;
        protected double fitness;

        public Bee()
        {
            minVal = new List<double>();
            maxVal = new List<double>();
            position = new PointXD();
            fitness = 0.0;
        }

        public virtual void calcFitness() { }


        public void goTo(PointXD newPos, List<Double> rangeList)
        {
            // Перелет в окрестность места, которое нашла другая пчела. Не в то же самое место!
            // К каждой из координат добавляем случайное значение
            for (int i = 0; i < newPos.getCoordinates().Count; i++)
                position.setCoordinate(i, newPos.getCoordinate(i) + Point.generateCoordinate(-rangeList[i], rangeList[i]));

            // Проверим, чтобы не выйти за заданные пределы
            checkPosition();
            // Расчитаем и сохраним целевую функцию
            calcFitness();
        }

        public void goToRandom()
        {
            for (int i = 0; i < position.getCoordinates().Count; i++)
                position.setCoordinate(i, Point.generateCoordinate(minVal[i], maxVal[i]));
            checkPosition();
            calcFitness();
        }

        public void checkPosition()
        {
            //Скорректировать координаты пчелы, если они выходят за установленные пределы
            for (int i = 0; i < position.getCoordinates().Count; i++)
            {
                if (position.getCoordinate(i) < minVal[i])
                    position.setCoordinate(i, minVal[i]);
                else
                if (position.getCoordinate(i) > maxVal[i])
                    position.setCoordinate(i, maxVal[i]);
            }
        }

        public Double getFitness()
        {
            return fitness;
        }

        public PointXD getPosition()
        {
            return position;
        }

        public bool otherPatch(List<Bee> beeList, List<Double> rangeList)
        {
            //Проверить находится ли пчела на том же участке, что и одна из пчел в bee_list.
            //rangeList - интервал изменения каждой из координат
            if (beeList.Count == 0)
                return true;

            foreach (Bee bee in beeList)
            {
                PointXD pos = bee.getPosition();
                for (int i = 0; i < position.getCoordinates().Count; i++)
                    if (Math.Abs(position.getCoordinate(i) - pos.getCoordinate(i)) > rangeList[i])
                        return true;
            }
            return false;
        }
    }
}
