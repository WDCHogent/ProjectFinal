using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using Xunit;

namespace xFitnessReservatieDomein
{
    public class UnitTestTijdslot
    {
        [Fact]
        public void ZetTijdslot_Valid()
        {
            Tijdslot ts = new(10);
            Assert.Equal(10, ts.Tslot);
            ts.ZetTijdslot(1);
            Assert.Equal(1, ts.Tslot);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(24)]
        public void ZetTijdslot_Invalid(int tijdslot)
        {
            Tijdslot ts = new(10);
            Assert.Throws<TijdslotException>(() => ts.ZetTijdslot(tijdslot));
        }

        [Fact]
        public void ctor_Valid()
        {
            Tijdslot ts = new(10);
            Assert.Equal(10, ts.Tslot);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(24)]
        public void ctor_Invalid(int tijdslot)
        {
            Assert.Throws<TijdslotException>(() => new Tijdslot(tijdslot));
        }
    }
}
