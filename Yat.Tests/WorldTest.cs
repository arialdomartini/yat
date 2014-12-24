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

        [SetUp]
        public void SetUp()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
        }

        [Test]
        public void ShouldGenerateARandomWalk()
        {
            var towns = new List<Town>();
            for(var i=0; i< 100; i++)
            {
                towns.Add(new Town(i, i));
            }
            var walk = new Walk(_randomNumberGenerator, towns);
            var sut = new World(_randomNumberGenerator);

            var actual = sut.GenerateRandomWalk(towns);

            actual.IsCompatibleWith(walk).Should().BeTrue();
            actual.IsACloneOf(walk).Should().BeFalse();

        }
    }
}

