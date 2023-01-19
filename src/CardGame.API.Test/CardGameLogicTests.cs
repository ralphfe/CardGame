namespace CardGame.API.Test;

using Models.Database;
using Services;

public class CardGameLogicTests
{
    private readonly CardGameService serviceService = new();

    /// <summary>
    /// Assume 2 players play total of 3 rounds, last round 1st player wins
    /// </summary>
    [Fact]
    public void CheckGameHasWinner_InputIs2Players3RoundsPlayer1Wins_ReturnTrue()
    {
        // Arrange
        var player1 = new Player { PlayerId = 1 };
        var player2 = new Player { PlayerId = 2 };

        var game = new CardGame();
        game.PlayerRoundInfos = new List<PlayerRoundInfo>()
        {
            new() { CardValue = "1", Player = player1 },
            new() { CardValue = "2", Player = player2 },
            new() { CardValue = "3", Player = player1 },
            new() { CardValue = "4", Player = player2 },
            new() { CardValue = "1", Player = player1 },
            new() { CardValue = "6", Player = player2 },
        };

        // Act
        var result = this.serviceService.CheckGameHasWinner(game);

        // Assert
        Assert.True(result, "A game should have a winner");
    }

    /// <summary>
    /// Assume 2 players play total of 2 rounds, last round 2nd player wins
    /// </summary>
    [Fact]
    public void CheckGameHasWinner_InputIs2Players2RoundsPlayer2Wins_ReturnTrue()
    {
        // Arrange
        var player1 = new Player { PlayerId = 1 };
        var player2 = new Player { PlayerId = 2 };

        var game = new CardGame();
        game.PlayerRoundInfos = new List<PlayerRoundInfo>()
        {
            new() { CardValue = "1", Player = player1 },
            new() { CardValue = "2", Player = player2 },
            new() { CardValue = "3", Player = player1 },
            new() { CardValue = "2", Player = player2 },
        };

        // Act
        var result = this.serviceService.CheckGameHasWinner(game);

        // Assert
        Assert.True(result, "A game should have a winner");
    }

    /// <summary>
    /// Assume 3 players play total of 2 rounds, last round 3rd player wins
    /// </summary>
    [Fact]
    public void CheckGameHasWinner_InputIs3Players3RoundsPlayer3Wins_ReturnTrue()
    {
        // Arrange
        var player1 = new Player { PlayerId = 1 };
        var player2 = new Player { PlayerId = 2 };
        var player3 = new Player { PlayerId = 3 };

        var game = new CardGame();
        game.PlayerRoundInfos = new List<PlayerRoundInfo>()
        {
            new() { CardValue = "1", Player = player1 },
            new() { CardValue = "2", Player = player2 },
            new() { CardValue = "3", Player = player3 },
            new() { CardValue = "4", Player = player1 },
            new() { CardValue = "5", Player = player2 },
            new() { CardValue = "3", Player = player3 },
        };

        // Act
        var result = this.serviceService.CheckGameHasWinner(game);

        // Assert
        Assert.True(result, "A game should have a winner");
    }

    /// <summary>
    /// Assume 3 players play total of 3 rounds, none of the players have won
    /// </summary>
    [Fact]
    public void CheckGameHasWinner_InputIs3Players3RoundsPlayerNoWins_ReturnFalse()
    {
        // Arrange
        var player1 = new Player { PlayerId = 1 };
        var player2 = new Player { PlayerId = 2 };
        var player3 = new Player { PlayerId = 3 };

        var game = new CardGame();
        game.PlayerRoundInfos = new List<PlayerRoundInfo>()
        {
            new() { CardValue = "1", Player = player1 },
            new() { CardValue = "2", Player = player2 },
            new() { CardValue = "3", Player = player3 },
            new() { CardValue = "4", Player = player1 },
            new() { CardValue = "5", Player = player2 },
            new() { CardValue = "6", Player = player3 },
        };

        // Act
        var result = this.serviceService.CheckGameHasWinner(game);

        // Assert
        Assert.False(result, "A game should have no winners");
    }
}
