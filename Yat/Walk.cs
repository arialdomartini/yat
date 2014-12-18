using System;
using System.Collections.Generic;

namespace Yat
{
    public class Walk
    {
        List<Town> _towns;

        public Walk (List<Town> towns)
        {
            _towns = towns;
        }
    
        public double Length { 
            get { 
                double length = 0;
                Town previous = null;
                foreach (var town in _towns) {
                    if (previous != null)
                    {
                        length += DistanceBetween (previous, town);
                    }
                    previous = town;
                }
                return length;

            } 
        }

        double DistanceBetween (Town town1, Town town2)
        {
            return Math.Sqrt (Math.Pow (town1.x - town2.x, 2) + Math.Pow (town1.y - town2.y, 2));
        }
    }
}

