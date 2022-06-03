using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Exceptions;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestAdmin
    {
        [Theory]
        [InlineData("A001", "A001")]
        [InlineData("A999", "A999")]
        [InlineData("A500   ", "A500")]
        public void ZetAdminnummer_Valid(string adminNrIn, string adminNrUit)
        {
            Admin a = new("A010", "Persoons", "Persoon");

            a.ZetAdminnummer(adminNrIn);
            Assert.Equal(adminNrUit, a.Adminnummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        [InlineData("A000")]
        [InlineData("AAAA")]
        [InlineData("B010")]
        [InlineData(" A500")]
        public void ZetAdminnummer_Invalid(string adminnr)
        {
            Admin a = new("A010", "Persoons", "Persoon");
            Assert.Throws<AdminException>(() => a.ZetAdminnummer(adminnr));
        }

        [Theory]
        [InlineData("Persoons", "Persoons")]
        [InlineData("   Persoons", "Persoons")]
        [InlineData("Persoons   ", "Persoons")]
        public void ZetNaam_Valid(string naamIn, string naamUit)
        {
            Admin a = new("A010", "Persoons", "Persoon");

            a.ZetNaam(naamIn);
            Assert.Equal(naamUit, a.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetNaam_Invalid(string naam)
        {
            Admin a = new("A010", "Persoons", "Persoon");
            Assert.Throws<AdminException>(() => a.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Persoon", "Persoon")]
        [InlineData("   Persoon", "Persoon")]
        [InlineData("Persoon   ", "Persoon")]
        public void ZetVoornaam_Valid(string voornaamIn, string voornaamUit)
        {
            Admin a = new("A010", "Persoons", "Persoon");

            a.ZetVoornaam(voornaamIn);
            Assert.Equal(voornaamUit, a.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetVoornaam_Invalid(string naam)
        {
            Admin a = new("A010", "Persoons", "Persoon");
            Assert.Throws<AdminException>(() => a.ZetVoornaam(naam));
        }

        [Theory]
        [InlineData("A010", "Persoons", "Persoon", "A010", "Persoons", "Persoon")]
        [InlineData("A001", "Persoons", "Persoon", "A001", "Persoons", "Persoon")]
        [InlineData("A999", "Persoons", "Persoon", "A999", "Persoons", "Persoon")]
        [InlineData("A500   ", "Persoons", "Persoon", "A500", "Persoons", "Persoon")]
        [InlineData("A010", "   Persoons", "Persoon", "A010", "Persoons", "Persoon")]
        [InlineData("A010", "Persoons   ", "Persoon", "A010", "Persoons", "Persoon")]
        [InlineData("A010", "Persoons", "   Persoon", "A010", "Persoons", "Persoon")]
        [InlineData("A010", "Persoons", "Persoon   ", "A010", "Persoons", "Persoon")]
        public void ctor_Valid(string adminNrIn, string naamIn, string voornaamIn, string adminNrUit, string naamUit, string voornaamUit)
        {
            Admin a = new(adminNrIn, naamIn, voornaamIn);

            Assert.Equal(adminNrUit, a.Adminnummer);
            Assert.Equal(naamUit, a.Naam);
            Assert.Equal(voornaamUit, a.Voornaam);
        }

        [Theory]
        [InlineData("", "Persoons", "Persoon")]
        [InlineData("   ", "Persoons", "Persoon")]
        [InlineData(null, "Persoons", "Persoon")]
        [InlineData("\n", "Persoons", "Persoon")]
        [InlineData("\r", "Persoons", "Persoon")]
        [InlineData("A000", "Persoons", "Persoon")]
        [InlineData("AAAA", "Persoons", "Persoon")]
        [InlineData("B010", "Persoons", "Persoon")]
        [InlineData(" A500", "Persoons", "Persoon")]

        [InlineData("A010", "", "Persoon")]
        [InlineData("A010", "   ", "Persoon")]
        [InlineData("A010", null, "Persoon")]
        [InlineData("A010", "\n", "Persoon")]
        [InlineData("A010", "\r", "Persoon")]

        [InlineData("A010", "Persoons", "")]
        [InlineData("A010", "Persoons", "   ")]
        [InlineData("A010", "Persoons", null)]
        [InlineData("A010", "Persoons", "\n")]
        [InlineData("A010", "Persoons", "\r")]
        public void ctor_Invalid(string adminnr, string naam, string voornaam)
        {
            Assert.Throws<AdminException>(() => new Admin(adminnr, naam, voornaam));
        }
    }
}
