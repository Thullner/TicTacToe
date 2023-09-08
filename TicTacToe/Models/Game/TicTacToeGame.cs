using TicTacToe.Enums;
using TicTacToe.Models.Board;

namespace TicTacToe.Models.Game;

public class TicTacToeGame : IGame
{
    public IBoard GameBoard { get; }
    public GameStateEnum GameState { get; private set; } = GameStateEnum.NotStarted;
    public PlayerMarkEnum CurrentPlayerMark { get; private set; }

    public TicTacToeGame(IBoard board)
    {
        if(board.NumberOfColumns != 3 || board.NumberOfRows != 3)
            throw new ArgumentException("Board must be 3x3 for TicTacToe.");
        
        GameBoard = board;
    }
    
    public void Start(PlayerMarkEnum startingPlayer)
    {
        GameBoard.Reset();
        CurrentPlayerMark = startingPlayer;
        GameState = GameStateEnum.Ongoing;
    }

    public bool TakeTurn(int rowIndex, int colIndex)
    {
        if (GameState != GameStateEnum.Ongoing)
        {
            throw new Exception("Game is not ongoing.");
        }
        
        var placedMark = GameBoard.SetCellState(rowIndex, colIndex, (CellStateEnum)CurrentPlayerMark);
        if (!placedMark) return false;

        if (CheckWin(CurrentPlayerMark))
        {
            GameState = CurrentPlayerMark == PlayerMarkEnum.X ? GameStateEnum.XWins : GameStateEnum.OWins;
        }
        if (CheckTie())
        {
            GameState = GameStateEnum.Tie;
        }
        if (GameState == GameStateEnum.Ongoing)
        {
            SwitchPlayer();
        }        
        return true;
    }
    
    private void SwitchPlayer()
    {
        CurrentPlayerMark = (CurrentPlayerMark == PlayerMarkEnum.X) ? PlayerMarkEnum.O : PlayerMarkEnum.X;
    }

    private bool CheckWin(PlayerMarkEnum playerMark)
    {
        return CheckRows(playerMark) || CheckColumns(playerMark) || CheckDiagonals(playerMark);
    }
    
    private bool CheckTie()
    {
        return GameBoard.IsFull() && !CheckWin(PlayerMarkEnum.X) && !CheckWin(PlayerMarkEnum.O);
    }

    private bool CheckRows(PlayerMarkEnum playerMark)
    {
        for (var rowIndex = 0; rowIndex < GameBoard.NumberOfRows; rowIndex++)
        {
            if (IsWinningLine(GameBoard.Cells[rowIndex, 0], GameBoard.Cells[rowIndex, 1], GameBoard.Cells[rowIndex, 2], playerMark))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckColumns(PlayerMarkEnum playerMark)
    {
        for (var colIndex = 0; colIndex < 3; colIndex++)
        {
            if (IsWinningLine(GameBoard.Cells[0, colIndex], GameBoard.Cells[1, colIndex], GameBoard.Cells[2, colIndex], playerMark))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckDiagonals(PlayerMarkEnum playerMark)
    {
        return IsWinningLine(GameBoard.Cells[0, 0], GameBoard.Cells[1, 1], GameBoard.Cells[2, 2], playerMark) ||
               IsWinningLine(GameBoard.Cells[0, 2], GameBoard.Cells[1, 1], GameBoard.Cells[2, 0], playerMark);
    }

    private static bool IsWinningLine(CellStateEnum a, CellStateEnum b, CellStateEnum c, PlayerMarkEnum playerMark)
    {
        return a == (CellStateEnum)playerMark && b == (CellStateEnum)playerMark && c == (CellStateEnum)playerMark;
    }
}