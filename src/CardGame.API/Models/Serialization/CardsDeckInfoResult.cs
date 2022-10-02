// <copyright file="CardsDeckInfoResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Serialization
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A deserialized entry from deckofcardsapi create a shuffled card deck endpoint,
    /// e.g. https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1 .
    /// </summary>
    public class CardsDeckInfoResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful or not.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the deck id retrieved from https://deckofcardsapi.com/ .
        /// </summary>
        [JsonPropertyName("deck_id")]
        public string? DeckId { get; set; }

        /// <summary>
        /// Gets or sets the remaining card count in the deck.
        /// </summary>
        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }
    }
}
