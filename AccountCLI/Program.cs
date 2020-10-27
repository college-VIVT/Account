using System;
using Logging;
using MySql.Data.MySqlClient;
using DBConnecting;

using static System.Console;

namespace AccountCLI
{
    class Program
    {
        static void Main()
        {
            var log = new LogToFile();
            log.ShowError += WriteLine;
            
            log.Info("Запуск программы");
            
            Write("Введите имя пользователя: ");
            var user = ReadLine();
            Write("Введите пароль пользователя: ");
            var password = ReadLine();
            
            log.Info("Ввод данных пользователя");
            
            var db = new DB();
            db.Error += WriteLine;
            db.Error += log.Error;
            db.Info += log.Info;
            db.Success += log.Success;

            db.Open();

            if (db.SearchUser(user, password))
            {
                WriteLine("Вход разрешён");
                log.Success("Вход разрешён");
            }
            else
            {
                WriteLine("Вход запрещён");
                log.Success("Вход запрещён");
            }
            
            db.Close();
        }
    }
}