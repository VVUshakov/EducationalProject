namespace EducationalProject.Services
{
    /// <summary>
    /// Программа 1: Калькулятор
    /// Демонстрирует базовые арифметические операции и обработку ввода пользователя
    /// </summary>
    public class Calculator : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Название программы для отображения в меню
        /// </summary>
        public override string Name => "Калькулятор";

        #endregion

        #region ===== ПЕРЕЧИСЛЕНИЯ =====

        /// <summary>
        /// Доступные математические операции
        /// </summary>
        private enum MathOperation
        {
            None,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Modulo
        }

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Главный метод запуска программы
        /// </summary>
        public override void Run()
        {
            ShowHeader(); // Показать заголовок

            // Получить первое число
            double firstNumber = GetNumber("первое");
            if(double.IsNaN(firstNumber)) return;

            // Получить второе число
            double secondNumber = GetNumber("второе");
            if(double.IsNaN(secondNumber)) return;

            // Получить операцию
            MathOperation operation = GetOperation();
            if(operation == MathOperation.None) return;

            // Выполнить расчет
            CalculationResult result = Calculate(firstNumber, secondNumber, operation);

            // Показать результат
            ShowCalculationResult(firstNumber, secondNumber, operation, result);

            WaitForContinue(); // Ожидать подтверждения
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Показать заголовок программы
        /// </summary>
        private void ShowHeader()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("                   КАЛЬКУЛЯТОР                ");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            Console.WriteLine("Программа выполняет базовые арифметические операции");
            Console.WriteLine("с двумя числами: сложение, вычитание, умножение,");
            Console.WriteLine("деление и нахождение остатка от деления.\n");
        }

        /// <summary>
        /// Получить число от пользователя
        /// </summary>
        /// <param name="numberName">Название числа (первое/второе)</param>
        /// <returns>Введенное число или NaN при ошибке</returns>
        private double GetNumber(string numberName)
        {
            Console.Write($">>> Введите {numberName} число: ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if(string.IsNullOrWhiteSpace(input))
            {
                ShowErrorMessage($"Вы не ввели {numberName} число!");
                return double.NaN;
            }

            if(!double.TryParse(input, out double number))
            {
                ShowErrorMessage($"'{input}' не является допустимым числом!");
                return double.NaN;
            }

            return number;
        }

        /// <summary>
        /// Получить математическую операцию от пользователя
        /// </summary>
        private MathOperation GetOperation()
        {
            Console.WriteLine("\n════════════ ДОСТУПНЫЕ ОПЕРАЦИИ ════════════");
            Console.WriteLine("+ : Сложение");
            Console.WriteLine("- : Вычитание");
            Console.WriteLine("* : Умножение");
            Console.WriteLine("/ : Деление");
            Console.WriteLine("% : Остаток от деления (модуль)");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            Console.Write(">>> Выберите операцию (+, -, *, /, %): ");
            char operationChar = Console.ReadKey().KeyChar;
            Console.WriteLine();

            return ParseOperation(operationChar);
        }

        /// <summary>
        /// Распознать операцию по символу
        /// </summary>
        private MathOperation ParseOperation(char operationChar)
        {
            switch(operationChar)
            {
                case '+': return MathOperation.Addition;
                case '-': return MathOperation.Subtraction;
                case '*': return MathOperation.Multiplication;
                case '/': return MathOperation.Division;
                case '%': return MathOperation.Modulo;
                default:
                    ShowErrorMessage($"Операция '{operationChar}' не поддерживается!");
                    return MathOperation.None;
            }
        }

        /// <summary>
        /// Показать результат расчета
        /// </summary>
        private void ShowCalculationResult(double a, double b, MathOperation operation, CalculationResult result)
        {
            Console.WriteLine("\n════════════════ РЕЗУЛЬТАТ ════════════════");

            if(!result.Success)
            {
                ShowErrorMessage(result.ErrorMessage);
                return;
            }

            string operationSymbol = GetOperationSymbol(operation);
            string operationName = GetOperationName(operation);

            Console.WriteLine($"Операция: {operationName}");
            Console.WriteLine($"Числа: {a:F2} {operationSymbol} {b:F2}");
            Console.WriteLine($"Результат: {result.Value:F2}");

            // Дополнительная информация для деления
            if(operation == MathOperation.Division && b != 0)
            {
                double remainder = a % b;
                if(Math.Abs(remainder) > 0.001)
                {
                    Console.WriteLine($"Остаток от деления: {remainder:F2}");
                }
            }

            Console.WriteLine("═══════════════════════════════════════════════");
        }

        /// <summary>
        /// Получить символ операции
        /// </summary>
        private string GetOperationSymbol(MathOperation operation)
        {
            return operation switch
            {
                MathOperation.Addition => "+",
                MathOperation.Subtraction => "-",
                MathOperation.Multiplication => "×",
                MathOperation.Division => "÷",
                MathOperation.Modulo => "%",
                _ => "?"
            };
        }

        /// <summary>
        /// Получить название операции
        /// </summary>
        private string GetOperationName(MathOperation operation)
        {
            return operation switch
            {
                MathOperation.Addition => "Сложение",
                MathOperation.Subtraction => "Вычитание",
                MathOperation.Multiplication => "Умножение",
                MathOperation.Division => "Деление",
                MathOperation.Modulo => "Остаток от деления",
                _ => "Неизвестная операция"
            };
        }

        /// <summary>
        /// Показать сообщение об ошибке
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nОШИБКА: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Ожидать нажатия клавиши для продолжения
        /// </summary>
        private void WaitForContinue()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        #endregion

        #region ===== МЕТОДЫ ВЫЧИСЛЕНИЙ =====

        /// <summary>
        /// Результат расчета с информацией об ошибках
        /// </summary>
        private struct CalculationResult
        {
            public bool Success { get; set; }
            public double Value { get; set; }
            public string ErrorMessage { get; set; }
        }

        /// <summary>
        /// Выполнить математическую операцию
        /// </summary>
        private CalculationResult Calculate(double a, double b, MathOperation operation)
        {
            CalculationResult result = new CalculationResult();

            try
            {
                switch(operation)
                {
                    case MathOperation.Addition:
                        result.Value = a + b;
                        result.Success = true;
                        break;

                    case MathOperation.Subtraction:
                        result.Value = a - b;
                        result.Success = true;
                        break;

                    case MathOperation.Multiplication:
                        result.Value = a * b;
                        result.Success = true;
                        break;

                    case MathOperation.Division:
                        if(Math.Abs(b) < double.Epsilon)
                        {
                            result.Success = false;
                            result.ErrorMessage = "Деление на ноль невозможно!";
                        }
                        else
                        {
                            result.Value = a / b;
                            result.Success = true;
                        }
                        break;

                    case MathOperation.Modulo:
                        if(Math.Abs(b) < double.Epsilon)
                        {
                            result.Success = false;
                            result.ErrorMessage = "Операция модуля с нулем невозможна!";
                        }
                        else
                        {
                            result.Value = a % b;
                            result.Success = true;
                        }
                        break;

                    default:
                        result.Success = false;
                        result.ErrorMessage = "Неизвестная операция!";
                        break;
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Ошибка при вычислении: {ex.Message}";
            }

            return result;
        }

        #endregion
    }
}