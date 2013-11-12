using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        [Test]
        public void should_get_left_capacity_from_locker()
        {
            var locker = new Locker(5);
            locker.Store(new Bag());

            Assert.AreEqual(4, locker.LeftCapacity);
        }

        [Test]
        public void shoud_store_bag_in_the_locker_which_has_lagest_capacity()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(2);
            locker.Store(new Bag());
            lockers.Add(locker);
            var secondLocker = new Locker(2);
            lockers.Add(secondLocker);

            var robot = new SmartRobot(lockers);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(bag, secondLocker.Pick(ticket));
        }

        [Test]
        public void should_return_the_bag_stored_in_smartrobot_by_the_ticket()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(1);
            lockers.Add(locker);

            var robot = new SmartRobot(lockers);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(robot.Pick(ticket), bag);
        }

        [Test]
        public void shoud_store_bag_in_the_locker_which_has_lagest_empty_ratio()
        {
            var lockerList = new List<Locker>();

            var smallEmptyRatioLocker = new Locker(6);
            smallEmptyRatioLocker.Store(new Bag());
            smallEmptyRatioLocker.Store(new Bag());
            smallEmptyRatioLocker.Store(new Bag());
            smallEmptyRatioLocker.Store(new Bag());


            var largerEmptyRatioLocker = new Locker(2);
            largerEmptyRatioLocker.Store(new Bag());

            lockerList.Add(smallEmptyRatioLocker);
            lockerList.Add(largerEmptyRatioLocker);

            var robot = new EmptyRatioRobot(lockerList);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(bag, largerEmptyRatioLocker.Pick(ticket));
        }

//        [Test]
//        public void should_throw_excption_when_robot_have_no_locker()
//        {
//            var robot = new SmartRobot(null);
//            try
//            {
//                robot.Store(new Bag());
//            }
//            catch (NullReferenceException e)
//            {
//                Assert.True(true);
//            }
//        }


    }

    public class EmptyRatioRobot : Robot
    {
        public EmptyRatioRobot(List<Locker> lockerList)
            : base(lockerList)
        {
        }

        public override Ticket Store(Bag bag)
        {
            return _lockerList.First(x => x.EmptyRatio == _lockerList.Max(y => y.EmptyRatio)).Store(bag);
        }
    }

    public class SmartRobot : Robot
    {
        public SmartRobot(IList<Locker> lockers) : base(lockers)
        {

        }

        public override Ticket Store(Bag bag)
        {
            return _lockerList.First(x => x.LeftCapacity == _lockerList.Max(y => y.LeftCapacity)).Store(bag);
        }
    }
}
