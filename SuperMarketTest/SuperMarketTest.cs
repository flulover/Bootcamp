using System.ComponentModel;
using System.Threading;
using NUnit.Framework;
using SuperMarket;

namespace SuperMarketTest
{
    [TestFixture]
    public class SuperMarketTest
    {
        [Test]
        public void should_store_bag_in_locker()
        {
            var locker = new Locker();
            var bag = new Bag();

            var ticket = locker.Store(bag);
            Assert.NotNull(ticket);
        }

        [Test]
        public void should_do_not_store_bag_when_locker_is_full()
        {
            var locker = new Locker(1);
            locker.Store(new Bag());

            var ticket = locker.Store(new Bag());
            Assert.Null(ticket);
        }

        [Test]
        public void should_use_ticket_to_pick_correct_bag_from_locker()
        {
            var locker = new Locker();
            var bag = new Bag();
            var ticket = locker.Store(bag);

            Assert.AreSame(bag, locker.Pick(ticket));
        }

        [Test]
        public void should_not_pick_bag_from_locker_when_ticket_is_uncorrect()
        {
            var locker = new Locker();
            var bag = new Bag();
            locker.Store(bag);

            Assert.Null(locker.Pick(new Ticket()));
        }

        [Test]
        public void should_not_pick_bag_from_locker_twice()
        {
            var locker = new Locker();
            var bag = new Bag();
            var ticket = locker.Store(bag);

            locker.Pick(ticket);
            Assert.Null(locker.Pick(ticket));
        }
    }
}
