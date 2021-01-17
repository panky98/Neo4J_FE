using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ZaposleniDTO
    {
        public int idzaposlenog { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public int starost { get; set; }
        public char pol { get; set; }
        public string datum_do { get; set; }
        public string datum_od { get; set; }

        public string naziv { get; set; }
    }
}
