using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{
    public class RobotManager
    {
        private readonly List<Locker> _lockerList;
        private readonly List<Robot> _robotList;

        public RobotManager(List<Locker> lockers, List<Robot> robots)
        {
            _lockerList = lockers;
            _robotList = robots;
        }

        
        public Ticket Store(Bag bag)
        {
            var lockerTicket = _lockerList.Select(locker => locker.Store(bag)).FirstOrDefault(ticket => ticket != null);
            return lockerTicket ?? _robotList.Select(robot => robot.Store(bag)).FirstOrDefault(ticket => ticket != null);
        }

        public Bag Pick(Ticket ticket)
        {
            foreach (var locker in _lockerList)
            {
                var bag = locker.Pick(ticket);
                if (bag != null)
                {
                    return bag;
                }
            }

            return _robotList.Select(robot => robot.Pick(ticket)).FirstOrDefault(bag => bag != null);
        }
    }
}