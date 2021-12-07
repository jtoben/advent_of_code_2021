namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            PartOne(lines.Select(Line.FromString).Where(x => x.LineDirection != LineDirection.Diagonal).ToList());
            PartTwo(lines.Select(Line.FromString).ToList());
        }

        private static void PartOne(List<Line> lines)
        {
            Console.WriteLine(FindAllLinePositions(lines));
        }

        private static void PartTwo(List<Line> lines)
        {
            Console.WriteLine(FindAllLinePositions(lines));
        }

        private static int FindAllLinePositions(List<Line> lines)
        {
            Dictionary<(int x, int y), int> linePositions = new();
            foreach (var line in lines)
            {
                switch (line.LineDirection)
                {
                    case LineDirection.Horizontal:
                        for (int i = line.X1; i <= line.X2; i++)
                        {
                            AddToDictionary(linePositions, (i, line.Y1));
                        }
                        break;
                    case LineDirection.Vertical:
                        for (int i = line.Y1; i <= line.Y2; i++)
                        {
                            AddToDictionary(linePositions, (line.X1, i));
                        }
                        break;
                    case LineDirection.Diagonal:
                        for (int i = 0; i <= line.X2 - line.X1; i++)
                        {
                            var addition = line.Y1 > line.Y2 ? -i : i;
                            AddToDictionary(linePositions, (line.X1 + i, line.Y1 + addition));
                        }
                        break;
                }
            }

            return linePositions.Select(x => x.Value).Where(x => x > 1).Count();
        }

        private static void AddToDictionary(Dictionary<(int x, int y), int> dictionary, (int x, int y) thingToAdd)
        {
            if (!dictionary.ContainsKey(thingToAdd))
            {
                dictionary[thingToAdd] = 0;
            }

            dictionary[thingToAdd]++;
        }
    }

    public enum LineDirection
    {
        Horizontal,
        Vertical,
        Diagonal
    }

    public record Line(int X1, int X2, int Y1, int Y2)
    {
        public LineDirection LineDirection
        {
            get
            {
                if (X1 == X2)
                {
                    return LineDirection.Vertical;
                } else if (Y1 == Y2)
                {
                    return LineDirection.Horizontal;
                } else
                {
                    return LineDirection.Diagonal;
                }
            }
        }

        public static Line FromString(string lineAsString)
        {
            var parts = lineAsString.Split(" -> ");

            int x1 = int.Parse(parts[0].Split(',')[0]);
            int y1 = int.Parse(parts[0].Split(',')[1]);
            int x2 = int.Parse(parts[1].Split(',')[0]);
            int y2 = int.Parse(parts[1].Split(',')[1]);

            // I always want X1 <= X2
            // For horizontal/vertical this means just take Math.Min and Math.Max of all X & Y.
            // For diagonal, I don't want 8,0 -> 0,8 to turn into 0,0 -> 8,8, so add some logic to switch the pairs
            // 8,0 -> 0,8 becomes 0,8 -> 8,0
            // 6,4 -> 2,0 becomes 2,0 -> 6,4
            if (x1 == x2 || y1 == y2)
            {
                return new Line(Math.Min(x1, x2), Math.Max(x1, x2), Math.Min(y1, y2), Math.Max(y1, y2));
            } else
            {
                if (x1 <= x2)
                {
                    return new Line(x1, x2, y1, y2);
                } else
                {
                    return new Line(x2, x1, y2, y1);
                }
            }
        }
    }
}

