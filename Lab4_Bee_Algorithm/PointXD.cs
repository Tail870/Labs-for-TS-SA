using System;
using System.Collections.Generic;

namespace Lab4_Bee_Algorithm
{
    public class PointXD : Point
    {

        private List<double> coordinates;

        public PointXD()
        {
            coordinates = new List<double>();
        }

        public PointXD(List<double> coordinates)
        {
            this.coordinates = coordinates;
        }

        public List<double> getCoordinates()
        {
            return coordinates;
        }

        public Double getCoordinate(int numOfCoordinate)
        {
            if (numOfCoordinate >= coordinates.Count || numOfCoordinate < 0)
                throw new Exception("Запрашиваеся отсутствующая координата");
            else
                return coordinates[numOfCoordinate];
        }

        public void setCoordinate(int numOfCoordinate, Double value)
        {
            if (numOfCoordinate < 0)
                throw new Exception("Запрашивается отсутствующая координата");
            else
            {
                if (numOfCoordinate > (coordinates.Count - 1))
                    coordinates.Add(value);
                else
                    coordinates[numOfCoordinate] = value;
            }
        }

        public String toString()
        {
            return "Coordinates {" + string.Join(", ", coordinates) + "}";
        }
    }
}
