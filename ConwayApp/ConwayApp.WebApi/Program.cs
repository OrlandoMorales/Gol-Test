
using ConwayApp.WebApi.Application.Board;
using ConwayApp.WebApi.Application.BoardStates;
using ConwayApp.WebApi.Application.Core.Configuration;
using ConwayApp.WebApi.Application.Game;
using ConwayApp.WebApi.Application.Interfaces;
using ConwayApp.WebApi.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConwayApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuration
            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<IMongoDBSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            // Application
            builder.Services.AddTransient<IGameService,GameService>();
            builder.Services.AddTransient<IBoardService,BoardService>();
            builder.Services.AddTransient<IBoardStateService, BoardStateService>();
            builder.Services.AddScoped(typeof(INoSqlRepository<>), typeof(MongoRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}