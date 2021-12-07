namespace Day7
{
    class Program
    {
        private static Dictionary<int, int> _partTwoDistances = new() { { 0, 0 } };
        static void Main(string[] args)
        {
            var crabPositions = File.ReadAllText("input.txt").Split(',').Select(int.Parse);

            PartOne(crabPositions.ToList());
            PartTwo(crabPositions.ToList());
        }

        private static void PartOne(List<int> crabPositions)
        {
            Console.WriteLine(GetLowestTotalDistance(crabPositions, usePartTwoDistanceRules: false));
        }

        private static void PartTwo(List<int> crabPositions)
        {
            Console.WriteLine(GetLowestTotalDistance(crabPositions, usePartTwoDistanceRules: true));
        }

        private static int GetLowestTotalDistance(List<int> crabPositions, bool usePartTwoDistanceRules)
        {
            int min = crabPositions.Min();
            int max = crabPositions.Max();

            var previousDistance = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                var totalDistance = 0;
                if (usePartTwoDistanceRules)
                {
                    totalDistance = crabPositions.Select(x => CalculatePartTwoDistance(Math.Abs(x - i))).Sum();
                } else
                {
                    totalDistance = crabPositions.Select(x => Math.Abs(x - i)).Sum();
                }

                if (totalDistance < previousDistance)
                {
                    previousDistance = totalDistance;
                }
                else
                {
                    // Done.
                    break;
                }
            }

            return previousDistance;
        }

        private static int CalculatePartTwoDistance(int distance)
        {
            if (!_partTwoDistances.ContainsKey(distance))
            {
                int totalDistance = 0;
                for (int i = 0; i <= distance; i++)
                {
                    totalDistance += i;
                }

                _partTwoDistances[distance] = totalDistance;
            }


            return _partTwoDistances[distance];
        }
    }
}
