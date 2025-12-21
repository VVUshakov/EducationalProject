namespace EducationalProject
{
    // Менеджер меню для управления программами
    public class MenuManager
    {
        #region ===== СВОЙСТВА И КОНСТРУКТОР =====

        // Массив всех доступных программ (вместо List<IProgram>)
        private BaseService[] programs;
        // Значение, соответствующее завершению программы
        const int EXIT = 0;

        // Конструктор класса - без бизнес-логики
        public MenuManager()
        {
            // Инициализация массива программ
            programs = new BaseService[0];
        }

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        // Инициализация программ (вынесена из конструктора)
        public void Initialize()
        {
            // Программы берутся из конфигурационного файла
            // Добавлять/убирать программы можно в ProgramsConfig.cs
            programs = ProgramsConfig.GetAllPrograms();

            ShowCountDownloadedPrograms(programs.Length); // Показать количество загруженных программ
            WaitForKey(); // Ждать нажатия любой клавиши
            Console.Clear(); // Очистить консоль
        }

        // Запустить выбранную программу
        private void RunProgram(int number)
        {
            Console.Clear(); // Очистить консоль
            ShowMessageStartingProgram(number); // Показать сообщение о запуске конкретной программы
            ChangeFontTo(40); // Изменить шрифт текста

            programs[number - 1].Run(); // Запустить выбранную программу
        }

        // Главный цикл программы
        public void Run()
        {
            ShowWelcome(); // Показать приветствие
            Initialize(); // Инициализировать список программ

            // Бесконечный цикл, пока не выберут выход
            while(true)
            {
                ShowMenu(); // Показать меню

                // Получить ввод от пользователя
                int userChoice = GetUserChoice();

                // Проверить выбор "0" - выход
                if(userChoice == EXIT)
                {
                    ShowFinalMessage(); // Показать прощальное сообщение
                    break; // Прервать (полностью выходит из ближайшего цикла или switch)
                }

                RunProgram(userChoice); // Запустить выбранную программу
                WaitForKey(); // Ждать нажатия любой клавиши

                Console.Clear(); // Очистить консоль
            }
        }

        #endregion

        #region ===== МЕТОД ПОЛУЧЕНИЯ ВВОДА (ПРОСТОЙ) =====

        // Получить ввод пользователя
        private int GetUserChoice()
        {
            while(true) // Повторять пока не получим правильный ввод
            {
                /* continue (продолжать) - 
                 * пропускает только текущую итерацию цикла,
                 * переходя к следующей итерации
                 * (проверяя условие и выполняя остаток кода итерации)
                */

                Console.Write(">>> Введите номер программы: ");
                string userInput = Console.ReadLine(); // Получить ввод с консоли
                int programNumber; // Переменная для хранения распознанного числа

                // Если введенные данные валидны...
                if(CheckUserInput(userInput, out programNumber))
                {
                    userInput = userInput.Trim(); // Убрать пробелы в конце и в начале (если имеются)
                    programNumber = Convert.ToInt32(userInput); // Конвертируем в число
                    return programNumber; // Возвращаем выбранный номер программы
                }
                else // .. иначе показать сообщение об ошибке...
                {
                    // Определяем, какая именно ошибка
                    if(IsEmpty(userInput))
                    {
                        ShowErrorMessage("Вы ничего не ввели!");
                    }
                    else if(!IsNumber(userInput, out _))
                    {
                        ShowErrorMessage($"'{userInput}' - это не число!\n" +
                                   "Пожалуйста, введите номер программы цифрами");
                    }
                    else if(!IsNumberInRange(programNumber))
                    {
                        ShowErrorMessage($"Нет программы № {programNumber}!\n" +
                                   $"Доступны программы от {EXIT} до {programs.Length}");
                    }

                    WaitForKey(); // Ждать нажатия любой клавиши
                    Console.Clear(); // Очистить консоль
                    ShowMenu(); // Показать меню
                    continue; // Начинаем цикл заново
                }
            }
        }

        #endregion

        #region ===== МЕТОД ПОЛУЧЕНИЯ ВВОДА (через ENUM) =====

        // Объявляем перечисление (enum) для типов ошибок валидации
        // Это как список возможных проблем с вводом пользователя
        private enum ValidationError
        {
            None,       // Нет ошибки - всё в порядке
            Empty,      // Ошибка: пустой ввод (ничего не ввели)
            NotNumber,  // Ошибка: ввели не число
            OutOfRange  // Ошибка: число вне допустимого диапазона
        }

        // Метод для получения типа ошибки валидации
        // Возвращает код ошибки И распознанное число, если удалось (через out-параметр)
        private ValidationError GetValidationError(string input, out int number)
        {
            // Устанавливаем значение по умолчанию для number
            number = EXIT;

            // Проверка 1: Если ввод пустой
            if(IsEmpty(input))
                return ValidationError.Empty; // Возвращаем код ошибки "пустой ввод"

            // Проверка 2: Если ввод не является числом
            if(!IsNumber(input, out number))
                return ValidationError.NotNumber; // Возвращаем код ошибки "не число"

            // Проверка 3: Если число вне допустимого диапазона
            if(!IsNumberInRange(number))
                return ValidationError.OutOfRange; // Возвращаем код ошибки "вне диапазона"

            // Если все проверки пройдены
            return ValidationError.None; // Возвращаем код "нет ошибок"
        }

        // Основной метод для получения выбора пользователя
        private int GetUserChoice2()
        {
            // Бесконечный цикл, пока пользователь не введёт корректные данные
            while(true)
            {
                // Выводим приглашение для ввода
                Console.Write(">>> Введите номер программы: ");

                // Читаем ввод пользователя с клавиатуры
                string userInput = Console.ReadLine();

                // Переменная для хранения распознанного числа
                int programNumber;

                // Получаем тип ошибки валидации для введённых данных
                ValidationError error = GetValidationError(userInput, out programNumber);

                // Используем switch для обработки разных типов ошибок
                switch(error)
                {
                    // Случай 1: Нет ошибок - всё в порядке
                    case ValidationError.None:
                        return programNumber; // Возвращаем распознанное число (успешный выход из метода)

                    // Случай 2: Ошибка - пустой ввод
                    case ValidationError.Empty:
                        ShowErrorMessage("Вы ничего не ввели!"); // Показываем сообщение об ошибке
                        break; // Выходим из switch

                    // Случай 3: Ошибка - ввели не число
                    case ValidationError.NotNumber:
                        ShowErrorMessage($"'{userInput}' - это не число!\n" +
                                       "Пожалуйста, введите номер программы цифрами"); // Показываем сообщение
                        break; // Выходим из switch

                    // Случай 4: Ошибка - число вне диапазона
                    case ValidationError.OutOfRange:
                        ShowErrorMessage($"Нет программы № {programNumber}!\n" +
                                       $"Доступны программы от {EXIT} до {programs.Length}"); // Показываем сообщение
                        break; // Выходим из switch
                }

                WaitForKey(); // Ждём, пока пользователь нажмёт любую клавишу
                Console.Clear(); // Очистить консоль
                ShowMenu(); // Показать меню
                continue; // Переходим к следующей итерации цикла (просим ввести данные заново)
            }
        }

        #endregion

        #region ===== МЕТОДЫ-ЧЕКЕРЫ =====

        // Проверка на пустой ввод
        private bool IsEmpty(string input)
        {
            input = input.Trim(); // Убрать пробелы в конце и в начале (если имеются)
            if(string.IsNullOrEmpty(input)) // Альтернативно: if (input == null || input.Trim() == "")
            {
                return true; // да, ввод пустой
            }
            else
            {
                return false; // нет, ввод не пустой
            }
        }

        // Проверка на число
        private bool IsNumber(string input, out int number)
        {
            // Пробуем превратить текст в число
            bool isNumber = int.TryParse(input, out number);

            // Если удачно
            if(isNumber)
            {
                return true; // да, это число
            }
            else
            {
                return false; // нет, это не число
            }
        }

        // Проверка вхождения в допустимый диапазон
        private bool IsNumberInRange(int number)
        {
            // Проверяем диапазон: от 0 (выход) до количества программ
            if(number < EXIT || number > programs.Length)
            {
                return false; // нет, число не в диапазоне
            }
            else
            {
                return true; // да, число в диапазоне
            }
        }

        // Проверить валидность введеных данных
        private bool CheckUserInput(string input, out int validNumber)
        {
            validNumber = EXIT;

            // Проверяем по порядку
            if(IsEmpty(input)) return false; // Проверка на пустой ввод
            if(!IsNumber(input, out validNumber)) return false; // Проверка на число
            if(!IsNumberInRange(validNumber)) return false; // Проверка вхождения в допустимый диапазон

            return true; // Все ок!
        }

        #endregion

        #region ===== МЕТОДЫ ФОРМАТИРОВАНИЯ ТЕКСТА =====
        // Написать текст указанным цветом
        private void ShowMessageInColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Изменить цвет текста на красный
            Console.WriteLine(text);
            Console.ResetColor(); // Вернуть стандартный цвет текста
        }

        // Изменить шрифт текста
        private void ChangeFontTo(int font)
        {
            Console.WriteLine(new string('=', font));
        }

        #endregion

        #region ===== МЕТОДЫ ВЫВОДА ТЕКСТА НА ЭКРАН =====

        // Показать количество загруженных программ
        private void ShowCountDownloadedPrograms(int countPrograms)
        {
            Console.WriteLine($"Загружено программ: {countPrograms}");
        }

        // Показать приветствие
        private void ShowWelcome()
        {
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║      ОБУЧАЮЩИЙ ПРОЕКТ C#        ║");
            Console.WriteLine("║   Демонстрация ООП подходов     ║");
            Console.WriteLine("╚═════════════════════════════════╝");
        }

        // Показать прощальное сообщение
        private void ShowFinalMessage()
        {
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("Спасибо за использование программы!");
            Console.WriteLine("До новых встреч! 👋");
            Console.WriteLine(new string('=', 40));


            Console.WriteLine("\n👋 До свидания! Возвращайтесь снова!");
        }

        // Показать главное меню
        private void ShowMenu()
        {
            Console.WriteLine("\n════════════ ГЛАВНОЕ МЕНЮ ════════════");

            // Показываем все программы с номерами
            for(int i = 0; i < programs.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {programs[i].Name}");
            }

            Console.WriteLine("0. Выход");
        }

        // Показать сообщение о запуске конкретной программы
        private void ShowMessageStartingProgram(int number)
        {
            Console.WriteLine($"\n🚀 Запускаем: {programs[number - 1].Name}");
        }

        // Ждать нажатия любой клавиши
        private void WaitForKey()
        {
            Console.WriteLine("════════════════════════════════════");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        // Показать сообщение об ошибке
        private void ShowErrorMessage(string message)
        {
            // Написать красным цветом
            ShowMessageInColor(
                color: ConsoleColor.Red,
                text: $"\nОШИБКА: {message}"
            );
            Console.WriteLine("\nПопробуйте ещё раз...");
        }

        #endregion
    }
}