using MySqlConnector;
using System.Data;
using IDENTITY_SERVICE.Models;
using System;
using System.Threading.Tasks;


namespace IDENTITY_SERVICE.Services
{
    public class UsersDAO
    {
        internal UsersDbContext datebase { get; set; }
        public UsersDAO(UsersDbContext datebase)
        {
            this.datebase = datebase;
        }

        public async Task<string> authentication(LogIn unknownUser)
        {
            var result = "N";
            try
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

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result = reader.GetValue(0).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                await datebase.Connection.CloseAsync();
            }
            return result;
        }

        public async Task<string> getUserPermissions(int userId)
        {
            var result = "N";
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`getPermissions`(@userId);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@userId",
                    DbType = DbType.Int16,
                    Value = userId,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result = reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                await datebase.Connection.CloseAsync();
            }
            return result;
        }
    }
}