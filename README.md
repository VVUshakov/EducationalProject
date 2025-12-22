# EducationalProject

// Вместо:
ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");

// Стало:
ConsoleHelper.WaitForAnyKey(BaseService.PRESS_ANY_KEY_TO_RETURN);

// Вместо:
ConsoleHelper.ShowError("Вы ничего не ввели!");

// Стало:
ConsoleHelper.ShowError(InputValidator.ERROR_EMPTY_INPUT);