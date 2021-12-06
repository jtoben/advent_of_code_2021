namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var binary = File.ReadAllLines("input.txt");

            PartOne(binary);
            PartTwo(binary);
        }

        private static void PartOne(string[] binary)
        {
            var gammaBinary = ""; 
            for (int i = 0; i < binary[0].Length; i++)
            {
                var zeroCount = binary.Count(x => x[i] == '0');
                var oneCount = binary.Count(x => x[i] == '1');

                gammaBinary += zeroCount > oneCount ? '0' : '1';
            }

            // Reverse it
            var deltaBinary = ReverseBinary(gammaBinary);

            Console.WriteLine(Convert.ToInt32(gammaBinary, 2) * Convert.ToInt32(deltaBinary, 2));
        }

        private static void PartTwo(string[] binary)
        {
            var oxygenGeneratorRating = "";
            var co2ScrubberRating = "";

            var mostPrevalentBits = "";

            for (int i = 0; i < binary[0].Length; i++)
            {
                var zeroCount = binary.Count(x => x[i] == '0');
                var oneCount = binary.Count(x => x[i] == '1');

                mostPrevalentBits += zeroCount > oneCount ? '0' : '1';
            }

            oxygenGeneratorRating = GetRating(binary.ToArray(), useMost: true);
            co2ScrubberRating = GetRating(binary.ToArray(), useMost: false);

            Console.WriteLine(Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(co2ScrubberRating, 2));
        }

        private static string GetRating(string[] binaryCopy, bool useMost)
        {
            for (int i = 0; i < binaryCopy[0].Length; i++)
            {
                var zeroCount = binaryCopy.Count(x => x[i] == '0');
                var oneCount = binaryCopy.Count(x => x[i] == '1');

                var preference = default(char);

                if (useMost)
                {
                    preference = zeroCount > oneCount ? '0' : '1';
                } else
                {
                    preference = oneCount < zeroCount ? '1' : '0';
                }

                binaryCopy = binaryCopy.Where(x => x[i] == preference).ToArray();

                if (binaryCopy.Length == 1)
                {
                    break;
                }
            }

            return binaryCopy[0];
        }

        private static string ReverseBinary(string originalBinary)
        {
            return new string(originalBinary.Select(x => x switch
            {
                '0' => '1',
                '1' => '0',
                _ => 'x'
            }).ToArray());
        }
    }
}

