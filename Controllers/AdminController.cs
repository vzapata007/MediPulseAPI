using MediPulseAPI.Data;
using MediPulseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediPulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly AdminDAL _AdminDAL;
        private readonly OrdersDAL _OrdersDAL;

        public AdminController(IConfiguration configuration, AdminDAL dal, OrdersDAL ordersDAL)
        {
            _configuration = configuration;
            _AdminDAL = dal;
            _OrdersDAL = ordersDAL;
        }

        [HttpPost]
        [Route("AddUpdateMedicine")]
        public async Task<IActionResult> AddUpdateMedicineAsync([FromBody] Medicines medicines)
        {
            try
            {
                var response = await _AdminDAL.AddUpdateMedicineAsync(medicines);

                if (response.StatusCode == 200)
                {
                    return Ok(response); // 200 OK for successful operation
                }
                else if (response.StatusCode == 400)
                {
                    return BadRequest(response); // 400 Bad Request for client-side errors
                }
                else
                {
                    return StatusCode(500, response); // 500 Internal Server Error for other errors
                }
            }
            catch (Exception ex)
            {
                var errorResponse = new Response
                {
                    StatusCode = 500,
                    StatusMessage = $"An error occurred: {ex.Message}"
                };
                return StatusCode(500, errorResponse); // Return 500 Internal Server Error for unexpected exceptions
            }
        }

        [HttpGet("viewallusers")]
        public async Task<IActionResult> ViewAllUsersAsync()
        {
            try
            {
                var response = await _AdminDAL.ViewAllUsersAsync();

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
