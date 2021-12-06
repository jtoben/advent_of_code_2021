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

        private static void PartOne(IEnumerable<(string instruction, int amount)> commands)
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var (instruction, amount) in commands) {
                switch (instruction)
                {
                    case "forward":
                        horizontal += amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }

        private static void PartTwo(IEnumerable<(string instruction, int amount)> commands)
        {
            int horizontal = 0;
            int aim = 0;
            int depth = 0;

            foreach (var (instruction, amount) in commands) { 

                switch (instruction)
                {
                    case "forward":
                        horizontal += amount;
                        depth += amount * aim;
                        break;
                    case "down":
                        aim += amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }
    }
}

