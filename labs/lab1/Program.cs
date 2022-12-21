using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.AppSettings["key1"];

            SqlConnection connection;

            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(0);
            }
            /*
             passengers
            */
            string getPassengersIds = "SELECT DISTINCT [PassengerId] FROM [Booking].[dbo].[Passangers]";

            SqlCommand commandPT = new SqlCommand(getPassengersIds, connection);
            SqlDataReader readerPT = commandPT.ExecuteReader();

            List<object> passengersIds = new List<object>();
            while(readerPT.Read())
            {
                passengersIds.Add(GetRow(readerPT));
            }

            int i = 1;
            Console.WriteLine("List of passengers:\n");
            foreach (var passengerId in passengersIds)
            {
                string passengers = "SELECT " +
                "[FullName]," +
                "[Address]," +
                "[Phone]" +
                "FROM " +
                "[Booking].[dbo].[Passangers] " +
                $"WHERE PassengerId = '{passengerId}'";

                SqlCommand command = new SqlCommand(passengers, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write($"{i++}. ");
                    ReadSingleRow(reader, 3);
                }

            }

            /*
           trains
          */
            string getTrainsIds = "SELECT DISTINCT [TrainId] FROM [Booking].[dbo].[Trains]";

            SqlCommand commandT = new SqlCommand(getTrainsIds, connection);
            SqlDataReader readerT = commandT.ExecuteReader();

            List<object> trainsIds = new List<object>();
            while (readerT.Read())
            {
                trainsIds.Add(GetRow(readerT));
            }

             i = 1;
            Console.WriteLine("List of trains:\n");
            foreach (var trainId in trainsIds)
            {
                string train = "SELECT " +
                         "[Train]," +
                         "[TrainType] " +
                         "FROM " +
                         "[Booking].[dbo].[Trains] " +
                           $"WHERE TrainId = '{trainId}'";

                SqlCommand command = new SqlCommand(train, connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Console.Write($"{i++}. ");
                    ReadSingleRow(reader, 2);
                }

            }

            /*
             Stations
            */

            string getStationIds = "SELECT DISTINCT [StationId] FROM [Booking].[dbo].[Stations]";

            SqlCommand commandST = new SqlCommand(getStationIds, connection);
            SqlDataReader readerST = commandST.ExecuteReader();

            List<object> stationsIds = new List<object>();
            while (readerST.Read())
            {
                stationsIds.Add(GetRow(readerST));
            }

             i = 1;
            Console.WriteLine("\nList of Stations:\n");
            foreach (var stationId in stationsIds)
            {
                string station = "SELECT " +
                "[Station]," +
                "[Distance]," +
                "[Cost]" +
                "FROM " +
                "[Booking].[dbo].[Stations] " +
                $"WHERE StationId = '{stationId}'";

                SqlCommand command = new SqlCommand(station, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write($"{i++}. ");
                    ReadSingleRow(reader, 3);
                }

            }

            /*
           Cars
          */

            string getCarsIds = "SELECT DISTINCT [CarId] FROM [Booking].[dbo].[Cars]";

            SqlCommand commandCars = new SqlCommand(getCarsIds, connection);
            SqlDataReader readerCars = commandCars.ExecuteReader();

            List<object> carsIds = new List<object>();
            while (readerCars.Read())
            {
                carsIds.Add(GetRow(readerCars));
            }

            i = 1;
            Console.WriteLine("\nList of Cars:\n");

            
          

            foreach (var carId in carsIds)
            {
                         
                string car = "SELECT " +
                "[Car], " +
                "[Trains].[Train], " +
                "[CarType], " +
                "[ExtraCarFee]" +
                " FROM " +
                "[Booking].[dbo].[Cars] " +
                "JOIN [Booking].[dbo].[Trains] " +
                "ON [Cars].[TrainId] = [Trains].[TrainId] " +
                $"WHERE CarId = '{carId}'";


                SqlCommand command = new SqlCommand(car, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write($"{i++}. ");
                    ReadSingleRow(reader, 4);
                }

            }



            /*
           Tickets
          */

            string getTicketsIds = "SELECT DISTINCT [TicketId] FROM [Booking].[dbo].[Tickets]";

            SqlCommand commandTickets = new SqlCommand(getTicketsIds, connection);
            SqlDataReader readerTickets = commandTickets.ExecuteReader();

            List<object> ticketsIds = new List<object>();
            while (readerTickets.Read())
            {
                ticketsIds.Add(GetRow(readerTickets));
            }

            i = 1;
            Console.WriteLine("\nList of Tickets:\n");

            foreach (var ticketId in ticketsIds)
            {

                string ticket = "SELECT " +
                "[Passangers].[FullName]," +
                "[Trains].[Train], " +
                "[Cars].[Car], " +
                "[DepartureDate], " +
                "[DepartureTime]," +
                "[ArrivalTime], " +
                "[Stations].[Station], " +
                "[ExtraTimeFee], " +
                "[Cars].[ExtraCarFee] " +
                " FROM " +
                "[Booking].[dbo].[Tickets] " +
                "JOIN [Booking].[dbo].[Trains] " +
                "ON [Tickets].[TrainId] = [Trains].[TrainId] " +

                "JOIN [Booking].[dbo].[Cars] " +
                "ON [Tickets].[CarId] = [Cars].[CarId] " +

                "JOIN [Booking].[dbo].[Stations] " +
                "ON [Tickets].[StationId] = [Stations].[StationId] " +

                "JOIN [Booking].[dbo].[Passangers] " +
                "ON [Tickets].[PassengerId] = [Passangers].[PassengerId] " +

                $"WHERE TicketId = '{ticketId}'";


                SqlCommand command = new SqlCommand(ticket, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.Write($"{i++}. ");
                    ReadSingleRow(reader, 9);
                }

            }


            connection.Close();
        }

        private static void ReadSingleRow(IDataRecord record, int countOfColumn)
        {
            for(int i = 0; i < countOfColumn - 1; i++) 
            {
                Console.Write($"{record[i]}, ");
            }
            Console.Write($"{record[countOfColumn - 1]}\n");
        }

        private static string GetRow(IDataRecord record) => record[0].ToString();
    }
}
