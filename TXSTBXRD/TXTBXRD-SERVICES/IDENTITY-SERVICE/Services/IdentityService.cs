using IDENTITY_SERVICE.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {
        private IMemoryCache cache;
        public IdentityService(IMemoryCache cache) => this.cache = cache;
        internal string Identification(Personal unknownUser)
        {
            cache.Set<string>("userId", "WAZAP!");
            return cache.Get<string>("userId");
        }
    }
}