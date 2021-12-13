namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            PartOne(GetOctopusMap(input));
            PartTwo(GetOctopusMap(input));
        }

        private static void PartOne(int[,] octopusMap)
        {
            int numberOfFlashes = 0;

            for (int i = 1; i < 101; i++)
            {
                numberOfFlashes += CalculateOneRound(octopusMap);
            }

            Console.WriteLine(numberOfFlashes);
        }

        private static void PartTwo(int[,] octopusMap)
        {
            int turn = 0;
            while (true)
            {
                turn++;

                int numberOfFlashes = CalculateOneRound(octopusMap);
                if (numberOfFlashes == octopusMap.GetLength(0) * octopusMap.GetLength(1))
                {
                    break;
                }
            }

            Console.WriteLine(turn);
        }

        private static int CalculateOneRound(int[,] octopusMap)
        {
            // Up the energy of all the octopus.
            int numberOfFlashes = 0;
            for (int x = 0; x < octopusMap.GetLength(0); x++)
            {
                for (int y = 0; y < octopusMap.GetLength(1); y++)
                {
                    octopusMap[x, y] += 1;
                }
            }

            // Then keep exploding/flashing until all have exploded.
            bool flashOccurred;
            do
            {
                flashOccurred = false;
                for (int x = 0; x < octopusMap.GetLength(0); x++)
                {
                    for (int y = 0; y < octopusMap.GetLength(1); y++)
                    {
                        if (octopusMap[x, y] > 9)
                        {
                            flashOccurred = true;
                            numberOfFlashes++;
                            var neighbours = GetNeighbours(octopusMap, (x, y));

                            foreach (var neighbour in neighbours)
                            {
                                if (octopusMap[neighbour.x, neighbour.y] != 0)
                                {
                                    octopusMap[neighbour.x, neighbour.y]++;
                                }
                            }

                            octopusMap[x, y] = 0;
                        }
                    }
                }
            } while (flashOccurred);

            return numberOfFlashes;
        }

        private static int[,] GetOctopusMap(string[] input)
        {
            int[,] octopusMap = new int[input[0].Length, input.Length];

            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    octopusMap[x, y] = int.Parse(input[y][x].ToString());
                }
            }

            return octopusMap;
        }

        private static List<(int x, int y)> GetNeighbours(int[,] octopusMap, (int x, int y) position)
        {
            (int x, int y)[] neighbourPositions = new (int x, int y)[8] {
                (position.x - 1, position.y),
                (position.x + 1, position.y),
                (position.x, position.y - 1),
                (position.x, position.y + 1),
                (position.x + 1, position.y + 1),
                (position.x + 1, position.y - 1),
                (position.x - 1, position.y + 1),
                (position.x - 1, position.y - 1),
            };

            List<(int x, int y)> neighbours = new();

            foreach (var (x, y) in neighbourPositions)
            {
                if (x >= 0 && x < octopusMap.GetLength(0) && y >= 0 && y < octopusMap.GetLength(1))
                {
                    neighbours.Add((x, y));
                }
            }

            return neighbours;
        }
    }
}
