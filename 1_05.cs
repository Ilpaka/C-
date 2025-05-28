using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Определить тип осадков");
            Console.WriteLine("2. Определить комфортность температуры");
            Console.WriteLine("3. Выход");
            Console.Write("Ваш выбор: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DeterminePrecipitationType();
                    break;
                case "2":
                    DetermineTemperatureComfort();
                    break;
                case "3":
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void DeterminePrecipitationType()
    {
        Console.Write("Введите уровень осадков (мм): ");
        if (double.TryParse(Console.ReadLine(), out double precipitation))
        {
            string result = precipitation < 0.1 ? "Без осадков"
                : (precipitation <= 2.5 ? "Небольшой дождь"
                : (precipitation <= 17 ? "Умеренный дождь"
                : "Сильный дождь"));
            Console.WriteLine("Результат: " + result);
        }
        else
        {
            Console.WriteLine("Ошибка ввода. Введите число.");
        }
    }

    static void DetermineTemperatureComfort()
    {
        Console.Write("Введите температуру воздуха (°C): ");
        if (double.TryParse(Console.ReadLine(), out double temp))
        {
            string comfort = temp > 25 ? "Жарко"
                : (temp < 10 ? "Холодно"
                : "Комфортно");
            Console.WriteLine("Результат: " + comfort);
        }
        else
        {
            Console.WriteLine("Ошибка ввода. Введите число.");
        }
    }
}
