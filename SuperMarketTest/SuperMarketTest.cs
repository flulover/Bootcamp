using System.Collections.Generic;
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

        [Test]
        public void should_return_a_valid_ticket_when_locker_availiable()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(1);
            lockers.Add(locker);

            var robot = new Robot(lockers);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(locker.Pick(ticket), bag);
        }

        [Test]
        public void should_return_null_when_no_locker_availiable()
        {
            var lockers = new List<Locker> {new Locker(1)};

            var robot = new Robot(lockers);
            robot.Store(new Bag());
            var ticket = robot.Store(new Bag());

            Assert.IsNull(ticket);
        }

        [Test]
        public void should_store_bags_into_multiple_lockers_in_order()
        {
            var locker1 = new Locker(1);
            var locker2 = new Locker(1);

            var lockers = new List<Locker> {locker1, locker2};

            var robot = new Robot(lockers);
            var bag1 = new Bag();
            var bag2 = new Bag();
            var ticket1 = robot.Store(bag1);
            var ticket2 = robot.Store(bag2);

            Assert.AreSame(locker1.Pick(ticket1), bag1);
            Assert.AreSame(locker2.Pick(ticket2), bag2);
        }

        [Test]
        public void should_return_the_bag_stored_by_the_ticket()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(1);
            lockers.Add(locker);

            var robot = new Robot(lockers);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(robot.Pick(ticket), bag);
        }
    }
}
