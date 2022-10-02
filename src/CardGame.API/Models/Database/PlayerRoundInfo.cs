// <copyright file="PlayerRoundInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CardGame.API.Models.Database
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A database model describing a player round information.
    /// </summary>
    public class PlayerRoundInfo
    {
        /// <summary>
        /// Gets or sets the round info id. Note, the value is a database generated key.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundInfoId { get; set; }

        /// <summary>
        /// Gets or sets a card value drawn in the set round.
        /// </summary>
        [Required]
        public string? CardValue { get; set; }

        /// <summary>
        /// Gets or sets a game associated with the round.
        /// </summary>
        public virtual CardGame? Game { get; set; }

        /// <summary>
        /// Gets or sets a player associated with the round.
        /// </summary>
        public virtual Player? Player { get; set; }
    }
}
