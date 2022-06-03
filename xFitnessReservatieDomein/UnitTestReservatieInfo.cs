using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Exceptions;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestReservatieInfo
    {
        private Toestel _t = new(10, "toestelnaam", Status.operatief, new ToestelType(1, "toesteltype"));

        [Fact]
        public void ZetReservatienummer_Valid()
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            Assert.Equal(10, ri.Reservatienummer);
            ri.ZetReservatienummer(1);
            Assert.Equal(1, ri.Reservatienummer);
        }

        [Fact]
        public void ZetReservatienummer_Invalid()
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            Assert.Equal(10, ri.Reservatienummer);
            Assert.Throws<ReservatieInfoException>(() => ri.ZetReservatienummer(0));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(23, 23)]
        public void ZetBeginuur_Valid(int beginuurIn, int beginuurUit)
        {
            ReservatieInfo ri = new(10, 8, 9, _t);

            ri.ZetBeginuur(beginuurIn);
            Assert.Equal(beginuurUit, ri.Beginuur);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(24)]
        public void ZetBeginuur_Invalid(int beginuur)
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            Assert.Throws<ReservatieInfoException>(() => ri.ZetBeginuur(beginuur));
        }

        [Theory]
        [InlineData(0, 1, 0, 1)]
        [InlineData(0, 2, 0, 2)]
        [InlineData(21, 22, 21, 22)]
        [InlineData(22, 23, 22, 23)]
        public void ZetEinduur_Valid(int beginuurIn, int einduurIn, int beginuurUit, int einduurUit)
        {
            ReservatieInfo ri = new(10, 8, 9, _t);

            ri.ZetBeginuur(beginuurIn); ri.ZetEinduur(einduurIn);
            Assert.Equal(einduurUit, ri.Einduur);
        }

        [Theory]
        [InlineData(0, -2)]
        [InlineData(0, -1)]
        [InlineData(23, 24)]
        [InlineData(23, 25)]
        [InlineData(8, 11)]
        [InlineData(11, 10)]
        public void ZetEinduur_Invalid(int beginuur, int einduur)
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            ri.ZetBeginuur(beginuur);
            Assert.Throws<ReservatieInfoException>(() => ri.ZetEinduur(einduur));
        }

        [Fact]
        public void ZetToestel_Valid()
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            Assert.Equal(_t, ri.Toestel);
            ri.ZetToestel(_t);
            Assert.Equal(_t, ri.Toestel);
        }

        [Fact]
        public void ZetToestel_Invalid()
        {
            ReservatieInfo ri = new(10, 8, 9, _t);
            Assert.Equal(10, ri.Reservatienummer);
            Assert.Throws<ReservatieInfoException>(() => ri.ZetToestel(null));
        }

        [Theory]
        [InlineData(10, 8, 9, 10, 8, 9)]
        [InlineData(10, 8, 10, 10, 8, 10)]
        public void ctor_Valid(int reservatieNrIn, int beginuurIn, int einduurIn, int reservatieNrUit, int beginuurUit, int einduurUit)
        {
            ReservatieInfo ri = new(reservatieNrIn, beginuurIn, einduurIn, _t);

            Assert.Equal(reservatieNrUit, ri.Reservatienummer);
            Assert.Equal(beginuurUit, ri.Beginuur);
            Assert.Equal(einduurUit, ri.Einduur);
            Assert.Equal(_t, ri.Toestel);
        }

        [Theory]
        [InlineData(0, 8, 9, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]

        [InlineData(10, -1, 1, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]
        [InlineData(10, 25, 27, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]
        [InlineData(10, 1, -1, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]
        [InlineData(10, 8, 11, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]
        [InlineData(10, 11, 8, 10, "toestelnaam", Status.operatief, 1, "toesteltype")]

        [InlineData(10, 8, 9, 0, null, null, 0, null)]
        public void ctor_Invalid(int reservatieNr, int beginuur, int einduur, int toestelNr, string toestelNaam, Status status, int toesteltypeNr, string toesteltypeNaam)
        {
            Toestel t;
            if (toestelNr != 0) t = new(toestelNr, toestelNaam, status, new ToestelType(toesteltypeNr, toesteltypeNaam));
            else t = null;

            Assert.Throws<ReservatieInfoException>(() => new ReservatieInfo(reservatieNr, beginuur, einduur, t));
        }
    }
}
