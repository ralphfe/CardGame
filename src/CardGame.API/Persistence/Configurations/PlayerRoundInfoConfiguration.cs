// <copyright file="PlayerRoundInfoConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Configurations
{
    using CardGame.API.Models.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <inheritdoc />
    public class PlayerRoundInfoConfiguration : IEntityTypeConfiguration<PlayerRoundInfo>
    {
        private const string PlayerKeyName = "PlayerId";
        private const string GameKeyName = "GameId";

        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<PlayerRoundInfo> builder)
        {
            builder.HasKey(p => p.RoundInfoId);
            builder.Property<int>(PlayerKeyName);
            builder.Property<int>(GameKeyName);

            builder.Property(p => p.RoundInfoId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(roundInfo => roundInfo.Player);
            builder.HasOne(roundInfo => roundInfo.Game);
        }
    }
}
