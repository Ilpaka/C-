using System;

namespace ServerMonitoringApp
{
    public class ServerMonitor
    {
        public event Action<string> ServerDown;
        private readonly Random _random = new Random();

        public void CheckServerStatus(string serverName)
        {
            if (_random.Next(0, 4) == 0)
                ServerDown?.Invoke(serverName);
        }
    }

    class Program
    {
        static void Main()
        {
            var monitor = new ServerMonitor();
            monitor.ServerDown += SendEmail;
            monitor.ServerDown += SendSms;

            const string serverName = "MainServer";
            for (int i = 0; i < 10; i++)
                monitor.CheckServerStatus(serverName);
        }

        static void SendEmail(string server)
            => Console.WriteLine($"[EMAIL] Сервис {server} недоступен!");

        static void SendSms(string server)
            => Console.WriteLine($"[SMS] Сервис {server} недоступен!");
    }
}
