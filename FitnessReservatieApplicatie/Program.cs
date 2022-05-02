﻿using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using System;

namespace FitnessReservatieApplicatie
{
    public class Program
    {
        static void Main(string[] args)
        {
            Klant klant = new Klant(1, "De Crop", "Wout", "woutdecrop@hotmail.com");
            Console.WriteLine(klant);

            ToestelType toesteltype = new ToestelType(3, "loopband");

            Toestel toestel = new Toestel(2, "Loopband", Status.operatief, toesteltype);

            Tijdslot tijdslot = new Tijdslot(DateTime.Now);

            Reservatie reservatie = new Reservatie(klant, DateTime.Now, tijdslot, toestel);
        }
    }
}
