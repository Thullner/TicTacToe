using System.Diagnostics.CodeAnalysis;
using TicTacToe.Enums;
using TicTacToe.Models.GameManager;

namespace TicTacToe.Views;

public class TicTacToeView : IView
{
    private GameManager GameManager { get; }

    public TicTacToeView(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public void Run()
    {
        GameManager.StartNewGame(PlayerMarkEnum.X);

        while (true)
        {
            PrintBoard(GameManager.Game.GameBoard.Cells);
            PrintCurrentPlayer(GameManager.Game.CurrentPlayerMark);
            
            var (rowIndex, colIndex) = GetUserMove();
            var result = GameManager.TakeTurn(rowIndex, colIndex);

            if (result == null)
            {
                PrintInvalidMove();
                continue;
            }

            if (!IsGameFinished(result.Value)) continue;
            
            PrintBoard(GameManager.Game.GameBoard.Cells);
            PrintGameResults(GameManager, result.Value);

            if (!CheckPlayAgain()) break;
            GameManager.NewRound();
        }
    }

    private static void PrintGameResults(GameManager gameManager, GameStateEnum result)
    {

        switch (result)
        {
            case GameStateEnum.XWins or GameStateEnum.OWins:
                Console.WriteLine($"Player {result.ToString()[0]} wins!");
                break;
            case GameStateEnum.Tie:
                Console.WriteLine("It's a tie!");
                break;
        }

        Console.WriteLine($"Score - X: {gameManager.PlayerXScore}, O: {gameManager.PlayerOScore}");
    }

    private static void PrintCurrentPlayer(PlayerMarkEnum currentPlayer)
    {
        Console.WriteLine($"Current Player: {currentPlayer}");
    }
    
    private static void PrintInvalidMove()
    {
        Console.WriteLine("Invalid Move. Try Again.");
    }

    private static (int rowIndex, int colIndex) GetUserMove()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter the rowIndex (0, 1, 2): ");
                var rowIndex = int.Parse(Console.ReadLine() ?? string.Empty);

                Console.Write("Enter the columnIndex (0, 1, 2): ");
                var colIndex = int.Parse(Console.ReadLine() ?? string.Empty);

                return (rowIndex, colIndex);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numbers for the rowIndex and columnIndex.");
            }
        }
    }

    private static bool IsGameFinished(GameStateEnum result)
    {
        return result == GameStateEnum.XWins || result == GameStateEnum.OWins || result == GameStateEnum.Tie;
    }

    private static bool CheckPlayAgain()
    {
        Console.Write("Play Again? (y/n): ");
        var playAgain = Console.ReadLine()?.ToLower() == "y";
        Console.WriteLine();
        return playAgain;
    }

    private static void PrintBoard(CellStateEnum[,] board)
    {
        for (var rowIndex = 0; rowIndex < 3; rowIndex++)
        {
            for (var colIndex = 0; colIndex < 3; colIndex++)
            {
                Console.Write(board[rowIndex, colIndex] == CellStateEnum.Empty ? "-" : board[rowIndex, colIndex].ToString());
                if (colIndex < 2)
                {
                    Console.Write("|");
                }
            }

            Console.WriteLine();
        }
    }
}