using System;
using MySql.Data.MySqlClient;
using DelegateMessage;

namespace DBConnecting
{
    public class Db
    {
        private readonly MySqlConnection _db;
        private readonly MySqlCommand _command;

        public event Message Error;
        public event Message Info;
        public event Message Success;

        public Db()
        {
            const string CONNECTION = "Server=mysql60.hostland.ru;Database=host1323541_vivt2;Uid=host1323541_vivt;Pwd=mhnqw7If;";
            _db = new MySqlConnection(CONNECTION);
            _command = new MySqlCommand();
            
            Info?.Invoke("Все необходимые классы проинициализированы");
        }

        public Db(string host, string dataBase, string user, string password)
        {
            var connection = $"Server={host}u;Database={dataBase};Uid={user};Pwd={password};";
            _db = new MySqlConnection(connection);
            _command = new MySqlCommand();
            Info?.Invoke("Все необходимые классы проинициализированы");
        }

        public void Open()
        {
            try
            {
                _db.Open();
                Success?.Invoke("БД открыта");
            }
            catch (MySqlException e)
            {
                Error?.Invoke(e.Message);
            }
        }

        public bool SearchUser(string user, string password)
        {
            var sql = $"SELECT login FROM table_account WHERE login = '{user}' AND password = '{password}';";
            _command.Connection = _db;
            _command.CommandText = sql;
            Info?.Invoke("Запрос в БД на поиск пользователя готов");

            try
            {
                var result = _command.ExecuteReader();
                return result.Read();
            }
            catch (Exception e)
            {
                Error?.Invoke(e.Message);
                throw;
            }
        }

        public bool CheckUser(string user)
        {
            var sql = $"SELECT login FROM table_account WHERE login = '{user}';";
            _command.Connection = _db;
            _command.CommandText = sql;
            Info?.Invoke("Запрос в БД на поиск пользователя готов");

            try
            {
                var result = _command.ExecuteReader();
                return result.Read();
            }
            catch (Exception e)
            {
                Error?.Invoke(e.Message);
                throw;
            }
        }

        public bool InputUser(string login, string password, string firstName, string lastName, string email)
        {
            var sql = $"INSERT INTO table_account (login, password) VALUES ('{login}', '{password}');";
            _command.Connection = _db;
            _command.CommandText = sql;
            
            Info?.Invoke("Запрос в БД на добавление пользователя готов");
            
            var result = _command.ExecuteNonQuery();

            if (result == 0)
            {
                Error?.Invoke("Ошибка записи в table_account");
                return false;
            }
            else
            {
                sql = $"INSERT INTO table_user (first_name, last_name, email) VALUES ('{firstName}', '{lastName}', '{email}');";
                _command.CommandText = sql;
                
                Info?.Invoke("Запрос в БД на добавление пользователя готов");

                var res = _command.ExecuteNonQuery();

                if (res == 0)
                {
                    Error?.Invoke("Ошибка записи в table_user");
                    return false;
                }
                else
                {
                    Success?.Invoke("Добавление новогопользователя прошло успешно");
                    return true;
                }
            }            
        }

        public void Close()
        {
            _db.Close();
            Info?.Invoke("БД закрыта");
        }
    }
}
