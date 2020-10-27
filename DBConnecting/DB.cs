﻿using System;
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

            MySqlDataReader result;
            try
            {
                result = _command.ExecuteReader();
                return result.Read();
            }
            catch (Exception e)
            {
                Error?.Invoke(e.Message);
                throw;
            }
        }

        public void Close()
        {
            _db.Close();
            Info?.Invoke("БД закрыта");
        }
    }
}