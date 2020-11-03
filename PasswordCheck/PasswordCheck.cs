using System;
using DelegateMessage;

namespace PasswordCheck
{
    public class PasswordCheck
    {
        private readonly int _minLength;

        private readonly string _setSymbols1;
        private readonly string _setSymbols2;
        private readonly string _setSymbols3;
        private readonly string _setSymbols4;
        
        private readonly string _setSymbolsAll;

        public event Message Warning;
        public event Message Success; 
        
        public PasswordCheck()
        {
            _minLength = 8;
            
            _setSymbols1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            _setSymbols2 = "abcdefghijklmnopqrstuvwxyz";
            _setSymbols3 = "0123456789";
            _setSymbols4 = "!@#$%^&*,.:;";
            
            _setSymbolsAll = _setSymbols1 + _setSymbols2 + _setSymbols3 + _setSymbols4;
        }
        public PasswordCheck(int minLength, string setSymbols1, string setSymbols2, string setSymbols3, string setSymbols4)
        {
            _minLength = minLength;
            
            _setSymbols1 = setSymbols1;
            _setSymbols2 = setSymbols2;
            _setSymbols3 = setSymbols3;
            _setSymbols4 = setSymbols4;

            _setSymbolsAll = _setSymbols1 + _setSymbols2 + _setSymbols3 + _setSymbols4;
        }

        public bool CheckSymbol(string password)
        {
            var passwordCheck1 = password.IndexOfAny(_setSymbols1.ToCharArray());
            var passwordCheck2 = password.IndexOfAny(_setSymbols2.ToCharArray());
            var passwordCheck3 = password.IndexOfAny(_setSymbols3.ToCharArray());
            var passwordCheck4 = password.IndexOfAny(_setSymbols4.ToCharArray());

            if (passwordCheck1 == -1 || passwordCheck2 == -1 || passwordCheck3 == -1 || passwordCheck4 == -1)
            {
                Warning?.Invoke("Пароль не соответствует минимальным требованиям");
                return false;
            }
            else
            {
                Success?.Invoke("Пароль соответствует минимальным требованиям");
                return true;
            }
        }

        public bool CheckLength(string password)
        {
            if (password.Length >= _minLength)
            {
                Success?.Invoke("Длина пароля больше либо равно минимальному количеству символов");
                return true;
            }
            else
            {
                Warning?.Invoke("Длина пароля меньше минимального количества символов");
                return false;
            }
        }

        public bool CheckAlphabet(string password)
        {
            foreach (var symbol in password)
            {
                if (_setSymbolsAll.Contains(symbol)) continue;
                Warning?.Invoke("Пароль содержит запрещённые символы");
                return false;
            }
            Success?.Invoke("Пароль не содержит запрещённых символов");
            return true;
        }
    }
}