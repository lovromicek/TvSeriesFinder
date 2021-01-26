using System;
using System.Collections.Generic;
using System.Text;

namespace Z5
{
    public class Episode
    {
        public int id { get; set; }
        public string name { get; set; }
        public int season { get; set; }
        public int? number { get; set; }
        public string summary { get; set; }

        public override string ToString()
        {
            return $"Episode {number}: {name}";
        }
    }
}
