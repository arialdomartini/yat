using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace Yat.Tests
{
    [TestFixture()]
    public class WalkTest
    {
        RandomNumberGenerator _randomNumberGenerator;

        [SetUp]
        public void SetUp()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
        }

        [Test()]
        public void AWalkWithNoTownsHasLength0()
        {
            var sut = new Walk(_randomNumberGenerator, new List<Town>());

            sut.Length.Should().Be(0);
        }

        [Test()]
        public void AWalkWithOnlyOneTownHasLength0()
        {
            var sut = new Walk(_randomNumberGenerator, new List<Town> { new Town(10, 20) });

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
            var sut = new Walk(_randomNumberGenerator, towns);

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
            var sut = new Walk(_randomNumberGenerator, townsInASquare);

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

            var shortWalk = new Walk(_randomNumberGenerator, shortPath);
            var longWalk = new Walk(_randomNumberGenerator, longPath);

            shortWalk.CompareTo(longWalk).Should().Be(-1);
        }

        [Test]
        public void TwoWalkAreCompatibleIfTheyWalksThrouTheSameTowns()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var sut = new Walk(_randomNumberGenerator, new List<Town> { town1, town2 });
            var other = new Walk(_randomNumberGenerator, new List<Town> { town1, town2 });

            sut.IsCompatibleWith(other).Should().BeTrue();
        }

        [Test]
        public void ContainsIsTrueWhenGivenTheSameExactList()
        {
            var towns = new List<Town> {
                new Town(1, 1),
                new Town(2, 2)
            };
            var sut = new Walk(_randomNumberGenerator, towns);

            sut.Contains(towns).Should().BeTrue();
        }

        [Test]
        public void AWalkContainsAListOfTownsEvenIfGivenInDifferentOrder()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });
            var sameTownsInAnotherOrder = new List<Town> {town1, town3, town2};

            var actual = sut.Contains(sameTownsInAnotherOrder);

            actual.Should().BeTrue();
        }

        [Test]
        public void AWalkDoesNotContainAListOfTownsIfTheListsHasOneMore()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var town4 = new Town(4, 4);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });
            var sameTownsInAnotherOrder = new List<Town> {town1, town2, town3, town4};

            var actual = sut.Contains(sameTownsInAnotherOrder);

            actual.Should().BeFalse();
        }

        [Test]
        public void AWalkDoesNotContainAListOfTownsIfTheListLacksOne()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });
            var sameTownsInAnotherOrder = new List<Town> {town1, town3};

            var actual = sut.Contains(sameTownsInAnotherOrder);

            actual.Should().BeFalse();
        }

        [Test]
        public void AWalkCanGenerateACompatibleChild()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });

            var child = sut.GenerateChild();

            child.IsCompatibleWith(sut).Should().BeTrue();
        }

        [Test]
        public void AWalkIsACloneOfAnotherWalkIfItContainsTheSameTownsInTheSameOrder()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });
            var other = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });

            var actual = sut.IsACloneOf(other);

            actual.Should().BeTrue();
        }

        [Test]
        public void AWalkIsContainsExatclyAListOfTownsfItContainsTheSameTownsInTheSameOrder()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });

            var actual = sut.ContainsExactly(new List<Town> { town1, town2, town3 });

            actual.Should().BeTrue();
        }

        [Test]
        public void AWalkGeneratesChildrenThatAreNotClones()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var sut = new Walk(_randomNumberGenerator, new List<Town> {town1, town2, town3 });

            var children = GenerateChildren(sut, 100);

            children.ForEach(c => c.IsACloneOf(sut).Should().BeFalse());
        }

        [Test]
        public void ChildrenAreMostlyRandom()
        {
            var towns = GenerateTowns(200);
            var walk = new Walk(_randomNumberGenerator, towns);

            var children = GenerateChildren(walk, 50);

            var countClones = CountClonesIn(children);

            countClones.Should().BeLessThan(10);

        }

        [Test]
        public void SwapItemsShouldInvertThePositionsOf2ItemsInAList()
        {
            var town0 = new Town(1, 0);
            var town1 = new Town(2, 0);
            var town2 = new Town(3, 0);
            var town3 = new Town(4, 0);
            var towns = new List<Town>() { town0, town1, town2, town3 };

            Walk.SwapItems(towns, 0, 1);

            towns[0].Should().Be(town1);
            towns[1].Should().Be(town0);
        }

        static int CountClonesIn(List<Walk> children)
        {
            int countClones = 0;
            foreach (var child in children) {
                if (children.Any(c => c.IsACloneOf(child) && c != child)) {
                    countClones++;
                }
            }
            return countClones;
        }

        List<Town> GenerateTowns(int count)
        {
            return GenerateItems(count, () => new Town(0, 0));
        }

        List<Walk> GenerateChildren(Walk walk, int count)
        {
            return GenerateItems(count, () => walk.GenerateChild());
        }

        List<T> GenerateItems<T>(int count, Func<T> func)
        {
            var items = new List<T>();
            Enumerable.Range(1, count).ToList().ForEach(i => items.Add(func()));
            return items;
        }
    }
}