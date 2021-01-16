using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class RadiNa
    {
        public int id { get; set; }

        public DateTime datum_od { get; set; }

        public DateTime datum_do { get; set; }

        public RadiNa()
        {
            datum_do = DateTime.MinValue;
        }
    }
}
