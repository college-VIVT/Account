using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Logging;
using DBConnecting;
using PasswordCheck = PasswordCheck.PasswordCheck;

namespace AccountGUI
{
    public partial class Registration
    {
        private readonly LogToFile _log = new LogToFile();
        private readonly Db _db = new Db();

        private bool _isTextUser = false;
        private bool _isTextPassword = false;
        private bool _isTextFirstName = false;
        private bool _isTextLastName = false;
        private bool _isTextEmail = false;
        private bool _isCheckPassword = false;
        
        public Registration()
        {
            _db.Info += _log.Info;
            _db.Success += _log.Success;
            _db.Error += _log.Error;
            
            InitializeComponent();
            
            Loaded += (sender, args) =>
            {
                _log.Info("Окно регистрации запустилось");
            };
            Closing += (sender, args) => _log.Info("Закрытие окна регистрации");
            Closed += (sender, args) =>
            {
                _log.Info("Закрытие окна регистрации");
            };
        }

        private void ButtonRegistration_OnClick(object sender, RoutedEventArgs e)
        {
            var user = InputUser.Text;
            var password = InputPassword.Text;
            var firstName = InputFirstName.Text;
            var lastName = InputLastName.Text;
            var email = InputEmail.Text;
            
            _db.Open();
            var searchUser = _db.CheckUser(user);
            _db.Close();
            
            if (searchUser)
            {
                MessageBox.Show($"Пользователь {user} уже существует!", "Account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _db.Open();
                var isInputUser = _db.InputUser(user, password, firstName, lastName, email);
                _db.Close();
                if (isInputUser)
                {
                    _log.Success("Пользователь успешно зарегистрирован");
                    MessageBox.Show("Пользователь успешно зарегистрирован", "Account", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _log.Error("Пользователь не зарегистрирован");
                    MessageBox.Show("Пользователь не зарегистрирован", "Account", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            if (_isTextUser && _isTextPassword && _isTextFirstName && _isTextLastName && _isTextEmail && _isCheckPassword)
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
            _isTextUser = InputUser.Text != "";

            EnableButton();
        }

        private void InputPassword_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputPassword.Text == "")
            {
                _isCheckPassword = false;
                _isTextPassword = false;
            }
            else
            {
                _isTextPassword = true;
                var password = InputPassword.Text;
                if (IsCheckLengt(password) && IsCheckSymbol(password) && IsCheckAlphabet(password))
                {
                    _isCheckPassword = true;
                }
                else
                {
                    _isCheckPassword = false;
                }
            }
            
            EnableButton();
        }

        private void InputFirstName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextFirstName = InputFirstName.Text != "";
            
            EnableButton();
        }
        
        private void InputLastName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextLastName = InputLastName.Text != "";
            
            EnableButton();
        }

        private void InputEmail_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextEmail = InputEmail.Text != "";
            
            EnableButton();
        }

        private bool IsCheckLengt(string password)
        {
            var passwordCheck = new global::PasswordCheck.PasswordCheck();

            if (passwordCheck.CheckLength(password))
            {
                LabelPasswordCheckLength.Foreground = Brushes.Green;
                LabelPasswordCheckLength.Text = "Длина пароля подходящая";
                return true;
            }
            else
            {
                LabelPasswordCheckLength.Foreground = Brushes.Red;
                LabelPasswordCheckLength.Text = "Длина пароля меньше требуемой";
                return false;
            }
        }
        
        private bool IsCheckSymbol(string password)
        {
            var passwordCheck = new global::PasswordCheck.PasswordCheck();

            if (passwordCheck.CheckSymbol(password))
            {
                LabelPasswordCheckSymbol.Foreground = Brushes.Green;
                LabelPasswordCheckSymbol.Text = "Пароль соответствует минимальным требованиям";
                return true;
            }
            else
            {
                LabelPasswordCheckSymbol.Foreground = Brushes.Red;
                LabelPasswordCheckSymbol.Text = "Пароль не соответствует минимальным требованиям";
                return false;
            }
        }
        private bool IsCheckAlphabet(string password)
        {
            var passwordCheck = new global::PasswordCheck.PasswordCheck();

            if (passwordCheck.CheckAlphabet(password))
            {
                LabelPasswordCheckAlphabet.Foreground = Brushes.Green;
                LabelPasswordCheckAlphabet.Text = "Пароль содержит только допустимые символы";
                return true;
            }
            else
            {
                LabelPasswordCheckAlphabet.Foreground = Brushes.Red;
                LabelPasswordCheckAlphabet.Text = "Пароль содержит недопустимые символы";
                return false;
            }
        }
    }
}
