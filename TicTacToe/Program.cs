using TicTacToe.Enums;
using TicTacToe.Models.Board;
using TicTacToe.Models.Game;
using TicTacToe.Models.GameManager;
using TicTacToe.Views;

namespace TicTacToe;

class Program
{
    public static void Main(string[] args)
    {
        var board = new Board(3, 3);
        var game = new TicTacToeGame(board);
        var gameManager = new GameManager(game);
        var view = new TicTacToeView(gameManager);
        
        view.Run();
    }
}