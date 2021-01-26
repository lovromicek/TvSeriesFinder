using System;
using System.Collections.Generic;
using System.Text;

namespace Z5
{
    public class Rating
    {
        public double? average { get; set; }
        public override string ToString()
        {
            return $"{average}";
        }
    }
}
