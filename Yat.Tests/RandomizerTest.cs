using System;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Yat.Tests
{
    [TestFixture]
    public class RandomNumberGeneratorTest
    {
        [Test]
        public void ShouldGenerateTwoRandomNumbers()
        {
            var sut = new RandomNumberGenerator();

            var firstNumbers = new List<int>();
            var secondNumbers = new List<int>();

            for(int i=0; i< 100; i++)
            {
                var couple = sut.GenerateCouple(0, 100);
                firstNumbers.Add(couple[0]);
                firstNumbers.Add(couple[1]);
            }

            StandardDeviation(firstNumbers).Should().BeGreaterThan(2);
        }

        [Test]
        public void TwoRandomNumbersShouldAlwaysBeDifferent()
        {
            var sut = new RandomNumberGenerator();

            for(int i=0; i< 100; i++)
            {
                var numbers = sut.GenerateCouple(0, 100);

                numbers[0].Should().NotBe(numbers[1]);
            }
        }

        double StandardDeviation(List<int> numbers)
        {
            var average = numbers.Average();
            var sumOfSquaresOfDifferences = numbers.Select(val => (val - average) * (val - average)).Sum();
            var standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / numbers.Count);

            return standardDeviation;
        }
    }
}

