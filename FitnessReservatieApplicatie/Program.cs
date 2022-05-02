using FitnessReservatieBL.Domeinen;
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
        }
    }
}
