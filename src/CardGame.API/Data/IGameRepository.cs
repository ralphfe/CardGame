// <copyright file="IGameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Data
{
    using CardGame.API.Models.Database;
    using CardGame.API.Models.Serialization;

    /// <summary>
    /// The game repository contract.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Gets all games from the repository.
        /// </summary>
        /// <returns>The collection of games.</returns>
        public Task<IEnumerable<CardGame>> GetGames();

        /// <summary>
        /// Gets the game with specified id from repository.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <returns>The game model.</returns>
        public Task<CardGame?> GetGameById(int gameId);

        /// <summary>
        /// Creates a new game with set of players and deckId from external https://deckofcardsapi.com/ api service provider.
        /// </summary>
        /// <param name="playerNames">The player names.</param>
        /// <param name="deckId">The deck id.</param>
        /// <returns>The game model.</returns>
        public Task<CardGame> CreateNewGame(IEnumerable<string> playerNames, string deckId);

        /// <summary>
        /// Updates existing game with new round information.
        /// </summary>
        /// <param name="gameId">The game id to update.</param>
        /// <param name="cardResults">The card results to build round info from.</param>
        /// <returns>The updated card game model.</returns>
        public Task<CardGame> UpdateRoundInformation(int gameId, IEnumerable<CardResult> cardResults);

        /// <summary>
        /// Updates existing game round information with winning state.
        /// </summary>
        /// <param name="gameId">The game id to update.</param>
        /// <param name="hasWinner">Specify whether the game has winner determined in the round or not.</param>
        /// <returns>The updated card game model.</returns>
        public Task<CardGame> UpdateRoundInformation(int gameId, bool hasWinner);
    }
}
