using EducationalProject.Core;
using EducationalProject.Models;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class BinaryConverterController : BaseController
    {
        private readonly BinaryConverterService _service;
        private readonly BinaryConverterView _view;

        public BinaryConverterController()
        {
            _service = new BinaryConverterService();
            _view = new BinaryConverterView();
        }

        public override string Name => "Двоичный преобразователь";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            // Показать информацию о двоичной системе
            ConsoleHelper.ShowInfoBlock(
                "О ДВОИЧНОЙ СИСТЕМЕ",
                new string[] {
                    "Двоичная система счисления - система, использующая",
                    "две цифры: 0 и 1. Она является основной для",
                    "работы компьютеров и цифровой техники.",
                    "",
                    $"Диапазон преобразования: {AppConfig.BinaryConverterConfig.MinValue}-{AppConfig.BinaryConverterConfig.MaxValue}",
                    $"Количество бит: {AppConfig.BinaryConverterConfig.BitsCount}"
                }
            );

            // 1. Получить тип преобразования
            int choice = _view.GetConversionChoice();

            BinaryConversion result;

            // 2. Выполнить преобразование
            if(choice == 1)
            {
                int decimalNumber = _view.GetDecimalInput();
                result = _service.ConvertToBinary(decimalNumber);
            }
            else
            {
                string binaryNumber = _view.GetBinaryInput();
                result = _service.ConvertToDecimal(binaryNumber);
            }

            // 3. Показать результат
            _view.ShowResult(result, choice);

            // 4. Показать дополнительную информацию
            _view.ShowAdditionalInfo();

            // 5. Ожидание нажатия клавиши
            ConsoleHelper.WaitForAnyKey();
        }
    }
}