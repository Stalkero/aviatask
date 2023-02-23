using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviatask.JobGen
{
    internal class classes
    {
        public class job_info
        {
            public string id { get; set; }
            public string job_name { get; set; }
            public double job_distance { get; set; }
            public string start_ICAO { get; set; }
            public string end_ICAO { get; set; }
            public int weight { get; set; }
            public string type { get; set; }
            public string description { get; set; }
            public double startLat { get; set; }
            public double startLon { get; set; }
            public double endLat { get; set; }
            public double endLon { get; set; }
        }
    }
}
