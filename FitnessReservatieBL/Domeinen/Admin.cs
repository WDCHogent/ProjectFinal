using FitnessReservatieBL.Exceptions;

namespace FitnessReservatieBL.Domeinen
{
    public class Admin
    {
        public Admin(string adminnummer, string naam, string voornaam)
        {
            Adminnummer = adminnummer;
            Naam = naam;
            Voornaam = voornaam;
        }

        public string Adminnummer { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }

        public void ZetAdminnummer(string adminnummer)
        {
            if (string.IsNullOrWhiteSpace(adminnummer)) throw new AdminException("Admin - ZetAdminnummer - 'Mag niet leeg zijn'");
            if (!adminnummer.StartsWith('A')) throw new AdminException("Admin - ZetAdminnummer - 'Ongeldig Adminnummer'");
            Adminnummer = adminnummer.Trim();
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new AdminException("Admin - ZetNaam - 'Mag niet leeg zijn'");
            Naam = naam.Trim();
        }

        public void ZetVoornaam(string voornaam)
        {
            if (string.IsNullOrWhiteSpace(voornaam)) throw new AdminException("Admin - ZetVoornaam - 'Mag niet leeg zijn'");
            Voornaam = voornaam.Trim();
        }

        public override string ToString()
        {
            return $"{Adminnummer},{Naam},{Voornaam}";
        }
    }
}
