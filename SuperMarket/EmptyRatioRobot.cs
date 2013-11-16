using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{
    public class EmptyRatioRobot : Robot
    {
        public EmptyRatioRobot(List<Locker> lockerList)
            : base(lockerList)
        {
        }

        public override Ticket Store(Bag bag)
        {
            return _lockerList.First(x => x.EmptyRatio == _lockerList.Max(y => y.EmptyRatio)).Store(bag);
        }
    }
}