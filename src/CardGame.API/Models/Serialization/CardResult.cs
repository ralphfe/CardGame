// <copyright file="CardResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Serialization
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A deserialized entry from deckofcardsapi draw cards endpoint describing a single card,
    /// e.g. "https://deckofcardsapi.com/api/deck/12345/draw/?count=2" .
    /// </summary>
    public class CardResult
    {
        /// <summary>
        /// Gets or sets the card code.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the card value.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the card suit.
        /// </summary>
        [JsonPropertyName("suit")]
        public string? Suit { get; set; }
    }
}
