using System;

namespace IDENTITY_SERVICE.Models
{
    public class СhangingPermissions
    {
        public Guid authorizationToken { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}