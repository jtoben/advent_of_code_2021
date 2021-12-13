namespace Day10
{
    class Program
    {
        private static readonly HashSet<char> _openBrackets = new()
        {
            '(',
            '[',
            '{',
            '<'
        };

        private static readonly Dictionary<char, char> _closedToOpenBrackets = new()
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' },
            { '>', '<' },
        };

        private static readonly Dictionary<char, char> _openToClosedBrackets = new()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' },
        };

        private static readonly Dictionary<char, int> _corruptedPointsPerBracket = new()
        {
            { ')', 3 },
            {']', 57 },
            {'}', 1197 },
            {'>', 25137 },
        };

        private static readonly Dictionary<char, int> _normalPointsPerBracket = new()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(string[] input)
        {
            var totalPoints = input.Select(IsCorrupt)
                .Where(x => x.isCorrupt)
                .Select(x => _corruptedPointsPerBracket[x.corruptCharacter])
                .GroupBy(x => x)
                .Select(x => x.Key * x.Count())
                .Sum();

            Console.WriteLine(totalPoints);
        }

        private static void PartTwo(string[] input)
        {
            var nonCorruptInput = input.Where(x => !IsCorrupt(x).isCorrupt).ToList();

            List<string> remainingBrackets = new();

            foreach (var line in nonCorruptInput)
            {
                Stack<char> stack = new();

                foreach (var c in line)
                {
                    if (_openBrackets.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        stack.Pop();
                    }
                }

                remainingBrackets.Add(new string(stack.Select(x => _openToClosedBrackets[x]).ToArray()));
            }

            var finalScores = remainingBrackets.Select(x => x.Aggregate(0L, (a, b) =>
            {
                a *= 5;
                a += _normalPointsPerBracket[b];

                return a;
            })).OrderBy(x => x).ToList();

            Console.WriteLine(finalScores[(finalScores.Count - 1) / 2]);
        }

        private static (bool isCorrupt, char corruptCharacter) IsCorrupt(string line)
        {
            Stack<char> stack = new();

            foreach (var c in line)
            {
                if (_openBrackets.Contains(c))
                {
                    stack.Push(c);
                }
                else
                {
                    if (Matches(c, stack.Peek()))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        // Corrupt.
                        return (true, c);
                    }
                }
            }

            return (false, default(char));
        }

        private static bool Matches(char c, char topOfStack)
        {
            return _closedToOpenBrackets[c] == topOfStack;
        }
    }
}
