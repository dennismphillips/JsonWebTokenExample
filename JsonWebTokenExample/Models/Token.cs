using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenExample.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "userToken")]
        public string userToken;
    }
}
