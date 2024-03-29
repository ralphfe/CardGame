﻿// <copyright file="GamesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Asp.Versioning;
    using CardGame.API.Models.Database;
    using CardGame.API.Models.Responses;
    using CardGame.API.Models.Serialization;
    using CardGame.API.Persistence.Repositories;
    using CardGame.API.Services;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for games API.
    /// </summary>
    [ApiController]
    [Route("api/games")]
    [ApiVersion("1.0")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly CardGameService cardGameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="gameRepository">The game repository.</param>
        /// <param name="cardGameService">The card game logic service.</param>
        public GamesController(IGameRepository gameRepository, CardGameService cardGameService)
        {
            this.gameRepository = gameRepository;
            this.cardGameService = cardGameService;
        }

        /// <summary>
        /// Gets all available game statistics.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatisticsResponse))]
        public async Task<ActionResult<IEnumerable<GameStatisticsResponse>>> GetGames()
        {
            var allGames = await this.gameRepository.GetGames();
            var games = allGames.Select(x => new GameStatisticsResponse(x));
            return this.Ok(games);
        }

        /// <summary>
        /// Gets statistics for the specified game.
        /// </summary>
        /// <param name="id">The game id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatisticsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GameStatisticsResponse>> GetGameById(int id)
        {
            var games = await this.gameRepository.GetGames();
            var matchingGame = games.FirstOrDefault(x => x.GameId == id);
            if (matchingGame == null)
            {
                return this.NotFound(new ProblemDetails() { Detail = $"Could not find matching game with id '{id}'" });
            }

            return new GameStatisticsResponse(matchingGame);
        }

        /// <summary>
        /// Creates a game with a set of players.
        /// </summary>
        /// <param name="playerNames">The player names to create a game for. Minimum players 2.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatisticsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GameStatisticsResponse>> CreateGame([FromBody] IEnumerable<string> playerNames)
        {
            var playerNameList = playerNames.ToList();

            if (playerNameList.Count < 2)
            {
                return this.BadRequest($"{nameof(playerNames)} count is less than required 2 players ({playerNameList.Count})");
            }

            // Todo: validate if players exist
            var deckId = await this.RequestDeckIdFromExternalApi();
            var game = await this.gameRepository.CreateNewGame(playerNameList, deckId);

            return new GameStatisticsResponse(game);
        }

        /// <summary>
        /// Plays a simulated game round.
        /// </summary>
        /// <param name="id">The game id to simulate round for.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatisticsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GameStatisticsResponse>> PlayRound(int id)
        {
            var game = await this.gameRepository.GetGameById(id);
            if (game == null)
            {
                return this.BadRequest($"The game with id '{id}' does not exist");
            }

            // If game has a winner just return the result.
            if (this.cardGameService.CheckGameHasWinner(game))
            {
                return new GameStatisticsResponse(game);
            }

            return await this.SimulateGameRound(game);
        }

        private async Task<GameStatisticsResponse> SimulateGameRound(CardGame game)
        {
            var drawCardsResult = await this.RequestCardsFromExternalApi(game.DeckId!, game.Players!.Count);

            if (drawCardsResult.Success)
            {
                game = await this.gameRepository.UpdateRoundInformation(game.GameId, drawCardsResult.Cards!);

                if (this.cardGameService.CheckGameHasWinner(game))
                {
                    game = await this.gameRepository.UpdateRoundInformation(game.GameId, hasWinner: true);
                }
            }

            return new GameStatisticsResponse(game);
        }

        private async Task<string> RequestDeckIdFromExternalApi()
        {
            using var httpClient = new HttpClient();
            using var httpResponse = await httpClient.GetAsync(@"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

            if (!httpResponse.IsSuccessStatusCode)
            {
                // Todo: exception should be specified type
                throw new Exception($"External API request failed with code {(int)httpResponse.StatusCode}");
            }

            var result = await httpResponse.Content.ReadFromJsonAsync<CardsDeckInfoResult>();

            if (result == null)
            {
                // Todo: exception should be specified type
                throw new Exception("Request card deck response from external API is null");
            }

            if (string.IsNullOrEmpty(result.DeckId))
            {
                // Todo: exception should be specified type
                throw new Exception("Request card deck response from external API DeckId is empty");
            }

            return result.DeckId;
        }

        private async Task<DrawCardsResult> RequestCardsFromExternalApi(string deckId, int count)
        {
            using var httpClient = new HttpClient();
            using var httpResponse = await httpClient.GetAsync(@$"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count={count}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                // Todo: exception should be specified type
                throw new Exception($"External API request failed with code {(int)httpResponse.StatusCode}");
            }

            var result = await httpResponse.Content.ReadFromJsonAsync<DrawCardsResult>();

            if (result == null)
            {
                // Todo: exception should be specified type
                throw new Exception("Draw cards response from external API is null");
            }

            return result;
        }
    }
}
