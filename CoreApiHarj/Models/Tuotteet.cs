using System;
using System.Collections.Generic;

namespace CoreApiHarj.Models
{
    public partial class Tuotteet
    {
        public int tuoteId { get; set; }
        public string tuotenimi { get; set; }
        public int hinta  { get; set; }
        public int varastosaldo { get; set; }
        public int tilattu { get; set; }
        public string kuvaus { get; set; }
        public string kuvalinkki1 { get; set; }
        public string kuvalinkki2 { get; set; }
        public string kuvalinkki3 { get; set; }
    }
}