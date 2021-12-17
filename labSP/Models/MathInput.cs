using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labSP.Models
{
    public class MathInput
    {
        public List<SolverRow> Alloy { get; set; } = new List<SolverRow>();
        public elementals Cuvalue { get; set; }
        public elementals Pbvalue { get; set; }
        public elementals Snvalue { get; set; }
        public elementals Znvalue { get; set; }
        public double requiredweight {get;set;}
    }

    public class elementals
    {
        public double min { get; set; }
        public double max { get; set; }
    }
}