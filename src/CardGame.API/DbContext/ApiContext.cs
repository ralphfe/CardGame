// <copyright file="ApiContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.DbContext
{
    using CardGame.API.Models.Database;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The API in-memory database session.
    /// </summary>
    public class ApiContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiContext"/> class.
        /// </summary>
        /// <param name="options">The db context options.</param>
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardGame>()
                .HasMany(game => game.Players)
                .WithMany(player => player.CardGames);

            modelBuilder.Entity<CardGame>()
                .HasMany(game => game.PlayerRoundInfos)
                .WithOne();

            modelBuilder.Entity<Player>()
                .HasMany(player => player.PlayerRoundInfos)
                .WithOne();

            modelBuilder.Entity<PlayerRoundInfo>()
                .HasOne(roundInfo => roundInfo.Player)
                .WithMany();

            modelBuilder.Entity<PlayerRoundInfo>()
                .HasOne(roundInfo => roundInfo.Game)
                .WithMany();
        }
    }
}
