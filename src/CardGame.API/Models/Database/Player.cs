// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Database
{
    /// <summary>
    /// A database model describing a player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player id. Note, the value is a database generated key.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the player round information associated with the player.
        /// </summary>
        public virtual ICollection<PlayerRoundInfo>? PlayerRoundInfos { get; set; }

        /// <summary>
        /// Gets or sets the card game information associated with the player.
        /// </summary>
        public virtual ICollection<CardGame>? CardGames { get; set; }
    }
}
