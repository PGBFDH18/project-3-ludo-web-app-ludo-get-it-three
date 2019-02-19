using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAppMVC.Models
{
    public class PlayerModelContainer
    {
        public Guid gameId { get; set; }
        public PlayerModel[] Players { get; set; }
    }
}