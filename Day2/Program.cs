namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = File.ReadAllLines("input.txt").Select(x => {
                var split = x.Split(' ');

                return (split[0], int.Parse(split[1]));
            });

            PartOne(commands);
            PartTwo(commands);
        }

        private static void PartOne(IEnumerable<(string Instruction, int Amount)> commands)
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var command in commands) {
                switch (command.Instruction)
                {
                    case "forward":
                        horizontal += command.Amount;
                        break;
                    case "down":
                        depth += command.Amount;
                        break;
                    case "up":
                        depth -= command.Amount;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }

        private static void PartTwo(IEnumerable<(string Instruction, int Amount)> commands)
        {
            int horizontal = 0;
            int aim = 0;
            int depth = 0;

            foreach (var command in commands) { 

                switch (command.Instruction)
                {
                    case "forward":
                        horizontal += command.Amount;
                        depth += command.Amount * aim;
                        break;
                    case "down":
                        aim += command.Amount;
                        break;
                    case "up":
                        aim -= command.Amount;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }
    }
}

