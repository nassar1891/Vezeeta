using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VezeetaAPI.Models;
using VezeetaAPI.Repositories;
using VezeetaAPI.Interfaces;

namespace VezeetaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding the connection string
            var connectionString = builder.Configuration.GetConnectionString("vezeetaConnection");
            builder.Services.AddDbContext<VezeetaContext>(options => options.UseSqlServer(connectionString));

            // Registering repositories
            builder.Services.AddScoped<ICrudRepository<Doctor>, DoctorRepository>();
            builder.Services.AddScoped<ICrudRepository<Patient>, PatientRepository>();
            builder.Services.AddScoped<ICrudRepository<Booking>, BookingRepository>();
            builder.Services.AddScoped<ICrudRepository<Coupon>, CouponRepository>();

            builder.Services.AddControllers();

            // Swagger configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enabling Swagger for Development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();

            // Endpoint routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
