using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class Walk : IComparable<Walk>
    {
        List<Town> _towns;

        public Walk(List<Town> towns)
        {
            _towns = towns;

            this.Memoized = Walk.Memoize( (List<Town> t) => CalculateLength(t));
        }

        #region IComparable implementation

        public int CompareTo(Walk other)
        {
            return Length.CompareTo(other.Length);
        }

        #endregion
        
        double CalculateLength(List<Town> towns)
        {
            if (towns.Count == 0)
            {
                return 0;
            }
            return Enumerable.Range(0, towns.Count - 1).Select(i => DistanceBetween(towns[i], towns[i + 1])).Sum();
        }

        public Func<List<Town>, double> Memoized;

        public double Length { 
            get { 
                return Memoized(_towns);
            } 
        }

        public static Func<TSource, TReturn> Memoize<TSource, TReturn>(Func<TSource, TReturn> func)
        {
            var cache = new Dictionary<TSource, TReturn>();
            return s =>
            {
                if (!cache.ContainsKey(s))
                {
                    cache[s] = func(s);
                }
                return cache[s];
            };
        }

        double DistanceBetween(Town town1, Town town2)
        {
            return Math.Sqrt(Math.Pow(town1.x - town2.x, 2) + Math.Pow(town1.y - town2.y, 2));
        }

    }
}

