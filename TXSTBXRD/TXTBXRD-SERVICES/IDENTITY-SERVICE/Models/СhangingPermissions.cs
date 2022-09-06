using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY_SERVICE.Models
{
    public class СhangingPermissions
    {
        public Guid authorizationToken { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}