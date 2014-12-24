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
            return new Walk(Mutate(_towns));
        }

        public static void SwapItems(List<Town> newList, int index1, int index2)
        {
            var t1 = newList[index1];
            var t2 = newList[index2];
            newList[index1] = t2;
            newList[index2] = t1;
        }

        List<Town> Mutate(List<Town> towns)
        {
            var newList = new List<Town>(towns);
            var randomNumbers = Enumerable.Range(0, towns.Count-1).OrderBy(x => _random.Next()).ToArray();

            var index1 = randomNumbers[0];
            var index2 = randomNumbers[1];

            SwapItems(newList, index1, index2);
            return newList;
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

