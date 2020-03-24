namespace Lab3_Ant_Algolithm
{
    public class AntEdge
    {

        public double PathLength;
        public double AmountOfPheromones;

        public AntEdge()
        {
            this.PathLength = 0;
            this.AmountOfPheromones = 0;
        }

        public AntEdge(AntEdge antEdge)
        {
            this.PathLength = antEdge.PathLength;
            this.AmountOfPheromones = antEdge.AmountOfPheromones;
        }

        public AntEdge(double pathLength, double amountOfPheromones)
        {
            this.PathLength = pathLength;
            this.AmountOfPheromones = amountOfPheromones;
        }

        public void addPheromones(double amountOfPheromones)
        { this.AmountOfPheromones += amountOfPheromones; }

        public void multiplyPheromones(double amountOfPheromones)
        { this.AmountOfPheromones *= amountOfPheromones; }
    }
}
