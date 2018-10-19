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
        public decimal GetNightlyCharge(BabysitterTime BTimeObj)
        {
            return 0.0M;
        }
    }
}
