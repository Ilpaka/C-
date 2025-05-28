using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Group { get; set; }
    public int[] Grades { get; set; }

    public Student(string firstName, string lastName, string group, int[] grades)
    {
        FirstName = firstName;
        LastName = lastName;
        Group = group;
        Grades = grades;
    }

    public double AverageGrade => Grades.Average();
}

class Program
{
    static List<Student> students = new List<Student>
    {
        new Student("Иван", "Петров", "A1", new[] {5, 4, 5, 5}),
        new Student("Мария", "Иванова", "A1", new[] {4, 4, 4, 5}),
        new Student("Алексей", "Сидоров", "A2", new[] {3, 4, 3, 4}),
        new Student("Ольга", "Кузнецова", "A2", new[] {5, 5, 5, 5}),
        new Student("Дмитрий", "Смирнов", "A3", new[] {4, 4, 4, 4}),
        new Student("Екатерина", "Волкова", "A1", new[] {5, 5, 4, 5}),
        new Student("Павел", "Семенов", "A3", new[] {3, 3, 4, 4}),
        new Student("Анна", "Федорова", "A2", new[] {5, 4, 5, 4}),
        new Student("Сергей", "Морозов", "A1", new[] {2, 3, 4, 3}),
        new Student("Юлия", "Андреева", "A3", new[] {5, 5, 5, 4}),
        new Student("Виктор", "Григорьев", "A2", new[] {4, 5, 4, 5}),
        new Student("Елена", "Попова", "A2", new[] {3, 4, 5, 5}),
        new Student("Игорь", "Лебедев", "A1", new[] {4, 4, 5, 4}),
        new Student("Татьяна", "Козлова", "A3", new[] {5, 5, 4, 4}),
        new Student("Максим", "Новиков", "A2", new[] {5, 3, 4, 5})
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Найти всех студентов, у которых средний балл выше 4");
            Console.WriteLine("2. Отсортировать студентов по фамилии и имени");
            Console.WriteLine("3. Получить список имён студентов, у которых хотя бы одна оценка равна 5");
            Console.WriteLine("4. Сгруппировать студентов по группам и вычислить средний балл для каждой группы");
            Console.WriteLine("5. Найти всех студентов из определённой группы со средним баллом выше 4, отсортировать по имени и вывести фамилии");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FindStudentsWithAvgAbove4();
                    break;
                case "2":
                    SortStudentsByName();
                    break;
                case "3":
                    ListStudentsWithGrade5();
                    break;
                case "4":
                    GroupStudentsAndAvg();
                    break;
                case "5":
                    FindGroupStudentsWithAvgAbove4();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void FindStudentsWithAvgAbove4()
    {
        var result = students.Where(s => s.AverageGrade > 4);
        Console.WriteLine("Студенты со средним баллом выше 4:");
        foreach (var s in result)
        {
            Console.WriteLine($"{s.FirstName} {s.LastName} ({s.Group}) - Средний балл: {s.AverageGrade:F2}");
        }
    }

    static void SortStudentsByName()
    {
        var result = students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName);

        Console.WriteLine("Студенты, отсортированные по фамилии и имени:");
        foreach (var s in result)
        {
            Console.WriteLine($"{s.LastName} {s.FirstName} ({s.Group})");
        }
    }

    static void ListStudentsWithGrade5()
    {
        var result = students
            .Where(s => s.Grades.Contains(5))
            .Select(s => s.FirstName)
            .Distinct();

        Console.WriteLine("Имена студентов, у которых есть хотя бы одна пятёрка:");
        foreach (var name in result)
        {
            Console.WriteLine(name);
        }
    }

    static void GroupStudentsAndAvg()
    {
        var groups = students
            .GroupBy(s => s.Group)
            .Select(g => new
            {
                Group = g.Key,
                Avg = g.Average(s => s.AverageGrade)
            });

        Console.WriteLine("Средний балл по группам:");
        foreach (var g in groups)
        {
            Console.WriteLine($"{g.Group}: {g.Avg:F2}");
        }
    }

    static void FindGroupStudentsWithAvgAbove4()
    {
        Console.Write("Введите название группы: ");
        string groupName = Console.ReadLine();

        var result = students
            .Where(s => s.Group == groupName && s.AverageGrade > 4)
            .OrderBy(s => s.FirstName)
            .Select(s => s.LastName);

        Console.WriteLine($"Фамилии студентов из группы {groupName} со средним баллом выше 4:");
        foreach (var lastName in result)
        {
            Console.WriteLine(lastName);
        }
    }
}
