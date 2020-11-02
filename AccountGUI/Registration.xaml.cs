using System.Windows;
using System.Windows.Controls;
using Logging;
using DBConnecting;

namespace AccountGUI
{
    public partial class Registration
    {
        private readonly LogToFile _log = new LogToFile();
        private readonly Db _db = new Db();

        private bool isTextUser = false;
        private bool isTextPassword = false;
        private bool isTextFirstName = false;
        private bool isTextLastName = false;
        private bool isTextEmail = false;
        
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonRegistration_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO Проверка пароля на соответствие требованиям
            //TODO Проверка логина в БД
            
            _db.Open();
            _db.InputUser("user_test", "1", "A", "S", "e");

        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            _log.Info("Очистка полей ввода текста окна регистрации");
            InputUser.Text = string.Empty;
            InputPassword.Text = string.Empty;
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть?", "Account", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void EnableButton()
        {
            if (isTextUser && isTextPassword && isTextFirstName && isTextLastName && isTextEmail)
            {
                ButtonRegistration.IsEnabled = true;
            }
            else
            {
                ButtonRegistration.IsEnabled = false;
            }
        }

        private void InputUser_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            isTextUser = InputUser.Text != "";

            EnableButton();
        }

        private void InputPassword_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            isTextPassword = InputPassword.Text != "";
            
            EnableButton();
        }

        private void InputFirstName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            isTextFirstName = InputFirstName.Text != "";
            
            EnableButton();
        }
        
        private void InputLastName_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            isTextLastName = InputLastName.Text != "";
            
            EnableButton();
        }

        private void InputEmail_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            isTextEmail = InputEmail.Text != "";
            
            EnableButton();
        }
    }
}
