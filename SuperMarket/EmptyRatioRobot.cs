using System.Collections.Generic;
using SuperMarket.LockerStrategy;

namespace SuperMarket
{
    public class EmptyRatioRobot : Robot
    {
        public EmptyRatioRobot(List<Locker> lockerList)
            : base(lockerList, new EmptyRatioLockerStrategy())
        {
        }
    }
}