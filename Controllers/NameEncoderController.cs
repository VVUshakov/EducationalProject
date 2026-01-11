using EducationalProject.Models;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class NameEncoderController : BaseController
    {
        private readonly NameEncoderService _service;
        private readonly NameEncoderView _view;

        public NameEncoderController()
        {
            _service = new NameEncoderService();
            _view = new NameEncoderView();
        }

        public override string Name => "Кодировщик имён";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            // Показать описание программы
            ConsoleHelper.ShowInfoBlock(
                "О ПРОГРАММЕ",
                new string[] {
                    "Эта программа кодирует имена тремя способами:",
                    "1. Алфавитный код: А=01, Б=02, ..., Я=33",
                    "2. Азбука Морзе: точки (.) и тире (-)",
                    "3. Числовой шифр: позиция буквы × 3",
                    "",
                    "Поддерживаются русские и английские буквы."
                }
            );

            // 1. Получить имя от пользователя
            string name = _view.GetNameInput();

            // 2. Получить метод кодирования
            int methodChoice = _view.GetEncodingMethod();

            // 3. Выполнить кодирование
            EncodingResult result = _service.EncodeName(name, methodChoice);

            // 4. Показать результат
            _view.ShowResult(result);

            // 5. Показать дополнительную информацию
            _view.ShowEncodingInfo();

            // 6. Ожидание нажатия клавиши
            ConsoleHelper.WaitForAnyKey();
        }
    }
}