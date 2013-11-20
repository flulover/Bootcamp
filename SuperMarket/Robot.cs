using System.Collections.Generic;
using SuperMarket.LockerStrategy;

namespace SuperMarket
{
    public class Robot : ILocker
    {
        protected readonly IList<Locker> LockerList;
        protected readonly IGetStoreLockerStrategy GetLockerStrategy;

        public Robot(IList<Locker> lockerList, IGetStoreLockerStrategy getStoreLockerStrategy = null)
        {
            LockerList = lockerList;
            if (getStoreLockerStrategy == null)
                GetLockerStrategy = new SequenceLockerStrategy(); 
            else
                GetLockerStrategy = getStoreLockerStrategy;
        }

        public virtual Ticket Store(Bag bag)
        {
            var locker = GetLockerStrategy.GetLocker(LockerList);
            if (locker == null)
                return null;

            return locker.Store(bag);
        }

        public Bag Pick(Ticket ticket)
        {
            foreach (var locker in LockerList)
            {
                var bag = locker.Pick(ticket);
                if (bag != null)
                {
                    return bag;
                }
            }
            return null;
        }

        public static Robot CreateSuperSmartRobot(List<Locker> lockerList)
        {
            return new Robot(lockerList, new EmptyRatioLockerStrategy());
        }

        public static Robot CreateSmartRobot(List<Locker> lockers)
        {
            return new Robot(lockers, new LeftCapacityLockerStrategy());
        }
    }
}