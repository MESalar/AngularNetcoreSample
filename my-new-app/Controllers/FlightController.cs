using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_new_app.Infrastructure.Models;

namespace my_new_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;

        private readonly Repositories.FlightRepository _flightRepo;
        private readonly Repositories.UserRepository _userRepo;

        public FlightController(ILogger<UserController> logger, Repositories.FlightRepository flightRepo, Repositories.UserRepository userRepo)
        {
            _logger = logger;
            _flightRepo = flightRepo;
            _userRepo = userRepo;
        }

        // public ActionResult Login(string NationalityCode , string Password)
        // {

        // }
        [HttpGet("Search/{sourceId}/{destinationId}/{count}/{**date}")]
        public ActionResult Search([FromRoute] int sourceId, [FromRoute] int destinationId, [FromRoute] int count, [FromRoute] string date)
        {
            ResponseModel<List<ViewModel.FlightVM>> result = new ResponseModel<List<ViewModel.FlightVM>>();

            try
            {
                var flights = _flightRepo.GetFlights(sourceId, destinationId, count, date);
                result.Data = flights;
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.Log(LogLevel.Error, "exception in Get flights", ex);
                return StatusCode(500);
            }

        }
        [TypeFilter(typeof(Infrastructure.AccessControlAttribute))]
        [HttpPost("Reserve/{flightId}")]
        public ActionResult Reserve([FromRoute] int flightId, [FromBody] List<ViewModel.ReseveFlightVM> model)
        {
            ResponseModel<int> result = new ResponseModel<int>();
            try
            {
                int userId = _userRepo.GetCurrentUserId();
                Models.Flight flight = _flightRepo.GetFlightById(flightId);
                if (flight == null)
                {
                    result.SetError("پرواز یافت نشد");
                    return Ok(result);
                }

                var reserveId = _flightRepo.ReserveFlight(userId, flightId, model);
                result.Data = reserveId;
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [TypeFilter(typeof(Infrastructure.AccessControlAttribute))]
        [HttpGet("GetTickets/{reserveId}")]
        public ActionResult GetTickets(int reserveId)
        {
            ResponseModel<ViewModel.ReseveTicketsVM> result = new ResponseModel<ViewModel.ReseveTicketsVM>();
            try
            {
                var tickets = _flightRepo.GetReserveTickets(reserveId);
                result.Data = tickets;
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("GetAirports")]
        public ActionResult GetAirports()
        {
            ResponseModel<List<ViewModel.SelectListItem>> result = new ResponseModel<List<ViewModel.SelectListItem>>();

            List<ViewModel.SelectListItem> airports = _flightRepo.GetAirPorts().Select(p => new ViewModel.SelectListItem() { Id = p.Id, Title = p.ToString() }).ToList();
            result.Data = airports;
            return Ok(result);

        }

        [TypeFilter(typeof(Infrastructure.AccessControlAttribute))]
        [HttpGet("GetFlight/{id}")]
        public ActionResult GetFlight([FromRoute] int id)
        {
            ResponseModel<string> result = new ResponseModel<string>();

            string data = _flightRepo.GetFlightVM(id).ToString();
            result.Data = data;
            return Ok(result);

        }


    }
}
