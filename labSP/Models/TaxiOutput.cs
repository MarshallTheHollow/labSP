using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labSP.Models
{
    public class TaxiOutput
    {
        public double value;
        public double distance;
        public double kolvo;
        public double time;
    }
    public class TaxiInput
    {
        public double distance { get; set; }
        public double kolvo { get; set; }
    }
}