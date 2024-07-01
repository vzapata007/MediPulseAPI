using MediPulseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MediPulseAPI.Data
{
    public class AdminDAL
    {
        private readonly AppDbContext _context;
        public AdminDAL(AppDbContext context)
        {
            _context = context;
        }

        //method for add and update medicine
        public async Task<Response> AddUpdateMedicineAsync(Medicines medicines)
        {
            var response = new Response();

            try
            {
                _context.Medicines.Add(medicines);
                await _context.SaveChangesAsync();

                response.StatusCode = 200;
                response.StatusMessage = "Medicine added/updated successfully";
            }
            catch (DbUpdateException ex)
            {
                response.StatusCode = 400; // Bad Request
                response.StatusMessage = $"Error adding/updating medicine: {ex.InnerException?.Message}";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<Response> ViewAllUsersAsync()
        {
            var response = new Response();

            try
            {
                var users = await _context.Users.ToListAsync();
                response.StatusCode = 200;
                response.StatusMessage = "Users Found";
                response.listUsers = users;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.StatusMessage = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
