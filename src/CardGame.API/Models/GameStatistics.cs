﻿// <copyright file="GameStatistics.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models
{
    /// <summary>
    /// A model describing current game information (statistics).
    /// </summary>
    public class GameStatistics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameStatistics"/> class.
        /// </summary>
        /// <param name="game">The card game data.</param>
        public GameStatistics(Database.CardGame game)
        {
            this.GameId = game.GameId;
            this.Players = game.Players?.Select(x => x.Name!);
            this.RoundsPlayed = game.RoundsPlayed;
            this.HasWinner = game.HasWinner;
        }

        /// <summary>
        /// Gets the game id.
        /// </summary>
        public int GameId { get; }

        /// <summary>
        /// Gets the active game player names.
        /// </summary>
        public IEnumerable<string>? Players { get; }

        /// <summary>
        /// Gets a value indicating whether the game has a winner or not.
        /// </summary>
        public bool HasWinner { get; }

        /// <summary>
        /// Gets the total number of rounds played.
        /// </summary>
        public int RoundsPlayed { get; }
    }
}
