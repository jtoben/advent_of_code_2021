namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var template = input[0];
            var rules = input.Skip(2).ToDictionary(x => x.Split(" -> ")[0], x => x.Split(" -> ")[1]);

            PartOne(template, rules);
            PartTwo(template, rules);
        }

        private static void PartOne(string template, Dictionary<string, string> rules)
        {
            var (min, max) = CalculatePolymer(template, rules, 10);
            Console.WriteLine(max - min);
        }

        private static void PartTwo(string template, Dictionary<string, string> rules)
        {
            var (min, max) = CalculatePolymer(template, rules, 40);
            Console.WriteLine(max - min);
        }

        private static (long min, long max) CalculatePolymer(string template, Dictionary<string, string> rules, int numberOfTurns)
        {
            Dictionary<string, long> pairCounts = new();
            Dictionary<char, long> charCounts = template.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());

            var temp = template.Zip(template.Skip(1), (first, second) => first.ToString() + second).ToArray();
            foreach (var x in temp)
            {
                AddToDictionary(pairCounts, x, 1);
            }

            for (int i = 0; i < numberOfTurns; i++)
            {
                Dictionary<string, long> copy = new();

                foreach (var kvp in pairCounts)
                {
                    var pair = kvp.Key;
                    var count = kvp.Value;

                    var newPart = rules[pair];

                    AddToDictionary(charCounts, newPart[0], count);

                    var newPairOne = pair[0] + newPart;
                    var newPairTwo = newPart + pair[1];

                    AddToDictionary(copy, newPairOne, count);
                    AddToDictionary(copy, newPairTwo, count);
                }

                pairCounts = copy;
            }

            var min = charCounts.Select(x => x.Value).Min();
            var max = charCounts.Select(x => x.Value).Max();

            return (min, max);
        }

        private static void AddToDictionary<TKey>(Dictionary<TKey, long> dictionary, TKey key, long value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = 0L;
            }
            dictionary[key] += value;
        }
    }
}
