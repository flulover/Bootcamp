using Bootcamp_session1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bootcamp_UnitTest
{
    [TestClass]
    public class LengthUnitTest
    {
        [TestMethod]
        public void should_equal_correct()
        {
            var metreLength = new Length(1, UnitType.Meter);
            var millimetreLength = new Length(100, UnitType.Millimetre);
            var centimetreLength = new Length(10, UnitType.Centimetre);

            Assert.AreEqual(metreLength, new Length(1, UnitType.Meter));
            Assert.AreEqual(millimetreLength, new Length(100, UnitType.Millimetre));
            Assert.AreEqual(centimetreLength, new Length(10, UnitType.Centimetre));
        }

        [TestMethod]
        public void should_unit_transform()
        {
            var metreLength = new Length(1, UnitType.Meter);
            var millimetreLength = metreLength.ConvertTo(UnitType.Centimetre);

            Assert.AreEqual(millimetreLength, new Length(100, UnitType.Centimetre));
        }

        [TestMethod]
        public void should_compare_two_length_with_different_unit()
        {
            Assert.AreEqual(new Length(1, UnitType.Meter), new Length(100, UnitType.Centimetre));
        }
    }
}
