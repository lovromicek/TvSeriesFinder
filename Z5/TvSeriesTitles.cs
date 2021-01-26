using System;
using System.Collections.Generic;
using System.Text;

namespace Z5
{
    public class TvSeriesTitles
    {
        public Show show { get; set; }
        public override string ToString()
        {
            return $"{show.name}";
        }
    }
}
