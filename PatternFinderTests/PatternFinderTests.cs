using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PatternFinderApp;
using System.Collections.Generic;
using System.Linq;

namespace PatternFinderTests
{
    [TestClass]
    public class PatternFinderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string must contain at least one symbol")]
        public void NoInputString()
        {
            String inputString = null;
            Int32 patternLength = 3;

            PatternFinder.CountPatterns(inputString, patternLength);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string must contain at least one symbol")]
        public void EmptyInputString()
        {
            String inputString = String.Empty;
            Int32 patternLength = 2;

            PatternFinder.CountPatterns(inputString, patternLength);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The input string length must be more than the pattern length")]
        public void InputStringLengthLessThanPatternLength()
        {
            String inputString = "ase";
            Int32 patternLength = 4;

            PatternFinder.CountPatterns(inputString, patternLength);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The pattern length must be more than zero")]
        public void PatternLengthEqualsZero()
        {
            String inputString = "ase";
            Int32 patternLength = 0;

            PatternFinder.CountPatterns(inputString, patternLength);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "The pattern length must be more than zero")]
        public void PatternLengthLessThanZero()
        {
            String inputString = "ase";
            Int32 patternLength = -1;

            PatternFinder.CountPatterns(inputString, patternLength);
        }

        [TestMethod]
        public void QuantityCheck()
        {
            String inputString = "ab3ab*7";
            Int32 patternLength = 2;

            Dictionary<String, Int32> patterns = PatternFinder.CountPatterns(inputString, patternLength);

            Assert.AreEqual(5, patterns.Count);
            Assert.AreEqual(2, patterns["ab"]);
            Assert.AreEqual(1, patterns["b3"]);
            Assert.AreEqual(1, patterns["3a"]);
            Assert.AreEqual(1, patterns["b*"]);
            Assert.AreEqual(1, patterns["*7"]);
        }

        [TestMethod]
        public void PatternLengthOneSymbol()
        {
            String inputString = "abcabcabc";
            Int32 patternLength = 1;

            Dictionary<String, Int32> patterns = PatternFinder.CountPatterns(inputString, patternLength);

            Assert.AreEqual(3, patterns.Count);
            Assert.AreEqual(3, patterns["a"]);
            Assert.AreEqual(3, patterns["b"]);
            Assert.AreEqual(3, patterns["c"]);
        }

        [TestMethod]
        public void OneSymbolInputString()
        {
            String inputString = "aaaa";
            Int32 patternLength = 2;

            Dictionary<String, Int32> patterns = PatternFinder.CountPatterns(inputString, patternLength);

            Assert.AreEqual(1, patterns.Count);
            Assert.AreEqual(3, patterns["aa"]);
        }

        [TestMethod]
        public void PatternAtTheEnd()
        {
            String inputString = "ab3ab";
            Int32 patternLength = 2;

            Dictionary<String, Int32> patterns = PatternFinder.CountPatterns(inputString, patternLength);

            Assert.AreEqual(3, patterns.Count);
            Assert.AreEqual(2, patterns["ab"]);
        }

        [TestMethod]
        public void PatternsIntersection()
        {
            String inputString = "#xc513a7*c2!xa7*+3a7ckrc51g=hra7*";
            Int32 patternLength = 3;

            Dictionary<String, Int32> allPatterns = PatternFinder.CountPatterns(inputString, patternLength);

            Dictionary<String, Int32> patterns = allPatterns.Where(x => x.Value > 1).ToDictionary(x => x.Key, y => y.Value);
            
            Assert.AreEqual(3, patterns.Count);
            Assert.AreEqual(2, patterns["3a7"]);
            Assert.AreEqual(2, patterns["c51"]);
            Assert.AreEqual(3, patterns["a7*"]);
        }
    }
}
