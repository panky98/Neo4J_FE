using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Komentar
    {
        public int id { get; set; }

        public String text { get; set; }

        public DateTime datum { get; set; }

        public IList<Firma> firme { get; set; }

        public Komentar()
        {
            firme = new List<Firma>();
        }

    }
}
