using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.LockerStrategy
{
    public class SequenceLockerStrategy : IGetStoreLockerStrategy
    {
        public Locker GetLocker(IList<Locker> lockerList)
        {
            return lockerList.FirstOrDefault(locker => locker.LeftCapacity != 0);
        }
    }
}