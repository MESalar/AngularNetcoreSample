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
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        private readonly Repositories.UserRepository _userRepo;

        public UserController(ILogger<UserController> logger, Repositories.UserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpPost("Login")]
        public ActionResult Login(ViewModel.loginVM model)
        {
            ResponseModel<ViewModel.loginResponseVM> result = new ResponseModel<ViewModel.loginResponseVM>();
            try
            {
                var token = _userRepo.Login(model.NationalityCode, model.Password);
                // if (!token.HasValue)
                //     {
                //         return NotFound();
                //     }
                result.Data = token;
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("error in register",ex);
                return StatusCode(500);
            }

        }
        [HttpPost("Register")]
        public ActionResult Register([FromBody] ViewModel.RegisterVM model)
        {
          ResponseModel<ViewModel.loginResponseVM> result = new ResponseModel<ViewModel.loginResponseVM>();

            try
            {
                int userId = _userRepo.Register(model.Name, model.lastName, model.NationalityCode,model.Password);
                var token = _userRepo.Login(model.NationalityCode,model .Password);
                if (token == null)
                    return Forbid();
                result.Data = token;
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("error in register",ex);
                return StatusCode(500);
            }

        }
        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
    }
}
