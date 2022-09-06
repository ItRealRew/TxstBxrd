using IDENTITY_SERVICE.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using IDENTITY_SERVICE.Types;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {
        private IMemoryCache cache;

        private readonly UsersDAO dao;

        private readonly SecurityService security;
        public IdentityService(IMemoryCache cache, UsersDAO dao, SecurityService security)
        {
            this.cache = cache;
            this.dao = dao;
            this.security = security;
        }
        internal async Task<Guid> Identification(LogIn unknownUser)
        {
            var salt = await dao.getSalt(unknownUser.Password);
            unknownUser.Password = security.GetSecurePassword(unknownUser.Password, salt, (int)IterationSalt.Standart);

            string userId = await dao.authentication(unknownUser);
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

        internal async Task<bool> Registration(Registration newUser)
        {
            string salt = security.GetUniqueKey(((int)GenerationLength.Standart), Alphabet.Hard.Value);
            newUser.Password = security.GetSecurePassword(newUser.Password, salt, (int)IterationSalt.Standart);
            return await dao.addUser(newUser, salt);
        }

        internal bool Verification(VerificationPermission userPermission) =>
                                cache.TryGetValue(userPermission.authorizationToken, out Personally received) ?
                                    received.Permissions.ContainsKey(userPermission.Permission) : false;

        internal async Task<string> ChangePermission(СhangingPermissions user) => cache.TryGetValue(user.authorizationToken, out Personally received) ?
                                await dao.ChangePermission(received.UserName,user.UserName,user.RoleName) : "false";
    }
}