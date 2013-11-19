using System.Collections.Generic;
using SuperMarket.LockerStrategy;

namespace SuperMarket
{
    public class SmartRobot : Robot
    {
        public SmartRobot(IList<Locker> lockers) 
            : base(lockers, new LeftCapacityLockerStrategy())
        {

        }
    }
}
