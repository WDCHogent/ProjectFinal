using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestKlant
    {
        [Fact]
        public void ZetKlantnummer_Valid()
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");
            Assert.Equal(10, k.Klantnummer);
            k.ZetKlantnummer(1);
            Assert.Equal(1, k.Klantnummer);
        }

        [Fact]
        public void ZetKlantnummer_Invalid()
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");
            Assert.Equal(10, k.Klantnummer);
            Assert.Throws<KlantException>(() => k.ZetKlantnummer(0));
        }

        [Theory]
        [InlineData("Persoons", "Persoons")]
        [InlineData("   Persoons", "Persoons")]
        [InlineData("Persoons   ", "Persoons")]
        public void ZetNaam_Valid(string naamIn, string naamUit)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");

            k.ZetNaam(naamIn);
            Assert.Equal(naamUit, k.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetNaam_Invalid(string naam)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");
            Assert.Throws<KlantException>(() => k.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Persoon", "Persoon")]
        [InlineData("   Persoon", "Persoon")]
        [InlineData("Persoon   ", "Persoon")]
        public void ZetVoornaam_Valid(string voornaamIn, string voornaamUit)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");

            k.ZetVoornaam(voornaamIn);
            Assert.Equal(voornaamUit, k.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetVoornaam_Invalid(string naam)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");
            Assert.Throws<KlantException>(() => k.ZetVoornaam(naam));
        }

        [Theory]
        [InlineData("Persoon.Persoons@email.com", "Persoon.Persoons@email.com")]
        [InlineData("   Persoon.Persoons@email.com", "Persoon.Persoons@email.com")]
        [InlineData("Persoon.Persoons@email.com   ", "Persoon.Persoons@email.com")]
        public void ZetMailadres_Valid(string mailadresIn, string mailadresUit)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");

            k.ZetMailadres(mailadresIn);
            Assert.Equal(mailadresUit, k.Mailadres);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        [InlineData("@email.com")]
        [InlineData("Persoon.Persoons@email.")]
        [InlineData("Persoon.Persoons@.com")]
        [InlineData("Persoon.Persoons@email")]
        [InlineData("Persoon.Persoons@")]
        [InlineData("Persoon.Persoons")]
        [InlineData("Persoon.Persoons.com")]
        public void ZetMailadres_Invalid(string mailadres)
        {
            Klant k = new(10, "Persoons", "Persoon", "Persoon.Persoons@email.com");
            Assert.Throws<KlantException>(() => k.ZetMailadres(mailadres));
        }

        [Theory]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "   Persoons", "Persoon", "Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons   ", "Persoon", "Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "   Persoon", "Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "Persoon   ", "Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "Persoon", "   Persoon.Persoons@email.com", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@email.com   ", 10, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        public void ctor_valid(int klantnummerIn, string naamIn, string voornaamIn, string mailadresIn, int klantnummerUit, string naamUit, string voornaamUit, string mailadresUit)
        {
            Klant k = new(klantnummerIn, naamIn, voornaamIn, mailadresIn);

            Assert.Equal(klantnummerUit, k.Klantnummer);
            Assert.Equal(naamUit, k.Naam);
            Assert.Equal(voornaamUit, k.Voornaam);
            Assert.Equal(mailadresUit, k.Mailadres);
        }

        [Theory]
        [InlineData(0, "Persoons", "Persoon", "Persoon.Persoons@email.com")]
        
        [InlineData(10, "", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "   ", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, null, "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "\n", "Persoon", "Persoon.Persoons@email.com")]
        [InlineData(10, "\r", "Persoon", "Persoon.Persoons@email.com")]

        [InlineData(10, "Persoons", "", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "   ", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", null, "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "\n", "Persoon.Persoons@email.com")]
        [InlineData(10, "Persoons", "\r", "Persoon.Persoons@email.com")]

        [InlineData(10, "Persoons", "Persoon", "")]
        [InlineData(10, "Persoons", "Persoon", "   ")]
        [InlineData(10, "Persoons", "Persoon", null)]
        [InlineData(10, "Persoons", "Persoon", "\n")]
        [InlineData(10, "Persoons", "Persoon", "\r")]
        [InlineData(10, "Persoons", "Persoon", "@email.com")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@email.")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@.com")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@email")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons@")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons")]
        [InlineData(10, "Persoons", "Persoon", "Persoon.Persoons.com")]
        public void ctor_invalid(int klantnr, string naam, string voornaam, string mailadres)
        {
            Assert.Throws<KlantException>(() => new Klant(klantnr, naam, voornaam, mailadres));
        }
    }
}
