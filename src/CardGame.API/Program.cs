// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API
{
    using Asp.Versioning;
    using CardGame.API.Persistence.Data;
    using CardGame.API.Services;

    /// <summary>
    /// The program class containing main entry point.
    /// </summary>
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
            builder.Services.AddSingleton<CardGameService>();
            builder.Services.AddControllers();
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