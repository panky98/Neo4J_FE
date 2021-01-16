using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Zaposlen
    {
        public Zaposleni zaposleni { get; set; }
        public Firma firma { get; set; }
        public String datum_od { get; set; }
        public String datum_do { get; set; }
        public String pozicija { get; set; }
    }
}
