// <copyright file="IPlayerRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Data
{
    using CardGame.API.Models.Database;

    /// <summary>
    /// The player repository contract.
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Gets all created players.
        /// </summary>
        /// <returns>The collection of players.</returns>
        public Task<IEnumerable<Player>> GetPlayers();

        /// <summary>
        /// Creates a new player with name.
        /// </summary>
        /// <param name="name">The player name.</param>
        /// <returns>The created player model.</returns>
        public Task<Player> CreatePlayer(string name);
    }
}
