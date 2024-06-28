using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmedicineBE.Models;

namespace EmedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MedicinesDAL _MedicinesDAL;
        private readonly OrdersDAL _OrdersDAL;

        public MedicinesController(IConfiguration configuration, MedicinesDAL dal, OrdersDAL ordersDAL)
        {
            _configuration = configuration;
            _MedicinesDAL = dal;
            _OrdersDAL = ordersDAL;
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] Medicines medicines)
        {
            try
            {
                var response = await _MedicinesDAL.AddToCartAsync(medicines);

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
                return StatusCode(500, $"An error occurred while adding to cart: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("placeOrder")]
        public async Task<IActionResult> PlaceOrder(int userId, [FromBody] List<OrderItems> orderItemsList)
        {
            var response = await _OrdersDAL.PlaceOrderAsync(userId, orderItemsList);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.StatusCode, response.StatusMessage);
            }
        }

    }
}
