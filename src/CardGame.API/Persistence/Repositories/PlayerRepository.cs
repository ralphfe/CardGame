// <copyright file="PlayerRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Repositories
{
    using CardGame.API.Models.Database;
    using CardGame.API.Persistence;

    /// <summary>
    /// The in-memory player repository.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApiContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="context">The API DB context.</param>
        public PlayerRepository(ApiContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Player>> GetPlayers()
        {
            return Task.FromResult<IEnumerable<Player>>(this.context.Players!.ToList());
        }

        /// <inheritdoc/>
        public async Task<Player> CreatePlayer(string name)
        {
            var res = await this.context.Players!.AddAsync(new Player { Name = name });
            await this.context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
