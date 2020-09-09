using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLocalizationJson.Helper.Route
{
    public class RouteLinkDTO
    {
        public class Common
        {
            public string lang { get; set; }
        }

        public class Product : Common
        {

            public string val { get; set; }
            public int id { get; set; }
            public int productid { get; set; }
        }
    }
}
