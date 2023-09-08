using TicTacToe.Enums;
using TicTacToe.Models.Board;

namespace TicTacToe.Models.Game;

public interface IGame
{
    public IBoard GameBoard { get; }
    public PlayerMarkEnum CurrentPlayerMark { get; }
    public GameStateEnum GameState { get; }

    public bool TakeTurn(int rowIndex, int colIndex);
    public void Start(PlayerMarkEnum startingPlayer);
}