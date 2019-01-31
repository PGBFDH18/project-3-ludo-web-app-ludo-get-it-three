using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApp
{
    public class Call
    {
        public string Sid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CallSegmentSid { get; set; }
        public string AccountSid { get; set; }
        public string Called { get; set; }
        public string Caller { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}