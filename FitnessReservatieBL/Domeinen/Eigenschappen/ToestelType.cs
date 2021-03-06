using FitnessReservatieBL.Exceptions;

namespace FitnessReservatieBL.Domeinen.Eigenschappen
{
    public class ToestelType
    {
        internal ToestelType(int toestelId, string toestelNaam) : this(toestelNaam)
        {
            ZetToestelId(toestelId);
        }
        internal ToestelType(string toestelNaam)
        {
            ZetToestelNaam(toestelNaam);
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
            if (string.IsNullOrWhiteSpace(toestelNaam)) throw new ToestelTypeException("ToestelType - ZetToestelNaam - 'Mag niet leeg zijn'");
            ToestelNaam = toestelNaam.Trim();
        }

        public override string ToString()
        {
            return $"{ToestelId},{ToestelNaam}";
        }
    }
}
