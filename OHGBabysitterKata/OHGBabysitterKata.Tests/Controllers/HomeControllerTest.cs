using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Http;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHGBabysitterKata;
using OHGBabysitterKata.Models;
using OHGBabysitterKata.Controllers;

namespace OHGBabysitterKata.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetCharge_NoBedtimeNoTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 1AM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = DateTime.Today.AddHours(18);
            babysitterTime.CheckOut = DateTime.Today.AddHours(23);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(60.0M, result);
        }

        [TestMethod]
        public void GetCharge_WithBedtimeAndTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 1AM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = DateTime.Today.AddHours(18);
            babysitterTime.CheckOut = DateTime.Today.AddHours(1);
            babysitterTime.Bedtime = DateTime.Today.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(76.0M, result);
        }

        [TestMethod]
        public void GetCharge_WithBedtimeNoTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 11PM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = DateTime.Today.AddHours(18);
            babysitterTime.CheckOut = DateTime.Today.AddHours(23);
            babysitterTime.Bedtime = DateTime.Today.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(52.0M, result);
        }

        [TestMethod]
        public void GetCharge_NoBedtimeWithTimePastMidnight()
        {
            // Arrange with a checkin of 6PM, checkout of 1AM, and no bedtime set
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = DateTime.Today.AddHours(18);
            babysitterTime.CheckOut = DateTime.Today.AddHours(1);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(88.0M, result);
        }

        [TestMethod]
        public void GetCharge_CheckInTooEarlyWithBedTime()
        {
            // Arrange with a checkin of 4PM, checkout of 12AM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();

            babysitterTime.CheckIn = DateTime.Today.AddHours(16);
            babysitterTime.CheckOut = DateTime.Today.AddHours(0);
            babysitterTime.Bedtime = DateTime.Today.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(72.0M, result);
        }

        [TestMethod]
        public void GetCharge_CheckOutTooLateWithBedTime()
        {
            // Arrange with a checkin of 6PM, checkout of 5AM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = DateTime.Today.AddHours(18);
            babysitterTime.CheckOut = DateTime.Today.AddHours(5);
            babysitterTime.Bedtime = DateTime.Today.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            var result = controller.GetNightlyCharge(babysitterTime);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(124.0M, result);
        }

        [TestMethod]
        public void GetCharge_WithUninitializedCheckIn()
        {
            // Arrange with a checkin not intitialized, checkout 11PM, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckOut = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckOut.AddHours(1);
            babysitterTime.Bedtime.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            try
            {
                var result = controller.GetNightlyCharge(babysitterTime);
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.BadRequest);
            }
        }

        [TestMethod]
        public void GetCharge_WithUninitializedCheckOut()
        {
            // Arrange with a checkin 6PM, checkout not initialized, and a bedtime of 9PM
            BabysitterTime babysitterTime = new BabysitterTime();
            babysitterTime.CheckIn = new DateTime();
            babysitterTime.Bedtime = new DateTime();
            babysitterTime.CheckIn.AddHours(18);
            babysitterTime.Bedtime.AddHours(21);

            TimeCalcController controller = new TimeCalcController();
            try
            {
                var result = controller.GetNightlyCharge(babysitterTime);
            }
            catch (HttpResponseException ex)
            {
                // Assert
                Assert.AreEqual(ex.Response.StatusCode, HttpStatusCode.BadRequest);
            }
        }
    }
}
