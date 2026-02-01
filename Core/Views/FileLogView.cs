using EducationalProject.Core.Interfaces;
using System.Text;

namespace EducationalProject.Core.Views
{
    public class FileLogView : IView
    {
        private readonly string _logFilePath;
        private readonly StringBuilder _logBuffer;
        private readonly object _lockObject = new object();

        public FileLogView(string logFilePath)
        {
            _logFilePath = logFilePath;
            _logBuffer = new StringBuilder();

            // Создаем директорию для лога, если её нет
            string directory = Path.GetDirectoryName(logFilePath);
            if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Инициализируем лог-файл
            InitializeLogFile();
        }

        private void InitializeLogFile()
        {
            lock(_lockObject)
            {
                try
                {
                    string header = $"=== Начало лога: {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===";
                    File.AppendAllText(_logFilePath, header + Environment.NewLine);
                }
                catch(Exception ex)
                {
                    // Если не можем записать в файл, выводим в консоль
                    Console.WriteLine($"Ошибка инициализации лог-файла: {ex.Message}");
                }
            }
        }

        public void ShowMessage(string message)
        {
            WriteToLog($"[INFO] {message}");
        }

        public void ShowError(string error)
        {
            WriteToLog($"[ERROR] {error}");
        }

        public string ReadInput(string prompt)
        {
            // Для лог-файла просто логируем запрос ввода
            WriteToLog($"[INPUT REQUEST] {prompt}");
            return string.Empty; // Файловое представление не получает ввод
        }

        public void Clear()
        {
            // Для лог-файла не очищаем содержимое, просто добавляем разделитель
            WriteToLog("=== Очистка экрана ===");
        }

        public void WaitForAnyKey()
        {
            WriteToLog("[WAIT] Ожидание нажатия клавиши...");
        }

        private void WriteToLog(string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string logMessage = $"{timestamp} - {message}";

            lock(_lockObject)
            {
                try
                {
                    // Добавляем в буфер
                    _logBuffer.AppendLine(logMessage);

                    // Пишем в файл (можно настроить периодическую запись)
                    File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
                }
                catch(Exception ex)
                {
                    // Если не можем записать в файл, пытаемся хотя бы в консоль
                    Console.WriteLine($"Ошибка записи в лог-файл: {ex.Message}");
                    Console.WriteLine($"Сообщение для лога: {logMessage}");
                }
            }
        }

        // Метод для получения содержимого лога
        public string GetLogContent()
        {
            lock(_lockObject)
            {
                try
                {
                    return File.ReadAllText(_logFilePath);
                }
                catch
                {
                    return "Не удалось прочитать лог-файл";
                }
            }
        }

        // Метод для очистки лог-файла
        public void ClearLogFile()
        {
            lock(_lockObject)
            {
                try
                {
                    File.WriteAllText(_logFilePath, string.Empty);
                    InitializeLogFile();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Ошибка очистки лог-файла: {ex.Message}");
                }
            }
        }
    }
}