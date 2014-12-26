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
        public void ShouldRememberItsWalks()
        {
            var towns = GenerateNTowns(3);

            var actual = _sut.GenerateRandomWalks(towns, 4);

            actual.Should().BeSameAs(_sut.Walks);
        }

        [Test]
        public void ShouldReturnTheWalksOrderedByLength()
        {
            var towns = GenerateNTowns(5);
            _sut.GenerateRandomWalks(towns, 5);

            var walks = _sut.WalksOrderedByLength;

            Walk prev = null;
            foreach(var walk in walks)
            {
                if(prev != null)
                {
                    walk.Length.Should().BeGreaterOrEqualTo(prev.Length);
                }
                prev = walk;
            }
        }

        [Test]
        public void ShouldBeAbleToKillNWalks()
        {
            var towns = GenerateNTowns(5);
            _sut.GenerateRandomWalks(towns, 5);

            _sut.Kill(3);

            var walks = _sut.WalksOrderedByLength;
            walks.Count().Should().Be(5-3);
        }

        [Test]
        public void TheShortestWalksSurvive()
        {
            var towns = GenerateNTowns(5);
            var walks = _sut.GenerateRandomWalks(towns, 10);
            var shortest = walks.OrderBy(w => w.Length).Take(3).ToList();

            _sut.Kill(7);

            var survivors = _sut.Walks.ToList();

            survivors.Count().Should().Be(3);
            survivors.Contains(shortest[0]);
            survivors.Contains(shortest[1]);
            survivors.Contains(shortest[2]);

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

