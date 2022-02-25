using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterApp.Models.User;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Exceptions;

namespace TwitterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private IRegisterService _registerService;


        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel userRegister)
        {
            try
            {
                _registerService.Register(userRegister);
                return StatusCode(StatusCodes.Status201Created, "You have been successfully registered!");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("google")]
        public IActionResult RegisterWithGoogle([FromBody] GoogleRegisterModel userRegister)
        {
            try
            {
                _registerService.RegisterWithGoogle(userRegister);
                return StatusCode(StatusCodes.Status201Created, "You have been successfully registered!");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
