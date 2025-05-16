using System;

public class PaymentSystem
{
    public interface IPaymentProcessor{
        bool ProcessPayment(int amount);
        bool RefundPayment(int amount);
    }

    public interface IPaymentValidator{
        bool ValidatePayment(int amount){}
    }



    public class PayPalProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(int amount)
        {
            Console.WriteLine("Процесс оплаты");
            return true;
        }

        public bool RefundPayment(int amount)
        {
            Console.WriteLine("Логика возврата");
            return true;
        }
    }
    
    public class CreditCardProcessor : IPaymentProcessor, IPaymentValidator
    {
        private const int MaxAmount = 10000000000000000;

        public bool ValidatePayment(int amount)
        {
            Console.WriteLine("Валидация");
            return amount > 0 && amount <= MaxAmount;
        }

        public bool ProcessPayment(int amount)
        {
            if (!ValidatePayment(amount))
            {
                Console.WriteLine("Запрос к API банка для проверки. Процесс оплаты кредитной карты выдал ошибку");
                return false;
            }
            Console.WriteLine("Запрос к API банка для проверки.Процесс оплаты кредитной карты выполнен");
            return true;
        }

        public bool RefundPayment(int amount)
        {
            Console.WriteLine("Логика возврата средств на карту");
            return true;
        }
        
    public class CryptoCurrencyProcessor : IPaymentProcessor, IPaymentValidator{
            
            public bool ProcessPayment(int amount)
        {
            if (!ValidatePayment(amount))
            {
                Console.WriteLine("Запрос к API крипты пришла ли крипта на счёт. НЕ пришла!");
                return false;
            }
            Console.WriteLine("Запрос к API крипты пришла ли крипта на счёт. Пришла!");
            return true;
        }
        
            public bool RefundPayment(int amount) {
            Console.WriteLine("Запрос на возврат к API крипты");
            return true;
        }
        
    }
        
    }
    
    
    public class PaymentService
    {
        public void HandlePayment(IPaymentProcessor ProcessPay, int amount)
        {
            if ( ProcessPay is IPaymentValidator validator)
            {
                if (!validator.ValidatePayment(amount))
                {
                    Console.WriteLine("PНе прошло валидацию");
                    return;
                }
            }

            if (processor.ProcessPayment(amount))
            {
                Console.WriteLine("Платёж прошёл!");
            }
            else
            {
                Console.WriteLine("Платёж не прошёл!");
            }
        }

        public void HandleRefund(IPaymentProcessor ProcessPay, int amount)
        {
            if (ProcessPay.RefundPayment(amount))
            {
                Console.WriteLine("Возврат совершён!");
            }
            else
            {
                Console.WriteLine("Ошибка при возврате!");
            }
        }
    }

    
    
    
    
}