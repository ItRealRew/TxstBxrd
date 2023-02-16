using TXSTBXRD_MIDDLEWARE.PERSONALITY;
using PERSONALITY_SERVICE.Models;
using Security;
using Security.Types;

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
            using (var transaction = database.Database.BeginTransaction())
            {
                try
                {
                    string salt = security.GetUniqueKey(((int)GenerationLength.Standart), Alphabet.Hard.Value);
                    newUser.Password = security.GetSecurePassword(newUser.Password, salt, (int)IterationSalt.Standart);


                    User user = new User { Login = newUser.Username, Password = newUser.Password };
                    database.Users.Add(user);
                    database.SaveChanges();

                    Detail detail = new Detail { UserId = user.Id, UserName = newUser.FirstName, Email = newUser.Email, LastName = newUser.LastName };
                    Salt _salt = new Salt { UserId = user.Id, Value = salt };
                    
                    user.Permissions.Add(database.Permissions.Where(c => c.Id == 2).First<Permission>());

                    database.Details.Add(detail);
                    database.Salts.Add(_salt);

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