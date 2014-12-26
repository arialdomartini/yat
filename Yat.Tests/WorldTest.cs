using System;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;

namespace Yat.Tests
{
    [TestFixture]
    public class WorldTest
    {
        RandomNumberGenerator _randomNumberGenerator;
        World _sut;

        [SetUp]
        public void SetUp()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
            _sut = new World(_randomNumberGenerator);
        }

        [Test]
        public void ShouldGenerateARandomWalk()
        {
            var towns = GenerateNTowns(15);

            var actual = _sut.GenerateRandomWalk(towns);

            actual.Contains(towns).Should().BeTrue();
            actual.ContainsExactly(towns).Should().BeFalse();
        }

        List<Town> GenerateNTowns(int n)
        {
            var towns = new List<Town>();
            for (var i = 0; i < n; i++) {
                towns.Add(new Town(i, i));
            }
            return towns;
        }
    }
}

