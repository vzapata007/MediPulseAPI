using MediPulseAPI.Data;
using MediPulseAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MediPulseAPI", Version = "v1" });
        });

        // Register AppDbContext
        var connectionString = builder.Configuration.GetConnectionString("EMedCS");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3); // Intenta 3 veces antes de fallar
            }));

        // Register repositories
        builder.Services.AddScoped<UsersDAL>();
        builder.Services.AddScoped<Users>();
        builder.Services.AddScoped<Medicines>();
        builder.Services.AddScoped<Orders>();
        builder.Services.AddScoped<OrderItems>();
        builder.Services.AddScoped<Cart>();
        builder.Services.AddScoped<Response>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Add this line to start the app
        app.Run();
    }
}
