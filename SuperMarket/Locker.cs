using System.Collections.Generic;

namespace SuperMarket
{
    public class Locker
    {
        private readonly Dictionary<Ticket, Bag> _bagsList = new Dictionary<Ticket, Bag>();
        private readonly int _capacity;

        public Locker()
        {
            _capacity = 10;
        }

        public Locker(int capacity)
        {
            _capacity = capacity;
        }

        public int LeftCapacity
        {
            get
            {
                return _capacity - _bagsList.Count;
            }
        }

        public float EmptyRatio
        {
            get
            {
                return (float)LeftCapacity/_capacity;
            }
        }

        public Ticket Store(Bag bag)
        {
            if (_bagsList.Count >= _capacity) return null;

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