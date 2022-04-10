﻿using LaserAPI.Interfaces.Dal;
using LaserAPI.Logic;
using LaserAPI.Models.Dto.Zones;
using LaserAPI.Models.Helper;
using LaserAPITests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace LaserAPITests.Tests.Logic
{
    [TestClass]
    public class ZoneLogicTest
    {
        private readonly List<ZoneDto> _zones;
        private readonly ZoneLogic _zoneLogic;

        public ZoneLogicTest()
        {
            IZoneDal zoneDal = new MockedZoneDal().ZoneDal;
            _zones = zoneDal
                .All()
                .Result;
            _zoneLogic = new MockedZoneLogic().ZoneLogic;
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, 4000, 0),
                new Point(-1000, 3000), new Point(-1000, 0));

            Assert.IsTrue(crossingPoint.X == -1000 && crossingPoint.Y == 2500);
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesTest2()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -2000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 3000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, 4000, 0),
                new Point(-1000, 3000), new Point(-1000, 0));

            Assert.IsTrue(crossingPoint.X == -1000 && crossingPoint.Y == 2500);
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesTest3()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, 4000, -4000),
                new Point(-4000, -4000), new Point(4000, 4000));

            Assert.IsTrue(crossingPoint.X == 0 && crossingPoint.Y == 0);
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesTest4()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, 4000, 0),
                new Point(-4000, 0), new Point(4000, 4000));

            Assert.IsTrue(crossingPoint.X == 0 && crossingPoint.Y == 2000);
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesLineDoesNotCrossTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, -4000, 0),
                new Point(4000, 0), new Point(4000, 4000));

            Assert.IsTrue(crossingPoint.X == -4001 && crossingPoint.Y == -4001);
        }

        [TestMethod]
        public void CalculateCrossingPointBetweenTwoLinesLineDoesNotCrossTest2()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            Point crossingPoint = ZoneLogic.CalculateCrossingPointOfZoneLineAndLaserPath(new LaserMessage(0, 0, 0, 4000, 4000),
                new Point(-4000, 0), new Point(4000, 0));

            Assert.IsTrue(crossingPoint.X == -4001 && crossingPoint.Y == -4001);
        }

        [TestMethod]
        public void GetLineHitByPathTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            LaserConnectionLogic.PreviousLaserMessage.Y = 4000;
            List<ZoneLine> zoneLinesHit = ZoneLogic.GetZoneLineHitByPath(_zones[0], new LaserMessage(0, 0, 0, 4000, 0));

            Point firstIntersectPosition = zoneLinesHit[0].CrossedPoint;
            Point secondIntersectPosition = zoneLinesHit[1].CrossedPoint;
            Assert.IsTrue(zoneLinesHit.Count == 2 &&
                          firstIntersectPosition.X == 1000 && firstIntersectPosition.Y == 1500 &&
                          secondIntersectPosition.X == -1000 && secondIntersectPosition.Y == 2500);
        }

        [TestMethod]
        public void GetLineHitByPathParallelogramZoneTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -3000;
            LaserConnectionLogic.PreviousLaserMessage.Y = -1000;
            List<ZoneLine> zoneLinesHit = ZoneLogic.GetZoneLineHitByPath(_zones[2], new LaserMessage(0, 0, 0, 3000, -1000));

            Point firstIntersectPosition = zoneLinesHit[0].CrossedPoint;
            Point secondIntersectPosition = zoneLinesHit[1].CrossedPoint;
            Assert.IsTrue(zoneLinesHit.Count == 2 &&
                          firstIntersectPosition.X == 2250 && firstIntersectPosition.Y == -1000 &&
                          secondIntersectPosition.X == -2250 && secondIntersectPosition.Y == -1000);
        }

        [TestMethod]
        public void GetLineHitByPath6PointZoneTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = -3000;
            LaserConnectionLogic.PreviousLaserMessage.Y = -1000;
            List<ZoneLine> zoneLinesHit = ZoneLogic.GetZoneLineHitByPath(_zones[3], new LaserMessage(0, 0, 0, 3000, -1000));

            Assert.IsTrue(zoneLinesHit.Count == 3);
        }

        [TestMethod]
        public void GetLineHitByPathPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            LaserConnectionLogic.PreviousLaserMessage.X = -4000;
            for (int i = -4000; i < 4000; i++)
            {
                LaserConnectionLogic.PreviousLaserMessage.Y = i;
                ZoneLogic.GetZoneLineHitByPath(_zones[1], new LaserMessage(0, 0, 0, 4000, 4000));
            }

            stopwatch.Stop();
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 100);
        }

        [TestMethod]
        public void GetPointsClosestToPreviousSendMessageTest()
        {
            LaserConnectionLogic.PreviousLaserMessage = new LaserMessage(0, 0, 0, -4000, 4000);
            List<LaserMessage> pointsToSort = new()
            {
                new LaserMessage(0, 0, 0, -400, 4000),
                new LaserMessage(0, 0, 0, -4000, 4000),
                new LaserMessage(0, 0, 0, 400, 4000),
            };
            List<LaserMessage> expectedOrder = new()
            {
                new LaserMessage(0, 0, 0, -4000, 4000),
                new LaserMessage(0, 0, 0, -400, 4000),
                new LaserMessage(0, 0, 0, 400, 4000),
            };

            List<LaserMessage> sortedPoints = ZoneLogic.SortPointsFromClosestToPreviousSendMessageToFarthest(pointsToSort);
            for (int i = 0; i < sortedPoints.Count; i++)
            {
                LaserMessage message = sortedPoints[i];
                LaserMessage expectedMessage = expectedOrder[i];
                Point sortedPoint = new(message.X, message.Y);
                Point expectedPoint = new(expectedMessage.X, expectedMessage.Y);
                Assert.IsTrue(sortedPoint.X == expectedPoint.X && sortedPoint.Y == expectedPoint.Y);
            }
        }

        [TestMethod]
        public void GetPointsOfZoneLinesHitByPathTest()
        {
            LaserConnectionLogic.PreviousLaserMessage = new LaserMessage(0, 0, 0, -4000, 4000);
            List<LaserMessage> messages = _zoneLogic.GetPointsOfZoneLinesHitByPath(new LaserMessage(0, 0, 0, 4000, -4000));
            Assert.IsNotNull(messages);
        }

        [TestMethod]
        public void IsInsidePolygonTest()
        {
            Point[] polygon = {
                new(-3500, 0),
                new(3500, 0),
                new(4000, -4000),
                new (-4000, -4000)
            };

            bool positionIsInsidePolygon = ZoneLogic.IsInsidePolygon(polygon, new Point(0, 0));
            Assert.IsTrue(positionIsInsidePolygon);
        }

        [TestMethod]
        public void IsNotInsidePolygonTest()
        {
            Point[] polygon = {
                new(-3500, 0),
                new(3500, 0),
                new(4000, -4000),
                new (-4000, -4000)
            };

            bool positionIsInsidePolygon = ZoneLogic.IsInsidePolygon(polygon, new Point(0, 4000));
            Assert.IsFalse(positionIsInsidePolygon);
        }

        [TestMethod]
        public void IsInsidePolygon6PointsTest()
        {
            Point[] polygon = {
                new(-3500, 0),
                new(0, 1000),
                new(3500, 0),
                new(4000, -4000),
                new(0, -3500),
                new (-4000, -4000)
            };

            bool positionIsInsidePolygon = ZoneLogic.IsInsidePolygon(polygon, new Point(0, -1000));
            Assert.IsTrue(positionIsInsidePolygon);
        }

        [TestMethod]
        public void GetZoneWherePathIsInsideTest()
        {
            LaserConnectionLogic.PreviousLaserMessage.X = 500;
            LaserConnectionLogic.PreviousLaserMessage.Y = 0;
            ZoneDto zone = _zoneLogic.GetZoneWherePathIsInside(new LaserMessage(0, 0, 0, 500, 0));
            Assert.IsTrue(zone.Uuid == Guid.Parse("fc220bc5-68ff-45d8-8e51-d884687e324b"));
        }
    }
}
