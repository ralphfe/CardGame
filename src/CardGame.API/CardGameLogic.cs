// <copyright file="CardGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API
{
    using CardGame.API.Models.Database;

    /// <summary>
    /// The card game logic.
    /// </summary>
    public class CardGameLogic
    {
        /// <summary>
        /// Checks whether the game has a winner or not.
        /// </summary>
        /// <param name="cardGame">The existing card game information.</param>
        /// <returns>True, if game has a winner. False, otherwise.</returns>
        public bool CheckGameHasWinner(CardGame cardGame)
        {
            if (cardGame == null)
            {
                throw new ArgumentNullException(nameof(cardGame));
            }

            if (cardGame.PlayerRoundInfos == null)
            {
                throw new ArgumentNullException(nameof(cardGame));
            }

            var gameRoundInfosList = cardGame.PlayerRoundInfos.ToList();
            var res = this.MapPlayerGameCardValuesToPlayerId(gameRoundInfosList);
            return res.Any(x => x.Value.Distinct().Count() != x.Value.Count());
        }

        /// <summary>
        /// Creates a player id to card values map for given collection of game rounds.
        /// </summary>
        /// <param name="gameRoundInfo">The collection of game rounds.</param>
        /// <returns>A dictionary where value represents card values for the given player id.</returns>
        private IDictionary<int, IEnumerable<string>> MapPlayerGameCardValuesToPlayerId(IEnumerable<PlayerRoundInfo> gameRoundInfo)
        {
            return gameRoundInfo
                .GroupBy(k => k.Player!)
                .ToDictionary(x => x.Key.PlayerId, x => x.Select(y => y.CardValue!));
        }
    }
}
