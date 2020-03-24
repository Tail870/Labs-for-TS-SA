using System;

namespace Lab4_Bee_Algorithm
{
    public abstract class Point
    {
        public static double generateCoordinate(double a, double b)
        { return a + new Random().NextDouble() * (b - a); }
    }
}