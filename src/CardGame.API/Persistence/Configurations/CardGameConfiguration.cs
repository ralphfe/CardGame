// <copyright file="CardGameConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Configurations
{
    using CardGame.API.Models.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <inheritdoc />
    public class CardGameConfiguration : IEntityTypeConfiguration<CardGame>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<CardGame> builder)
        {
            builder.HasKey(p => p.GameId);

            builder.Property(p => p.GameId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasMany(game => game.Players)
                .WithMany(player => player.CardGames);

            builder.HasMany(game => game.PlayerRoundInfos)
                .WithOne();
        }
    }
}
