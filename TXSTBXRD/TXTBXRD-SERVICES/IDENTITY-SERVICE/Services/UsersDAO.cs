using MySqlConnector;
using System.Data;
using IDENTITY_SERVICE.Models;

namespace IDENTITY_SERVICE.Services
{
    public class UsersDAO
    {
        internal UsersDbContext datebase { get; set; }
        public UsersDAO(UsersDbContext datebase)
        {
            this.datebase = datebase;
        }

        public bool authentication(Personal unknownUser)
        {
            datebase.Connection.Open();

            using var cmd = datebase.Connection.CreateCommand();
            cmd.CommandText = @"CALL `userstxstbxrd`.`authentication`(@login, @password);";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@login",
                DbType = DbType.String,
                Value = unknownUser.Login,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = unknownUser.Password,
            });

            string result = "";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader.GetString(0);
                }
            }

            datebase.Connection.Close();
            
            if (result == "Y")
                return true;
            else
                return false;
        }
    }
}