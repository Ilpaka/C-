using System;
using System.Collections.Generic;

class Program
{
    static Stack<string> bookStack = new Stack<string>();

    static Dictionary<string, int> inventory = new Dictionary<string, int>
    {
        { "Меч", 1 },
        { "Щит", 2 },
        { "Зелье здоровья", 5 }
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1. Управление книгами в стопке");
            Console.WriteLine("2. Управление инвентарём в игре");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите действие: ");
            string mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    BookStackMenu();
                    break;
                case "2":
                    InventoryMenu();
                    break;
                case "3":
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }

    static void BookStackMenu()
    {
        while (true)
        {
            Console.WriteLine("\nУправление книгами в стопке:");
            Console.WriteLine("1. Положить книгу на верх стопки");
            Console.WriteLine("2. Убрать книгу сверху");
            Console.WriteLine("3. Просмотреть книгу сверху");
            Console.WriteLine("4. Вывести всю стопку");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название книги: ");
                    string book = Console.ReadLine();
                    bookStack.Push(book);
                    Console.WriteLine($"Книга \"{book}\" добавлена на верх стопки.");
                    break;
                case "2":
                    if (bookStack.Count > 0)
                    {
                        string removed = bookStack.Pop();
                        Console.WriteLine($"Книга \"{removed}\" убрана с верхушки стопки.");
                    }
                    else
                    {
                        Console.WriteLine("Стопка пуста, нечего убирать.");
                    }
                    break;
                case "3":
                    if (bookStack.Count > 0)
                        Console.WriteLine($"Текущая книга сверху: \"{bookStack.Peek()}\"");
                    else
                        Console.WriteLine("Стопка пуста.");
                    break;
                case "4":
                    if (bookStack.Count == 0)
                    {
                        Console.WriteLine("Стопка пуста.");
                    }
                    else
                    {
                        Console.WriteLine("Текущая стопка книг (сверху вниз):");
                        foreach (var b in bookStack)
                        {
                            Console.WriteLine("- " + b);
                        }
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }

    static void InventoryMenu()
    {
        while (true)
        {
            Console.WriteLine("\nУправление инвентарём:");
            Console.WriteLine("1. Добавить предмет");
            Console.WriteLine("2. Удалить предмет");
            Console.WriteLine("3. Просмотреть содержимое инвентаря");
            Console.WriteLine("4. Найти предмет по названию");
            Console.WriteLine("5. Вернуться в главное меню");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Название предмета: ");
                    string item = Console.ReadLine();
                    Console.Write("Количество: ");
                    if (int.TryParse(Console.ReadLine(), out int addQty) && addQty > 0)
                    {
                        if (inventory.ContainsKey(item))
                            inventory[item] += addQty;
                        else
                            inventory[item] = addQty;
                        Console.WriteLine($"Добавлено: {item} (x{addQty})");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите корректное положительное число.");
                    }
                    break;
                case "2":
                    Console.Write("Название предмета: ");
                    string removeItem = Console.ReadLine();
                    Console.Write("Сколько удалить: ");
                    if (int.TryParse(Console.ReadLine(), out int removeQty) && removeQty > 0)
                    {
                        if (inventory.ContainsKey(removeItem))
                        {
                            inventory[removeItem] -= removeQty;
                            if (inventory[removeItem] <= 0)
                            {
                                inventory.Remove(removeItem);
                                Console.WriteLine($"Предмет \"{removeItem}\" полностью удалён из инвентаря.");
                            }
                            else
                            {
                                Console.WriteLine($"Удалено: {removeItem} (x{removeQty})");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Предмет \"{removeItem}\" не найден в инвентаре.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: введите корректное положительное число.");
                    }
                    break;
                case "3":
                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("Инвентарь пуст.");
                    }
                    else
                    {
                        Console.WriteLine("Текущий инвентарь:");
                        foreach (var pair in inventory)
                        {
                            Console.WriteLine($"- {pair.Key}: {pair.Value}");
                        }
                    }
                    break;
                case "4":
                    Console.Write("Название предмета: ");
                    string findItem = Console.ReadLine();
                    if (inventory.ContainsKey(findItem))
                        Console.WriteLine($"Количество \"{findItem}\": {inventory[findItem]}");
                    else
                        Console.WriteLine($"Предмет \"{findItem}\" не найден в инвентаре.");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }
}
