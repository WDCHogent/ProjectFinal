using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestToestelType
    {
        [Fact]
        public void ZetToestelId_Valid()
        {
            ToestelType tt = new(10, "toesteltype");
            Assert.Equal(10, tt.ToestelId);
            tt.ZetToestelId(1);
            Assert.Equal(1, tt.ToestelId);
        }

        [Fact]
        public void ZetToestelId_Invalid()
        {
            ToestelType tt = new(10, "toesteltype");
            Assert.Equal(10, tt.ToestelId);
            Assert.Throws<ToestelTypeException>(() => tt.ZetToestelId(0));
        }

        [Theory]
        [InlineData("Toesteltype", "Toesteltype")]
        [InlineData("   Toesteltype", "Toesteltype")]
        [InlineData("Toesteltype   ", "Toesteltype")]
        public void ZetToestelNaam_Valid(string toestelNaamIn, string toestelNaamUit)
        {
            ToestelType tt = new(10, "toesteltype");
            tt.ZetToestelNaam(toestelNaamIn);
            Assert.Equal(toestelNaamUit, tt.ToestelNaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ZetToestelNaam_InValid(string toestelNaam)
        {
            ToestelType tt = new(10, "toesteltype");
            Assert.Throws<ToestelTypeException>(() => tt.ZetToestelNaam(toestelNaam));
        }
    }
}
