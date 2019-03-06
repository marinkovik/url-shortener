using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;
using Newtonsoft.Json;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string s, int? placeholder)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // Encoded String because the query we receive from the site convert the plus sign into space
                var tmpEncodedString = Server.UrlEncode(s);
                var tmpStringWithoutPlus = s.Substring(0, s.Length - 1);
                if (tmpEncodedString[tmpEncodedString.Length-1].Equals('+'))
                {
                    var tmpFileContents = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"));
                    var tmpResult = JsonConvert.DeserializeObject<List<Urls>>(tmpFileContents);
                    foreach (var tmpUrl in tmpResult)
                    {
                        if (tmpUrl.shortString == tmpStringWithoutPlus)
                        {
                            var resultForSave = JsonConvert.SerializeObject(tmpResult);
                            return Content("This shorten link has " + tmpUrl.visits + " clicks and they are on: "+ string.Join(",", tmpUrl.visitsTime));
                        }
                    }
                    return Content("Sorry, wrong link");
                }
                // Adding visits time
                var fileContents = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"));
                var result = JsonConvert.DeserializeObject<List<Urls>>(fileContents);
                foreach (var tmpUrl in result)
                {
                    if (tmpUrl.shortString == s)
                    {
                        var tmpVisits = tmpUrl.visits + 1;
                        tmpUrl.visits = tmpVisits;
                        tmpUrl.visitsTime.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        var resultForSave = JsonConvert.SerializeObject(result);
                        System.IO.File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"), resultForSave);
                        return Redirect("http://" + tmpUrl.longString);
                    }
                }
                return Content("Sorry, wrong link");
            }
            else
                return View(new Urls());
        }

        [HttpPost]
        public ActionResult Index(string fieldForLongUrl)
        {
            System.Threading.Thread.Sleep(3000);
            var fileContents = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"));
            var result = JsonConvert.DeserializeObject<List<Urls>>(fileContents);
            foreach (var tmpUrl in result)
            {
                if (tmpUrl.longString == fieldForLongUrl)
                    return Content("Your short link is: http://localhost:49221/?s=" + tmpUrl.shortString);
            }
            return Content("Error");
        }
    }
}