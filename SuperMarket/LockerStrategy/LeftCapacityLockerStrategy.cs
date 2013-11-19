﻿using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.LockerStrategy
{
    public class LeftCapacityLockerStrategy : IGetStoreLockerStrategy
    {
        public Locker GetLocker(IList<Locker> lockerList)
        {
            return lockerList.FirstOrDefault(x => x.LeftCapacity == lockerList.Max(y => y.LeftCapacity));
        }
    }
}