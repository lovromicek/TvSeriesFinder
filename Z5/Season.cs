using System;
using System.Collections.Generic;
using System.Text;

namespace Z5
{
    public class Season
    {
        public int id { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string premiereDate { get; set; }
        public string endDate { get; set; }

        public override string ToString()
        {
            return $"Season {number}";
        }
    }
}
