using System;
using System.Data;
using Npgsql;

class Program
{
    static string connStr = "Host=localhost;Username=postgres;Password=;Database=electronics_store";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить продукт");
            Console.WriteLine("2. Обновить продукт");
            Console.WriteLine("3. Удалить продукт");
            Console.WriteLine("4. Добавить категорию");
            Console.WriteLine("5. Добавить клиента");
            Console.WriteLine("6. Добавить заказ");
            Console.WriteLine("7. Поиск заказов по email клиента");
            Console.WriteLine("8. Фильтрация продуктов по цене");
            Console.WriteLine("9. Сумма заказа");
            Console.WriteLine("10. Средняя цена по категориям");
            Console.WriteLine("11. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddProduct(); break;
                case "2": UpdateProduct(); break;
                case "3": DeleteProduct(); break;
                case "4": AddCategory(); break;
                case "5": AddCustomer(); break;
                case "6": AddOrder(); break;
                case "7": FindOrdersByEmail(); break;
                case "8": FilterProductsByPrice(); break;
                case "9": OrderTotal(); break;
                case "10": AvgPriceByCategory(); break;
                case "11": return;
                default: Console.WriteLine("Некорректный выбор."); break;
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine();
        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Id категории: ");
        int catId = int.Parse(Console.ReadLine());
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "INSERT INTO Products (Name, Price, CategoryId) VALUES (@n, @p, @c)";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("n", name);
        cmd.Parameters.AddWithValue("p", price);
        cmd.Parameters.AddWithValue("c", catId);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Продукт добавлен!");
    }

    static void UpdateProduct()
    {
        Console.Write("Id продукта: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Новое название: ");
        string name = Console.ReadLine();
        Console.Write("Новая цена: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Id категории: ");
        int catId = int.Parse(Console.ReadLine());
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "UPDATE Products SET Name=@n, Price=@p, CategoryId=@c WHERE Id=@id";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("n", name);
        cmd.Parameters.AddWithValue("p", price);
        cmd.Parameters.AddWithValue("c", catId);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Продукт обновлён!");
    }

    static void DeleteProduct()
    {
        Console.Write("Id продукта: ");
        int id = int.Parse(Console.ReadLine());
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "DELETE FROM Products WHERE Id=@id";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Продукт удалён!");
    }

    static void AddCategory()
    {
        Console.Write("Название категории: ");
        string name = Console.ReadLine();
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "INSERT INTO Categories (Name) VALUES (@n)";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("n", name);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Категория добавлена!");
    }

    static void AddCustomer()
    {
        Console.Write("Имя: ");
        string first = Console.ReadLine();
        Console.Write("Фамилия: ");
        string last = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "INSERT INTO Customers (FirstName, LastName, Email) VALUES (@f, @l, @e)";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("f", first);
        cmd.Parameters.AddWithValue("l", last);
        cmd.Parameters.AddWithValue("e", email);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Клиент добавлен!");
    }

    static void AddOrder()
    {
        Console.Write("Id клиента: ");
        int custId = int.Parse(Console.ReadLine());
        Console.Write("Статус заказа: ");
        string status = Console.ReadLine();
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "INSERT INTO Orders (Date, CustomerId, Status) VALUES (@d, @c, @s) RETURNING Id";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("d", DateTime.Now);
        cmd.Parameters.AddWithValue("c", custId);
        cmd.Parameters.AddWithValue("s", status);
        int orderId = (int)cmd.ExecuteScalar();

        Console.Write("Сколько товаров в заказе? ");
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Id продукта {i + 1}: ");
            int prodId = int.Parse(Console.ReadLine());
            Console.Write("Количество: ");
            int qty = int.Parse(Console.ReadLine());
            string sqlDet = "INSERT INTO OrderDetails (OrderId, ProductId, Quantity) VALUES (@o, @p, @q)";
            using var cmdDet = new NpgsqlCommand(sqlDet, conn);
            cmdDet.Parameters.AddWithValue("o", orderId);
            cmdDet.Parameters.AddWithValue("p", prodId);
            cmdDet.Parameters.AddWithValue("q", qty);
            cmdDet.ExecuteNonQuery();
        }
        Console.WriteLine("Заказ добавлен!");
    }

    static void FindOrdersByEmail()
    {
        Console.Write("Email клиента: ");
        string email = Console.ReadLine();
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = @"
SELECT o.Id, o.Date, o.Status 
FROM Orders o
JOIN Customers c ON o.CustomerId = c.Id
WHERE c.Email = @e";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("e", email);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Заказы клиента:");
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader["Id"]}, Дата: {reader["Date"]}, Статус: {reader["Status"]}");
        }
    }

    static void FilterProductsByPrice()
    {
        Console.Write("Максимальная цена: ");
        decimal price = decimal.Parse(Console.ReadLine());
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = "SELECT Id, Name, Price FROM Products WHERE Price < @p";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("p", price);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Товары дешевле " + price + ":");
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader["Id"]}, Название: {reader["Name"]}, Цена: {reader["Price"]}");
        }
    }

    static void OrderTotal()
    {
        Console.Write("Id заказа: ");
        int orderId = int.Parse(Console.ReadLine());
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = @"
SELECT SUM(od.Quantity * p.Price) AS Total
FROM OrderDetails od
JOIN Products p ON od.ProductId = p.Id
WHERE od.OrderId = @o";
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("o", orderId);
        var total = cmd.ExecuteScalar();
        Console.WriteLine("Сумма заказа: " + (total == DBNull.Value ? 0 : total));
    }

    static void AvgPriceByCategory()
    {
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();
        string sql = @"
SELECT c.Name, AVG(p.Price) AS AvgPrice
FROM Products p
JOIN Categories c ON p.CategoryId = c.Id
GROUP BY c.Name";
        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Средняя цена по категориям:");
        while (reader.Read())
        {
            Console.WriteLine($"Категория: {reader["Name"]}, Средняя цена: {reader["AvgPrice"]:0.00}");
        }
    }
}
