using System;

namespace IDENTITY_SERVICE.Models
{
    public class VerificationPermission
    {
        public Guid authorizationToken { get; set; }
        public string Permission { get; set; }
    }
}