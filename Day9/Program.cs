namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            int[,] heightMap = new int[input[0].Length, input.Length];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    heightMap[x, y] = int.Parse(input[y][x].ToString());
                }
            }

            PartOne(heightMap);
            PartTwo(heightMap);
        }

        private static void PartOne(int[,] heightMap)
        {
            var lowestPoints = GetLowestPoints(heightMap);

            Console.WriteLine(lowestPoints.Select(z => heightMap[z.x, z.y] + 1).Sum());
            Console.WriteLine();
        }

        private static void PartTwo(int[,] heightMap)
        {
            var lowestPoints = GetLowestPoints(heightMap);

            List<int> basinSizes = new();
            foreach (var point in lowestPoints)
            {
                HashSet<(int x, int y)> visitedPoints = new() { point };
                Stack<(int x, int y)> pointsToVisit = new();
                pointsToVisit.Push(point);

                int basinSize = 0;
                while (pointsToVisit.Count > 0)
                {
                    var current = pointsToVisit.Pop();
                    basinSize++;

                    var neighbours = GetNeighbours(heightMap, current)
                        .Where(z => heightMap[z.x, z.y] >= heightMap[current.x, current.y] && heightMap[z.x, z.y] != 9)
                        .Where(z => !visitedPoints.Contains(z));

                    foreach (var n in neighbours)
                    {
                        visitedPoints.Add(n);
                        pointsToVisit.Push(n);
                    }
                }

                basinSizes.Add(basinSize);
            }

            Console.WriteLine(basinSizes.OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b));
        }

        private static List<(int x, int y)> GetNeighbours(int[,] heightMap, (int x, int y) position)
        {
            (int x, int y)[] neighbourPositions = new (int x, int y)[4] {
                (position.x - 1, position.y),
                (position.x + 1, position.y),
                (position.x, position.y - 1),
                (position.x, position.y + 1)
            };

            List<(int x, int y)> neighbours = new();

            foreach (var (x, y) in neighbourPositions)
            {
                if (x >= 0 && x < heightMap.GetLength(0) && y >= 0 && y < heightMap.GetLength(1)) {
                    neighbours.Add((x, y));
                }
            }

            return neighbours;
        }

        private static List<(int x, int y)> GetLowestPoints(int[,] heightMap)
        {
            var lowestPoints = new List<(int x, int y)>();
            for (int x = 0; x < heightMap.GetLength(0); x++)
            {
                for (int y = 0; y < heightMap.GetLength(1); y++)
                {
                    var neighbours = GetNeighbours(heightMap, (x, y));

                    if (!neighbours.Any(z => heightMap[z.x, z.y] <= heightMap[x, y]))
                    {
                        lowestPoints.Add((x, y));
                    }
                }
            }

            return lowestPoints;
        }
    }
}
