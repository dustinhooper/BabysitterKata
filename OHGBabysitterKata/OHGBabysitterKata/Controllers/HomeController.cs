using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OHGBabysitterKata.Models;
using Newtonsoft.Json;

namespace OHGBabysitterKata.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl;
        HttpClient client;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Index(BabysitterTime BtObj)
        {
            apiUrl = Request.Url.ToString() + "api/TimeCalc";
            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, BtObj);

            string responseData = response.Content.ReadAsStringAsync().Result;

            decimal chargeTotal = JsonConvert.DeserializeObject<decimal>(responseData);

            if (response.IsSuccessStatusCode)
            {

                ViewBag.ErrorMsg = "";
            }
            else
            {
                ViewBag.ErrorMsg = "Could not calculate.";
            }

            return View(BtObj);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}