using MySqlConnector;
using System.Data;
using IDENTITY_SERVICE.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TXSTBXRD_MIDDLEWARE.IDENTITY;


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

        public async Task<bool> addUser(Create newUser, string salt)
        {
            var result = false;
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`addUser`(@Login, @pasword, @name, @email, @salt, @secondname);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@Login",
                    DbType = DbType.String,
                    Value = newUser.Username,
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
                    Value = newUser.FirstName,
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

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@secondname",
                    DbType = DbType.String,
                    Value = newUser.LastName,
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

        public async Task<string> ChangePermission(string adminName, string login, string roleName)
        {
            string result = "false";
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`changePermissions`(@logword, @rolename);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@logword",
                    DbType = DbType.String,
                    Value = login,
                });

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@rolename",
                    DbType = DbType.String,
                    Value = roleName,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        switch (reader.GetValue(0).ToString())
                        {
                            case "D":
                                Console.WriteLine(adminName + " удалил права " + roleName + " у пользователя " + login);
                                result = "delete";
                                break;
                            case "A":
                                Console.WriteLine(adminName + " добавил права " + roleName + " пользователю " + login);
                                result = "added";
                                break;
                        }
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

        public async Task<UserDetails> GetDetailsByLogin(String userName)
        {
            UserDetails result = new UserDetails();

            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`getDetailsByUserName`(@name);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@name",
                    DbType = DbType.String,
                    Value = userName,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.FirstName = reader.GetValue(2).ToString();
                        result.Email = reader.GetValue(3).ToString();
                        result.LastName = reader.GetValue(4).ToString();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
            finally
            {
                await datebase.Connection.CloseAsync();
            }
        }

        public async Task<UserDetails> GetDetailsByUserId(int userId)
        {
            UserDetails result = new UserDetails();

            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`getUserDetails`(@id);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@id",
                    DbType = DbType.String,
                    Value = userId,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.FirstName = reader.GetValue(2).ToString();
                        result.Email = reader.GetValue(3).ToString();
                        result.LastName = reader.GetValue(4).ToString();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
            finally
            {
                await datebase.Connection.CloseAsync();
            }
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            try
            {
                datebase.Connection.Open();

                using var cmd = datebase.Connection.CreateCommand();
                cmd.CommandText = @"CALL `userstxstbxrd`.`getUserIdByEmail`(@mail);";

                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@mail",
                    DbType = DbType.String,
                    Value = email,
                });

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        return int.Parse(reader.GetValue(0).ToString());
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
            return -1;
        }
    }
}