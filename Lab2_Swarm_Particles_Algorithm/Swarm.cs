using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab2_Swarm_Particles_Algorithm
{
    public class Swarm
    {
        private const int iter = 100000;
        public List<Particle> Particles { set; get; }
        public int SwarmSize { set; get; }
        public double MinValue { set; get; }
        public double MaxValue { set; get; }
        public double GlobalMaxSpeed { set; get; }

        public Swarm(int swarmSize, double minValue, double maxValue)
        {
            this.SwarmSize = swarmSize;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            Particles = new List<Particle>();
        }

        public static double functionValue(double x, int numOfFormula)
        {
            //Функции из методички
            switch (numOfFormula)
            {
                case 0:
                    //Console.WriteLine(-x * Math.Sin(Math.Pow(Math.Abs(x), 1.0 / 2.0)));
                    return (-x * Math.Sin(Math.Pow(Math.Abs(x), 1.0 / 2.0)));
                case 1:
                    // Console.WriteLine(3 * Math.Pow(x, 3) + 5 * Math.Pow(x, 2));
                    return 3 * Math.Pow(x, 3) + 5 * Math.Pow(x, 2);
                case 2:
                    //  Console.WriteLine(x * Math.Cos(Math.Pow(Math.Abs(x), 1.0 / 4.0)));
                    return (x * Math.Cos(Math.Pow(Math.Abs(x), 1.0 / 4.0)));
                case 3:
                    //   Console.WriteLine(x * Math.Cos(Math.Pow(x, (1.0 / 4.0))));
                    return (x * Math.Cos(Math.Pow(x, (1.0 / 4.0))));
            }
            throw new Exception("Некорректный номер формулы");
        }

        public void clearParticles()
        {
            if (Particles != null)
                Particles.Clear();
            Particles = new List<Particle>();
        }

        public double CalcMax(int numOfFormula)
        {
            Random rand = new Random();
            for (int i = 0; i < SwarmSize; i++)
                Particles.Add(new Particle(0, 0, MinValue + rand.NextDouble() * (MaxValue - MinValue)));
            GlobalMaxSpeed = Particles[0].LocalMaxSpeed;
            foreach (Particle p in Particles)
            {
                if (functionValue(p.X, numOfFormula) > functionValue(p.LocalMaxSpeed, numOfFormula))
                {
                    p.LocalMaxSpeed = p.X;
                    if (functionValue(p.LocalMaxSpeed, numOfFormula) > functionValue(GlobalMaxSpeed, numOfFormula))
                        GlobalMaxSpeed = p.LocalMaxSpeed;
                }
            }
            for (int i = 0; i < iter; i++)
            {
                //Подсчитать скорость
                foreach (Particle p in Particles)
                    p.NextIterationMax(GlobalMaxSpeed, MinValue, MaxValue, numOfFormula);
                foreach (Particle p in Particles)
                    if (Swarm.functionValue(p.LocalMaxSpeed, numOfFormula) > Swarm.functionValue(GlobalMaxSpeed, numOfFormula))
                    { GlobalMaxSpeed = p.LocalMaxSpeed; }
                //Вычислить лучшую скорость, значение функции в ней должно быть минимально
            }
            return GlobalMaxSpeed;
        }

        public double CalcMin(int numOfFormula)
        {
            Random rand = new Random();
            for (int i = 0; i < SwarmSize; i++)
                Particles.Add(new Particle(0, 0, MinValue + rand.NextDouble() * (MaxValue - MinValue)));
            GlobalMaxSpeed = Particles[0].LocalMaxSpeed;
            foreach (Particle p in Particles)
            {
                if (functionValue(p.X, numOfFormula) < functionValue(p.LocalMaxSpeed, numOfFormula))
                {
                    p.LocalMaxSpeed = p.X;
                    if (functionValue(p.LocalMaxSpeed, numOfFormula) < functionValue(GlobalMaxSpeed, numOfFormula))
                        GlobalMaxSpeed = p.LocalMaxSpeed;
                }
            }
            for (int i = 0; i < iter; i++)
            {
                //Подсчитать скорость
                foreach (Particle p in Particles)
                    p.nextIterationMin(GlobalMaxSpeed, MinValue, MaxValue, numOfFormula);
                foreach (Particle p in Particles)
                    if (Swarm.functionValue(p.LocalMaxSpeed, numOfFormula) < Swarm.functionValue(GlobalMaxSpeed, numOfFormula))
                        GlobalMaxSpeed = p.LocalMaxSpeed;
                //Вычислить лучшую скорость, значение функции в ней должно быть минимально
            }
            return GlobalMaxSpeed;
        }
    }
}