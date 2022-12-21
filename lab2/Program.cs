using lab_2.EFCore;
using System;
using System.Linq;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                /*
                 passengers
                */
                var passengers = db.Passangers.ToList();
                Console.WriteLine("List of passengers:\n");

                int i = 1;
                foreach (var p in passengers)
                {
                    Console.WriteLine($"{i++}. {p.FullName}, {p.Address}, {p.Phone}" );
                }
                Console.WriteLine();

                /*
                stations
               */
                var stations = db.Stations.ToList();
                Console.WriteLine("List of stations:\n");

                 i = 1;
                foreach (var s in stations)
                {
                    Console.WriteLine($"{i++}. {s.Station}, {s.Distance}, {s.Cost}");
                }
                Console.WriteLine();

                /*
             trains
            */
                var trains = db.Trains.ToList();
                Console.WriteLine("List of trains:\n");

                i = 1;
                foreach (var tr in trains)
                {
                    Console.WriteLine($"{i++}. {tr.Train}, {tr.TrainType}");
                }
                Console.WriteLine();

                /*
          cars
         */
                var cars = db.Cars.ToList();
                Console.WriteLine("List of cars:\n");

                i = 1;
                foreach (var c in cars)
                {
                    Console.WriteLine($"{i++}. {c.Car}, {c.CarType}, {c.ExtraCarFee}");


                    var carFullInfo = db.Cars.Join(db.Trains,
                        cars => cars.TrainId,
                        trains => trains.TrainId,
                        (cars, trains) => new
                        {
                            TrainId = cars.TrainId,
                            Train = trains.Train,
                            TrainType = trains.TrainType,
                            Car = cars.Car,
                            CarType = cars.CarType,
                            ExtraCarFee = cars.ExtraCarFee
                        }
                        ).Where(cars => c.TrainId == cars.TrainId).ToList();

                   Console.WriteLine($"\tTrain: {carFullInfo[0].Train}, type: {carFullInfo[0].TrainType}");

                }
                Console.WriteLine();

                /*
        tickets
       */
                Console.WriteLine("List of tickets:\n");

                i = 1;

                var tickets = (from ticket in db.Tickets
                                 join passenger in db.Passangers on ticket.PassengerId equals passenger.PassengerId
                                 join train in db.Trains on ticket.TrainId equals train.TrainId
                                 join car in db.Cars on ticket.CarId equals car.CarId
                                 join station in db.Stations on ticket.StationId equals station.StationId

                                    select new
                                    {
                        FullName = passenger.FullName,
                        Address = passenger.Address,
                        Phone = passenger.Phone,

                        Train = train.Train,
                        TrainType = train.TrainType,

                        Car = car.Car,
                        CarType = car.CarType,
                        ExtraCarFee = car.ExtraCarFee,

                        DepartureDate = ticket.DepartureDate,
                        DepartureTime = ticket.DepartureTime,
                        ArrivalTime = ticket.ArrivalTime,

                        Station = station.Station,
                        Distance = station.Distance,
                        Cost = station.Cost,

                        ExtraTimeFee = ticket.ExtraTimeFee
                                    }).ToList();


                foreach (var t in tickets)
                {
                    Console.WriteLine($"Ticket {i++}: \n\t Passenger - {t.FullName}, {t.Address}, {t.Phone} " +
                        $"\n\t Train - {t.Train}, {t.TrainType}" +
                        $"\n\t Car - {t.Car}, {t.CarType}, {t.ExtraCarFee} grn. " +
                        $"\n\t Station - {t.Station} " +
                        $"\n\t Departure date and time - {t.DepartureDate.ToShortDateString()}," +
                        $" {t.DepartureTime.ToString()}" +
                        $"\n\t Arrival time - {t.ArrivalTime.ToString()}" +
                        $"\n\t ExtraTimeFee - {t.ExtraTimeFee} \n");
                }

                Console.WriteLine();


            }
        }
    }
}
