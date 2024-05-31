using Bank.Api.Middleware;
using Bank.Application.Services;
using Bank.Domain.Repositories;
using Bank.Domain.Services;
using Bank.Infrastructure;
using Bank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddDbContext<BankDbContext>(options => options.UseInMemoryDatabase("BankDb"));

        // Add respositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

        // Add domain services
        builder.Services.AddScoped<IAccountCustomerService, AccountCustomerService>();

        // Add application services
        builder.Services.AddScoped<ICustomerActionService, CustomerActionService>();

        // Load controllers
        builder.Services.AddControllers();


        var app = builder.Build();

        // Configure middleware
        app.UseMiddleware<ExceptionMiddleware>();

        // Seed the data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
            context.Database.EnsureCreated();
            context.SeedData(); // Seed the data
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}