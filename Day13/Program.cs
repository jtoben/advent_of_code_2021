namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var coordinates = input.TakeWhile(x => x != "").ToArray();
            var instructions = input.SkipWhile(x => x != "").Skip(1).Select(x => x.Split(' ')[2]).ToList();

            PartOne(GetPaper(coordinates), instructions.First());
            PartTwo(GetPaper(coordinates), instructions);
        }

        private static void PartOne(bool[,] paper, string firstInstruction)
        {
            paper = Fold(paper, firstInstruction);

            int dotCount = 0;
            for (int x = 0; x < paper.GetLength(0); x++)
            {
                for (int y = 0; y < paper.GetLength(1); y++)
                {
                    if (paper[x, y])
                    {
                        dotCount++;
                    }
                }
            }
            Console.WriteLine(dotCount);
        }

        private static void PartTwo(bool[,] paper, List<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                paper = Fold(paper, instruction);
            }

            PrintPaper(paper);
        }

        private static bool[,] Fold(bool[,] paper, string instruction)
        {
            int foldLine = int.Parse(instruction.Split('=')[1]);

            bool[,] newPaper;
            if (instruction[0] == 'y')
            {
                newPaper = new bool[paper.GetLength(0), foldLine];
                for (int x = 0; x < paper.GetLength(0); x++)
                {
                    for (int y = 0; y < paper.GetLength(1); y++)
                    {
                        if (y == foldLine)
                        {
                            continue;
                        }

                        if (y > foldLine)
                        {
                            var difference = y - foldLine;
                            newPaper[x, foldLine - difference] = newPaper[x, foldLine - difference] || paper[x, y];
                        }
                        else
                        {
                            newPaper[x, y] = paper[x, y];
                        }
                    }
                }
            }
            else
            {
                newPaper = new bool[foldLine, paper.GetLength(1)];
                for (int x = 0; x < paper.GetLength(0); x++)
                {
                    for (int y = 0; y < paper.GetLength(1); y++)
                    {
                        if (x == foldLine)
                        {
                            continue;
                        }

                        if (x > foldLine)
                        {
                            var difference = x - foldLine;
                            newPaper[foldLine - difference, y] = newPaper[foldLine - difference, y] || paper[x, y];
                        }
                        else
                        {
                            newPaper[x, y] = paper[x, y];
                        }
                    }
                }
            }

            return newPaper;
        }

        private static bool[,] GetPaper(string[] input)
        {
            var minX = input.Select(x => int.Parse(x.Split(',')[0])).Min();
            var maxX = input.Select(x => int.Parse(x.Split(',')[0])).Max();
            var minY = input.Select(x => int.Parse(x.Split(',')[1])).Min();
            var maxY = input.Select(x => int.Parse(x.Split(',')[1])).Max();

            bool[,] paper = new bool[maxX + 1, maxY + 1];

            foreach (var line in input)
            {
                paper[int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1])] = true;
            }

            return paper;
        }

        private static void PrintPaper(bool[,] paper)
        {
            Console.WriteLine("----------------------------------------");
            for (int y = 0; y < paper.GetLength(1); y++)
            {
                for (int x = 0; x < paper.GetLength(0); x++)
                {
                    var character = paper[x, y] ? "#" : ".";
                    Console.Write(character);
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------------");
        }
    }
}
