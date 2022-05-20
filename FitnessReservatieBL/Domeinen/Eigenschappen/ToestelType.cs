﻿using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen.Eigenschappen
{
    public class ToestelType
    {
        public ToestelType(int toestelId, string toestelNaam)
        {
            ToestelId = toestelId;
            ToestelNaam = toestelNaam;
        }

        public int ToestelId { get; private set; }
        public string ToestelNaam { get; private set; }

        public void ZetToestelId(int toestelId) 
        {
            if (toestelId <= 0) throw new ToestelTypeException("ToestelType - ZetToestelId - 'Mag niet leeg zijn'");
            ToestelId = toestelId;
        }

        public void ZetToestelNaam(string toestelNaam)
        {
            if (string.IsNullOrEmpty(toestelNaam)) throw new ToestelException("ToestelType - ZetToestelNaam - 'Mag niet leeg zijn'");
            ToestelNaam = toestelNaam.Trim();
        }

        public override string ToString()
        {
            return $"{ToestelNaam}";
        }
    }
}
