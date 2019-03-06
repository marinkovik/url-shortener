using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class Urls
    {
        public string longString { get; set; }
        public string shortString { get; set; }
        public int id { get; set; }
        public int visits { get; set; }
        public List<string> visitsTime { get; set; }
    }
}