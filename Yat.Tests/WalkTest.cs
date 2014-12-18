using NUnit.Framework;
using FluentAssertions;
using System;

namespace Yat.Tests
{
    [TestFixture ()]
    public class WalkTest
    {
        [Test ()]
        public void AnEmptyWalkHasLength0 ()
        {
            var sut = new Walk ();

            sut.Length.Should ().Be (0);
        }
    }
}

