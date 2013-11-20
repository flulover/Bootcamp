using System.Collections.Generic;
using NUnit.Framework;
using SuperMarket;
using SuperMarket.LockerStrategy;

namespace SuperMarketTest
{
    [TestFixture]
    class SmartRobotTest
    {
        [Test]
        public void shoud_store_bag_in_the_locker_used_robot_which_has_lagest_capacity()
        {
            var lockers = new List<Locker>();
            var locker = new Locker(2);
            locker.Store(new Bag());
            lockers.Add(locker);
            var secondLocker = new Locker(2);
            lockers.Add(secondLocker);

            var robot = Robot.CreateSmartRobot(lockers);
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

            var robot = Robot.CreateSmartRobot(lockers);
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

            var robot = Robot.CreateSuperSmartRobot(lockerList);
            var bag = new Bag();
            var ticket = robot.Store(bag);

            Assert.AreSame(bag, largerEmptyRatioLocker.Pick(ticket));
        }
    }
}
