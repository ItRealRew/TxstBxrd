using IDENTITY_SERVICE.Models;
using TXSTBXRD_MIDDLEWARE.IDENTITY;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Security.Types;
using Security;

namespace IDENTITY_SERVICE.Services
{
    public class IdentityService
    {
        private IMemoryCache cache;

        private readonly UsersDAO dao;

        private readonly CriptoSevice security;
        public IdentityService(IMemoryCache cache, UsersDAO dao, CriptoSevice security)
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
                    result.Login = unknownUser.Login;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                                 .SetSize(1)
                                    .SetPriority(CacheItemPriority.High)
                                    .SetSlidingExpiration(TimeSpan.FromHours(2))
                                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));

                    cache.Set<Personally>(authorizationToken, result, cacheEntryOptions);

                    return authorizationToken;
            }
        }

        internal async Task<bool> Registration(Create newUser)
        {
            string salt = security.GetUniqueKey(((int)GenerationLength.Standart), Alphabet.Hard.Value);
            newUser.Password = security.GetSecurePassword(newUser.Password, salt, (int)IterationSalt.Standart);
            return await dao.addUser(newUser, salt);
        }

        internal bool loginOut(LogOut loginOut)
        {
            if (cache.TryGetValue(loginOut.authorizationToken, out Personally received))
            {
                cache.Remove(loginOut.authorizationToken);
                return true;
            }
            return false;
        }

        internal bool Verification(VerificationPermission userPermission) =>
                                cache.TryGetValue(userPermission.authorizationToken, out Personally received) ?
                                    received.Permissions.ContainsKey(userPermission.Permission) : false;

        internal string FindLoginByToken(Guid authorizationToken) =>  cache.TryGetValue(authorizationToken, out Personally received) ?
                                    received.Login: "false";

        internal async Task<string> ChangePermission(СhangingPermissions user) => cache.TryGetValue(user.authorizationToken, out Personally received) ?
                                await dao.ChangePermission(received.Login, user.UserName, user.RoleName) : "false";

        internal async Task<UserDetails> GetUserDetailsByEmail(string email) => await dao.GetDetailsByUserId(await dao.GetUserIdByEmail(email));
        internal async Task<UserDetails> GetUserDetailsByToken(Guid userToken) => await dao.GetDetailsByLogin(FindLoginByToken(userToken));
    }
}