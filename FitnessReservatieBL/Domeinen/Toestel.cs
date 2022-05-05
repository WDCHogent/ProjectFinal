﻿using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen
{
    public class Toestel
    {
        public Toestel(int toestelNummer, string toestelNaam, Status status, ToestelType toestelType)
        {
            ZetToestelNummer(toestelNummer);
            ZetToestelNaam(toestelNaam);
            ZetStatus(status);
            ZetToestelType(toestelType);
        }

        public int ToestelNummer { get; private set; }
        public string ToestelNaam { get; private set; }
        public Status Status { get; private set; }
        public ToestelType ToestelType { get; private set; }

        private List<Reservatie> _reservaties = new List<Reservatie>();

        public void ZetToestelNummer(int toestelNummer)
        {
            if (toestelNummer <= 0) throw new ToestelException("Toestel - ZetToestelNummer - 'Mag niet leeg zijn'");
            ToestelNummer = toestelNummer;
        }

        public void ZetToestelNaam(string toestelNaam)
        {
            if (string.IsNullOrEmpty(toestelNaam)) throw new ToestelException("Toestel - ZetToestelNaam - 'Mag niet leeg zijn'");
            ToestelNaam = toestelNaam.Trim();
        }

        public void ZetStatus(Status status)
        {
            if (status.GetType() != typeof(Status)) throw new ToestelException("Toestel - ZetStatus - 'Geen geldige status'");
            Status = status;
        }

        public void ZetToestelType(ToestelType toestelType)
        {
            ToestelType = toestelType;
        }

        //TODO : Change to "internal"
        public void VoegReservatieToe(Reservatie reservatie)
        {
            if (reservatie == null) throw new ToestelException("Toestel - VoegReservatieToe");
            if (reservatie.Toestel != this) throw new ToestelException("Toestel - VoegReservatieToe");
            if (this.HeeftReservatie(reservatie)) throw new ToestelException("Toestel - VoegReservatieToe - 'Deze reservatie bestaat al'");
            _reservaties.Add(reservatie);
        }

        public bool HeeftReservatie(Reservatie reservatie)
        {
            return _reservaties.Contains(reservatie);
        }

        public IReadOnlyList<Reservatie> GeefReservaties()
        {
            return _reservaties;
        }

        public override string ToString()
        {
            return $"{ToestelNummer}, {ToestelNaam}, {Status.ToString()}, {ToestelType}";
        }
    }
}