using finalproject.Domain.Interface;
using finalproject.Infrastructure.Persistence;
using finalproject.Infrastructure.Repository;
using Dapper;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddSingleton<DapperContext>();
        builder.Services.AddScoped<IUserService, UserRepository>();
        builder.Services.AddScoped<UserRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}