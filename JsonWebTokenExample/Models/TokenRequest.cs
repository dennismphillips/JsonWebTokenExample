using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenExample.Models
{
    public class TokenRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
