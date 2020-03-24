using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Bee_Algorithm
{
    public class Hive
    {

        public static int Max_ScoutBees = 50;
        public static int Max_BestBee = 50;
        public static int Max_OthersBee = 50;
        public static int Max_COORDINATE_ABS = 300;
        private int scoutBees; //Количество пчёл разведчиков
        private int amountOfBestBees; //Количесто пчёл, посылаемых на лучшие участки
        private int amountOfOthersBees; // Количество пчёл, посылаемых на оставшие участки после лучших

        private int amountOfBestAreas; //Количество лучших участков
        private int amountOfOthersAreas; //Количество оставшихся участков
        private int amountOfBees; //Суммарное количество пчёл
        private List<Bee> bestAreasList; //Лучшие области, их количество определяется параметром bestAreas (Фактически, данный список состоит из ПЧЁЛ, которые нашли лучшие участки)
        private List<Bee> othersAreasList; //Оставшие области после лучших, их количесво определяется параметром othersAreas (Фактически, данный список состоит из ПЧЁЛ, которые нашли оставшиеся участки за лучшими)
        private List<double> rangeList; //Список с разбросом для каждой из N координат
        private List<Bee> swarm; //Рой пчёл, который заполняется случайным образом в конструкторе
        private List<Bee> sendedBees;
        private PointXD bestPosition; //Лучшая позиция за все итерации
        private double bestFitness; //Лучшее здоровье пчелы

        public Hive(int scoutBees, int amountOfBestBees, int amountOfOthersBees, int amountOfBestAreas, int amountOfOthersAreas, List<Double> rangeList)
        {
            this.scoutBees = Math.Min(scoutBees, Max_ScoutBees);
            this.amountOfBestBees = Math.Min(amountOfBestBees, Max_BestBee);
            this.amountOfOthersBees = Math.Min(amountOfOthersBees, Max_OthersBee);
            this.amountOfBestAreas = amountOfBestAreas;
            this.amountOfOthersAreas = amountOfOthersAreas;
            this.bestAreasList = new List<Bee>();
            this.othersAreasList = new List<Bee>();
            this.rangeList = new List<double>(rangeList);
            this.bestPosition = null;
            this.bestFitness = int.MinValue;
            this.amountOfBees = scoutBees + amountOfOthersAreas * amountOfOthersBees + amountOfBestAreas * amountOfBestBees;
            this.swarm = new List<Bee>();
            for (int i = 0; i < amountOfBees; i++)
                this.swarm.Add(new SphereBee());
            this.swarm.Sort((o1, o2) => o2.getFitness().CompareTo(o1.getFitness()));
            this.bestPosition = this.swarm[0].getPosition();
            this.bestFitness = this.swarm[0].getFitness();
        }

        public void nextStep()
        {
            this.bestAreasList = new List<Bee>();
            this.bestAreasList.Add(swarm[0]);  //В начало списка должна добавиться первая пчела

            this.sendedBees = new List<Bee>();

            foreach (Bee bee in swarm)
            {
                //Если пчела находится в пределах уже отмеченного лучшего участка, то ее положение не считаем
                if (bee.otherPatch(bestAreasList, rangeList))
                {
                    bestAreasList.Add(bee);
                    if (bestAreasList.Count == amountOfBestAreas)
                        break;
                }
            }

            othersAreasList = new List<Bee>();
            foreach (Bee bee in swarm)
            {
                if (bee.otherPatch(bestAreasList, rangeList) && bee.otherPatch(othersAreasList, rangeList))
                {
                    othersAreasList.Add(bee);
                    if (othersAreasList.Count == amountOfOthersAreas)
                        break;
                }
            }
            //Номер очередной отправляемой пчелы. 0-ую пчелу никуда не отправляем
            int beeIndex = 1;
            foreach (Bee bee in bestAreasList)
                beeIndex = sendBees(bee.getPosition(), beeIndex, amountOfBestBees);
            foreach (Bee bee in othersAreasList)
                beeIndex = sendBees(bee.getPosition(), beeIndex, amountOfOthersBees);


            //Оставшихся пчел пошлем куда попадет
            foreach (Bee bee in swarm)
                if (!sendedBees.Contains(bee))
                    bee.goToRandom();

            this.swarm.Sort((o1, o2) => o2.getFitness().CompareTo(o1.getFitness()));
            this.bestPosition = swarm[0].getPosition();
            this.bestFitness = swarm[0].getFitness();
        }

        public int sendBees(PointXD pos, int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                //Чтобы не выйти за пределы улея
                if (index == swarm.Count)
                    break;
                Bee bee = swarm[index];

                if (!(bestAreasList.Contains(bee) || othersAreasList.Contains(bee) || sendedBees.Contains(bee)))
                {
                    //Пчела не на лучших или выбранных позициях
                    bee.goTo(pos, rangeList);
                    sendedBees.Add(bee);
                }
                index++;
            }
            return index;
        }

        public PointXD getBestPosition()
        { return bestPosition; }

        public double getBestFitness()
        { return bestFitness; }

        public List<Double> getRangeList()
        { return rangeList; }

        public void setRangeList(List<Double> rangeList)
        { this.rangeList = rangeList; }
    }
}
