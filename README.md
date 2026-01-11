# Учебный проект C# для школьников 7-9 классов
Проект демонстрирует основы программирования на C# с использованием упрощённой архитектуры MVC (Model-View-Controller).

## Наглядность:
Чёткое разделение: "данные" (Models), "логика" (Services), "интерфейс" (Views)
Этот подход помогает разделить код на логические части, что делает программу более понятной, удобной для изменений и обучения.
Можно изучать каждую часть отдельно
Легко добавлять новые функции
Легкость изменений - чтобы поменять интерфейс отображения (например, консоль → Веб), меняем только Views

Проект специально разработан для понимания основ:
- Простые типы данных (int, string, double)
- Чёткое разделение ответственности
- Минимум сложных конструкций
- Понятные комментарии на русском

✅ Чистая архитектура (упрощённый MVC)
✅ SOLID принципы
✅ Минимальный код в каждой сущности
✅ Самочитаемость кода
✅ Гибкая настройка в одном месте (AppConfig)
✅ Удобная структура папок
✅ Простые типы данных (для школьников)
✅ Профессиональный уровень кода

------------------------------------------------------------------------------

## 📁 Структура проекта
EducationalProject/
├── Controllers/ # Контроллеры (координаторы)
├── Models/ # Модели данных
├── Services/ # Бизнес-логика
├── Views/ # Представления (ввод/вывод)
├── Core/ # Базовые утилиты и настройки
└── Program.cs # Точка входа


## 📁 Полная структура проекта
EducationalProject/						# 📁 Корневая папка проекта
│   
├── Controllers/						# 🎮 КОНТРОЛЛЕРЫ/координаторы (управляют программой)
│   │
│   ├── MainMenuController.cs			# Главное меню (выбор программы)
│   ├── CalculatorController.cs			# Управляет калькулятором
│   ├── BinaryConverterController.cs	# Управляет двоичным преобразователем
│   ├── NameEncoderController.cs		# Управляет кодировщиком имён
│   ├── PasswordGeneratorController.cs	# Управляет генератором паролей
│   └── AssignmentDemoController.cs		# Управляет демонстрацией операций присваивания
│
├── Models/								# 📊 МОДЕЛИ данных (хранят данные)
│   │
│   ├── CalculationData.cs				# Данные для калькулятора (числа, операция, результат)
│   ├── BinaryConversion.cs				# Данные для преобразования чисел (десятичное, двоичное)
│   ├── EncodingResult.cs				# Данные для кодирования (оригинал, закодированное, метод)
│   ├── PasswordSettings.cs				# Настройки пароля (какие символы использовать)
│   ├── PasswordResult.cs				# Результат генерации пароля (пароль, длина, надежность)
│   └── OperationData.cs				# Данные для демо операций (значения, операции, результат)
│
├── Services/							# ⚙️ СЕРВИСЫ (выполняют логику)
│   │
│   ├── CalculatorService.cs			# Логика вычислений (+, -, *, /, %)
│   ├── BinaryConverterService.cs		# Логика преобразования чисел (десятичное ↔ двоичное)
│   ├── NameEncoderService.cs			# Логика кодирования имён (3 метода)
│   ├── PasswordGeneratorService.cs		# Логика генерации паролей
│   └── AssignmentDemoService.cs		# Логика демонстрации операций присваивания
│
├── Views/								# 👁️ ПРЕДСТАВЛЕНИЯ (показывают информацию, ввод/вывод)
│   │
│   ├── CalculatorView.cs				# Показывает калькулятор (ввод чисел, результат)
│   ├── BinaryConverterView.cs			# Показывает преобразователь чисел
│   ├── NameEncoderView.cs				# Показывает кодировщик имён
│   ├── PasswordGeneratorView.cs		# Показывает генератор паролей
│   └── AssignmentDemoView.cs			# Показывает демо операций присваивания
│
├── Core/								# 🧠 ЯДРО (основные/базовые утилиты и настройки)
│   │
│   ├── AppConfig.cs					# ⚙️ НАСТРОЙКИ (всё в одном месте!)
│   │									# Здесь хранятся все сообщения, числа, настройки
│   │									# Пример: MaxPasswordLength = 50, WelcomeMessage = "Добро пожаловать!"
│   │
│   ├── ConsoleHelper.cs				# 🖥️ ПОМОЩНИК ДЛЯ КОНСОЛИ
│   │									# Помогает красиво показывать текст в консоли
│   │									# Пример: ShowHeader("Заголовок"), ShowError("Ошибка")
│   │
│   └── InputValidator.cs				# ✅ ПРОВЕРКА ВВОДА
│										# Проверяет, что пользователь ввёл правильные данные
│										# Пример: Проверяет число, текст, выбор меню
│
├── Program.cs							# 🚀 ТОЧКА ВХОДА (начало программы)
│										# Отсюда начинается выполнение программы
│										# Создаёт MainMenuController и запускает его
│
└── README.md							# 📖 ИНСТРУКЦИЯ (описание проекта)

