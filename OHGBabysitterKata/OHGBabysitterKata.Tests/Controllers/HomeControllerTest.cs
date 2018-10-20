using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHGBabysitterKata;
using OHGBabysitterKata.Controllers;

namespace OHGBabysitterKata.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetCharge_WithBedtimeAndTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 1AM, and a bedtime of 9PM
            var babysitterTime = new OHGBabysitterKata.Models.BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckIn.AddHours(18);
            babysitterTime.CheckOut.AddHours(1);
            babysitterTime.Bedtime.AddHours(21);

            var controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 120.0M);
        }

        [TestMethod]
        public void GetCharge_WithBedtimeNoTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 11PM, and a bedtime of 9PM
            var babysitterTime = new OHGBabysitterKata.Models.BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckIn.AddHours(18);
            babysitterTime.CheckOut.AddHours(23);
            babysitterTime.Bedtime.AddHours(21);

            var controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 104.0M);
        }

        [TestMethod]
        public void GetCharge_NoBedtimeWithTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 1AM, and a bedtime of 9PM
            var babysitterTime = new OHGBabysitterKata.Models.BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.CheckIn.AddHours(18);
            babysitterTime.CheckOut.AddHours(1);

            var controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 88.0M);
        }

        public void GetCharge_CheckInTooEarlyWithBedTime()
        {
            // Arrange with a checkin of 6PM, checkout of 11AM, and a bedtime of 9PM
            var babysitterTime = new OHGBabysitterKata.Models.BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckIn.AddHours(16);
            babysitterTime.CheckOut.AddHours(0);
            babysitterTime.Bedtime.AddHours(21);

            var controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 60.0M);
        }

        public void GetCharge_CheckOutTooLateWithBedTime()
        {
            // Arrange with a checkin of 6PM, checkout of 11AM, and a bedtime of 9PM
            var babysitterTime = new OHGBabysitterKata.Models.BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckIn.AddHours(18);
            babysitterTime.CheckOut.AddHours(5);
            babysitterTime.Bedtime.AddHours(21);

            var controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 124.0M);
        }
    }
}
