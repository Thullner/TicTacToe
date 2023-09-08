using System;
using Xunit;
using TicTacToe.Models.Board;
using TicTacToe.Enums;

namespace TicTacToe.Tests;

public class BoardTests
{
    [Fact]
    public void Should_CreateBoard_When_ValidArguments()
    {
        // Arrange & Act
        var board = new Board(3, 3);

        // Assert
        Assert.Equal(3, board.NumberOfRows);
        Assert.Equal(3, board.NumberOfColumns);
        Assert.False(board.IsFull());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    public void Should_ThrowException_When_InvalidRowsOrColumns(int rows, int cols)
    {
        // Arrange & Act & Assert
        Assert.Throws<Exception>(() => new Board(rows, cols));
    }

    [Fact]
    public void Should_SetCellState_When_ValidIndexAndEmptyCell()
    {
        // Arrange
        var board = new Board(3, 3);

        // Act
        var result = board.SetCellState(1, 1, CellStateEnum.X);

        // Assert
        Assert.True(result);
        Assert.Equal(CellStateEnum.X, board.Cells[1, 1]);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(3, 0)]
    [InlineData(0, 3)]
    public void Should_NotSetCellState_When_InvalidIndex(int rowIndex, int colIndex)
    {
        // Arrange
        var board = new Board(3, 3);

        // Act
        var result = board.SetCellState(rowIndex, colIndex, CellStateEnum.X);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Should_NotSetCellState_When_CellAlreadyFilled()
    {
        // Arrange
        var board = new Board(3, 3);
        board.SetCellState(1, 1, CellStateEnum.X);

        // Act
        var result = board.SetCellState(1, 1, CellStateEnum.O);

        // Assert
        Assert.False(result);
        Assert.Equal(CellStateEnum.X, board.Cells[1, 1]);
    }

    [Fact]
    public void Should_ResetBoard_When_ResetCalled()
    {
        // Arrange
        var board = new Board(3, 3);
        board.SetCellState(0, 0, CellStateEnum.X);

        // Act
        board.Reset();

        // Assert
        Assert.All(board.Cells.Cast<CellStateEnum>(), cell => Assert.Equal(CellStateEnum.Empty, cell));
    }

    [Fact]
    public void Should_BeFull_When_AllCellsFilled()
    {
        // Arrange
        var board = new Board(2, 2);

        // Act
        board.SetCellState(0, 0, CellStateEnum.X);
        board.SetCellState(0, 1, CellStateEnum.X);
        board.SetCellState(1, 0, CellStateEnum.X);
        board.SetCellState(1, 1, CellStateEnum.X);

        // Assert
        Assert.True(board.IsFull());
    }
}