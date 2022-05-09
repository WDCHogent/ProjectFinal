using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class ToestelTypeInfo
    {
        public ToestelTypeInfo(string toesteltype)
        {
            Toesteltype = toesteltype;
        }

        public string Toesteltype { get; set; }

        public override string ToString()
        {
            return $"{Toesteltype}";
        }
    }
}
