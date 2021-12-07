namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var lanternFish = File.ReadAllText("input.txt").Split(',').Select(int.Parse);

            PartOne(lanternFish.ToList());
            PartTwo(lanternFish.ToList());
        }

        private static void PartOne(List<int> lanternFish)
        {
            Console.WriteLine(CalculateNumberOfFishAfterXDays(lanternFish, 80));
        }

        private static void PartTwo(List<int> lanternFish)
        {
            Console.WriteLine(CalculateNumberOfFishAfterXDays(lanternFish, 256));
        }

        private static long CalculateNumberOfFishAfterXDays(List<int> lanternFish, int days)
        {
            var fishGroupedBySpawnTimer = new long[9];
            foreach (var fish in lanternFish)
            {
                fishGroupedBySpawnTimer[fish]++;
            }

            for (int i = 0; i < days; i++)
            {
                long originalZeroes = fishGroupedBySpawnTimer[0];

                for (int j = 0; j <= 7; j++)
                {
                    fishGroupedBySpawnTimer[j] = fishGroupedBySpawnTimer[j + 1];
                }

                fishGroupedBySpawnTimer[6] += originalZeroes;
                fishGroupedBySpawnTimer[8] = originalZeroes;
            }

            long numberOfLanternFish = 0;
            for (int i = 0; i < fishGroupedBySpawnTimer.Length; i++)
            {
                numberOfLanternFish += fishGroupedBySpawnTimer[i];
            }

            return numberOfLanternFish;

        }
    }
}

