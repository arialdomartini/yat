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
        Walk _sut;

        [SetUp]
        public void SetUp()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
            _sut = new Walk(_randomNumberGenerator);
        }

        [Test()]
        public void AWalkWithNoTownsHasLength0()
        {
            _sut.CalculateLength(new List<Town>()).Should().Be(0);
        }

        [Test()]
        public void AWalkWithOnlyOneTownHasLength0()
        {
            var towns = new List<Town> {
                new Town(10, 20)
            };

            _sut.CalculateLength(towns).Should().Be(0);
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

            _sut.CalculateLength(towns).Should().Be(distance);
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

            var actual = _sut.CalculateLength(townsInASquare);

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

            _sut.ComparePaths(shortPath, longPath).Should().Be(-1);
        }

        [Test]
        public void TwoWalkAreCompatibleIfTheyWalksThrouTheSameTowns()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var path1 = new List<Town> { town1, town2 };
            var path2 = new List<Town> { town1, town2 };
            
            _sut.AreCompatible(path1, path2).Should().BeTrue();
        }

        [Test]
        public void ContainsIsTrueWhenGivenTheSameExactList()
        {
            var towns = new List<Town> {
                new Town(1, 1),
                new Town(2, 2)
            };

            _sut.ContainsTheSameTowns(towns, towns).Should().BeTrue();
        }

        [Test]
        public void AWalkContainsAListOfTownsEvenIfGivenInDifferentOrder()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var path1 = new List<Town> { town1, town2, town3 };
            var sameTownsInAnotherOrder = new List<Town> {town1, town3, town2};

            var actual = _sut.ContainsTheSameTowns(path1, sameTownsInAnotherOrder);

            actual.Should().BeTrue();
        }

        [Test]
        public void AWalkDoesNotContainAListOfTownsIfTheListsHasOneMore()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var town4 = new Town(4, 4);
            var path1 = new List<Town> { town1, town2, town3 };
            var sameTownsInAnotherOrder = new List<Town> {town1, town2, town3, town4};

            var actual = _sut.ContainsTheSameTowns(path1, sameTownsInAnotherOrder);

            actual.Should().BeFalse();
        }

        [Test]
        public void AWalkDoesNotContainAListOfTownsIfTheListLacksOne()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var path1 = new List<Town> { town1, town2, town3 };
            var sameTownsInAnotherOrder = new List<Town> {town1, town3};

            var actual = _sut.ContainsTheSameTowns(path1, sameTownsInAnotherOrder);

            actual.Should().BeFalse();
        }

        [Test]
        public void AWalkCanGenerateACompatibleChild()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var path1 = new List<Town> { town1, town2, town3 };

            var child = _sut.GenerateChild(path1);

            _sut.AreCompatible(child, path1).Should().BeTrue();
        }

        [Test]
        public void AWalkIsACloneOfAnotherWalkIfItContainsTheSameTownsInTheSameOrder()
        {
            var town1 = new Town(1, 1);
            var town2 = new Town(2, 2);
            var town3 = new Town(3, 3);
            var path1 = new List<Town> { town1, town2, town3 };
            var path2 = new List<Town> { town1, town2, town3 };

            var actual = _sut.AreClones(path1, path2);

            actual.Should().BeTrue();
        }

        [Test]
        public void AWalkGeneratesChildrenThatAreNotClones()
        {
            var path = GenerateTowns(3);

            var children = GenerateChildren(path, 100);

            children.ForEach(c => _sut.AreClones(c, path).Should().BeFalse());
        }

        [Test]
        public void ChildrenAreMostlyRandom()
        {
            var towns = GenerateTowns(200);

            var children = GenerateChildren(towns, 50);

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

            var actual = _sut.SwapItemsNewList(towns, 0, 1);

            actual[0].Should().Be(town1);
            actual[1].Should().Be(town0);
        }

        [Test]
        public void ShouldGenerteACompletelyShuffledPath()
        {
            var towns = new List<Town>();
            for(var i=0; i< 100; i++)
            {
                towns.Add(new Town(i, i));
            }

            var actual = _sut.GenerateRandom(towns);

            _sut.AreCompatible(actual, towns).Should().BeTrue();
            _sut.AreClones(actual, towns).Should().BeFalse();

        }

        int CountClonesIn(List<List<Town>> children)
        {
            int countClones = 0;
            foreach (var child in children) {
                if (children.Any(c => _sut.AreClones(child, c) && c != child)) {
                    countClones++;
                }
            }
            return countClones;
        }

        List<Town> GenerateTowns(int count)
        {
            return GenerateItems(count, () => new Town(0, 0));
        }

        List<List<Town>> GenerateChildren(List<Town> path, int count)
        {
            return GenerateItems(count, () => _sut.GenerateChild(path));
        }



        List<T> GenerateItems<T>(int count, Func<T> func)
        {
            var items = new List<T>();
            Enumerable.Range(1, count).ToList().ForEach(i => items.Add(func()));
            return items;
        }
    }
}