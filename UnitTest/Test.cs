using Bootcamp_session1;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class LengthUnitTest
    {
        [TestCase]
        public void should_equal_correct()
        {
            var metreLength = new Length(UnitType.Meter, 1);
            var millimetreLength = new Length(UnitType.Millimetre, 100);
            var centimetreLength = new Length(UnitType.Centimetre, 10);

            Assert.AreEqual(metreLength, new Length(UnitType.Meter, 1));
            Assert.AreEqual(millimetreLength, new Length(UnitType.Millimetre, 100));
            Assert.AreEqual(centimetreLength, new Length(UnitType.Centimetre, 10));
        }

        [TestCase(UnitType.Meter, 1, UnitType.Centimetre, 100)]
        [TestCase(UnitType.Meter, 1, UnitType.Millimetre, 1000)]
        [TestCase(UnitType.Centimetre, 1, UnitType.Millimetre, 10)]
        [TestCase(UnitType.Millimetre, 500, UnitType.Meter, 0.5)]
        [TestCase(UnitType.Millimetre, 500, UnitType.Centimetre, 50)]
        [TestCase(UnitType.Centimetre, 100, UnitType.Meter, 1)]
        public void should_unit_transform(UnitType sourceUnitType, double sourceValue, UnitType targetUnitType, double targetValue)
        {
            Assert.AreEqual(new Length(sourceUnitType, sourceValue), new Length(targetUnitType, targetValue));
        }
    }
}
