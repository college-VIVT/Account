using System;
using MySql.Data.MySqlClient;
using DelegateMessage;

namespace DBConnecting
{
    public class DB
    {
        private readonly string _connection;
        private readonly MySqlConnection _db;
        private MySqlCommand _command;

        public event Message Error;

        public DB()
        {
            _connection = "Server=mysql60.hostland.ru;Database=host1323541_vivt2;Uid=host1323541_vivt;Pwd=mhnqw7If;";
            _db = new MySqlConnection(_connection);
            _command = new MySqlCommand();
        }

        public DB(string host, string data_base, string user, string password)
        {
            _connection = $"Server={host}u;Database={data_base};Uid={user};Pwd={password};";
            _db = new MySqlConnection(_connection);
            _command = new MySqlCommand();
        }

        public void Open()
        {
            try
            {
                _db.Open();
            }
            catch (MySqlException e)
            {
                Error?.Invoke(e.Message);
            }
        }

        public bool SearchUser(string user, string password)
        {
            var sql = $"SELECT login FROM table_account WHERE login = '{user}' AND password = '{password}';";
            if (_db != null) _command.Connection = _db;
            _command.CommandText = sql;

            var result = _command.ExecuteReader();
            return result.Read();
        }

        public void Close()
        {
            _db.Close();
        }
    }
}