// <copyright file="GameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.DbContext
{
    using CardGame.API.Models.Database;
    using CardGame.API.Models.Serialization;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The in-memory games repository.
    /// </summary>
    public class GameRepository : IGameRepository
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<CardGame>> GetGames()
        {
            using (var context = new ApiContext())
            {
                return await context.CardGames!
                    .Include(x => x.Players) !
                    .Include(x => x.PlayerRoundInfos) !
                    .ThenInclude(x => x.Game)
                    .Include(x => x.PlayerRoundInfos) !
                    .ThenInclude(x => x.Player)
                    .ToListAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<CardGame?> GetGameById(int gameId)
        {
            using (var context = new ApiContext())
            {
                var games = await this.GetGames();
                return games.FirstOrDefault(x => x.GameId == gameId);
            }
        }

        /// <inheritdoc/>
        public Task<CardGame> CreateNewGame(IEnumerable<string> playerNames, string deckId)
        {
            using (var context = new ApiContext())
            {
                var validPlayers = context.Players!.Where(x => playerNames.Contains(x.Name));
                var cardGame = new CardGame();
                cardGame.PlayerRoundInfos ??= new List<PlayerRoundInfo>();
                cardGame.Players ??= new List<Player>();
                cardGame.Players = validPlayers.ToList();
                cardGame.DeckId = deckId;
                context.CardGames!.Attach(cardGame);
                context.SaveChanges();
                return Task.FromResult(cardGame);
            }
        }

        /// <inheritdoc/>
        public async Task<CardGame> UpdateRoundInformation(int gameId, bool hasWinner, IEnumerable<CardResult> cardResults)
        {
            using (var context = new ApiContext())
            {
                var cardGame = await context.CardGames!
                    .Include(x => x.Players!)
                    .ThenInclude(x => x.PlayerRoundInfos!)
                    .ThenInclude(x => x.Game!)
                    .Include(x => x.PlayerRoundInfos!)
                    .FirstOrDefaultAsync(x => x.GameId == gameId);
                var players = cardGame?.Players!.ToList();
                var cards = cardResults.ToList();

                for (var i = 0; i < players?.Count(); i++)
                {
                    var player = players[i];
                    var info = new PlayerRoundInfo();
                    info.Player = player;
                    info.Game = cardGame;
                    info.CardValue = cards[i].Value;
                    player.PlayerRoundInfos!.Add(info);
                    cardGame!.PlayerRoundInfos!.Add(info);
                }

                cardGame!.HasWinner = hasWinner;
                cardGame.RoundsPlayed++;
                context.SaveChanges();
                return cardGame;
            }
        }
    }
}
