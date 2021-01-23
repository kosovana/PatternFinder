using System;
using System.Collections.Generic;

namespace PatternFinderApp
{
    public class Program
    {
        /// <summary>
        /// The main entrance
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>Code 0 if success</returns>
        public static Int32 Main(String[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(String.Format("{0} <pattern length> <string>",
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name));
                return 1;
            }

            String patternLengthString = args[0];
            String inputString = args[1];

            Int32 patternLength;
            if (!Int32.TryParse(patternLengthString, out patternLength))
            {
                Console.WriteLine("The pattern length must be a positive number");
                return 1;
            }

            try
            {
                Controller.ProcessInput(new PatternIO(patternLength, inputString));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 2;
            }

            return 0;
        }
    }

    /// <summary>
    /// Business logic for a user interface
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Processes data and outputs calculated results
        /// </summary>
        /// <param name="patternIO">Input-output interface</param>
        public static void ProcessInput(IPatternIO patternIO)
        {
            Dictionary<String, Int32> patterns =
                PatternFinder.CountPatterns(patternIO.InputString, patternIO.PatternLength);

            Boolean patternFound = false;
            foreach (var keyValue in patterns)
            {
                if (keyValue.Value > 1)
                {
                    patternFound = true;
                    patternIO.Display(String.Format("Pattern: {0}, quantity: {1}", keyValue.Key, keyValue.Value));
                }
            }

            if (!patternFound)
                patternIO.Display("There are no repeated patterns");
        }
    }

    /// <summary>
    /// Describes the input arguments and a way of output
    /// </summary>
    public interface IPatternIO
    {
        /// <summary>
        /// Length of the pattern
        /// </summary>
        Int32 PatternLength { get; }

        /// <summary>
        /// A string that will be searched for patterns in it
        /// </summary>
        String InputString { get; }

        /// <summary>
        /// Displays a string to the user 
        /// </summary>
        /// <param name="line">A line to display</param>
        void Display(String line);
    }

    public class PatternIO : IPatternIO
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="patternLength">Length of the pattern</param>
        /// <param name="inputString">A string that will be searched for patterns in it</param>
        public PatternIO(Int32 patternLength, String inputString)
        {
            PatternLength = patternLength;
            InputString = inputString;
        }

        /// <summary>
        /// Length of the pattern
        /// </summary>
        public Int32 PatternLength { get; }

        /// <summary>
        /// A string that will be searched for patterns in it
        /// </summary>
        public String InputString { get; }

        /// <summary>
        /// Displays a string to the user
        /// </summary>
        /// <param name="line">A line to display</param>
        public void Display(String line)
        {
            Console.WriteLine(line);
        }
    }

    /// <summary>
    /// Finds patterns in strings
    /// </summary>
    public static class PatternFinder
    {
        /// <summary>
        /// Finds and counts all the patterns of the length "patternLength" in "inputString"
        /// </summary>
        /// <remarks>The complexity of the algorithm and the memory usage is O(n)</remarks>
        /// <param name="inputString">A string that will be searched for patterns in it</param>
        /// <param name="patternLength">Length of the pattern</param>
        /// <returns>Dictionary: key - a pattern, value - the number of times the pattern occured in the inputString</returns>
        public static Dictionary<String, Int32> CountPatterns(String inputString, Int32 patternLength)
        {
            if (String.IsNullOrEmpty(inputString))
                throw new ArgumentException("The input string must contain at least one symbol");
            if (patternLength <= 0)
                throw new ArgumentException("The pattern length must be more than zero");
            if (inputString.Length < patternLength)
                throw new ArgumentException("The input string length must be more than the pattern length");

            Dictionary<String, Int32> patterns = new Dictionary<String, Int32>();
            for (Int32 i = 0; i <= inputString.Length - patternLength; i++)
            {
                String pattern = inputString.Substring(i, patternLength);
                if (patterns.ContainsKey(pattern))
                    patterns[pattern]++;
                else
                    patterns.Add(pattern, 1);
            }

            return patterns;
        }
    }
}
