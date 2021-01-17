using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Zaposleni
    {
        public int id { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public int starost { get; set; }
        public char pol { get; set; }

        public IList<Zaposlen> listaFirmi { get; set; }

    }
}