------------------------------------------------------------------------------

##  🎯 Что делает каждая папка (простыми словами):
Папка           Для чего нужна          Пример
-----------------------------------------------------------------
Controllers     Управляют программой    Как дирижёр в оркестре
Models          Хранят данные           Как листок с записями
Services        Выполняют вычисления    Как калькулятор в уме
Views           Показывают информацию   Как экран телевизора
Core            Помогают программе      Как инструменты в ящике

# ПОДРОБНО:
# 📚 Принципы архитектуры (упрощённый MVC)
Проект использует упрощённую версию архитектуры MVC, специально адаптированную для обучения:

## 🎮 Контроллеры (Controllers)
Роль: "Координаторы" или "Управляющие" (координация работы - "Дирижер" над сущностями проекта)
Что делают: Связывают модель и представление, управляют потоком выполнения программы
Пример: CalculatorController.cs получает данные от пользователя, передаёт их сервису для вычислений, затем показывает результат
Для школьников: Как дирижёр в оркестре - управляет всеми частями программы

## 📊 Модели (Models)
Роль: "Хранители данных"
Что делают: Содержат только данные (поля, свойства), без логики
Пример: CalculationData.cs хранит два числа, операцию и результат
Для школьников: Как анкета или бланк - просто хранит информацию

## ⚙️ Сервисы (Services)
Роль: "Вычислители" или "Логика" (без ввода/вывода)
Что делают: Выполняют вычисления и обработку данных
Пример: CalculatorService.cs складывает, вычитает, умножает числа
Для школьников: Как калькулятор в руках - выполняет вычисления

## 👁️ Представления (Views)
Роль: "Интерфейс" или "Окно в мир" (только ввод/вывод, без вычислений)
Что делают: Показывают информацию пользователю и получают от него данные
Пример: CalculatorView.cs показывает "Введите первое число:" и получает ответ
Для школьников: Как экран телефона - показывает информацию и принимает касания

------------------------------------------------------------------------------

## 🔄 Как это работает вместе:
Пользователь → View (показывает, спрашивает) → Controller (управляет) → Service (вычисляет и сохраняет) → Model (хранит данные)
                     ↓
Результат ← Controller (получает результат) ← Service (получает и обрабатывает данные) ← Model (данные)
                     ↓
           View (показывает результат) → Пользователь (видит ответ)

# ПОДРОБНО:
# 📚 Как работает программа:

##  1. Запуск программы:
Program.cs → MainMenuController → Показывает меню

##  2. Выбор программы в меню::
Пользователь выбирает "1. Калькулятор" →
MainMenuController создаёт CalculatorController →
CalculatorController запускается

##  3. Работа калькулятора:
CalculatorController:
  1. Создаёт CalculatorView (показывает интерфейс)
  2. Создаёт CalculatorService (выполняет вычисления)
  3. CalculatorView просит ввести числа
  4. CalculatorService вычисляет результат
  5. CalculatorView показывает результат

##  4. Возврат в меню:
Калькулятор завершил работу →
Возвращаемся в MainMenuController →
Снова показываем меню


