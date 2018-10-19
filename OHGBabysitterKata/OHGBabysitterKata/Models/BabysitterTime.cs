using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHGBabysitterKata.Models
{
    public class BabysitterTime
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime Bedtime { get; set; }
    }
}