using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;
using System.Data.Entity;
using System.Collections;
using System.Web.Http;
using Newtonsoft.Json;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace UrlShortener.Controllers.Api
{
    public class UrlController : ApiController
    {
        // GET api/url
        public IEnumerable<Urls> Get()
        {
            var fileContents = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"));
            var result = JsonConvert.DeserializeObject<List<Urls>>(fileContents);
            return result;
        }

        // POST api/url
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            var fileContents = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"));
            var result = JsonConvert.DeserializeObject<List<Urls>>(fileContents);
            // result.Add(new Urls { longString = value, shortString = result.Count.ToString() });
            result.Add(new Urls { longString = value, id = result.Count, shortString = ToBase26(result.Count), visits = 0, visitsTime = new List<string>()});
            var resultForSave = JsonConvert.SerializeObject(result);
            System.IO.File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"~/JsonFiles/Urls.json"), resultForSave);
            return new HttpStatusCodeResult(200);
        }

        private static string ToBase26(int number)
        {
            var list = new LinkedList<int>();
            list.AddFirst((number - 1) % 26);
            while ((number = --number / 26 - 1) > 0)
            {
                list.AddFirst(number % 26);
            }
            return new string(list.Select(s => (char)(s + 65)).ToArray());
        }
    }
}