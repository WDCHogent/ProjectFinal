﻿using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FitnessReservatieBL.Domeinen
{
    public class Klant
    {
        public Klant(int klantnummer, string naam, string voornaam, string mailadres)
        {
            ZetKlantnummer(klantnummer);
            ZetNaam(naam);
            ZetVoornaam(voornaam);
            ZetMailadres(mailadres);
        }

        public int Klantnummer { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        public string Mailadres { get; private set; }

        private List<Reservatie> _reservaties = new List<Reservatie>();

        public void ZetKlantnummer(int klantnummer)
        {
            if (klantnummer <= 0) throw new KlantException("Klant - ZetKlantnummer - 'Mag niet leeg zijn'");
            Klantnummer = klantnummer;
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrEmpty(naam)) throw new KlantException("Klant - ZetNaam - 'Mag niet leeg zijn'");
            Naam = naam.Trim();
        }

        public void ZetVoornaam(string voornaam)
        {
            if (string.IsNullOrEmpty(voornaam)) throw new KlantException("Klant - ZetVoornaam - 'Mag niet leeg zijn'");
            Voornaam = voornaam.Trim();
        }

        public void ZetMailadres(string mailadres)
        {
            string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            if(regex.IsMatch(mailadres)) Mailadres = mailadres;
            else throw new KlantException("Klant - ZetMailadres - 'Geen geldig mailadres'");
        }

        //TODO : Change to "internal"
        public void VoegReservatieToe(Reservatie reservatie)
        {
            if (reservatie == null) throw new KlantException("Klant - VoegReservatieToe");
            if (reservatie.Klant != this) throw new KlantException("Klant - VoegReservatieToe");
            if (this.HeeftReservatie(reservatie)) throw new KlantException("Klant - VoegReservatieToe - 'Deze reservatie bestaat al'");
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
            return $"{Klantnummer}, {Naam}, {Voornaam}, {Mailadres}";
        }
    }
}