using NUnit.Framework;
using System;

namespace Yat.Tests
{
    [TestFixture ()]
    public class DnaTest
    {
        [Test ()]
        public void AnEmptyWalkHasLength0 ()
        {
            var sut = new Walk ();

            var actual = sut.Length;

            Assert.AreEqual (0, actual);
        }
    }
}

