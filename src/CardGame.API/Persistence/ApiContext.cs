﻿// <copyright file="ApiContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence
{
    using CardGame.API.Models.Database;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The API in-memory database session.
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <summary>
        /// Gets or sets the CardGames database set.
        /// </summary>
        public DbSet<CardGame>? CardGames { get; set; }

        /// <summary>
        /// Gets or sets the Players database set.
        /// </summary>
        public DbSet<Player>? Players { get; set; }

        /// <summary>
        /// Gets or sets the PlayerRoundInfo database set.
        /// </summary>
        public DbSet<PlayerRoundInfo>? RoundInfo { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CardGameDb");
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiContext).Assembly);
        }
    }
}
