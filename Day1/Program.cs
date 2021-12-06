namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var depthMeasurements = File.ReadAllLines("input.txt").Select(int.Parse).ToList();

            PartOne(depthMeasurements);
            PartTwo(depthMeasurements);
        }

        private static void PartOne(List<int> depthMeasurements)
        {
            int increases = 0;
            for (int i = 1; i < depthMeasurements.Count; i++)
            {
                if (depthMeasurements[i] > depthMeasurements[i - 1])
                {
                    increases++;
                }
            }

            Console.WriteLine(increases);
        }

        private static void PartTwo(List<int> depthMeasurements)
        {
            int increases = 0;
            for (int i = 0; i < depthMeasurements.Count; i++)
            {
                if (i + 3 >= depthMeasurements.Count) {
                    break;
                }

                if (depthMeasurements[i + 3] > depthMeasurements[i])
                {
                    increases++;
                }
            }

            Console.WriteLine(increases);
        }
    }
}

