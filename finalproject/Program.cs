using finalproject.Domain.Interface;
using finalproject.Infrastructure.Persistence;
using finalproject.Infrastructure.Repository;
using Dapper;
using FluentValidation.AspNetCore;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddOpenApi();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddSingleton<DapperContext>();
        builder.Services.AddScoped<IUserService, UserRepository>();
        //builder.Services.AddScoped<UserRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        var app = builder.Build();


        app.UseCors("AllowFrontend");



        if (app.Environment.IsDevelopment())

        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>

            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");

                c.RoutePrefix = string.Empty; 

            });

        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}