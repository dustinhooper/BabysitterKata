using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OHGBabysitterKata.Models;

namespace OHGBabysitterKata.Controllers
{
    public class TimeCalcController : ApiController
    {
        readonly DateTime startTime = DateTime.Today.AddHours(17);//5PM
        readonly DateTime endTime = DateTime.Today.AddHours(4);//4AM

        [HttpPost]
        public decimal GetNightlyCharge(BabysitterTime BTimeObj)
        {
            decimal nightlyCharge = 0.0M;

            //If the CheckIn or CheckOut times are less than today, they are likely not initialized
            if (BTimeObj.CheckIn < DateTime.Today || BTimeObj.CheckOut < DateTime.Today)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //Eliminate the extra hours before 5PM
            if (BTimeObj.CheckIn < startTime && BTimeObj.CheckIn > endTime)
            {
                BTimeObj.CheckIn = startTime;
            }

            //Eliminate the extra hours after 4AM
            if (BTimeObj.CheckOut > endTime && BTimeObj.CheckOut < startTime)
            {
                BTimeObj.CheckOut = endTime;
            }
            
            nightlyCharge = Calculate(BTimeObj);

            return nightlyCharge;
        }

        private decimal Calculate(BabysitterTime BTimeObj)
        {
            bool checkOutIsAfterMidnight = (BTimeObj.CheckOut.Hour < BTimeObj.CheckIn.Hour);

            int lastHourOfDay = 0;
            int hoursBeforeBedTime = 0;
            int hoursAfterBedTime = 0;
            int hoursAfterMidnight = 0;

            //Get the number of hours afte midnight, if any,
            //and set the lastHourOfDay, to simplify future math
            if (checkOutIsAfterMidnight)
            {
                hoursAfterMidnight = BTimeObj.CheckOut.Hour;
                lastHourOfDay = 24;
            }
            else
            {
                hoursAfterMidnight = 0;
                lastHourOfDay = BTimeObj.CheckOut.Hour;
            }

            //Determine the number of hours before and after bedtime
            if (BTimeObj.Bedtime >= startTime && BTimeObj.Bedtime.Hour <= lastHourOfDay)
            {
                hoursBeforeBedTime = BTimeObj.Bedtime.Hour - BTimeObj.CheckIn.Hour;
                hoursAfterBedTime = lastHourOfDay - BTimeObj.Bedtime.Hour;
            }
            else
            {
                hoursBeforeBedTime = lastHourOfDay - BTimeObj.CheckIn.Hour;
            }

            return (hoursBeforeBedTime * 12) + (hoursAfterBedTime * 8) + (hoursAfterMidnight * 16);
        }
    }
}
