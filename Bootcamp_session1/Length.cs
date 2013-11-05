using System;
using System.Collections.Generic;

namespace Bootcamp_session1
{
    public class Length
    {
        private readonly double _value;
        private readonly UnitType _unitType;
        private const double TOLERANCE = 0.001;

        private static readonly Dictionary<UnitType, int> MapToWeight = new Dictionary<UnitType, int>()
        {
            {UnitType.Meter, 1000},
            {UnitType.Centimetre, 10},
            {UnitType.Millimetre, 1},
        };

        public Length(UnitType unitType, double value)
        {
            _unitType = unitType;
            _value = value;
        }

        protected bool Equals(Length other)
        {
            if (_unitType == other._unitType)
                return Math.Abs(_value - other._value) < TOLERANCE;

            return Equals(other.ConvertTo(_unitType));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Length) obj);
        }

        

        public Length ConvertTo(UnitType targetUnitType)
        {
            double mm = 0;
            if (MapToWeight.ContainsKey(_unitType))
            {
                mm = MapToWeight[_unitType]*_value;
            }

            double targetValue = 0;
            if (MapToWeight.ContainsKey(targetUnitType))
            {
                targetValue = mm / MapToWeight[targetUnitType];
            }

            return new Length(targetUnitType, targetValue);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_value.GetHashCode()*397) ^ (int) _unitType;
            }
        }
    }
}