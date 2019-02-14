using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVC.Models
{
    public class ClientInfo
    {
        public string Name { get; set; }
        public Guid gameId = new Guid();
    }
}
