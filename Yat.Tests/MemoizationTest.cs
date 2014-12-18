using System;
using NSubstitute;
using NUnit.Framework;
using System.Net.NetworkInformation;
using FluentAssertions;

namespace Yat.Tests
{
    [TestFixture()]
    public class MemoizationTest
    {
        Memoizator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Memoizator();
        }

        [Test()]
        public void ShouldCallAMethodSeveralTimeIfArgumentsAreDifferent()
        {
            var func = Substitute.For<Func<string, string>>();

            var memoized = _sut.Memoize(func);

            memoized("a");
            memoized("b");
            memoized("c");

            func.Received(3);
        }

        [Test()]
        public void ShouldAvoidCallingAMethodMoreThanOnceWithTheSameArguments()
        {
            var func = Substitute.For<Func<string, string>>();
            func("a").Returns("foo");
            var memoized = _sut.Memoize(func);

            memoized("a");
            memoized("a");
            memoized("a");

            func.Received(1);
        }


        [Test()]
        public void ShouldMemoizeMethodCallsWithTheSameArguments()
        {
            var func = Substitute.For<Func<string, string>>();
            func("a").Returns("foo");
            var memoized = _sut.Memoize(func);

            memoized("a");
            var result = memoized("a");

            result.Should().Be("foo");
        }
    }
}