# 🔍 Пример кода с пояснениями:
## Models/CalculationData.cs:
// 📊 МОДЕЛЬ: хранит данные для калькулятора
public class CalculationData
{
   public double FirstNumber { get; set; }     // Первое число (например: 10)
   public double SecondNumber { get; set; }    // Второе число (например: 5)
   public char Operation { get; set; }         // Операция (например: '+')
   public double Result { get; set; }          // Результат (например: 15)
    
   // Свойство только для чтения
   public bool IsValid => !double.IsNaN(Result); // Проверяет, правильный ли результат
}

## Services/CalculatorService.cs:
// ⚙️ СЕРВИС: выполняет вычисления
public class CalculatorService
{
   public CalculationData Calculate(CalculationData data)
   {
       // Выполняем операцию
       data.Result = data.Operation switch
       {
           '+' => data.FirstNumber + data.SecondNumber,  // Сложение
           '-' => data.FirstNumber - data.SecondNumber,  // Вычитание
           '*' => data.FirstNumber * data.SecondNumber,  // Умножение
           '/' => data.SecondNumber != 0 ? data.FirstNumber / data.SecondNumber : double.NaN, // Деление
           _ => double.NaN // Если операция неизвестна
       };
       return data; // Возвращаем данные с результатом
   }
}

## Views/CalculatorView.cs:
// 👁️ ПРЕДСТАВЛЕНИЕ: показывает калькулятор
public class CalculatorView
{
    public void ShowResult(CalculationData data)
    {
        if (!data.IsValid)
        {
            ConsoleHelper.ShowError("Ошибка вычисления!"); // Показываем ошибку
        }
        else
        {
            // Показываем результат
            Console.WriteLine($"Результат: {data.FirstNumber} {data.Operation} {data.SecondNumber} = {data.Result:F2}");
        }
    }
}

## Controllers/CalculatorController.cs:
// 🎮 КОНТРОЛЛЕР: управляет калькулятором
public class CalculatorController
{
   private CalculatorService _service; // Сервис для вычислений
   private CalculatorView _view;       // Представление для показа
    
   public void Run()
   {
       // 1. Получить данные через View
       var data = _view.GetInput();
       // 2. Вычислить через Service
       var result = _service.Calculate(data);
       // 3. Показать результат через View
       _view.ShowResult(result);
   }
}

------------------------------------------------------------------------------

# 📝 Как добавить новую программу

## 📌 Правила для добавления новой программы:
1. Создай Model (что хранить?) → Models/НоваяМодель.cs
2. Создай Service (что вычислять?) → Services/НовыйСервис.cs
3. Создай View (что показывать?) → Views/НовоеПредставление.cs
4. Создай Controller (как управлять?) → Controllers/НовыйКонтроллер.cs
5. Добавь в меню → MainMenuController.cs

## ПОДРОБНО: 
## 1. Создайте модель в папке Models/:
// Models/MyProgramData.cs
public class MyProgramData
{
    public string Input { get; set; }
    public string Output { get; set; }
}

## 2. Создайте сервис в папке Services/:
1. // Services/MyProgramService.cs
public class MyProgramService
{
    public MyProgramData Process(MyProgramData data)
    {
        // Ваша логика здесь
        data.Output = data.Input.ToUpper();
        return data;
    }
}

##  3.Создайте представление в папке Views/:
// Views/MyProgramView.cs
public class MyProgramView
{
   public MyProgramData GetInput()
   {
       Console.WriteLine("Введите текст:");
       string input = Console.ReadLine();
       return new MyProgramData { Input = input };
   }
    
   public void ShowResult(MyProgramData data)
   {
       Console.WriteLine($"Результат: {data.Output}");
   }
}

##  4.Создайте контроллер в папке Controllers/:
// Controllers/MyProgramController.cs
public class MyProgramController : BaseController
{
   private MyProgramService _service = new();
   private MyProgramView _view = new();
    
   public override string Name => "Моя программа";
    
   public override void Run()
   {
       var data = _view.GetInput();
       var result = _service.Process(data);
       _view.ShowResult(result);
   }
}

##  5.Добавьте контроллер в главное меню (MainMenuController.cs):
_controllers = new List<BaseController>
{
    // ... существующие контроллеры
    new MyProgramController() // Добавьте эту строку
};