using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class Walk
    {
        RandomNumberGenerator _randomNumberGenerator;

        public Walk(RandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public int ComparePaths(Path firstPath, Path secondPath)
        {
            return CalculateLength(firstPath).CompareTo(CalculateLength(secondPath));
        }
        
        public double CalculateLength(Path path)
        {
            if (path.Count == 0)
            {
                return 0;
            }
            return Enumerable.Range(0, path.Count - 1).Sum(i => DistanceBetween(path[i], path[i + 1]));
        }

        double DistanceBetween(Town town1, Town town2)
        {
            return Math.Sqrt(Math.Pow(town1.X - town2.X, 2) + Math.Pow(town1.Y - town2.Y, 2));
        }

        public Path GenerateChild(Path path)
        {
            return Mutate(path);
        }

        Path Mutate(Path path)
        {
            var randomNumbers = _randomNumberGenerator.GenerateCouple(0, path.Count);
            return SwapItems(path, randomNumbers[0], randomNumbers[1]);
        }

        public Path SwapItems(Path path, int index1, int index2)
        {
            var newPath = new Path(path);
            newPath[index1] = path[index2];
            newPath[index2] = path[index1];

            return newPath;
        }

        public bool AreCompatible(Path path1, Path path2)
        {
            return path1.All(path2.Contains) && path2.All(path1.Contains);
        }

        public bool AreClones(Path path1, Path path2)
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

        public Path GenerateRandom(Path path)
        {
            return new Path(path.OrderBy(i => Guid.NewGuid()).ToList());
        }

        public IEnumerable<Path> GeneratePopulation(Path towns, int population)
        {
            return from i in Enumerable.Range(1, population)
                             select GenerateRandom(towns);
        }
    }
}