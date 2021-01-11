using System;
using System.Collections.Generic;
using System.Linq;
using my_new_app.Infrastructure;
using Microsoft.EntityFrameworkCore;
using my_new_app.Infrastructure;
namespace my_new_app.Repositories

{
    public class FlightRepository
    {
        Models.FlightContext _db;
        public FlightRepository(Models.FlightContext db)
        {
            _db = db;
        }

        /// <summary>
        /// جستجوی پرواز ها براساس مبدا مقصد و تاریخ 
        /// قاعدتا برای امروز به بعد را می آورد
        /// </summary>
        /// <param name="SourceId"></param>
        /// <param name="DestinationId"></param>
        /// <returns></returns>
        public List<ViewModel.FlightVM> GetFlights(int sourceId, int destinationId,int count , string date)
        {
            return _db.Flights.Include(p => p.Source).Include(p => p.Destination)
            .Where(p => p.DestinationId == destinationId && p.SourceId == sourceId && p.Date >= DateTime.Now && p.Date.Date == date.ToDatetime().Date && p.Quantity>=count)
            .Select(q => new ViewModel.FlightVM(q)).ToList();
        }
        public Models.Flight GetFlightById(int id)
        {
            return _db.Flights.Find(id);
        }

        public int ReserveFlight(int userId, int flightId, List<ViewModel.ReseveFlightVM> model)
        {

            using (var tran = _db.Database.BeginTransaction())
            {

                Models.Reserve r = new Models.Reserve()
                {
                    ReserveDate = DateTime.Now,
                    UserId = userId,

                };
                _db.Reserves.Add(r);
                _db.SaveChanges();
                try
                {

                    foreach (var item in model)
                    {
                        //به ازای هر یک از اطلاعات مسافران اگر آیدی یا کد ملی وجود داشته باشد همان را از دیتابیس می گیریم
                        // در صورتی که اطلاعات در دیتابیس وجود نداشته باشد آنرا میسازیم 
                        var passenger = _db.Passengers.FirstOrDefault(p => p.NationalityCode == item.NationalityCode || p.Id == item.Id);


                        if (passenger == null)
                        {
                            passenger = new Models.Passenger()
                            {
                                Age = item.Age,
                                NationalityCode = item.NationalityCode,
                                CreateDate = DateTime.Now,
                                FullName = item.FullName
                            };
                            _db.Passengers.Add(passenger);

                            _db.SaveChanges();
                        }

                        int? maxCod = _db.Tickets.Any() ? _db.Tickets.Max(p => p.Code) : 0;
                        Models.Ticket t = new Models.Ticket()
                        {
                            PassengerId = passenger.Id,
                            Code = maxCod.HasValue ? maxCod.Value + 1 : 1,
                            ReseveId = r.Id,
                            Price = 1000,
                            FlightId = flightId
                        };
                        _db.Tickets.Add(t);
                        _db.SaveChanges();
                    }

                    tran.Commit();

                    return r.Id;
                }
                catch (System.Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

        }

        public IList<Models.Ticket> GetTickets(int reserveId)
        {
            return _db.Tickets.Where(r => r.ReseveId == reserveId).ToList();
        }
        public ViewModel.ReseveTicketsVM GetReserveTickets(int reserveId)
        {
            var tickets = _db.Tickets.Where(r => r.ReseveId == reserveId)
            .Select(p => new ViewModel.TicketsVM()
            {
                Destination = p.Flight.Destination.ToString(),
                Source = p.Flight.Source.ToString(),
                flightDate = p.Flight.Date.ToPersianDateString(),
                FullName = p.Passenger.FullName,
                Price = p.Flight.GetPrice(),
                TicketCode = p.Flight.Title
            }).ToList();

            ViewModel.ReseveTicketsVM result  = _db.Reserves.Where(p=>p.Id==reserveId).Select(p=> new ViewModel.ReseveTicketsVM()
            {
                ReserveId = p.Id,
                userName = p.User.FirstName +" "+p.User.LastName,
                ReseveDate = p.ReserveDate.ToPersianDateString(),
                Tickets = tickets
            }).FirstOrDefault();
            
            
            return result;
        }

        public List<Models.AirPort> GetAirPorts()
        {
            return _db.AirPorts.ToList();
        }
        
        public ViewModel.FlightVM GetFlightVM(int id)
        {
             return _db.Flights.Include(p=>p.Source).Include(p=>p.Destination).Where(p=>p.Id ==id).Select(p=>new ViewModel.FlightVM(p)).FirstOrDefault();
        }
    }
}