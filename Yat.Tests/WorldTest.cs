using System;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;

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

        [Test]
        public void ShouldGenerateNWalks()
        {
            var towns = GenerateNTowns(20);

            var actual = _sut.GenerateRandomWalks(towns, 100).ToList();

            actual.Count.Should().Be(100);
            actual.ForEach(w => w.Contains(towns));
        }

        [Test]
        public void WorldShouldRememberItsWalks()
        {
            var towns = GenerateNTowns(3);

            var actual = _sut.GenerateRandomWalks(towns, 4);

            actual.Should().BeSameAs(_sut.Walks);
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

