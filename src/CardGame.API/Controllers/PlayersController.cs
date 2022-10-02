// <copyright file="PlayersController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Controllers
{
    using System.Linq;
    using Asp.Versioning;
    using CardGame.API.DbContext;
    using CardGame.API.Models;
    using CardGame.API.Models.Database;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for players API.
    /// </summary>
    [ApiController]
    [Route("api/players")]
    [ApiVersion("1.0")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public PlayersController(IPlayerRepository playerRepository, ILoggerFactory loggerFactory)
        {
            this.playerRepository = playerRepository;
            this.logger = loggerFactory.CreateLogger<GamesController>();
        }

        /// <summary>
        /// Gets all players.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<string>>> GetPlayers()
        {
            try
            {
                var res = await this.playerRepository.GetPlayers();

                if (res is not null)
                {
                    return this.GetAllPlayerNames(res).ToList();
                }

                return this.NoContent();
            }
            catch (Exception e)
            {
                this.logger.LogError(default(EventId), e, $"An exception was caught while processing request '{nameof(this.GetPlayers)}'");
                throw;
            }
        }

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="name">The name of the player to create.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatistics))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreatePlayer(string name)
        {
            try
            {
                var players = await this.playerRepository.GetPlayers();
                if (this.CheckPlayerNameExists(name, players))
                {
                    return this.BadRequest($"Player name '{name}' already taken");
                }

                var nameResult = await this.AddPlayerToRepository(name);
                if (nameResult != null)
                {
                    return nameResult;
                }

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to create player.");
            }
            catch (Exception e)
            {
                this.logger.LogError(default(EventId), e, $"An exception was caught while processing request '{nameof(this.CreatePlayer)}'");
                throw;
            }
        }

        private async Task<string?> AddPlayerToRepository(string name)
        {
            var res = await this.playerRepository.CreatePlayer(name);

            if (res != null)
            {
                return res.Name;
            }

            return null;
        }

        private bool CheckPlayerNameExists(string name, IEnumerable<Player> players)
        {
            return players.Where(x => x.Name == name).Any();
        }

        private IEnumerable<string> GetAllPlayerNames(IEnumerable<Player> res)
        {
            return res.Select(x => x.Name!);
        }
    }
}
