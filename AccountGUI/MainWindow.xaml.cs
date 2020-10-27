using System.Windows;
using Logging;
using DBConnecting;

namespace AccountGUI
{
    public partial class MainWindow
    {
        private readonly LogToFile _log = new LogToFile();
        private readonly Db _db = new Db();
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
            InputUser.Text = string.Empty;
            InputPassword.Text = string.Empty;
        }

        private void Button_LogIn_OnClick(object sender, RoutedEventArgs e)
        {
            var user = InputUser.Text;
            var password = InputPassword.Text;
            
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
