using System;

class Program
{
    static void Main(string[] args)
    {
        string[][] movies = {
            new string[] { "Матрица", "Интерстеллар", "Время" }, 
            new string[] { "Крёстный отец", "Казино", "Славные парни" }, 
            new string[] { "Аватар", "Человек-паук", "Железный человек" } 
        };

        string[] genres = { "Фантастика", "Криминал", "Экшн" };

        for (int i = 0; i < movies.Length; i++)
        {
            Console.WriteLine($"{genres[i]}:");
            foreach (var movie in movies[i])
            {
                Console.WriteLine($"- {movie}");
            }
            Console.WriteLine();
        }
    }
}
