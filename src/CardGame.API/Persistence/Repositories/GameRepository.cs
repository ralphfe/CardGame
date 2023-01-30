// <copyright file="GameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Persistence.Repositories
{
    using CardGame.API.Models.Database;
    using CardGame.API.Models.Serialization;
    using CardGame.API.Persistence;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The in-memory games repository.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private readonly ApiContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="context">The API DB context.</param>
        public GameRepository(ApiContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CardGame>> GetGames()
        {
            return await this.context.CardGames!
                .Include(x => x.Players) !
                .Include(x => x.PlayerRoundInfos) !
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<CardGame?> GetGameById(int gameId)
        {
            var games = await this.GetGames();
            return games.FirstOrDefault(x => x.GameId == gameId);
        }

        /// <inheritdoc/>
        public async Task<CardGame> CreateNewGame(IEnumerable<string> playerNames, string deckId)
        {
            var validPlayers = this.context.Players!.Where(x => playerNames.Contains(x.Name));
            var cardGame = new CardGame();
            cardGame.PlayerRoundInfos ??= new List<PlayerRoundInfo>();
            cardGame.Players ??= new List<Player>();
            cardGame.Players = validPlayers.ToList();
            cardGame.DeckId = deckId;
            this.context.CardGames!.Attach(cardGame);
            await this.context.SaveChangesAsync();
            return cardGame;
        }

        /// <inheritdoc/>
        public async Task<CardGame> UpdateRoundInformation(int gameId, IEnumerable<CardResult> cardResults)
        {
            var cardGame = await this.context.CardGames!
                .Include(x => x.Players!)
                .Include(x => x.PlayerRoundInfos!)
                .FirstOrDefaultAsync(x => x.GameId == gameId);
            var players = cardGame?.Players!.ToList();
            var cards = cardResults?.ToList();

            for (var i = 0; i < players?.Count; i++)
            {
                var player = players[i];
                var info = new PlayerRoundInfo();
                info.Player = player;
                info.Game = cardGame;
                info.CardValue = cards?[i].Value;
                player.PlayerRoundInfos!.Add(info);
                cardGame!.PlayerRoundInfos!.Add(info);
            }

            cardGame!.RoundsPlayed++;
            await this.context.SaveChangesAsync();
            return cardGame;
        }

        /// <inheritdoc/>
        public async Task<CardGame> UpdateRoundInformation(int gameId, bool hasWinner)
        {
            var cardGame = await this.context.CardGames!
                .Include(x => x.Players!)
                .Include(x => x.PlayerRoundInfos!)
                .FirstOrDefaultAsync(x => x.GameId == gameId);

            cardGame!.HasWinner = true;
            await this.context.SaveChangesAsync();
            return cardGame;
        }
    }
}
