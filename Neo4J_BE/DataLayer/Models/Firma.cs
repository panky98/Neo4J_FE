using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Firma
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string adresa { get; set; }
        public string PIB { get; set; }

        public IList<Projekat> Projekti { get; set; }

        public Firma()
        {
            Projekti = new List<Projekat>();
        }
    }
}
