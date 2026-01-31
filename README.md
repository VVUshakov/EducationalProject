# EducationalProject

ProgramStructure/
├── Program.cs                      # Точка входа
├── Core/
│   ├── Interfaces/
│   │   ├── IProgram.cs
│   │   ├── IView.cs
│   │   └── IController.cs
│   ├── Models/
│   │   ├── MenuItem.cs
│   │   └── Menu.cs
│   ├── Controllers/
│   │   ├── MainController.cs
│   │   └── ProgramController.cs
│   ├── Views/
│   │   ├── ConsoleView.cs
│   │   └── MenuView.cs
│   └── Services/
│       ├── ProgramFactory.cs
│       ├── CalculatorService.cs
│       └── NameEncoderService.cs
├── Programs/
│   ├── BaseProgram.cs
│   ├── CalculatorProgram.cs
│   ├── NameEncoderProgram.cs
│   └── NewProgram.cs              # Пример для добавления новой программы
└── Utils/
    ├── ConsoleHelper.cs
    └── Validator.cs