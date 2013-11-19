using System;
using System.Collections.Generic;
using NUnit.Framework;
using SuperMarket;
using SuperMarket.LockerStrategy;

namespace SuperMarketTest
{
    [TestFixture]
    class RobotManagerTest
    {
        [Test]
        public void should_store_bag_in_self_locker_when_self_locker_available()
        {
            var sequenceLocker = new Locker(1);
            var selfLocker = new Locker(1);
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    new Robot(new List<Locker>{sequenceLocker}, 
                    new SequenceLockerStrategy())
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, selfLocker.Pick(ticket));

        }

        [Test]
        public void should_store_bag_in_robot_when_self_locker_not_available()
        {
            var sequenceLocker = new Locker(1);
            var selfLocker = new Locker(0);
            var robot = new Robot(new List<Locker> { sequenceLocker }, new SequenceLockerStrategy());
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    robot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, robot.Pick(ticket));
        }

        [Test]
        public void should_store_bag_in_second_robot_when_self_locker_not_available_and_first_robot_not_available()
        {
            var selfLocker = new Locker(0);
            var sequenceLocker = new Locker(0);
            var capacityLocker = new Locker(1);
            var robot = new Robot(new List<Locker>{sequenceLocker}, new SequenceLockerStrategy());
            var smartRobot = new Robot(new List<Locker> { capacityLocker }, new LeftCapacityLockerStrategy());
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    robot, smartRobot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, smartRobot.Pick(ticket));
        }

        [Test]
        public void should_store_bag_in_first_robot_when_two_robots_are_not_full()
        {
            var selfLocker = new Locker(0);
            var sequenceLocker = new Locker(1);
            var capacityLocker = new Locker(1);
            var robot = new Robot(new List<Locker> { sequenceLocker }, new SequenceLockerStrategy());
            var smartRobot = new Robot(new List<Locker> { capacityLocker }, new LeftCapacityLockerStrategy());
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    robot, smartRobot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, robot.Pick(ticket));
            Assert.IsNull(smartRobot.Pick(ticket));
        }

        [Test]
        public void should_throw_exception_when_locker_and_robot_are_both_null()
        {
            var robotManager = new RobotManager(null, null);

            Assert.Throws<ArgumentNullException>(() => robotManager.Store(new Bag()));
        }

        [Test]
        public void should_return_null_when_locker_and_robot_are_both_full()
        {
            var sequenceLocker = new Locker(0);
            var selfLocker = new Locker(0);
            var robot = new Robot(new List<Locker> { sequenceLocker }, new SequenceLockerStrategy());
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    robot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.IsNull(ticket);
        }

        [Test]
        public void should_pick_bag_from_self_locker()
        {
            var selfLocker = new Locker(1);
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                null
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, robotManager.Pick(ticket));
        }

        [Test]
        public void should_pick_bag_from_robot_when_bag_is_store_in_robot()
        {
            var sequenceLocker = new Locker(1);
            var selfLocker = new Locker(0);
            var robot = new Robot(new List<Locker> { sequenceLocker }, new SequenceLockerStrategy());
            var robotManager = new RobotManager(
                new List<Locker>
                {
                    selfLocker
                },
                new List<Robot>
                {
                    robot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, robotManager.Pick(ticket));
        }
    }
}
