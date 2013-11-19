using System.Collections.Generic;

namespace SuperMarket.LockerStrategy
{
    public interface IGetStoreLockerStrategy
    {
        Locker GetLocker(IList<Locker> lockerList);
    }
}