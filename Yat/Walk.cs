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

        public Walk ()
        {
        }

        public Walk (Town town)
        {

        }


        public double Length { 
            get { 
                if(_towns == null || _towns.Count == 0)
                {
                    return 0;
                }
                else
                {
                    var town1 = _towns [0];
                    var town2 = _towns [1];
                    return Math.Sqrt (Math.Pow (town1.x - town2.x, 2) + Math.Pow (town1.y - town2.y, 2));
                }
            } 
        }

    }
}

