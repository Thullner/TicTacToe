using Moq;
using Xunit;
using TicTacToe.Enums;
using TicTacToe.Models.Game;
using TicTacToe.Models.GameManager;

namespace TicTacToe.Tests;

public class GameManagerTests
{
    private readonly Mock<IGame> mockGame;

    public GameManagerTests()
    {
        mockGame = new Mock<IGame>();
    }

    [Fact]
    public void Should_StartNewGame_And_SetStartingPlayer()
    {
        var gameManager = new GameManager(mockGame.Object);
        gameManager.StartNewGame(PlayerMarkEnum.X);
        mockGame.Verify(g => g.Start(PlayerMarkEnum.X), Times.Once);
    }

    [Fact]
    public void Should_ThrowException_When_NewRoundCalled_Before_GameStarted()
    {
        var gameManager = new GameManager(mockGame.Object);
        Assert.Throws<Exception>(() => gameManager.NewRound());
    }

    [Fact]
    public void Should_SwitchStartingPlayer_When_NewRoundCalled()
    {
        var gameManager = new GameManager(mockGame.Object);
        gameManager.StartNewGame(PlayerMarkEnum.X);

        gameManager.NewRound();

        mockGame.Verify(g => g.Start(PlayerMarkEnum.O), Times.Once);
    }

    [Fact]
    public void Should_ThrowException_When_TakeTurnCalled_Before_GameStarted()
    {
        var gameManager = new GameManager(mockGame.Object);

        Assert.Throws<Exception>(() => gameManager.TakeTurn(0, 0));
    }

    [Fact]
    public void Should_UpdatePlayerScore_When_GameState_Is_XWins()
    {
        mockGame.Setup(g => g.TakeTurn(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        mockGame.Setup(g => g.GameState).Returns(GameStateEnum.XWins);
        var gameManager = new GameManager(mockGame.Object);
        gameManager.StartNewGame(PlayerMarkEnum.X);

        gameManager.TakeTurn(0, 0);

        Assert.Equal(1, gameManager.PlayerXScore);
    }

    [Fact]
    public void Should_UpdatePlayerScore_When_GameState_Is_OWins()
    {
        mockGame.Setup(g => g.TakeTurn(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
        mockGame.Setup(g => g.GameState).Returns(GameStateEnum.OWins);
        var gameManager = new GameManager(mockGame.Object);
        gameManager.StartNewGame(PlayerMarkEnum.O);

        gameManager.TakeTurn(0, 0);

        Assert.Equal(1, gameManager.PlayerOScore);
    }
    
    [Fact]
    public void Should_ReturnNull_When_GameTakeTurn_ReturnsFalse()
    {
        mockGame.Setup(g => g.TakeTurn(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
        var gameManager = new GameManager(mockGame.Object);
        gameManager.StartNewGame(PlayerMarkEnum.X);

        var result = gameManager.TakeTurn(0, 0);

        Assert.Null(result);
    }

}