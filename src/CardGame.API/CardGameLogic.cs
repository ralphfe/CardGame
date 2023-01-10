// <copyright file="CardGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API
{
    using CardGame.API.Models.Database;
    using CardGame.API.Models.Serialization;

    /// <summary>
    /// The card game logic.
    /// </summary>
    public class CardGameLogic
    {
        /// <summary>
        /// Checks whether the game has a winner or not.
        /// </summary>
        /// <param name="cardsResults">The freshly queried card result.</param>
        /// <param name="gameRoundInfos">The existing player round information.</param>
        /// <returns>True, if game has a winner. False, otherwise.</returns>
        public bool CheckGameHasWinner(IEnumerable<CardResult> cardsResults, IEnumerable<PlayerRoundInfo> gameRoundInfos)
        {
            var gameRoundInfosList = gameRoundInfos.ToList();
            var res = this.MapPlayerGameCardValuesToPlayerId(gameRoundInfosList);
            var cardResultsList = cardsResults.ToList();
            var gameRoundInfoList = gameRoundInfosList.ToList();

            for (var i = 0; i < res.Count; i++)
            {
                var match = cardResultsList.ElementAtOrDefault(i)?.Value!.Contains(gameRoundInfoList[i].CardValue!) ?? false;

                if (match)
                {
                    return true;
                }
            }

            return false;
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
