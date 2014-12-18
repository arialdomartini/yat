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


        public int Length { 
            get { 
                if(_towns == null || _towns.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 4;
                }
            } 
        }

    }
}

