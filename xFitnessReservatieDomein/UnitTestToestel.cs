using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Exceptions;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestToestel
    {
        private ToestelType _tt = new(1, "toestel");

        [Fact]
        public void ZetToestelNummer_Valid()
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            Assert.Equal(10, t.ToestelNummer);
            t.ZetToestelNummer(1);
            Assert.Equal(1, t.ToestelNummer);
        }

        [Fact]
        public void ZetToestelNummer_Invalid()
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            Assert.Equal(10, t.ToestelNummer);
            Assert.Throws<ToestelException>(() => t.ZetToestelNummer(0));
        }

        [Theory]
        [InlineData("Toestel", "Toestel")]
        [InlineData("   Toestel", "Toestel")]
        [InlineData("Toestel   ", "Toestel")]
        public void ZetToestelNaam_Valid(string toestelNaamIn, string toestelNaamUit)
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            t.ZetToestelNaam(toestelNaamIn);
            Assert.Equal(toestelNaamUit, t.ToestelNaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetToestelNaam_InValid(string toestelNaam)
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            Assert.Throws<ToestelException>(() => t.ZetToestelNaam(toestelNaam));
        }

        [Theory]
        [InlineData(Status.operatief, Status.operatief)]
        [InlineData(Status.onderhoud, Status.onderhoud)]
        [InlineData(Status.verwijderd, Status.verwijderd)]
        public void ZetToestelStatus_Valid(Status toestelStatusIn, Status toestelStatusUit)
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            t.ZetStatus(toestelStatusIn);
            Assert.Equal(toestelStatusUit, t.Status);
        }

        [Fact]
        public void ZetToestelType_Valid()
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            t.ZetToestelType(_tt);
            Assert.Equal(_tt, t.ToestelType);
        }

        [Fact]
        public void ZetToestelType_InValid()
        {
            Toestel t = new(10, "toestel", Status.operatief, _tt);
            Assert.Throws<ToestelException>(() => t.ZetToestelType(null));
        }

        [Theory]
        [InlineData(10, "Toestel", Status.operatief, 10, "Toestel", Status.operatief)]
        [InlineData(10, "Toestel", Status.onderhoud, 10, "Toestel", Status.onderhoud)]
        [InlineData(10, "Toestel", Status.verwijderd, 10, "Toestel", Status.verwijderd)]

        [InlineData(10, "   Toestel", Status.verwijderd, 10, "Toestel", Status.verwijderd)]
        [InlineData(10, "Toestel   ", Status.verwijderd, 10, "Toestel", Status.verwijderd)]
        public void ctor_Valid(int toestelNrIn, string toestelNaamIn, Status statusIn, int toestelNrUit, string toestelNaamUit, Status statusUit)
        {
            Toestel t = new(toestelNrIn, toestelNaamIn, statusIn, _tt);

            Assert.Equal(toestelNrUit, t.ToestelNummer);
            Assert.Equal(toestelNaamUit, t.ToestelNaam);
            Assert.Equal(statusUit, t.Status);
            Assert.Equal(_tt, t.ToestelType);
        }

        [Theory]
        [InlineData(0, "Toestel", Status.operatief, 1, "toesteltype")]

        [InlineData(10, "", Status.operatief, 1, "toesteltype")]
        [InlineData(10, "   ", Status.operatief, 1, "toesteltype")]
        [InlineData(10, null, Status.operatief, 1, "toesteltype")]
        [InlineData(10, "\n", Status.operatief, 1, "toesteltype")]
        [InlineData(10, "\r", Status.operatief, 1, "toesteltype")]

        [InlineData(0, "Toestel", Status.operatief, 0, null)]
        public void ctor_Invalid(int toestelNr, string toestelNaam, Status status, int toesteltypeNr, string toesteltypeNaam)
        {
            ToestelType tt;
            if (toesteltypeNr != 0) tt = new(toesteltypeNr, toesteltypeNaam);
            else tt = null;

            Assert.Throws<ToestelException>(() => new Toestel(toestelNr, toestelNaam, status, tt));
        }
    }
}
