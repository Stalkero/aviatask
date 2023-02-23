using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviatask.LogBook
{
    internal class Classes
    {
        public class flightHistory
        {
            public string jobID {get;set;}
            public string jobName { get;set;}
            public string jobDescription { get;set;}
            public string jobType { get;set;}
            public string weight { get;set;}
            public string time { get;set;}
            public string startICAO { get;set;}
            public string endICAO { get;set;}
            public double distance { get;set;}

        }

    }
}
