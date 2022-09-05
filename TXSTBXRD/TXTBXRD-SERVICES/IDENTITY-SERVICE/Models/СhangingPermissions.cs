using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY_SERVICE.Models
{
    public class СhangingPermissions
    {
        public Guid Admin { get; set; }
        public string UserName { get; set; }
        public int RoleName { get; set; }
    }
}