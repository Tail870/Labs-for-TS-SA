using System;

namespace Lab2_Swarm_Particles_Algorithm
{
    public class Particle
    {
        public double CurrentSpeed { set; get; }
        public double LocalMaxSpeed { set; get; }
        public double X { set; get; }

        public Particle(double currentSpeed, double localMaxSpeed, double x)
        {
            this.CurrentSpeed = currentSpeed;
            this.LocalMaxSpeed = localMaxSpeed;
            this.X = x;
        }

        public void nextIterationMax(double globalMaxSpeed, double minValue, double maxValue, int numOfFormula)
        {
            Random rand = new Random();
            double C = 1; //Сигма
            double a, b;
            a = rand.NextDouble();
            b = 1 - a;
            double y = rand.NextDouble(); //Коэффициент сдерживания окружающей среды
            CurrentSpeed = (y * (CurrentSpeed + a * (LocalMaxSpeed - X) + b * (globalMaxSpeed - X)));
            X += C * CurrentSpeed;
            if (X < minValue)
                X = minValue;
            if (X > maxValue)
                X = maxValue;
            if (Swarm.functionValue(X, numOfFormula) > Swarm.functionValue(LocalMaxSpeed, numOfFormula))
            { LocalMaxSpeed = X; }
        }

        public void nextIterationMin(double globalMaxSpeed, double MinValue, double MaxValue, int numOfFormula)
        {
            Random rand = new Random();
            double t = 1; //какой-то коэффициент
            double a, b;
            a = rand.NextDouble();
            b = 1 - a;
            double y = rand.NextDouble(); //Коэффициент сдерживания окружающей среды
            CurrentSpeed = (y * (CurrentSpeed + a * (LocalMaxSpeed - X) + b * (globalMaxSpeed - X)));
            X += t * CurrentSpeed;
            if (X < MinValue)
                X = MinValue;
            if (X > MaxValue)
                X = MaxValue;
            if (Swarm.functionValue(X, numOfFormula) < Swarm.functionValue(LocalMaxSpeed, numOfFormula))
                LocalMaxSpeed = X;
        }
    }
}