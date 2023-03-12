using TXSTBXRD_MIDDLEWARE.PERSONALITY;
using PERSONALITY_SERVICE.Models;
using TXSTBXRD_LIBS.Security;
using TXSTBXRD_LIBS.Security.Types;

namespace PERSONALITY_SERVICE.Services
{
    public class Personality
    {
        private readonly UserstxstbxrdContext database;

        private readonly CriptoSevice security;

        public Personality(UserstxstbxrdContext database, CriptoSevice security)
        {
            this.database = database;
            this.security = security;
        }

        public async Task<bool> AddNewUser(Registration newUser)
        {
            if (newUser == null || newUser.Password == null || newUser.Username == null)
                return false;

            if (database.Users!.Where(c => c.Login == newUser.Username).Any())
                return false;

            using (var transaction = database.Database.BeginTransaction())
            {
                try
                {
                    string _salt = security.GetUniqueKey(((int)GenerationLength.Standart), Alphabet.Hard.Value);
                    newUser.Password = security.GetSecurePassword(newUser.Password, _salt, (int)IterationSalt.Standart);


                    User user = new User { Login = newUser.Username, Password = newUser.Password, Enabled = false };
                    database.Users!.Add(user);
                    database.SaveChanges();

                    Detail detail = new Detail { UserId = user.Id, UserName = newUser.FirstName, Email = newUser.Email, LastName = newUser.LastName };
                    Salt salt = new Salt { UserId = user.Id, Value = _salt };

                    user.Permissions.Add(database.Permissions!.Where(c => c.Name == "Users").First<Permission>());

                    database.Details!.Add(detail);
                    database.Salts!.Add(salt);

                    database.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }
    }
}