using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class Walk : IComparable<Walk>
    {
        readonly List<Town> _towns;
        Random _random;

        public Walk(List<Town> towns)
        {
            _towns = towns;
            _random = new Random();
        }

        #region IComparable implementation

        public int CompareTo(Walk other)
        {
            return Length.CompareTo(other.Length);
        }

        #endregion
        
        double CalculateLength(IList<Town> towns)
        {
            if (towns.Count == 0)
            {
                return 0;
            }
            return Enumerable.Range(0, towns.Count - 1).Sum(i => DistanceBetween(towns[i], towns[i + 1]));
        }

        public double Length
        {
            get { 
                return CalculateLength(_towns);
            } 
        }

        double DistanceBetween(Town town1, Town town2)
        {
            return Math.Sqrt(Math.Pow(town1.X - town2.X, 2) + Math.Pow(town1.Y - town2.Y, 2));
        }

        public Walk GenerateChild()
        {
            var newList = new List<Town>(_towns);

            var index1 = _random.Next(0, _towns.Count);
            var t1 = newList[index1];

            var index2 = _random.Next(0, _towns.Count);
            var t2 = newList[index2];

            newList[index1] = t2;
            newList[index2] = t1;

            return new Walk(newList);
        }

        public bool Contains(List<Town> theirTowns)
        {
            var myTowns = _towns;
            return myTowns.All(theirTowns.Contains) && theirTowns.All(myTowns.Contains);
        }

        public bool IsCompatibleWith(Walk other)
        {
            return other.Contains(_towns);
        }


        public bool IsACloneOf(Walk other)
        {
            return other.ContainsExactly(_towns);
        }


        public bool ContainsExactly(List<Town> theirTowns)
        {
            if( theirTowns.Count != _towns.Count)
            {
                return false;
            }
            for (int i = 0; i < _towns.Count; i++)
            {
                if( _towns[i] != theirTowns[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

