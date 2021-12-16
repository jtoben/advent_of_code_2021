using System.Diagnostics;

namespace Day15
{
    class Program
    {
        private static int _maxX, _maxY;
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            PartOne(GetRiskMap(input));
            PartTwo(GetRiskMap(input));
        }

        private static void PartOne(int[,] riskMap)
        {
            var solution = SolveMap(riskMap);
            Console.WriteLine(solution);
        }

        private static void PartTwo(int[,] riskMap)
        {
            var bigRiskMap = new int[riskMap.GetLength(0) * 5, riskMap.GetLength(1) * 5];
            for (int a = 0; a < 5; a++)
            {
                for (int z = 0; z < 5; z++)
                {
                    for (int x = 0; x < riskMap.GetLength(0); x++)
                    {
                        for (int y = 0; y < riskMap.GetLength(1); y++)
                        {
                            var finalX = x + riskMap.GetLength(0) * z;
                            var finalY = y + riskMap.GetLength(1) * a;

                            bigRiskMap[finalX, finalY] = riskMap[x, y] + z + a;
                            if (bigRiskMap[finalX, finalY] > 9)
                            {
                                bigRiskMap[finalX, finalY] -= 9;
                            }
                        }
                    }
                }
            }

            var solution = SolveMap(bigRiskMap);
            Console.WriteLine(solution);
        }

        private static (int x, int y, int value) SolveMap(int[,] riskMap)
        {
            _maxX = riskMap.GetLength(0);
            _maxY = riskMap.GetLength(1);

            SortedSet<Node> unvisitedNodes = new();
            HashSet<Node> visitedNodes = new();
            Dictionary<(int x, int y), Node> nodesByPosition = new();
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    var newNode = new Node(x, y);
                    if (x == 0 && y == 0)
                    {
                        newNode.Value = 0;
                    }

                    unvisitedNodes.Add(newNode);
                    nodesByPosition.Add((x, y), newNode);
                }
            }

            while (unvisitedNodes.Count > 0)
            {
                var current = unvisitedNodes.Min;
                if (current.X == _maxX - 1 && current.Y == _maxY - 1)
                {
                    return (current.X, current.Y, current.Value);
                }

                var neighbours = GetNeighbours(nodesByPosition, current);
                foreach (var neighbour in neighbours)
                {
                    if (visitedNodes.Contains(neighbour))
                    {
                        continue;
                    }

                    var newValue = current.Value + riskMap[neighbour.X, neighbour.Y];
                    if (newValue < neighbour.Value)
                    {
                        unvisitedNodes.Remove(neighbour);
                        neighbour.Value = newValue;
                        unvisitedNodes.Add(neighbour);
                    }
                }

                unvisitedNodes.Remove(current);
                visitedNodes.Add(current);
            }

            return (-1, -1, -1);
        }

        private static int[,] GetRiskMap(string[] input)
        {
            int[,] riskMap = new int[input[0].Length, input.Length];

            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    riskMap[x, y] = int.Parse(input[y][x].ToString());
                }
            }

            return riskMap;
        }

        private static List<Node> GetNeighbours(Dictionary<(int x, int y), Node> nodesByPosition, Node currentNode)
        {
            List<Node> neighbours = new();

            int x1 = currentNode.X + 1;
            int x2 = currentNode.X - 1;
            int y1 = currentNode.Y + 1;
            int y2 = currentNode.Y - 1;

            if (x1 < _maxX)
            {
                neighbours.Add(nodesByPosition[(x1, currentNode.Y)]);
            }
            if (x2 >= 0)
            {
                neighbours.Add(nodesByPosition[(x2, currentNode.Y)]);
            }
            if (y1 < _maxY)
            {
                neighbours.Add(nodesByPosition[(currentNode.X, y1)]);
            }
            if (y2 >= 0)
            {
                neighbours.Add(nodesByPosition[(currentNode.X, y2)]);
            }

            return neighbours;
        }
    }
}
