using Microsoft.EntityFrameworkCore;
using EmedicineBE.Models;
using System.Threading.Tasks;
using EmedicineBE.Data;

public class UsersDAL
{
    private readonly AppDbContext _context;

    public UsersDAL(AppDbContext context)
    {
        this._context = context;
    }

    #region "Users"

    public async Task<Response> RegisterAsync(Users users)
    {
        var response = new Response();

        try
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            response.StatusCode = 200;
            response.StatusMessage = "User Registered Successfully";
        }
        catch (Exception ex)
        {
            response.StatusCode = 500; // Internal Server Error
            response.StatusMessage = $"An error occurred during registration: {ex.Message}";
        }

        return response;
    }

    public async Task<Response> LoginAsync(Users users)
    {
        var response = new Response();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == users.Email && u.Password == users.Password);

            if (user != null)
            {
                response.user = user;
                response.StatusCode = 200;
                response.StatusMessage = "Login Successful";
            }
            else
            {
                response.user = null;
                response.StatusCode = 100;
                response.StatusMessage = "Login Failed";
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500; // Internal Server Error
            response.StatusMessage = $"An error occurred during login: {ex.Message}";
        }

        return response;
    }

    public async Task<Response> ViewUserAsync(Users users)
    {
        var response = new Response();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == users.ID);

            if (user != null)
            {
                response.user = user;
                response.StatusCode = 200;
                response.StatusMessage = "User Found";
            }
            else
            {
                response.user = null;
                response.StatusCode = 100;
                response.StatusMessage = "User Not Found";
            }
        }
        catch (Exception ex)
        {
            response.StatusCode = 500; // Internal Server Error
            response.StatusMessage = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<Response> UpdateUserAsync(Users users)
    {
        var response = new Response();

        try
        {
            _context.Users.Update(users);
            await _context.SaveChangesAsync();

            response.StatusCode = 200;
            response.StatusMessage = "User Updated Successfully";
        }
        catch (Exception ex)
        {
            response.StatusCode = 500; // Internal Server Error
            response.StatusMessage = $"An error occurred: {ex.Message}";
        }

        return response;
    }

    public async Task<Response> DeleteUserAsync(Users users)
    {
        var response = new Response();

        try
        {
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            response.StatusCode = 200;
            response.StatusMessage = "User Deleted Successfully";
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


    #endregion

}