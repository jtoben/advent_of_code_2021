namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var bingoNumbers = input[0].Split(',').Select(int.Parse).ToList();

            PartOne(bingoNumbers, input.Skip(1).Select(x => BingoBoard.FromString(x)).ToList());
            PartTwo(bingoNumbers, input.Skip(1).Select(x => BingoBoard.FromString(x)).ToList());
        }

        private static void PartOne(List<int> bingoNumbers, List<BingoBoard> bingoBoards)
        {
            BingoBoard? winningBoard = null;
            foreach (var number in bingoNumbers)
            {
                foreach (var board in bingoBoards)
                {
                    board.MarkNumber(number);

                    if (board.IsFinished())
                    {
                        winningBoard = board;
                        break;
                    }
                }

                if (winningBoard != null)
                {
                    Console.WriteLine(winningBoard.GetWinningValue() * number);
                    break;
                }
            }
        }

        private static void PartTwo(List<int> bingoNumbers, List<BingoBoard> bingoBoards)
        {
            BingoBoard? winningBoard = null;
            foreach (var number in bingoNumbers)
            {
                foreach (var board in bingoBoards)
                {
                    board.MarkNumber(number);
                }

                bingoBoards = bingoBoards.Where(x => !x.IsFinished()).ToList();

                if (bingoBoards.Count == 1)
                {
                    winningBoard = bingoBoards[0];
                } else if (bingoBoards.Count == 0)
                {
                    Console.WriteLine(winningBoard!.GetWinningValue() * number);
                    break;
                }
            }

        }
    }
}

