using System;

namespace TravelAgency
{
    public abstract class Reservation
    {
        public Guid ReservationID { get; private set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        protected Reservation(string customerName, DateTime start, DateTime end)
        {
            CustomerName = customerName;
            StartDate = start;
            EndDate = end;
        }

        public abstract int CalculatePrice() {
        };

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"ID бронирвоания: {ReservationID}");
            Console.WriteLine($"Покупатель: {CustomerName}");
            Console.WriteLine($"Даты заезда: {StartDate:d} To: {EndDate:d}");
        }
    }

    public class HotelReservation : Reservation
    {
        public string RoomType { get; set; }
        public string MealPlan { get; set; }
        private const int PricePerNight = 100000;

        public HotelReservation(string customerName, DateTime start, DateTime end,
                                string roomType, string mealPlan)
            : base(customerName, start, end) => (RoomType, MealPlan) = (roomType, mealPlan);

        public int CalculatePrice()
        {
            return (EndDate - StartDate) * PricePerNight;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Тип: Номер: {RoomType} | План питания: {MealPlan} | Цена: {CalculatePrice()}");
        }
    }

    // Бронирование авиаперелета
    public class FlightReservation : Reservation
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }

        private const int ForPeople = 100000;

        public FlightReservation(string customerName, DateTime start, DateTime end,
                                 string from, string to)
            : base(customerName, start, end)
        {
            DepartureAirport = from;
            ArrivalAirport = to;
        }

        public override int CalculatePrice()
        {
            return ForPeople * 10;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Тип полёта, From: {DepartureAirport}, To: {ArrivalAirport}");
            Console.WriteLine($"Итоговая цена: {CalculatePrice()}");
        }
    }

    // Бронирование проката автомобиля
    public class CarRentalReservation : Reservation
    {
        public string CarType { get; set; }
        public string InsuranceOptions { get; set; }
        private const int PerDay = 4000;

        public CarRentalReservation(string customerName, DateTime start, DateTime end,
                                    string carType, string insurance)
            : base(customerName, start, end)
        {
            CarType = carType;
            InsuranceOptions = insurance;
        }

        public override int CalculatePrice()
        {

            return (EndDate - StartDate) * PerDay;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Тип машины: {CarType}, Страховка: {InsuranceOptions}");
            Console.WriteLine($"Итоговая цена: {CalculatePrice()}");
        }
    }

    public class BookingSystem
    {
        private readonly List<Reservation> reservations = new List<Reservation>();

        public Reservation CreateReservation(string reservationType)
        {
            Console.Write("Введите имя клиента: ");
            var name = Console.ReadLine();
            Console.Write("Введите дату начала (yyyy-MM-dd): ");
            var start = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите дату окончания (yyyy-MM-dd): ");
            var end = DateTime.Parse(Console.ReadLine());

            Reservation reservation = reservationType.ToLower() switch
            {
                "hotel" => CreateHotel(name, start, end),
                "flight" => CreateFlight(name, start, end),
                "car" => CreateCar(name, start, end),
            };

            reservations.Add(reservation);
            return reservation;

            #параметры c, s и e — просто краткие имена для: c → customerName (имя клиента) s → start (дата начала бронирования) e → end (дата окончания бронирования)

            HotelReservation CreateHotel(string c, DateTime s, DateTime e)
            {
                Console.Write("Тип номера: "); var room = Console.ReadLine();
                Console.Write("План питания: "); var meal = Console.ReadLine();
                return new HotelReservation(c, s, e, room, meal);
            }

            FlightReservation CreateFlight(string c, DateTime s, DateTime e)
            {
                Console.Write("Аэропорт отправления: "); var from = Console.ReadLine();
                Console.Write("Аэропорт прибытия: "); var to = Console.ReadLine();
                return new FlightReservation(c, s, e, from, to);
            }

            CarRentalReservation CreateCar(string c, DateTime s, DateTime e)
            {
                Console.Write("Тип автомобиля: "); var carType = Console.ReadLine();
                Console.Write("Опции страховки: "); var ins = Console.ReadLine();
                return new CarRentalReservation(c, s, e, carType, ins);
            }
        }

        // Отменяет бронирование по идентификатору
        public bool CancelReservation(Guid reservationID) { }

        // Возвращает суммарную стоимость всех бронирований
        public int GetTotalBookingValue()
        {
            int total = 0;
            foreach (var r in _reservations)
                total += r.CalculatePrice();
            return total;
        }
    }

}