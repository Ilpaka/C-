using System;

namespace DeliveryApp
{
    public interface IDeliveryStrategy
    {
        int Calculate(int orderAmount, int distance);
    }

    public class FixedCostStrategy : IDeliveryStrategy
    {
        private readonly int _fixedCost;
        public FixedCostStrategy(int fixedCost) => _fixedCost = fixedCost;
        public int Calculate(int orderAmount, int distance) => _fixedCost;
    }

    public class PercentageCostStrategy : IDeliveryStrategy
    {
        private readonly int _percentage;
        public PercentageCostStrategy(int percentage) => _percentage = percentage;
        public int Calculate(int orderAmount, int distance) => orderAmount * _percentage / 100;
    }

    public class DistanceBasedStrategy : IDeliveryStrategy
    {
        private readonly int _ratePerKm;
        public DistanceBasedStrategy(int ratePerKm) => _ratePerKm = ratePerKm;
        public int Calculate(int orderAmount, int distance) => distance * _ratePerKm;
    }

    public class DeliveryCalculator
    {
        private IDeliveryStrategy _strategy;
        public void SetStrategy(IDeliveryStrategy strategy) => _strategy = strategy;
        public int CalculateDelivery(int orderAmount, int distance) => _strategy.Calculate(orderAmount, distance);
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Выберите способ расчета доставки:");
            Console.WriteLine("1 - Фиксированная стоимость");
            Console.WriteLine("2 - Процент от стоимости");
            Console.WriteLine("3 - Зависимость от расстояния");
            var choice = Console.ReadLine();

            Console.Write("Введите стоимость заказа: ");
            var orderAmount = int.Parse(Console.ReadLine());
            Console.Write("Введите расстояние (если применимо): ");
            var distance = int.Parse(Console.ReadLine());

            var calculator = new DeliveryCalculator();

            switch (choice)
            {
                case "1":
                    calculator.SetStrategy(new FixedCostStrategy(100));
                    break;
                case "2":
                    calculator.SetStrategy(new PercentageCostStrategy(10));
                    break;
                case "3":
                    calculator.SetStrategy(new DistanceBasedStrategy(5));
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }

            var deliveryCost = calculator.CalculateDelivery(orderAmount, distance);
            Console.WriteLine($"Итоговая стоимость доставки: {deliveryCost}");
        }
    }
}
