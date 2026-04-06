# LW03-HW: Quality Gate Analyzer

![Иишка в помощь](https://preview.redd.it/the-origin-of-dog-closing-eyes-meme-yakuza-3-v0-txfwdc8oi2ve1.jpg?width=567&format=pjpg&auto=webp&s=0b51ed14c2acfbeed5e54329f158187a8e881e32)

# Иишка в помощь


## Опис системи

Консольний інструмент для автоматичної перевірки якості коду перед злиттям у головну гілку
Система читає метрики, аналізує їх за правилами та формує структурований звіт.



## Схема Pipeline

```
[Input] --> [Parser] --> [Rules Analyzer] --> [Reporter] --> [Output]
```

1. **Parser** — читає рядок або файл формату `key=value` і повертає словник метрик.  
2. **Rules Analyzer** — перевіряє кожну метрику за заданим правилом (Coverage, Duplication, Complexity).  
3. **Reporter** — виводить результат у текстовому або JSON форматі.



## Патерни проектування

| Патерн | Клас(и) | Опис |
|---|---|---|
| **Strategy** | `IMetricParser`, `KeyValueParser`, `FileParser` | Взаємозамінні алгоритми парсингу |
| **Strategy** | `IAnalysisRule`, `CoverageRule`, `DuplicationRule`, `ComplexityRule` | Взаємозамінні правила аналізу |
| **Strategy** | `IReporter`, `ConsoleReporter`, `JsonReporter` | Взаємозамінні формати звіту |
| **Factory** | `ParserFactory`, `ReporterFactory` | Створення об'єктів за типом |
| **Decorator** | `RuleDecorator` | Логування кожного правила без зміни його логіки |
| **Observer** | `IObserver`, `LoggerObserver` | Сповіщення підписників при кожному FAIL |
| **Plugin** | `PluginLoader`, `LW03-HW.Plugins` | Динамічне завантаження правил з зовнішнього assembly |



## Структура проєкту
```
LW03-HW/
├── LW03-HW.slnx
├── README.md
└── src/
    ├── LW03-HW.Core/           — основна логіка
    │   ├── Interfaces/
    │   │   ├── IMetricParser.cs
    │   │   ├── IAnalysisRule.cs
    │   │   ├── IReporter.cs
    │   │   └── IObserver.cs
    │   ├── Parsers/
    │   │   ├── KeyValueParser.cs
    │   │   ├── FileParser.cs
    │   │   └── ParserFactory.cs
    │   ├── Rules/
    │   │   ├── CoverageRule.cs
    │   │   ├── DuplicationRule.cs
    │   │   ├── ComplexityRule.cs
    │   │   └── RuleDecorator.cs
    │   ├── Reporters/
    │   │   ├── ConsoleReporter.cs
    │   │   ├── JsonReporter.cs
    │   │   ├── ReporterFactory.cs
    │   │   └── LoggerObserver.cs
    │   ├── Plugins/
    │   │   └── PluginLoader.cs
    │   ├── AnalysisPipeline.cs
    │   ├── AnalysisResult.cs
    │   ├── ReportEventArgs.cs
    │   └── Program.cs
    ├── LW03-HW.Tests/           — юніт-тести
    │   ├── ParserTests.cs
    │   ├── RuleTests.cs
    │   └── ReporterTests.cs
    └── LW03-HW.Plugins/         — приклад плагіна
        └── TestPassRateRule.cs
```

```bash
## Команди для запуску

# Збудувати проєкт
dotnet build

# Запустити тести
dotnet test

# Запустити з inline-рядком (консольний звіт)
dotnet run --project src/LW03-HW.Core -- inline "coverage=85,duplication=5,complexity=8" console

# Запустити з inline-рядком (JSON звіт)
dotnet run --project src/LW03-HW.Core -- inline "coverage=72,duplication=18,complexity=12" json

# Запустити з файлом
dotnet run --project src/LW03-HW.Core -- file metrics.txt console

