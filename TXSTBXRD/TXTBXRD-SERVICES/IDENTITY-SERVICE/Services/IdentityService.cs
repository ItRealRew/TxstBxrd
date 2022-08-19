using IDENTITY_SERVICE.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {
        private IMemoryCache cache;

        private readonly UsersDAO dao;
        public IdentityService(IMemoryCache cache, UsersDAO dao){
           this.cache = cache;
           this.dao = dao; 
        }
        internal string Identification(Personal unknownUser)
        {
            cache.Set<string>("userId", "WAZAP!");
            return dao.authentication(unknownUser) ? "WAZAP" : "Account is Null!";
        }
    }
}