using System;
using System.Data;
using Microsoft.Data.Sqlite; 

class Program
{
    static string dbName = "library.db";
    static string connStr = $"Data Source={dbName}";

    static void Main()
    {
        CreateTableIfNotExists();

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Обновить книгу");
            Console.WriteLine("3. Удалить книгу");
            Console.WriteLine("4. Просмотреть все книги");
            Console.WriteLine("5. Просмотреть книгу по Id");
            Console.WriteLine("6. Поиск книг по автору");
            Console.WriteLine("7. Поиск книг по жанру");
            Console.WriteLine("8. Поиск книг по году публикации");
            Console.WriteLine("9. Фильтрация книг по цене");
            Console.WriteLine("10. Средний рейтинг по жанрам");
            Console.WriteLine("11. Общее количество страниц в доступных книгах");
            Console.WriteLine("12. Самая дорогая и самая дешевая книга");
            Console.WriteLine("13. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddBook(); break;
                case "2": UpdateBook(); break;
                case "3": DeleteBook(); break;
                case "4": ViewAllBooks(); break;
                case "5": ViewBookById(); break;
                case "6": SearchByAuthor(); break;
                case "7": SearchByGenre(); break;
                case "8": SearchByYear(); break;
                case "9": FilterByPrice(); break;
                case "10": AverageRatingByGenre(); break;
                case "11": TotalPagesAvailable(); break;
                case "12": MaxMinPriceBooks(); break;
                case "13": return;
                default: Console.WriteLine("Некорректный выбор."); break;
            }
        }
    }

    static void CreateTableIfNotExists()
    {
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = @"
CREATE TABLE IF NOT EXISTS Books (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    Genre TEXT NOT NULL,
    PublicationYear INTEGER NOT NULL,
    Pages INTEGER NOT NULL,
    Price REAL NOT NULL,
    IsAvailable BOOLEAN NOT NULL,
    Rating REAL NOT NULL,
    Description TEXT
);";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    static void AddBook()
    {
        Console.Write("Название: ");
        string title = Console.ReadLine();
        Console.Write("Автор: ");
        string author = Console.ReadLine();
        Console.Write("Жанр: ");
        string genre = Console.ReadLine();
        Console.Write("Год публикации: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Страниц: ");
        int pages = int.Parse(Console.ReadLine());
        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Доступна (true/false): ");
        bool isAvailable = bool.Parse(Console.ReadLine());
        Console.Write("Рейтинг (0-5): ");
        double rating = double.Parse(Console.ReadLine());
        Console.Write("Описание: ");
        string description = Console.ReadLine();

        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = @"INSERT INTO Books 
        (Title, Author, Genre, PublicationYear, Pages, Price, IsAvailable, Rating, Description)
        VALUES (@title, @author, @genre, @year, @pages, @price, @isAvailable, @rating, @description)";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@author", author);
        cmd.Parameters.AddWithValue("@genre", genre);
        cmd.Parameters.AddWithValue("@year", year);
        cmd.Parameters.AddWithValue("@pages", pages);
        cmd.Parameters.AddWithValue("@price", price);
        cmd.Parameters.AddWithValue("@isAvailable", isAvailable ? 1 : 0);
        cmd.Parameters.AddWithValue("@rating", rating);
        cmd.Parameters.AddWithValue("@description", description);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Книга добавлена!");
    }

    static void UpdateBook()
    {
        Console.Write("Введите Id книги для обновления: ");
        int id = int.Parse(Console.ReadLine());
        using var conn = new SqliteConnection(connStr);
        conn.Open();

        string checkSql = "SELECT COUNT(*) FROM Books WHERE Id=@id";
        using (var checkCmd = new SqliteCommand(checkSql, conn))
        {
            checkCmd.Parameters.AddWithValue("@id", id);
            if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
            {
                Console.WriteLine("Книга не найдена.");
                return;
            }
        }

        Console.Write("Новое название (оставьте пустым для без изменений): ");
        string title = Console.ReadLine();
        Console.Write("Новый автор: ");
        string author = Console.ReadLine();
        Console.Write("Новый жанр: ");
        string genre = Console.ReadLine();
        Console.Write("Новый год публикации: ");
        string yearStr = Console.ReadLine();
        Console.Write("Новое количество страниц: ");
        string pagesStr = Console.ReadLine();
        Console.Write("Новая цена: ");
        string priceStr = Console.ReadLine();
        Console.Write("Доступна (true/false): ");
        string isAvailableStr = Console.ReadLine();
        Console.Write("Новый рейтинг: ");
        string ratingStr = Console.ReadLine();
        Console.Write("Новое описание: ");
        string description = Console.ReadLine();

        var fields = new System.Text.StringBuilder();
        var cmd = new SqliteCommand();
        cmd.Connection = conn;

        void AddField(string field, string value, DbType type)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (fields.Length > 0) fields.Append(", ");
                fields.Append($"{field} = @{field}");
                cmd.Parameters.Add(new SqliteParameter($"@{field}", type) { Value = value });
            }
        }

        AddField("Title", title, DbType.String);
        AddField("Author", author, DbType.String);
        AddField("Genre", genre, DbType.String);
        if (!string.IsNullOrWhiteSpace(yearStr)) AddField("PublicationYear", yearStr, DbType.Int32);
        if (!string.IsNullOrWhiteSpace(pagesStr)) AddField("Pages", pagesStr, DbType.Int32);
        if (!string.IsNullOrWhiteSpace(priceStr)) AddField("Price", priceStr, DbType.Decimal);
        if (!string.IsNullOrWhiteSpace(isAvailableStr)) AddField("IsAvailable", isAvailableStr.ToLower() == "true" ? "1" : "0", DbType.Int32);
        if (!string.IsNullOrWhiteSpace(ratingStr)) AddField("Rating", ratingStr, DbType.Double);
        AddField("Description", description, DbType.String);

        if (fields.Length == 0)
        {
            Console.WriteLine("Нет изменений.");
            return;
        }

        cmd.CommandText = $"UPDATE Books SET {fields} WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Книга обновлена!");
    }

    static void DeleteBook()
    {
        Console.Write("Введите Id книги для удаления: ");
        int id = int.Parse(Console.ReadLine());
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "DELETE FROM Books WHERE Id=@id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        int rows = cmd.ExecuteNonQuery();
        if (rows > 0)
            Console.WriteLine("Книга удалена!");
        else
            Console.WriteLine("Книга не найдена.");
    }

    static void ViewAllBooks()
    {
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books";
        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows)
        {
            Console.WriteLine("Нет книг.");
            return;
        }
        while (reader.Read())
            PrintBook(reader);
    }

    static void ViewBookById()
    {
        Console.Write("Введите Id книги: ");
        int id = int.Parse(Console.ReadLine());
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books WHERE Id=@id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
            PrintBook(reader);
        else
            Console.WriteLine("Книга не найдена.");
    }

    static void SearchByAuthor()
    {
        Console.Write("Введите автора: ");
        string author = Console.ReadLine();
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books WHERE Author LIKE @author";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@author", "%" + author + "%");
        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows)
            Console.WriteLine("Книги не найдены.");
        else
            while (reader.Read())
                PrintBook(reader);
    }

    static void SearchByGenre()
    {
        Console.Write("Введите жанр: ");
        string genre = Console.ReadLine();
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books WHERE Genre LIKE @genre";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@genre", "%" + genre + "%");
        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows)
            Console.WriteLine("Книги не найдены.");
        else
            while (reader.Read())
                PrintBook(reader);
    }

    static void SearchByYear()
    {
        Console.Write("Введите год публикации: ");
        int year = int.Parse(Console.ReadLine());
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books WHERE PublicationYear=@year";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@year", year);
        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows)
            Console.WriteLine("Книги не найдены.");
        else
            while (reader.Read())
                PrintBook(reader);
    }

    static void FilterByPrice()
    {
        Console.Write("Введите максимальную цену: ");
        decimal price = decimal.Parse(Console.ReadLine());
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT * FROM Books WHERE Price < @price";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@price", price);
        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows)
            Console.WriteLine("Книги не найдены.");
        else
            while (reader.Read())
                PrintBook(reader);
    }

    static void AverageRatingByGenre()
    {
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT Genre, AVG(Rating) as AvgRating FROM Books GROUP BY Genre";
        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Средний рейтинг по жанрам:");
        while (reader.Read())
        {
            Console.WriteLine($"Жанр: {reader["Genre"]}, Средний рейтинг: {Convert.ToDouble(reader["AvgRating"]):F2}");
        }
    }

    static void TotalPagesAvailable()
    {
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sql = "SELECT SUM(Pages) as TotalPages FROM Books WHERE IsAvailable=1";
        using var cmd = new SqliteCommand(sql, conn);
        var res = cmd.ExecuteScalar();
        int total = res != DBNull.Value ? Convert.ToInt32(res) : 0;
        Console.WriteLine($"Общее количество страниц во всех доступных книгах: {total}");
    }

    static void MaxMinPriceBooks()
    {
        using var conn = new SqliteConnection(connStr);
        conn.Open();
        string sqlMax = "SELECT * FROM Books ORDER BY Price DESC LIMIT 1";
        string sqlMin = "SELECT * FROM Books ORDER BY Price ASC LIMIT 1";
        using var cmdMax = new SqliteCommand(sqlMax, conn);
        using var cmdMin = new SqliteCommand(sqlMin, conn);
        using var readerMax = cmdMax.ExecuteReader();
        Console.WriteLine("Самая дорогая книга:");
        if (readerMax.Read()) PrintBook(readerMax);
        readerMax.Close();
        using var readerMin = cmdMin.ExecuteReader();
        Console.WriteLine("Самая дешевая книга:");
        if (readerMin.Read()) PrintBook(readerMin);
    }

    static void PrintBook(IDataRecord r)
    {
        Console.WriteLine($"\nId: {r["Id"]}\nНазвание: {r["Title"]}\nАвтор: {r["Author"]}\nЖанр: {r["Genre"]}\nГод: {r["PublicationYear"]}\nСтраниц: {r["Pages"]}\nЦена: {r["Price"]}\nДоступна: {(Convert.ToInt32(r["IsAvailable"]) == 1 ? "да" : "нет")}\nРейтинг: {r["Rating"]}\nОписание: {r["Description"]}");
    }
}
