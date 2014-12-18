using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace Yat.Tests
{
    [TestFixture()]
    public class WalkTest
    {
        [Test()]
        public void AWalkWithNoTownsHasLength0()
        {
            var sut = new Walk(new List<Town>());

            sut.Length.Should().Be(0);
        }

        [Test()]
        public void AWalkWithOnlyOneTownHasLength0()
        {
            var sut = new Walk(new List<Town> { new Town(10, 20) });

            sut.Length.Should().Be(0);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 10, 0, 2, 8)]
        [TestCase(0, 0, 3, 4, 5)]
        public void AWalkWith2TownsHasLengthEqualToTheDistanceBetweenTheTown(int x1, int y1, int x2, int y2, double distance)
        {
            var towns = new List<Town> {
                new Town(x1, y1),
                new Town(x2, y2)
            };
            var sut = new Walk(towns);

            sut.Length.Should().Be(distance);
        }

        [Test]
        public void AWalkWithSeveralTownsHasLengthEqualToTheSumOfDistances()
        {
            var townsInASquare = new List<Town> {
                new Town(0, 0),
                new Town(10, 0),
                new Town(10, 10),
                new Town(0, 10)
            };
            var sut = new Walk(townsInASquare);

            var actual = sut.Length;

            actual.Should().Be(10 + 10 + 10);
        }

        [Test]
        public void WalksAreComparedBasedOnTheirLength()
        {
            var shortPath = new List<Town> {
                new Town(0, 0),
                new Town(10, 0),
                new Town(10, 10),
                new Town(0, 10)
            };
            var longPath = new List<Town> {
                new Town(0, 0),
                new Town(100, 0),
                new Town(100, 10),
                new Town(0, 100)
            };

            var shortWalk = new Walk(shortPath);
            var longWalk = new Walk(longPath);

            shortWalk.CompareTo(longWalk).Should().Be(-1);
        }

    }
}