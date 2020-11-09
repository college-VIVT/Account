using System;
using System.IO;
using System.Security;
using DelegateMessage;

namespace Logging
{
    public class LogToFile
    {
        private readonly string _path;

        public event Message ShowError;

        public LogToFile()
        {
            _path = "account_event.log";
        }

        public LogToFile(string path)
        {
            _path = path;
        }

        private void WriteToFile(string message)
        {
            try
            {
                using var file = new StreamWriter(_path, true);
                try
                {
                    file.WriteLineAsync(message);
                }
                catch (ObjectDisposedException)
                {
                    ShowError?.Invoke("Удалено средство записи потока");
                }
                catch (InvalidOperationException)
                {
                    ShowError?.Invoke("Средство записи потока в настоящее время используется предыдущей операцией записи");
                }
                catch (Exception e)
                {
                    ShowError?.Invoke(e.Message);
                }
            }
            catch (ArgumentException)
            {
                ShowError?.Invoke("Параметр пути к файлу пуст или содержит имя системного устройства (com1, com2 и т. д.)");
            }
            catch (DirectoryNotFoundException)
            {
                ShowError?.Invoke("Указанный путь недопустим (например, он соответствует неподключенному диску)");
            }
            catch (PathTooLongException)
            {
                ShowError?.Invoke("Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе");
            }
            catch (IOException)
            {
                ShowError?.Invoke("Параметр пути к файлу включает неверный или недопустимый синтаксис имени файла, имени каталога или метки тома");
            }
            catch (SecurityException)
            {
                ShowError?.Invoke("У вызывающего объекта отсутствует необходимое разрешение");
            }
            catch (Exception e)
            {
                ShowError?.Invoke(e.Message);
            }
        }

        public void Info(string message)
        {
            var msg = $"{DateTime.Now:G} INFO : {message}";
            WriteToFile(msg);
        }
        public void Success(string message)
        {
            var msg = $"{DateTime.Now:G} SUCCESS : {message}";
            WriteToFile(msg);
        }
        public void Error(string message)
        {
            var msg = $"{DateTime.Now:G} ERROR : {message}";
            WriteToFile(msg);
        }
    }
}