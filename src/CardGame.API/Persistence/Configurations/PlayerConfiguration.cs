// <copyright file="PlayerConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Configurations
{
    using CardGame.API.Models.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <inheritdoc />
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.PlayerId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasMany(player => player.PlayerRoundInfos)
                .WithOne();
        }
    }
}