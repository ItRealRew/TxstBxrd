using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PERSONALITY_SERVICE.Models;

namespace PERSONALITY_SERVICE.Services
{
    public class Personality
    {
        private readonly UserstxstbxrdContext database;

        public Personality(UserstxstbxrdContext database)
        {
            this.database = database;
        }

        public async Task<List<User>> GetAllUsers()
        {
            using (UserstxstbxrdContext db = new UserstxstbxrdContext())
            {
                return database.Users.ToList();
            }
        }
    }
}