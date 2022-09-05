using MySqlConnector;
using System.Data;
using IDENTITY_SERVICE.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


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

        public async Task<string> getSalt(string LogIn)
        {
            var result = "N";
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`getSalt`(@LogIn);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@LogIn",
                    DbType = DbType.Int16,
                    Value = LogIn,
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

        public async Task<Dictionary<string, bool>> getUserPermissions(int userId)
        {
            var result = new Dictionary<string, bool>();
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
                        result.Add(reader.GetString(0), true);
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

        public async Task<bool> addUser(Registration newUser, string salt)
        {
            var result = false;
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`addUser`(@Login, @pasword, @name, @email, @salt);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@Login",
                    DbType = DbType.String,
                    Value = newUser.Login,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@pasword",
                    DbType = DbType.String,
                    Value = newUser.Password,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = newUser.UserName,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@email",
                    DbType = DbType.String,
                    Value = newUser.Email,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@salt",
                    DbType = DbType.String,
                    Value = salt,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (reader.GetValue(0).ToString() == "Y")
                            result = true;
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

        public async Task<bool> ChangePermission(string AdminName, string UserName, int RoleName)
        {
            var result = false;
             try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`changePermissions`(@Login, @Rolename);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@Login",
                    DbType = DbType.String,
                    Value = UserName,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@Rolename",
                    DbType = DbType.String,
                    Value = RoleName,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (reader.GetValue(0).ToString() == "Y")
                            result = true;
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