using Moq;
using TicTacToe;
using TicTacToe.Enums;
using TicTacToe.Models;
using TicTacToe.Models.Board;
using TicTacToe.Models.Game;
using Xunit;

namespace TicTacToeTest.Models;

public class TicTacToeGameTests
{
    private Mock<IBoard> CreateMockBoardWithDefaults()
    {
        var cells = new CellStateEnum[3, 3];
        var mockBoard = new Mock<IBoard>();
    
        mockBoard.SetupGet(b => b.NumberOfRows).Returns(3);
        mockBoard.SetupGet(b => b.NumberOfColumns).Returns(3);
        mockBoard.Setup(b => b.Cells).Returns(cells);
        mockBoard.Setup(b => b.SetCellState(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CellStateEnum>()))
            .Callback<int, int, CellStateEnum>((rowIndex, colndex, state) => cells[rowIndex, colndex] = state)
            .Returns(true);
             
        return mockBoard;
    }

    [Fact]
    public void Constructor_ThrowsException_WhenInvalidBoardSize()
    {
        var mockBoard = CreateMockBoardWithDefaults();
        mockBoard.SetupGet(b => b.NumberOfRows).Returns(4);

        Assert.Throws<ArgumentException>(() => new TicTacToeGame(mockBoard.Object));
    }

    [Fact]
    public void Restart_InitializesGame()
    {
        var mockBoard = CreateMockBoardWithDefaults();
        var game = new TicTacToeGame(mockBoard.Object);

        game.Start(PlayerMarkEnum.X);

        Assert.Equal(PlayerMarkEnum.X, game.CurrentPlayerMark);
        Assert.Equal(GameStateEnum.Ongoing, game.GameState);
        mockBoard.Verify(b => b.Reset(), Times.Once);
    }

    [Fact]
    public void TakeTurn_ThrowsException_WhenGameNotOngoing()
    {
        var mockBoard = CreateMockBoardWithDefaults();
        var game = new TicTacToeGame(mockBoard.Object);

        Assert.Throws<Exception>(() => game.TakeTurn(0, 0));
    }

    [Fact]
    public void TakeTurn_ReturnsFalse_WhenInvalidMove()
    {
        var mockBoard = CreateMockBoardWithDefaults();
        mockBoard.Setup(b => b.SetCellState(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CellStateEnum>()))
            .Returns(false);
        var game = new TicTacToeGame(mockBoard.Object);
        game.Start(PlayerMarkEnum.X);

        var result = game.TakeTurn(0, 0);

        Assert.False(result);
    }

    [Fact]
    public void TakeTurn_UpdatesGameState_ToXWins()
    {
        var mockBoard = CreateMockBoardWithDefaults();

        mockBoard.Object.Cells[0, 0] = CellStateEnum.X;
        mockBoard.Object.Cells[1, 0] = CellStateEnum.X;

        var game = new TicTacToeGame(mockBoard.Object);
        game.Start(PlayerMarkEnum.X);

        game.TakeTurn(2, 0); // X places mark on the bottom-left, completing a vertical line

        Assert.Equal(GameStateEnum.XWins, game.GameState);
    }

    [Fact]
    public void TakeTurn_UpdatesGameState_ToTie()
    {
        var mockBoard = CreateMockBoardWithDefaults();
        mockBoard.Setup(b => b.Cells).Returns(new CellStateEnum[3, 3]
        {
            { CellStateEnum.X, CellStateEnum.O, CellStateEnum.X },
            { CellStateEnum.X, CellStateEnum.O, CellStateEnum.X },
            { CellStateEnum.O, CellStateEnum.X, CellStateEnum.O }
        });
        mockBoard.Setup(b => b.IsFull()).Returns(true);

        var game = new TicTacToeGame(mockBoard.Object);
        game.Start(PlayerMarkEnum.X);

        game.TakeTurn(0, 1); // Doesn't matter where X places the mark

        Assert.Equal(GameStateEnum.Tie, game.GameState);
    }
    
            [Fact]
        public void TakeTurn_UpdatesGameState_ToOWins()
        {
            var mockBoard = CreateMockBoardWithDefaults();
            
            mockBoard.Object.Cells[0, 0] = CellStateEnum.O;
            mockBoard.Object.Cells[1, 0] = CellStateEnum.O;
            mockBoard.Object.Cells[2, 0] = CellStateEnum.O;

            var game = new TicTacToeGame(mockBoard.Object);
            game.Start(PlayerMarkEnum.O);

            game.TakeTurn(0, 1);  // O places mark on the top-middle, completing a vertical line

            Assert.Equal(GameStateEnum.OWins, game.GameState);
        }

        [Fact]
        public void TakeTurn_SwitchesPlayer_AfterEachValidMove()
        {
            var mockBoard = CreateMockBoardWithDefaults();
            var game = new TicTacToeGame(mockBoard.Object);
            game.Start(PlayerMarkEnum.X);

            game.TakeTurn(0, 0);  // X makes a move
            Assert.Equal(PlayerMarkEnum.O, game.CurrentPlayerMark);

            game.TakeTurn(0, 1);  // O makes a move
            Assert.Equal(PlayerMarkEnum.X, game.CurrentPlayerMark);
        }

        [Fact]
        public void TakeTurn_ThrowsException_WhenTryingToPlayAfterGameEnded()
        {
            var mockBoard = CreateMockBoardWithDefaults();
            
            mockBoard.Object.Cells[0, 0] = CellStateEnum.X;
            mockBoard.Object.Cells[0, 1] = CellStateEnum.X;

            var game = new TicTacToeGame(mockBoard.Object);
            game.Start(PlayerMarkEnum.X);

            game.TakeTurn(0, 2);  // X wins with this move

            Assert.Throws<Exception>(() => game.TakeTurn(1, 1));
        }

        [Fact]
        public void TakeTurn_DoesNotSwitchPlayer_WhenInvalidMove()
        {
            var mockBoard = CreateMockBoardWithDefaults();
            mockBoard.Setup(b => b.SetCellState(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CellStateEnum>())).Returns(false);
            var game = new TicTacToeGame(mockBoard.Object);
            game.Start(PlayerMarkEnum.X);

            game.TakeTurn(0, 0);  // Invalid move

            Assert.Equal(PlayerMarkEnum.X, game.CurrentPlayerMark);  // Player should not switch
        }
}