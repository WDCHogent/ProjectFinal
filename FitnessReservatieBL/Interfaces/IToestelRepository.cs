﻿using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IToestelRepository
    {
        IReadOnlyList<Toestel> GeefVrijToestelVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur);
    }
}
