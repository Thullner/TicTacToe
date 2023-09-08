using TicTacToe.Enums;
using TicTacToe.Models.Game;

namespace TicTacToe.Models.GameManager;

public class GameManager : IGameManager
{
    public int PlayerXScore { get; private set; }
    public int PlayerOScore { get; private set; }
    private PlayerMarkEnum? StartingPlayerMark { get; set; }
    
    public IGame Game { get; }

    public GameManager(IGame game)
    {
        Game = game;
    }
    
    public void StartNewGame(PlayerMarkEnum startingPlayerMark)
    {
        StartingPlayerMark = startingPlayerMark;
        Game.Start(StartingPlayerMark.Value);
    }
    
    public void NewRound()
    {
        if (StartingPlayerMark == null)
        {
            throw new Exception("Game has not been started.");
        }
        SwitchStartingPlayer();
        Game.Start(StartingPlayerMark.Value);
    }
    
    public GameStateEnum? TakeTurn(int rowIndex, int colIndex)
    {
        if (StartingPlayerMark == null)
        {
            throw new Exception("Game has not been started.");
        }
        
        var tookTurn = Game.TakeTurn(rowIndex, colIndex);

        if (!tookTurn)
        {
            return null;
        }

        var gameState = Game.GameState;
        
        UpdatePlayerScore(gameState);
        
        return gameState;
    }
    


    private void UpdatePlayerScore(GameStateEnum gameStateEnum)
    {
        if (gameStateEnum == GameStateEnum.XWins)
        {
            PlayerXScore++;
            return;
        }

        if (gameStateEnum == GameStateEnum.OWins)
        {
            PlayerOScore++;
        }
    }
    
    private void SwitchStartingPlayer()
    {
        StartingPlayerMark = (StartingPlayerMark == PlayerMarkEnum.X) ? PlayerMarkEnum.O : PlayerMarkEnum.X;
    }
}