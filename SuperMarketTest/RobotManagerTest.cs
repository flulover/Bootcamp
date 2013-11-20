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
            var selfLocker = new Locker(1);
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                null
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
            var smartRobot = Robot.CreateSmartRobot(new List<Locker> { sequenceLocker });
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                new List<ILocker>
                {
                    smartRobot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, smartRobot.Pick(ticket));
        }

        [Test]
        public void should_store_bag_in_second_robot_when_self_locker_not_available_and_first_robot_not_available()
        {
            var selfLocker = new Locker(0);
            var sequenceLocker = new Locker(0);
            var capacityLocker = new Locker(1);
            var smartRobot = Robot.CreateSmartRobot(new List<Locker> { sequenceLocker });
            var superSmartRobot = Robot.CreateSuperSmartRobot(new List<Locker> { capacityLocker });
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                new List<ILocker>
                {
                    smartRobot, superSmartRobot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, superSmartRobot.Pick(ticket));
        }

        [Test]
        public void should_store_bag_in_first_robot_when_two_robots_are_not_full()
        {
            var selfLocker = new Locker(0);
            var sequenceLocker = new Locker(1);
            var capacityLocker = new Locker(1);
            var smartRobot = Robot.CreateSmartRobot(new List<Locker> { sequenceLocker });
            var superSmartRobot = Robot.CreateSuperSmartRobot(new List<Locker> { capacityLocker });
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                new List<ILocker>
                {
                    smartRobot, superSmartRobot
                }
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, smartRobot.Pick(ticket));
            Assert.IsNull(superSmartRobot.Pick(ticket));
        }

        [Test]
        public void should_return_null_when_locker_and_robot_are_both_null()
        {
            var robotManager = new RobotManager(null, null);

            Assert.IsNull(robotManager.Store(new Bag()));
        }

        [Test]
        public void should_return_null_when_locker_and_robot_are_both_full()
        {
            var sequenceLocker = new Locker(0);
            var selfLocker = new Locker(0);
            var smartRobot = Robot.CreateSmartRobot(new List<Locker> { sequenceLocker });
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                new List<ILocker>
                {
                    smartRobot
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
                new List<ILocker>
                {
                    selfLocker
                },
                null
            );

            var bag = new Bag();
            var ticket = robotManager.Store(bag);

            Assert.AreSame(bag, selfLocker.Pick(ticket));
        }

        [Test]
        public void should_pick_bag_from_robot_when_bag_is_store_in_robot()
        {
            var sequenceLocker = new Locker(1);
            var selfLocker = new Locker(0);
            var smartRobot = Robot.CreateSmartRobot(new List<Locker> { sequenceLocker });
            var robotManager = new RobotManager(
                new List<ILocker>
                {
                    selfLocker
                },
                new List<ILocker>
                {
                    smartRobot
                }
            );

            var bag = new Bag();
            var ticket = sequenceLocker.Store(bag);

            Assert.AreSame(bag, robotManager.Pick(ticket));
        }
    }
}
