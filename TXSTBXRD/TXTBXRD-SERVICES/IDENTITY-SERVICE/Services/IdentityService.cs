using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDENTITY_SERVICE.Models;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {    
        internal static Personal AuthenticationResult() {
            var d = new Personal();
            d.Login = "first login";
            d.Password = "asdasd";
            return d;
        }
    }
}