using System.Collections.Generic;

namespace Bootcamp_session1
{
    public class LengthUnit
    {
        private static readonly Dictionary<UnitType, int> MapToWeight = new Dictionary<UnitType, int>()
        {
            {UnitType.Meter, 1000},
            {UnitType.Centimetre, 10},
            {UnitType.Millimetre, 1},
        };

        static public double ConvertTo(double sourceValue, UnitType sourceUnitType, UnitType targetUnitType)
        {
            double mm = 0;
            if (MapToWeight.ContainsKey(sourceUnitType))
            {
                mm = MapToWeight[sourceUnitType] * sourceValue;
            }

            double targetValue = 0;
            if (MapToWeight.ContainsKey(targetUnitType))
            {
                targetValue = mm / MapToWeight[targetUnitType];
            }

            return targetValue;
        }
    }
}