using System.Collections.Generic;
using SuperMarket.LockerStratagy;

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