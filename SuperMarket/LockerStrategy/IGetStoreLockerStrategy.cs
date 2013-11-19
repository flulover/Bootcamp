using System.Collections.Generic;

namespace SuperMarket.LockerStratagy
{
    public interface IGetStoreLockerStrategy
    {
        Locker GetLocker(IList<Locker> lockerList);
    }
}