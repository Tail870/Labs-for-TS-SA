﻿using System;

namespace Lab2_Swarm_Particles_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №2 - \"Роевоей алгоритм частиц\"\n" +
                "ИКБО-13-17, Шатилов Анатолий Александрович\n");
            int swarmSize = 100;
            double minValue = -500, maxValue = 500;
            Swarm swarm = new Swarm(swarmSize, minValue, maxValue);
            int numOfFormula = 0;
            calculateSwarmAlgorithm(swarm, numOfFormula);
            minValue = -1; maxValue = 0;
            Swarm swarm2 = new Swarm(swarmSize, minValue, maxValue);
            numOfFormula = 1;
            calculateSwarmAlgorithm(swarm2, numOfFormula);
            minValue = -500; maxValue = 500;
            Swarm swarm3 = new Swarm(swarmSize, minValue, maxValue);
            numOfFormula = 2;
            calculateSwarmAlgorithm(swarm3, numOfFormula);
            minValue = -500; maxValue = 500;
            Swarm swarm4 = new Swarm(swarmSize, minValue, maxValue);
            numOfFormula = 3;
            calculateSwarmAlgorithm(swarm4, numOfFormula);
            minValue = -500; maxValue = 600;
            Swarm swarm5 = new Swarm(swarmSize, minValue, maxValue);
            numOfFormula = 3;
            calculateSwarmAlgorithm(swarm5, numOfFormula);
            Console.WriteLine("The END.");
            Console.ReadKey();
        }

        private static void calculateSwarmAlgorithm(Swarm swarm, int numOfFormula)
        {
            swarm.calculateMax(numOfFormula);
            Console.WriteLine("\nАргумент при поиске максимума: " + swarm.GlobalMaxSpeed + "\n");
            Console.WriteLine("\nЗначение функции: " + Swarm.functionValue(swarm.GlobalMaxSpeed, numOfFormula) + "\n");
            swarm.clearParticles();
            Console.WriteLine("\n~~~~~~~~~~~~~\n");
            swarm.calculateMin(numOfFormula);
            Console.WriteLine("\nАргумент при поиске минимум: " + swarm.GlobalMaxSpeed + "\n");
            Console.WriteLine("\nЗначение функции: " + Swarm.functionValue(swarm.GlobalMaxSpeed, numOfFormula) + "\n");
            swarm.clearParticles();
            Console.WriteLine("\n~~~~~~~~~~~~~\n");
        }
    }
}