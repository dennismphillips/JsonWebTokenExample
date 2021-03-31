using JsonWebTokenExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenExample.Logic
{
    public interface IJsonWebTokenLogic
    {
        public string GenerateToken(string userName, string password);

        public bool ValidateToken(string token);
    }
}
