using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Nagrada
    {
        public int id { get; set; }
        public String naziv { get; set; }
        public String kategorija { get; set; }
        public DateTime datum { get; set; }

        public List<Firma> firme { get; set; }

    }
}
