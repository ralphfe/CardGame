// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API
{
    using System.Text.Json.Serialization;
    using Asp.Versioning;
    using CardGame.API.Persistence.Repositories;
    using CardGame.API.Services;
    using Microsoft.EntityFrameworkCore;
    using Serilog;
    using Serilog.Sinks.SystemConsole.Themes;

    /// <summary>
    /// The program class containing main entry point.
    /// </summary>
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.Seq(builder.Configuration["ConnectionStrings:SeqLoggerAddress"])
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add services to the container.
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddDbContext<ApiContext>(
                opt => opt.UseNpgsql(builder.Configuration["ConnectionStrings:CardGameApiDb"]));

            builder.Services.AddSingleton<CardGameService>();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("version"),
                    new HeaderApiVersionReader("X-Version"));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "CardGame.API.xml");
                setup.IncludeXmlComments(filePath);
            });

            // Register services for logging service
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}