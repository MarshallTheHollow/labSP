using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labSP.Models
{
    public class MathOutput
    {
        public MathInput inputs;
        public List<double> answers = new List<double>();
        public double cusum;
        public double snsum;
        public double pbsum;
        public double znsum;
        public double totalcost;
        public double totalweight;
    }
}