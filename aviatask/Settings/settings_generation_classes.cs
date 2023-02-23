using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviatask.Settings
{
    internal class settings_generation_classes
    {
        public class quick_job_generation
        {
            public double maxDistance { get; set; }
            public int AirportPeopleIterations { get; set; }
            public int CargoJobGenIterations { get; set; }
            
            public int CargoJobHelicopterLoadCount { get; set; }
            public int CargoJobPlaneLoadCount { get; set; }
        }
    }
}
