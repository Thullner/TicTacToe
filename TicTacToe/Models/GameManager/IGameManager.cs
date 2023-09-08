using TicTacToe.Enums;

namespace TicTacToe.Models.GameManager;

public interface IGameManager
{
    public int PlayerXScore { get;  }
    public int PlayerOScore { get; }

    public void StartNewGame(PlayerMarkEnum startingPlayerMark);

    public GameStateEnum? TakeTurn(int rowIndex, int colIndex);

    public void NewRound();


}