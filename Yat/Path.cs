using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Yat
{
    public class Path : List<Town>
    {
        public Path() : base() { }
        public Path(Path path) : base(path) { }
        public Path(List<Town> towns) : base(towns) { }
    }
}

