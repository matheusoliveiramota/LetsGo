using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.Web.API.Models.Requests
{
    public class PostStateRequest
    {
        public string Token { get; set; }
        public short IdPinButton { get; set; }
        public short IsAvailable { get; set; }
    }
}
