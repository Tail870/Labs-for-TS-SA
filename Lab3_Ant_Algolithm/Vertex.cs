using System;

namespace Lab3_Ant_Algolithm
{
    public class Vertex
    {
        public int X, Y;
        public String Name;

        public Vertex(int X, int Y, String Name)
        {
            this.X = X;
            this.Y = Y;
            this.Name = Name;
        }

        public String toString()
        {
            return "Вершина: \t" +
                    "Имя: '" + Name + '\'' +
                    ", x=" + X +
                    ", y=" + Y;
        }
    }
}
