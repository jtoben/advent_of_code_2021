namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            PartOne(CreateCaves(input));
            PartTwo(CreateCaves(input));
        }

        private static void PartOne(List<Cave> caves)
        {
            var numberOfRoutes = FindAllRoutes(caves, strictRules: true);
            Console.WriteLine(numberOfRoutes);
        }

        private static void PartTwo(List<Cave> caves)
        {
            var numberOfRoutes = FindAllRoutes(caves, strictRules: false);
            Console.WriteLine(numberOfRoutes);
        }

        private static int FindAllRoutes(List<Cave> caves, bool strictRules)
        {
            Stack<List<Cave>> stack = new();
            stack.Push(new List<Cave> { caves.Single(x => x.Identifier == "start") });

            List<List<Cave>> routes = new();
            while (stack.Count > 0)
            {
                var currentRoute = stack.Pop();
                var currentCave = currentRoute.Last();

                foreach (var neighbour in currentCave.Neighbours)
                {
                    if (!neighbour.IsLowercase || CanSmallCaveBeAdded(currentRoute, neighbour, strictRules))
                    {
                        var newRoute = new List<Cave>();
                        newRoute.AddRange(currentRoute);
                        newRoute.Add(neighbour);

                        if (neighbour.Identifier == "end")
                        {
                            routes.Add(newRoute);
                        }
                        else
                        {
                            stack.Push(newRoute);
                        }
                    }
                }
            }

            return routes.Count;
        }

        private static List<Cave> CreateCaves(string[] input)
        {
            List<Cave> caves = new();
            foreach (var line in input)
            {
                var firstCave = new Cave(line.Split('-')[0]);
                var secondCave = new Cave(line.Split('-')[1]);

                if (caves.Contains(firstCave))
                {
                    firstCave = caves.Single(x => x.Identifier == firstCave.Identifier);
                    caves.Remove(firstCave);
                }
                if (caves.Contains(secondCave))
                {
                    secondCave = caves.Single(x => x.Identifier == secondCave.Identifier);
                    caves.Remove(secondCave);
                }

                firstCave.Neighbours.Add(secondCave);
                secondCave.Neighbours.Add(firstCave);

                caves.Add(firstCave);
                caves.Add(secondCave);
            }

            return caves;
        }

        private static bool CanSmallCaveBeAdded(List<Cave> route, Cave smallCave, bool strictRules)
        {
            int maximumCount = smallCave.Identifier == "start" || smallCave.Identifier == "end" ? 0 : 1;

            if (strictRules)
            {
                maximumCount = 0;
            } else if (route.GroupBy(x => x.Identifier).Any(x => x.First().IsLowercase && x.Count() > 1))
            {
                maximumCount = 0;
            }

            return route.Count(x => x.Identifier == smallCave.Identifier) <= maximumCount;
        }
    }
}
