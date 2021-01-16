using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Projekat
    {
        public int id { get; set; }

        public string naziv { get; set; }

        public string opis { get; set; }

        public DateTime datum_od { get; set; }

        public DateTime datum_do { get; set; }

        public IList<Firma> FirmeNaProjektu { get; set; }


        public IList<Zaposleni> ZaposleniNaProjektu { get; set; }

        public Projekat()
        {
            datum_do = DateTime.MinValue;
            FirmeNaProjektu = new List<Firma>();
            ZaposleniNaProjektu = new List<Zaposleni>();
        }
    }
}
