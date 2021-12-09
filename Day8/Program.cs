namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            PartOne(input.Select(x => x.Split(" | ")[1]).Select(x => x.Split(' ')).SelectMany(x => x).ToList());
            PartTwo(input);
        }

        private static void PartOne(List<string> outputValues)
        {
            var heights = new List<int>
            {
                2,
                3,
                4,
                7
            };

            Console.WriteLine(outputValues.Count(x => heights.Contains(x.Length)));
        }

        private static void PartTwo(string[] input)
        {
            // Sides of a digit:
            //     0000
            //    1    2
            //    1    2
            //     3333
            //    4    5
            //    4    5
            //     6666

            // Digit 1, 7, 4 and 8 have a unique length (2,3,4,7);
            // Three digits have length 5 (2,3,5).
            // They have 3 sides (0, 3, 6) in common. One of these is also present in digit 4.
            // This is side 3.
            // Of the other 2 sides, 1 is not found in digit 7.
            // This is side 6.
            // The last one is thus side 0.
            // Digit 9 has a length of 6, with side 3, and all of the sides of digit 1.
            // Digit 0 has a length of 6, but not side 3.
            // Digit 6 is the last remaining digit of length 6.
            // Of digits 2,3,5, one has 2 unique numbers that are also found in digit 1.
            // This is digit 3.
            // Digit 5 is length 5, where all of its sides are also in digit 6.
            // Finally, digit 2 is the last remaining length 5


            List<string> finalNumbers = new();
            foreach (var value in input)
            {
                var firstPart = value.Split(" | ")[0].Split(' ').ToList();

                var digitOne = firstPart.Single(x => x.Length == 2);
                var digitSeven = firstPart.Single(x => x.Length == 3);
                var digitFour = firstPart.Single(x => x.Length == 4);
                var digitEight = firstPart.Single(x => x.Length == 7);

                var digitsWithLengthFive = firstPart.Where(x => x.Length == 5).ToList();

                char[] overlappingCharachters = new char[3];
                int numberCounter = 0;
                for (int i = 0; i < digitsWithLengthFive[0].Length; i++)
                {
                    var currentChar = digitsWithLengthFive[0][i];
                    if (digitsWithLengthFive[1].Contains(currentChar) && digitsWithLengthFive[2].Contains(currentChar))
                    {
                        overlappingCharachters[numberCounter] = currentChar;
                        numberCounter++;
                    }
                }

                var workingCopy = overlappingCharachters.ToArray();

                var sideThree = workingCopy.Single(digitFour.Contains);
                workingCopy = workingCopy.Where(x => x != sideThree).ToArray();
                var sideSix = workingCopy.Single(x => !digitSeven.Contains(x));
                var sideZero = workingCopy.Single(x => x != sideSix);

                var digitNine = firstPart.Single(x => x.Length == 6 && x.Contains(sideThree) && x.Contains(digitOne[0]) && x.Contains(digitOne[1]));
                var digitZero = firstPart.Single(x => x.Length == 6 && !x.Contains(sideThree));
                var digitSix = firstPart.Single(x => x.Length == 6 && x != digitNine && x != digitZero);

                var digitThree = "";
                foreach (var digit in digitsWithLengthFive)
                {
                    var uniqueDigits = digit.Where(x => !overlappingCharachters.Contains(x)).ToList();
                    if (uniqueDigits.All(digitOne.Contains))
                    {
                        digitThree = digit;
                        break;
                    }
                }
                digitsWithLengthFive.Remove(digitThree);

                var digitTwo = "";
                var digitFive = "";
                foreach (var digit in digitsWithLengthFive)
                {
                    int differenceCount = digit.Count(x => !digitSix.Contains(x));
                    if (differenceCount == 0)
                    {
                        digitFive = digit;
                    } else
                    {
                        digitTwo = digit;
                    }

                }

                var secondPart = value.Split(" | ")[1].Split(' ').ToList();

                var digitsToIntegers = new Dictionary<string, int>
                {
                    { digitZero, 0 },
                    { digitOne, 1 },
                    { digitTwo, 2 },
                    { digitThree, 3 },
                    { digitFour, 4 },
                    { digitFive, 5 },
                    { digitSix, 6 },
                    { digitSeven, 7 },
                    { digitEight, 8 },
                    { digitNine, 9 },
                };

                var finalNumber = "";
                foreach (var x in secondPart)
                {
                    foreach (var key in digitsToIntegers.Keys)
                    {
                        if (x.Length != key.Length)
                        {
                            continue;
                        }

                        if (x.All(key.Contains))
                        {
                            finalNumber += digitsToIntegers[key];
                            break;
                        }
                    }
                }
                finalNumbers.Add(finalNumber);
            }

            var totalNumber = finalNumbers.Select(int.Parse).Sum();
            Console.WriteLine(totalNumber);
        }
    }
}
