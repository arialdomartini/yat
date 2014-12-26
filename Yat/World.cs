using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class World
    {
        readonly RandomNumberGenerator _randomNumberGenerator;

        public World(RandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Walk GenerateRandomWalk(List<Town> towns)
        {
            return new Walk(_randomNumberGenerator, towns.OrderBy(i => Guid.NewGuid()).ToList());
        }

        public List<Walk> GenerateRandomWalks(List<Town> towns, int numberOfWalks)
        {
            var numbers = Enumerable.Range(1, numberOfWalks);

            var walks = from n in numbers select GenerateRandomWalk(towns);
            return walks.ToList();
        }
    }
}


