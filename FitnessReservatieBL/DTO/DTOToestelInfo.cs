namespace FitnessReservatieBL.DTO
{
    public class DTOToestelInfo
    {
        public DTOToestelInfo(int toestelnummer, string toestelnaam, string status, string toesteltypenaam)
        {
            Toestelnummer = toestelnummer;
            Toestelnaam = toestelnaam;
            Status = status;
            Toesteltypenaam = toesteltypenaam;
        }

        public int Toestelnummer { get; set; }
        public string Toestelnaam { get; set; }
        public string Status { get; set; }
        public string Toesteltypenaam { get; set; }

        public override string ToString()
        {
            return $"{Toestelnaam} | {Status.ToString()}";
        }
    }
}
