using EmedicineBE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EmedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsersDAL _dal;

        public UsersController(IConfiguration configuration, UsersDAL dal)
        {
            _configuration = configuration;
            _dal = dal;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] Users users)
        {
            try
            {
                var response = await _dal.RegisterAsync(users);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during registration: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users users)
        {
            try
            {
                var response = await _dal.LoginAsync(users);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during login: {ex.Message}");
            }
        }

        [HttpGet("viewuser")]
        public async Task<IActionResult> ViewUser([FromBody] Users users)
        {
            try
            {
                var response = await _dal.ViewUserAsync(users);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during user viewing: {ex.Message}");
            }
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody] Users users)
        {
            try
            {
                var response = await _dal.UpdateUserAsync(users);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during user updating: {ex.Message}");
            }
        }

        [HttpPost("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromBody] Users users)
        {
            try
            {
                var response = await _dal.DeleteUserAsync(users);

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during user deletion: {ex.Message}");
            }
        }

        [HttpGet("viewallusers")]
        public async Task<IActionResult> ViewAllUsersAsync()
        {
            try
            {
                var response = await _dal.ViewAllUsersAsync();

                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 status code
                return StatusCode(500, $"An error occurred during user viewing: {ex.Message}");
            }
        }

    }
}