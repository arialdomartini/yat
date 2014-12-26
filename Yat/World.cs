using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class World
    {
        public IEnumerable<Walk> Walks {
            get;
            private set;
        }

        readonly RandomNumberGenerator _randomNumberGenerator;

        public World(RandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Walk GenerateRandomWalk(List<Town> towns)
        {
            return new Walk(_randomNumberGenerator, towns.OrderBy(i => Guid.NewGuid()).ToList());
        }

        public IEnumerable<Walk> GenerateRandomWalks(List<Town> towns, int numberOfWalks)
        {
            Walks = from n in Enumerable.Range(1, numberOfWalks)
                select GenerateRandomWalk(towns);
            return Walks;
        }
    }
}


