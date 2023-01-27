// <copyright file="CardGame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Database
{
    /// <summary>
    /// A database model describing a card game.
    /// </summary>
    public class CardGame
    {
        /// <summary>
        /// Gets or sets the game id. Note, the value is a database generated key.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets the deck id associated with https://deckofcardsapi.com/ service.
        /// </summary>
        public string? DeckId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game has winner or not.
        /// </summary>
        public bool HasWinner { get; set; }

        /// <summary>
        /// Gets or sets successfully played rounds value.
        /// </summary>
        public int RoundsPlayed { get; set; }

        /// <summary>
        /// Gets or sets the players associated to the current game.
        /// </summary>
        public virtual ICollection<Player>? Players { get; set; }

        /// <summary>
        /// Gets or sets the player round information associated to the current game.
        /// </summary>
        public virtual ICollection<PlayerRoundInfo>? PlayerRoundInfos { get; set; }
    }
}
