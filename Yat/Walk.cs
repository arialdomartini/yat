using System;
using System.Collections.Generic;
using System.Linq;

namespace Yat
{
    public class Walk
    {
        List<Town> _towns;

        public Walk(List<Town> towns)
        {
            _towns = towns;
        }

        double CalculateLength(List<Town> towns)
        {
            if (towns.Count <= 1) {
                return 0;
            }

            return DistanceBetween(towns[0], towns[1]) + CalculateLength(towns.Skip(1).ToList());
        }

        public double Length { 
            get { 
                return CalculateLength(_towns);
            } 
        }

        double DistanceBetween(Town town1, Town town2)
        {
            return Math.Sqrt(Math.Pow(town1.x - town2.x, 2) + Math.Pow(town1.y - town2.y, 2));
        }
    }
}

