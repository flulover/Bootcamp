using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuperMarket;

namespace SuperMarketTest
{
    [TestFixture]
    class RobotTest
    {
        [Test]
        public void should_return_a_valid_ticket_used_robot_when_locker_availiable()
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
        public void should_return_null_used_robot_when_no_locker_availiable()
        {
            var lockers = new List<Locker> { new Locker(1) };

            var robot = new Robot(lockers);
            robot.Store(new Bag());
            var ticket = robot.Store(new Bag());

            Assert.IsNull(ticket);
        }

        [Test]
        public void should_store_bags_into_multiple_lockers_in_order_use_robot()
        {
            var locker1 = new Locker(1);
            var locker2 = new Locker(1);

            var lockers = new List<Locker> { locker1, locker2 };

            var robot = new Robot(lockers);
            var bag1 = new Bag();
            var bag2 = new Bag();
            var ticket1 = robot.Store(bag1);
            var ticket2 = robot.Store(bag2);

            Assert.AreSame(locker1.Pick(ticket1), bag1);
            Assert.AreSame(locker2.Pick(ticket2), bag2);
        }

        [Test]
        public void should_return_the_bag_stored_by_the_ticket_used_robot()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(1);
            lockers.Add(locker);

            var robot = new Robot(lockers);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(robot.Pick(ticket), bag);
        }

        [Test]
        public void should_throw_excption_when_robot_have_no_locker()
        {
            var robot = new Robot(null);
            Assert.Throws<ArgumentNullException>(() => robot.Store(new Bag()));
        }
    }
}
