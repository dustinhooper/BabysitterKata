using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHGBabysitterKata.Models
{
    public class BabysitterTime
    {
        public BabysitterTime()
        {
            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today;
        }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime Bedtime { get; set; }
    }
}