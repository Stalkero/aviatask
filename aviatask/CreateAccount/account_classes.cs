using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviatask.CreateAccount
{
    internal class account_classes
    {
        public struct PilotDetails
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Country { get; set; }
            public string Type { get; set; }
            public string ICAO { get; set; }

            public float LatDec { get; set; }
            public float LongDec { get; set; }
        }
    }
}
