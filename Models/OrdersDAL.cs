using Microsoft.EntityFrameworkCore;
using EmedicineBE.Models;
using System.Threading.Tasks;

namespace EmedicineBE.Models
{
    public class OrdersDAL
    {
        private readonly AppDbContext _context;

        public OrdersDAL(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Response> AddToOrdersAsync(Orders orders)
        {
            var response = new Response();

            try
            {
                _context.Orders.Add(orders);
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.StatusMessage = "Order Added Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<Response> UpdateOrderAsync(Orders orders)
        {
            var response = new Response();

            try
            {
                _context.Orders.Update(orders);
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.StatusMessage = "Order Updated Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<Response> DeleteOrderAsync(Orders orders)
        {
            var response = new Response();

            try
            {
                _context.Orders.Remove(orders);
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.StatusMessage = "Order Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<Response> ViewOrderAsync(Orders orders)
        {
            Response response = new Response();

            try
            {
                response.orders = await _context.Orders
                    .FirstOrDefaultAsync(m => m.ID == orders.ID);
                response.StatusCode = 200;
                response.StatusMessage = "Order Viewed Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }
            return response;
        }

         public async Task<Response> PlaceOrderAsync(int userId, List<OrderItems> orderItemsList)
        {
            var response = new Response();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Create a new order
                var newOrder = new Orders
                {
                    UserID = userId,
                    OrderNo = Guid.NewGuid().ToString(), // Generate a unique order number
                    OrderTotal = orderItemsList.Sum(item => item.TotalPrice),
                    OrderStatus = "Placed"
                };

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                // Add order items
                foreach (var item in orderItemsList)
                {
                    item.OrderId = newOrder.ID;
                    _context.OrderItems.Add(item);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                response.StatusCode = 200;
                response.StatusMessage = "Order Placed Successfully";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }
            return response;
        }
    }
}


