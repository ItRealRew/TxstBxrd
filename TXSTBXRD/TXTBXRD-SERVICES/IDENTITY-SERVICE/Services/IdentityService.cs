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

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                                 .SetSize(1)
                                    .SetPriority(CacheItemPriority.High)
                                    .SetSlidingExpiration(TimeSpan.FromHours(2))
                                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    cache.Set<Personally>(authorizationToken, result, cacheEntryOptions);

                    return authorizationToken;
            }
        }

        internal async Task<bool> Registration(Registration newUser) => await dao.addUser(newUser);

        internal bool Verification(VerificationPermission userPermission) =>
                                cache.TryGetValue(userPermission.authorizationToken, out Personally received) ?
                                    received.Permissions.ContainsKey(userPermission.Permission) : false;
    }
}