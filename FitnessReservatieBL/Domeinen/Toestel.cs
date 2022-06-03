using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Exceptions;

namespace FitnessReservatieBL.Domeinen
{
    public class Toestel
    {
        internal Toestel(int toestelNummer, string toestelNaam, Status status, ToestelType toestelType) : this(toestelNaam, status, toestelType)
        {
            ZetToestelNummer(toestelNummer);
        }

        internal Toestel(string toestelNaam, Status status, ToestelType toestelType)
        {
            ZetToestelNaam(toestelNaam);
            ZetStatus(status);
            ZetToestelType(toestelType);
        }


        public int ToestelNummer { get; private set; }
        public string ToestelNaam { get; private set; }
        public Status Status { get; private set; }
        public ToestelType ToestelType { get; private set; }

        public void ZetToestelNummer(int toestelNummer)
        {
            if (toestelNummer <= 0) throw new ToestelException("Toestel - ZetToestelNummer - 'Mag niet leeg zijn'");
            ToestelNummer = toestelNummer;
        }

        public void ZetToestelNaam(string toestelNaam)
        {
            if (string.IsNullOrWhiteSpace(toestelNaam)) throw new ToestelException("Toestel - ZetToestelNaam - 'Mag niet leeg zijn'");
            ToestelNaam = toestelNaam.Trim();
        }

        public void ZetStatus(Status status)
        {
            if (status.GetType() != typeof(Status)) throw new ToestelException("Toestel - ZetStatus - 'Geen geldige status'");
            Status = status;
        }

        public void ZetToestelType(ToestelType toesteltype)
        {
            if (toesteltype == null) throw new ToestelException("Toestel - ZetToestelType - 'Mag niet leeg zijn'");
            ToestelType = toesteltype;
        }

        public override string ToString()
        {
            return $"{ToestelNummer},{ToestelNaam},{Status.ToString()},{ToestelType}";
        }
    }
}
