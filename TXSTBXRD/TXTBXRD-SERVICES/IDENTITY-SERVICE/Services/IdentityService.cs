using IDENTITY_SERVICE.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {
        private IMemoryCache cache;

        private readonly UsersDAO dao;
        public IdentityService(IMemoryCache cache, UsersDAO dao)
        {
            this.cache = cache;
            this.dao = dao;
        }
        internal async Task<Guid> Identification(LogIn unknownUser)
        {
            var userId = await dao.authentication(unknownUser);
            switch (userId)
            {
                case "N":
                    return Guid.Empty;
                default:
                    Guid authorizationToken = Guid.NewGuid();

                    var result = new Personally();
                    result.Permissions = await dao.getUserPermissions(Convert.ToInt16(userId));
                    result.UserName = unknownUser.Login;

                    cache.Set<Personally>(authorizationToken, result);

                    return authorizationToken;
            }
        }

        internal string Registration(Registration newUser)
        {
            return "it Work!";
        }
    }
}