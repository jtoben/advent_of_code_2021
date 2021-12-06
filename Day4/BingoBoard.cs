namespace Day4
{
    internal class BingoBoard
    {
        private bool[,] _markedPositions = new bool[5, 5];

        public int[,] Numbers { get;} = new int[5, 5];

        public void MarkNumber(int number)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (Numbers[x, y] == number)
                    {
                        _markedPositions[x, y] = true;
                        return;
                    }
                }
            }
        }

        public bool IsFinished()
        {
            for (int x = 0; x < 5; x++)
            {
                bool lineIsFinished = true;
                for (int y = 0; y < 5; y++)
                {
                    if (!_markedPositions[x, y])
                    {
                        lineIsFinished = false;
                        break;
                    }
                }

                if (lineIsFinished)
                {
                    return true;
                }
            }

            for (int y = 0; y < 5; y++)
            {
                bool rowIsFinished = true;
                for (int x = 0; x < 5; x++)
                {
                    if (!_markedPositions[x, y])
                    {
                        rowIsFinished = false;
                        break;
                    }
                }

                if (rowIsFinished)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetWinningValue()
        {
            int value = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (!_markedPositions[x, y])
                    {
                        value += Numbers[x, y];
                    }
                }
            }

            return value;
        }
        
        public static BingoBoard FromString(string boardAsString)
        {
            var board = new BingoBoard();

            var lines = boardAsString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int x = 0; x < lines.Length; x++)
            {
                var numbers = lines[x].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                for (int y = 0; y < numbers.Length; y++)
                {
                    board.Numbers[x, y] = numbers[y];
                }
            }

            return board;
        }
    }
}
