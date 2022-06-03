using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Exceptions;
using System;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestReservatie
    {
        private Klant _k = new(10,"Persoons", "Persoon", "Persoon.Persoons@email.com");
        private DateTime _d = DateTime.Now.Date;

        [Fact]
        public void ZetReservatienummer_Valid()
        {
            Reservatie r = new(10,_k, _d);
            Assert.Equal(10, r.Reservatienummer);
            r.ZetReservatienummer(1);
            Assert.Equal(1, r.Reservatienummer);
        }

        [Fact]
        public void ZetReservatienummer_Invalid()
        {
            Reservatie r = new(10, _k, _d);
            Assert.Equal(10, r.Reservatienummer);
            Assert.Throws<ReservatieException>(() => r.ZetReservatienummer(0));
        }

        [Fact]
        public void ZetKlant_Valid()
        {
            Reservatie r = new(10, _k, _d);

            r.ZetKlant(_k);
            Assert.Equal(_k, r.Klant);
        }

        [Fact]
        public void ZetKlant_Invalid()
        {
            Reservatie r = new(10, _k, _d);
            Assert.Equal(_k, r.Klant);
            Assert.Throws<ReservatieException>(() => r.ZetKlant(null));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        public void ZetDatum_Valid(int dagindex)
        {
            Reservatie r = new(10, _k, _d);

            r.ZetDatum(_d.AddDays(dagindex));
            Assert.Equal(_d.AddDays(dagindex), r.Datum);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-365)]
        [InlineData(8)]
        [InlineData(365)]
        public void ZetDatum_Invalid(int dagindex)
        {
            Reservatie r = new(10, _k, _d);
            Assert.Equal(_d, r.Datum);
            Assert.Throws<ReservatieException>(() => r.ZetDatum(_d.AddDays(dagindex)));
        }

        [Theory]
        [InlineData(10, 0, 10, 0)]
        [InlineData(10, 7, 10, 7)]
        public void ctor_Valid(int reservatieNrIn, int dagindexIn, int reservatieNrUit, int dagIndexUit)
        {
            Reservatie r = new(reservatieNrIn, _k, _d.AddDays(dagindexIn));

            Assert.Equal(reservatieNrUit, r.Reservatienummer);
            Assert.Equal(_k, r.Klant);
            Assert.Equal(_d.AddDays(dagIndexUit), r.Datum);
        }

        [Theory]
        [InlineData(0, 10, "Persoons", "Persoon", "Persoon.Persoons@email.com", 0)]
        [InlineData(10, 10, "Persoons", "Persoon", "Persoon.Persoons@email.com", -1)]
        [InlineData(10, 10, "Persoons", "Persoon", "Persoon.Persoons@email.com", -365)]
        [InlineData(10, 10, "Persoons", "Persoon", "Persoon.Persoons@email.com", 8)]
        [InlineData(10, 10, "Persoons", "Persoon", "Persoon.Persoons@email.com", 365)]
        [InlineData(10, 0, null, null, null, 0)]
        public void ctor_Invalid(int reservatieNr, int klantNr, string klantNaam, string klantVoornaam, string klantEmail, int dagindex)
        {
            Klant k;
            if (klantNr != 0) k = new(klantNr, klantNaam, klantVoornaam, klantEmail);
            else k = null;

            Assert.Throws<ReservatieException>(() => new Reservatie(reservatieNr, k, _d.AddDays(dagindex)));
        }
    }
}
