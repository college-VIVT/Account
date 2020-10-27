using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Logging;
using DBConnecting;
using AccountModelData;

namespace AccountGUI
{
    public partial class MainWindow : Window
    {
        private LogToFile _log = new LogToFile();
        private DB _db = new DB();
        public MainWindow()
        {
            _db.Info += _log.Info;
            _db.Success += _log.Success;
            _db.Error += _log.Error;
            
            InitializeComponent();
            
            Loaded += (sender, args) =>
            {
                _log.Info("Приложение запустилось");
            };
            Closing += (sender, args) => _log.Info("Закрытие приложения");
            Closed += (sender, args) =>
            {
                _log.Info("Приложение закрылось");
            };
        }

        private void Button_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            _log.Info("Очистка полей ввода текста");
            input_User.Text = string.Empty;
            input_Password.Text = string.Empty;
        }

        private void Button_LogIn_OnClick(object sender, RoutedEventArgs e)
        {
            var user = input_User.Text;
            var password = input_Password.Text;
            
            _db.Open();

            if (_db.SearchUser(user, password))
            {
                _log.Success("Вход разрешён");
                MessageBox.Show("Вход разрешён", "Account", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _log.Error("Вход запрещён");
                MessageBox.Show("Вход запрещён!", "Account", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            
            _db.Close();
        }
    }
}
