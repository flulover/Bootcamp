using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{
    public class Robot
    {
        protected readonly IList<Locker> _lockerList;

        public Robot(IList<Locker> lockerList)
        {
            _lockerList = lockerList;
        }

        public virtual Ticket Store(Bag bag)
        {
            return _lockerList.Select(locker => locker.Store(bag)).FirstOrDefault(ticket => ticket != null);
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
            return null;
        }
    }
}