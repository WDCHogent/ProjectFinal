﻿using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieRepository
    {
        IReadOnlyList<ReservatieInfo> SelecteerReservatie(int? klantnummer, int? toestelnummer);
    }
}
