using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{
    public class SmartRobot : Robot
    {
        public SmartRobot(IList<Locker> lockers) : base(lockers)
        {

        }

        public override Ticket Store(Bag bag)
        {
            return _lockerList.First(x => x.LeftCapacity == _lockerList.Max(y => y.LeftCapacity)).Store(bag);
        }
    }
}
