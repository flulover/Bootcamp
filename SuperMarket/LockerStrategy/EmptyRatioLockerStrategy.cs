using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.LockerStratagy
{
    public class EmptyRatioLockerStrategy : IGetStoreLockerStrategy
    {
        public Locker GetLocker(IList<Locker> lockerList)
        {
            return lockerList.FirstOrDefault(x => Math.Abs(x.EmptyRatio - lockerList.Max(y => y.EmptyRatio)) < TOLERANCE);
        }

        static private float TOLERANCE { get { return 0.0001f; } }
    }
}