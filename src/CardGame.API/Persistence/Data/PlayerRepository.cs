// <copyright file="PlayerRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Data
{
    using CardGame.API.Models.Database;
    using CardGame.API.Persistence;

    /// <summary>
    /// The in-memory player repository.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        /// <inheritdoc/>
        public Task<IEnumerable<Player>> GetPlayers()
        {
            using var context = new ApiContext();
            return Task.FromResult<IEnumerable<Player>>(context.Players!.ToList());
        }

        /// <inheritdoc/>
        public async Task<Player> CreatePlayer(string name)
        {
            await using var context = new ApiContext();
            var res = await context.Players!.AddAsync(new Player { Name = name });
            await context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
