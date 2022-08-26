using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY_SERVICE.Models
{
    public class AuthorizationData
    {
        public string Salt { get; set; }

        public string Password { get; set; }
    }
}