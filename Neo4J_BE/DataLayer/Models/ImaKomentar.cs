using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ImaKomentar
    {
        public int id { get; set; }

        public Firma firma { get; set; }

        public Komentar komentar { get; set; }

    }
}
