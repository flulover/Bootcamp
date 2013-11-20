using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{
    public class RobotManager
    {
        private readonly List<ILocker> _lockerList = new List<ILocker>();

        public RobotManager(IEnumerable<ILocker> lockers, IEnumerable<ILocker> robots)
        {
            if (lockers != null)
            {
                _lockerList.AddRange(lockers);
            }
            if (robots != null)
            {
                _lockerList.AddRange(robots);
            }
        }
        
        public Ticket Store(Bag bag)
        {
            return _lockerList.Select(locker => locker.Store(bag)).FirstOrDefault(ticket => ticket != null);
        }

        public Bag Pick(Ticket ticket)
        {
            return _lockerList.Select(locker => locker.Pick(ticket)).FirstOrDefault(bag => bag != null);
        }
    }
}