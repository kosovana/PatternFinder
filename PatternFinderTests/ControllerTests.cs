using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PatternFinderApp;
using System.Collections.Generic;

namespace PatternFinderTests
{
    [TestClass]
    public class ControllerTests
    {
        public class TestPatternIO : IPatternIO
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="patternLength">Length of the pattern</param>
            /// <param name="inputString">A string that will be searched for patterns in it</param>
            public TestPatternIO(Int32 patternLength, String inputString)
            {
                PatternLength = patternLength;
                InputString = inputString;
                Output = new List<String>();
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
            /// Actual output
            /// </summary>
            public List<String> Output { get; }

            /// <summary>
            /// Displays a string to the user
            /// </summary>
            /// <param name="line">A line to display</param>
            public void Display(String line)
            {
                Output.Add(line);
            }
        }

        [TestMethod]
        public void NoPatternsOutput()
        {
            String inputString = "abc";
            Int32 patternLength = 2;

            TestPatternIO testPatternIO = new TestPatternIO(patternLength, inputString);
            Controller.ProcessInput(testPatternIO);

            Assert.AreEqual(1, testPatternIO.Output.Count);
            Assert.AreEqual("There are no repeated patterns", testPatternIO.Output[0]);
        }

        [TestMethod]
        public void OnePatternOutput()
        {
            String inputString = "aaa";
            Int32 patternLength = 2;

            TestPatternIO testPatternIO = new TestPatternIO(patternLength, inputString);
            Controller.ProcessInput(testPatternIO);

            Assert.AreEqual(1, testPatternIO.Output.Count);
            Assert.AreEqual("Pattern: aa, quantity: 2", testPatternIO.Output[0]);
        }

        [TestMethod]
        public void TwoPatternOutput()
        {
            String inputString = "aabyaabaa";
            Int32 patternLength = 2;

            TestPatternIO testPatternIO = new TestPatternIO(patternLength, inputString);
            Controller.ProcessInput(testPatternIO);

            Assert.AreEqual(2, testPatternIO.Output.Count);
            Assert.AreEqual("Pattern: aa, quantity: 3", testPatternIO.Output[0]);
            Assert.AreEqual("Pattern: ab, quantity: 2", testPatternIO.Output[1]);
        }
    }
}
