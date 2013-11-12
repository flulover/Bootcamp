using System.Collections.Generic;

namespace SuperMarket
{
    public class Locker
    {
        private Dictionary<Ticket, Bag> _bagsList = new Dictionary<Ticket, Bag>();
        private int _count;

        public Locker()
        {
            _count = 10;
        }

        public Locker(int count)
        {
            _count = count;
        }

        public int LeftCapacity
        {
            get
            {
                return _count - _bagsList.Count;
            }
        }

        public float EmptyRatio
        {
            get
            {
                return (float)LeftCapacity/_count;
            }
        }

        public Ticket Store(Bag bag)
        {
            if (_bagsList.Count >= _count) return null;

            var ticket = new Ticket();
            _bagsList.Add(ticket, bag);
            return ticket;
        }

        public Bag Pick(Ticket ticket)
        {
            if (_bagsList.ContainsKey(ticket))
            {
                Bag bag = _bagsList[ticket];
                _bagsList.Remove(ticket);
                return bag;
            }
            return null;
        }
    }
}