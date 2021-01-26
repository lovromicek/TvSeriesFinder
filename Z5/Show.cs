using System;
using System.Collections.Generic;
using System.Text;

namespace Z5
{
    public class Show
    {
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public List<string> genres { get; set; }
        public Rating rating { get; set; }
        public string summary { get; set; }
    }
}
