using System;
using MySqlConnector;

namespace IDENTITY_SERVICE.Services
{
    public class UsersDbContext: IDisposable
    {
        public MySqlConnection Connection { get; }

        public UsersDbContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}