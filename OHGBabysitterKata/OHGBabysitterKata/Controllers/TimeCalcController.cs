using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OHGBabysitterKata.Models;

namespace OHGBabysitterKata.Controllers
{
    public class TimeCalcController : ApiController
    {
        readonly DateTime startTime = DateTime.Today.AddHours(17);//5PM
        readonly DateTime endTime = DateTime.Today.AddHours(4);//4AM
        readonly DateTime midnight = DateTime.Today;

        public decimal GetNightlyCharge(BabysitterTime BTimeObj)
        {
            decimal nightlyCharge = 0.0M;
            
            if (BTimeObj.CheckIn < DateTime.Today || BTimeObj.CheckOut < DateTime.Today)
            {
                //One of the times were likely not entered
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                
            }

            return nightlyCharge;
        }

        
    }
}
