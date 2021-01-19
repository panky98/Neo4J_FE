using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Zaposlen
    {
        public int id { get; set; }
        public Zaposleni zaposleni { get; set; }
        public Firma firma { get; set; }
        public DateTime datum_od { get; set; }
        public DateTime datum_do { get; set; }
        public String pozicija { get; set; }

        public Zaposlen()
        {
            datum_do = DateTime.MinValue;
        }
    }
}
