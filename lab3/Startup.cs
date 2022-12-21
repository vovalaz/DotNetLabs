using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace lab_3
{
    public class Startup
    {

        private readonly string _connectionString;

        private readonly string _getPassengersIds;
        private readonly string _getCarsIds;
        private readonly string _getTrainsIds;
        private readonly string _getStationsIds;
        private readonly string _getTicketsIds;


        private string _passengers;
        private string _trains;
        private string _stations;
        private string _tickets;
        private string _carsWithTrains;

        public Startup(IHostEnvironment env)
        {
            _connectionString = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build()
                .GetSection("ConnectionStrings:DefaultConnection").Value;
            _getPassengersIds = "SELECT DISTINCT [PassengerId] FROM [Booking].[dbo].[Passangers]";
            _getTrainsIds = "SELECT DISTINCT [TrainId] FROM [Booking].[dbo].[Trains]";
            _getStationsIds = "SELECT DISTINCT [StationId] FROM [Booking].[dbo].[Stations]";
            _getCarsIds = "SELECT DISTINCT [CarId] FROM [Booking].[dbo].[Cars]";
            _getTicketsIds = "SELECT DISTINCT [TicketId] FROM [Booking].[dbo].[Tickets]";
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHsts();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync(Index());
                });
            });
        }

        private string Index()
        {
            var connection = new SqlConnection(_connectionString);
            var result = new System.Text.StringBuilder();
            try
            {
                connection.Open();

                /*
                 * passengers
                 * */
                var commandPassengers = new SqlCommand(_getPassengersIds, connection);
                var dataPassengers = commandPassengers.ExecuteReader();

                List<object> passengersIds = new List<object>();
                while (dataPassengers.Read())
                {
                    passengersIds.Add(GetRow(dataPassengers));
                }

                result.Append("<html><head><meta charset='utf-8'>" +
                    "<link rel='stylesheet' href='./styles.css'>" +
                    "</head><body>");
                result.Append("<div class='positioned' >" +
                    "<img src='./ukrzaliznitsa.png' " +
                    "height=200'/>" + 
                    "</div>");

                result.Append("<div class='container'>" +
                    "<h2>The list of passengers</h2>");

                result.Append("<ul class='responsive-table'>" +
                "<li class='table-header'>" +
                  "<div class='col col-1'>ID</div>" +
                  "<div class='col col-2'>Full Name</div>" +
                  "<div class='col col-3'>Address</div>" +
                  "<div class='col col-4'>Phone</div>" +
                "</li>");

                foreach (var passengerId in passengersIds)
                {

                   _passengers = "SELECT " +
                        "[PassengerId]," +
                        "[FullName]," +
                        "[Address]," +
                        "[Phone] " +
                        "FROM " +
                        "[Booking].[dbo].[Passangers] " +
                          $"WHERE PassengerId = '{passengerId}'";

                    SqlCommand command = new SqlCommand(_passengers, connection);
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        result.Append(string.Format(
                            "<li class='table-row'>" +
                  "<div class='col col-1' data-label='PassengerId'>{0}</div>" +
                  "<div class='col col-2' data-label='FullName'>{1}</div>" +
                  "<div class='col col-3' data-label='Address'>{2}</div>" +
                  "<div class='col col-4' data-label='Phone'>{3}</div>" +
                             " </li>",
                            reader["PassengerId"].ToString(),
                            reader["FullName"].ToString(), 
                            reader["Address"].ToString(),
                            reader["Phone"].ToString()));
                    }
                }
                result.Append("</ul>");


                /*
        * trains
        * */
                var commandTrains = new SqlCommand(_getTrainsIds, connection);
                var dataTrains = commandTrains.ExecuteReader();

                List<object> trainsIds = new List<object>();
                while (dataTrains.Read())
                {
                    trainsIds.Add(GetRow(dataTrains));
                }

                result.Append("<div class='container'>" +
                    "<h2>The list of trains</h2>");

                result.Append("<ul class='responsive-table'>" +
                "<li class='table-header'>" +
                  "<div class='col col-1'>ID</div>" +
                  "<div class='col col-2'>Train</div>" +
                  "<div class='col col-3'>Train Type</div>" +
                "</li>");

                foreach (var trainId in trainsIds)
                {

                    _trains = "SELECT " +
                         "[TrainId]," +
                         "[Train]," +
                         "[TrainType] " +
                         "FROM " +
                         "[Booking].[dbo].[Trains] " +
                           $"WHERE TrainId = '{trainId}'";

                    SqlCommand command = new SqlCommand(_trains, connection);
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        result.Append(string.Format(
                            "<li class='table-row'>" +
                  "<div class='col col-1' data-label='Train Id'>{0}</div>" +
                  "<div class='col col-2' data-label='Train'>{1}</div>" +
                  "<div class='col col-3' data-label='Train Type'>{2}</div>" +
                             " </li>",
                            reader["TrainId"].ToString(),
                            reader["Train"].ToString(),
                            reader["TrainType"].ToString()));
                    }
                }
                result.Append("</ul>");


                /*
        * stations
        * */


                var commandStations = new SqlCommand(_getStationsIds, connection);
                var dataStations = commandStations.ExecuteReader();

                List<object> stationsIds = new List<object>();

                while (dataStations.Read())
                {
                    stationsIds.Add(GetRow(dataStations));
                }

                result.Append("<div class='container'>" +
                    "<h2>The list of stations</h2>");

                result.Append("<ul class='responsive-table'>" +
                "<li class='table-header'>" +
                  "<div class='col col-1'>ID</div>" +
                  "<div class='col col-2'>Station</div>" +
                  "<div class='col col-3'>Distance</div>" +
                  "<div class='col col-4'>Cost</div>" +
                "</li>");

                foreach (var stationId in stationsIds)
                {

                    _stations = "SELECT " +
                "[StationId]," +
                        "[Station]," +
                "[Distance]," +
                "[Cost]" +
                "FROM " +
                "[Booking].[dbo].[Stations] " +
                $"WHERE StationId = '{stationId}'";


                    SqlCommand command = new SqlCommand(_stations, connection);
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        result.Append(string.Format(
                            "<li class='table-row'>" +
                  "<div class='col col-1' data-label='Station Id'>{0}</div>" +
                  "<div class='col col-2' data-label='Station'>{1}</div>" +
                  "<div class='col col-3' data-label='Distance'>{2}</div>" +
                  "<div class='col col-4' data-label='Cost'>{3}</div>" +
                             " </li>",
                            reader["StationId"].ToString(),
                            reader["Station"].ToString(),
                            reader["Distance"].ToString(),
                            reader["Cost"].ToString()
                            ));
                    }
                }
                result.Append("</ul>");

                /*
               * cars
               * */


                var commandCars = new SqlCommand(_getCarsIds, connection);
                var dataCars = commandCars.ExecuteReader();

                List<object> carsIds = new List<object>();

                while (dataCars.Read())
                {
                    carsIds.Add(GetRow(dataCars));
                }

                result.Append("<div class='container'>" +
                    "<h2>The list of cars</h2>");

                result.Append("<ul class='responsive-table'>" +
                "<li class='table-header'>" +
                  "<div class='col col-1'>ID</div>" +
                  "<div class='col col-2'>Car</div>" +
                  "<div class='col col-3'>Train</div>" +
                  "<div class='col col-4'>Car Type</div>" +
                  "<div class='col col-4'>Extra Car Fee</div>" +
                "</li>");

                foreach (var carId in carsIds)
                {

                    _carsWithTrains = "SELECT " +
                "[CarId], " +
                "[Car], " +
                "[Trains].[Train], " +
                "[CarType], " +
                "[ExtraCarFee]" +
                " FROM " +
                "[Booking].[dbo].[Cars] " +
                "JOIN [Booking].[dbo].[Trains] " +
                "ON [Cars].[TrainId] = [Trains].[TrainId] " +
                $"WHERE CarId = '{carId}'";

                    SqlCommand command = new SqlCommand(_carsWithTrains, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Append(string.Format(
                            "<li class='table-row'>" +
                  "<div class='col col-1' data-label='Car Id'>{0}</div>" +
                  "<div class='col col-2' data-label='Car'>{1}</div>" +
                  "<div class='col col-3' data-label='Train'>{2}</div>" +
                  "<div class='col col-4' data-label='Car Type'>{3}</div>" +
                  "<div class='col col-4' data-label='ExtraCarFee'>{4}</div>" +

                             " </li>",
                            reader["CarId"].ToString(),
                            reader["Car"].ToString(),
                            reader["Train"].ToString(),
                            reader["CarType"].ToString(),
                            reader["ExtraCarFee"].ToString()
                            ));
                    }
                }

                result.Append("</ul>");

                /*
               * tickets
               * */


                var commandTickets = new SqlCommand(_getTicketsIds, connection);
                var dataTickets = commandTickets.ExecuteReader();

                List<object> ticketsIds = new List<object>();

                while (dataTickets.Read())
                {
                    ticketsIds.Add(GetRow(dataTickets));
                }

                result.Append("<div class='container'>" +
                    "<h2>The list of tickets</h2>");

                result.Append("<ul class='responsive-table'>" +
                "<li class='table-header'>" +
                  "<div class='col col-1'>ID</div>" +
                  "<div class='col col-2'>Passenger</div>" +
                  "<div class='col col-3'>Train</div>" +
                  "<div class='col col-4'>Car</div>" +
                  "<div class='col col-4'>Departure Time</div>" +
                  "<div class='col col-4'>Arrival Time</div>" +
                  "<div class='col col-4'>Statuon</div>" +
                  "<div class='col col-4'>Extra Time Fee</div>" +
                "</li>");

                foreach (var ticketId in ticketsIds)
                {

                    _tickets = "SELECT " +
                "[TicketId], " +
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


                    SqlCommand command = new SqlCommand(_tickets, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Append(string.Format(
                            "<li class='table-row'>" +
                  "<div class='col col-1' data-label='Ticket Id'>{0}</div>" +
                  "<div class='col col-2' data-label='Passenger'>{1}</div>" +
                  "<div class='col col-3' data-label='Train'>{2}</div>" +
                  "<div class='col col-4' data-label='Car'>{3}</div>" +
                  "<div class='col col-4' data-label='Departure DateTime'>{4}</div>" +
                  "<div class='col col-4' data-label='Arrival Time'>{5}</div>" +
                  "<div class='col col-4' data-label='Station'>{6}</div>" +
                  "<div class='col col-4' data-label='Extra Time Fee'>{7}</div>" +

                             " </li>",
                            reader["TicketId"].ToString(),
                            reader["FullName"].ToString(),
                            reader["Train"].ToString(),
                            reader["Car"].ToString(),
                            reader["DepartureDate"].ToString()
                            .Remove(reader["DepartureDate"].ToString().Length - 7)
                            + " " + reader["DepartureTime"].ToString(),
                            reader["ArrivalTime"].ToString(),
                            reader["Station"].ToString(),
                            reader["ExtraTimeFee"].ToString()
                            ));
                    }
                }



                result.Append("</ul></body></html>");



            }
            catch (Exception ex)
            {
                result.Clear();
                result.Append($"<b>{ex.Message}</b>");
            }
            finally
            {
                connection.Close();
            }
            return result.ToString();
        }
        private static void ReadSingleRow(IDataRecord record, int countOfColumn)
        {
            for (int i = 0; i < countOfColumn - 1; i++)
            {
                Console.Write($"{record[i]}, ");
            }
            Console.Write($"{record[countOfColumn - 1]}\n");
        }

        private static string GetRow(IDataRecord record) => record[0].ToString();
    }
}
