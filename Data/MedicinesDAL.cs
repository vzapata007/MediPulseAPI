using Microsoft.EntityFrameworkCore;
using MediPulseAPI.Models;
using System.Threading.Tasks;

namespace MediPulseAPI.Data
{
    public class MedicinesDAL
    {
        private readonly AppDbContext _context;

        public MedicinesDAL(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Response> AddToCartAsync(Medicines medicines)
        {
            var response = new Response();

            try
            {
                _context.Medicines.Add(medicines);
                await _context.SaveChangesAsync();
                response.StatusCode = 200;
                response.StatusMessage = "Medicine Added To Cart Successfully";
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
