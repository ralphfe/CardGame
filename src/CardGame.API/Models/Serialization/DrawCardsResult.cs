// <copyright file="DrawCardsResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Serialization
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A deserialized entry from deckofcardsapi draw cards endpoint, e.g. "https://deckofcardsapi.com/api/deck/12345/draw/?count=2" .
    /// </summary>
    public class DrawCardsResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful or not.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the deck id associated with https://deckofcardsapi.com/ .
        /// </summary>
        [JsonPropertyName("deck_id")]
        public string? DeckId { get; set; }

        /// <summary>
        /// Gets or sets the card results.
        /// </summary>
        [JsonPropertyName("cards")]
        public IEnumerable<CardResult>? Cards { get; set; }

        /// <summary>
        /// Gets or sets the remaining card count in the deck.
        /// </summary>
        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }
    }
}
