using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class RandomNumberGenerator
    {
        readonly Random _random;

        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        public List<int> GenerateCouple(int from, int to)
        {
            return Enumerable.Range(0, to - 1).OrderBy(x => _random.Next()).Take(2).ToList<int>();
        }

    }
}

