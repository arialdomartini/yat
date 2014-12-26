using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Yat
{
    public class Walk
    {
        RandomNumberGenerator _randomNumberGenerator;

        public Walk(RandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public int ComparePaths(List<Town> firstPath, List<Town> secondPath)
        {
            return CalculateLength(firstPath).CompareTo(CalculateLength(secondPath));
        }
        
        public double CalculateLength(IList<Town> towns)
        {
            if (towns.Count == 0)
            {
                return 0;
            }
            return Enumerable.Range(0, towns.Count - 1).Sum(i => DistanceBetween(towns[i], towns[i + 1]));
        }

        double DistanceBetween(Town town1, Town town2)
        {
            return Math.Sqrt(Math.Pow(town1.X - town2.X, 2) + Math.Pow(town1.Y - town2.Y, 2));
        }

        public List<Town> GenerateChild(List<Town> path)
        {
            return Mutate(path);
        }

        public void SwapItems(List<Town> path, int index1, int index2)
        {
            var t1 = path[index1];
            var t2 = path[index2];
            path[index1] = t2;
            path[index2] = t1;
        }

        public List<Town> SwapItemsNewList(List<Town> path, int index1, int index2)
        {
            var newPath = new List<Town>(path);
            var t1 = newPath[index1];
            var t2 = newPath[index2];
            newPath[index1] = t2;
            newPath[index2] = t1;

            return newPath;
        }

        List<Town> Mutate(List<Town> towns)
        {
            var newList = new List<Town>(towns);
            var randomNumbers = _randomNumberGenerator.GenerateCouple(0, towns.Count);

            var index1 = randomNumbers[0];
            var index2 = randomNumbers[1];

            SwapItems(newList, index1, index2);
            return newList;
        }

        public bool ContainsTheSameTowns(List<Town> path1, List<Town> path2)
        {
            return path1.All(path2.Contains) && path2.All(path1.Contains);
        }

        public bool AreCompatible(List<Town> path1, List<Town> path2)
        {
            return path1.All(path2.Contains) && path2.All(path1.Contains);
                      
        }

        public bool AreClones(List<Town> path1, List<Town> path2)
        {
            if( path1.Count != path2.Count)
            {
                return false;
            }
            for (int i = 0; i < path1.Count; i++)
            {
                if( path1[i] != path2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainsExactly(List<Town> path1, List<Town> path2)
        {
            if( path1.Count != path2.Count)
            {
                return false;
            }
            for (int i = 0; i < path1.Count; i++)
            {
                if( path1[i] != path2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

