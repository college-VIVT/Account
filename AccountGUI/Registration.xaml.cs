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
        private bool isTextName = false;
        private bool isTextEmail = false;
        
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonRegistration_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO Отправка запроса на добавление данных в БД
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
            if (isTextUser && isTextPassword && isTextName && isTextEmail)
            {
                ButtonRegistration.IsEnabled = true;
            }
        }

        private void InputUser_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputUser.Text != "")
            {
                isTextUser = true;
            }

            EnableButton();
        }

        private void InputPassword_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputPassword.Text != "")
            {
                isTextPassword = true;
            }
            
            EnableButton();
        }

        private void InputName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputName.Text != "")
            {
                isTextName = true;
            }
            
            EnableButton();
        }

        private void InputEmail_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputEmail.Text != "")
            {
                isTextEmail = true;
            }
            
            EnableButton();
        }
    }
}
