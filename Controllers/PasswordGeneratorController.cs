using EducationalProject.Core;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class PasswordGeneratorController : BaseController
    {
        private readonly PasswordGeneratorService _service;
        private readonly PasswordGeneratorView _view;
        public override string Name => AppConfig.PasswordGeneratorConfig.Name;

        public PasswordGeneratorController()
        {
            _service = new PasswordGeneratorService();
            _view = new PasswordGeneratorView();
        }

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            // Показать описание программы
            ConsoleHelper.ShowInfoBlock(
                "О ГЕНЕРАТОРЕ ПАРОЛЕЙ",
                new string[] {
                    "Эта программа создает безопасные пароли.",
                    "Вы можете настроить:",
                    "• Длину пароля",
                    "• Типы используемых символов",
                    "• Уровень сложности",
                    "",
                    "Рекомендуется использовать пароли",
                    "длиной не менее 12 символов."
                }
            );

            // 1. Получить длину пароля
            int length = _view.GetPasswordLength();

            // 2. Получить настройки символов
            var settings = _view.GetPasswordSettings();

            // 3. Сгенерировать пароль
            var result = _service.GeneratePassword(settings, length);

            // 4. Показать результат
            _view.ShowResult(result);

            // 5. Ожидание нажатия клавиши
            ConsoleHelper.WaitForAnyKey();
        }
    }
}