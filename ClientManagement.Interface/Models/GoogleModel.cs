using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rubix.UI.Models
{
    public class CurrentResults
    {
        public GoogleModel[] currentResults { get; set; }
    }
    public class GoogleModel
    {
        public Address_Component[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }
    public class Address_Component
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }


    public class Geometry
    {
        public Boundry bounds { get; set; }
        public CoordinatesF location { get; set; }
        public string location_type { get; set; }
        public Boundry viewport { get; set; }

    }

    public class CoordinatesA
    {
        public string j { get; set; }
        public string a { get; set; }
        public override string ToString()
        {
            return string.Format("{0},{1}", j, a);
        }
    }

    public class CoordinatesF
    {
        public string lng { get; set; }
        public string lat { get; set; }

        public string G { get; set; }
        public string K { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", GetLatitude(), GetLongitude());
        }

        public string GetLongitude()
        {
            return string.IsNullOrEmpty(lng) ? K : lng;
        }

        public string GetLatitude()
        {
            return string.IsNullOrEmpty(lat) ? G : lat;
        }
    }

    public class Boundry
    {
        public CoordinatesA Ra { get; set; }
        public CoordinatesA za { get; set; }
    }
}