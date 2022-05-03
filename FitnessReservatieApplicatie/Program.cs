using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using System;

namespace FitnessReservatieApplicatie
{
    public class Program
    {
        static void Main(string[] args)
        {
            Klant klant1 = new Klant(1, "De Crop", "Wout", "woutdecrop@hotmail.com");
            Klant klant2 = new Klant(2, "Ugu", "Wagladudu", "uguwagladudu@hotmail.com");

            ToestelType toesteltype1 = new ToestelType(3, "loopband");
            ToestelType toesteltype2 = new ToestelType(5, "fiets");

            Toestel toestel1 = new Toestel(2, "Loopband", Status.operatief, toesteltype1);
            Toestel toestel2 = new Toestel(3, "fiets", Status.operatief, toesteltype2);

            klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(1).Date, new Tijdslot(9), toestel1));
            klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(9), toestel1));
            klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(3).Date, new Tijdslot(9), toestel1));
            klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(4).Date, new Tijdslot(9), toestel1));

            klant2.VoegReservatieToe(new Reservatie(klant2, DateTime.Now.AddDays(1).Date, new Tijdslot(9), toestel2));
            klant2.VoegReservatieToe(new Reservatie(klant2, DateTime.Now.AddDays(2).Date, new Tijdslot(9), toestel2));
            klant2.VoegReservatieToe(new Reservatie(klant2, DateTime.Now.AddDays(3).Date, new Tijdslot(9), toestel2));
            klant2.VoegReservatieToe(new Reservatie(klant2, DateTime.Now.AddDays(4).Date, new Tijdslot(9), toestel2));

            //controle zelfde reservatie
            klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(9), toestel1));

            //controle juiste klant
            //klant1.VoegReservatieToe(new Reservatie(klant2, DateTime.Now.AddDays(2).Date, new Tijdslot(9), toestel1)); //check

            //controle max 1 week
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(10).Date, new Tijdslot(9), toestel1)); //check

            //controle zelfde tijdslot (met ander toestel)
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(9), toestel2));

            //controle max 2 tijdsloten na elkaar
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(10), toestel1));
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(11), toestel1));

            //controle max 4 tijdsloten per dag
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(12), toestel1));
            //klant1.VoegReservatieToe(new Reservatie(klant1, DateTime.Now.AddDays(2).Date, new Tijdslot(13), toestel1));

            foreach (var k in klant1.GeefReservaties())
            {
                Console.WriteLine(k);
            }

            Console.WriteLine();

            foreach (var k in klant2.GeefReservaties())
            {
                Console.WriteLine(k);
            }
        }
    }
}
