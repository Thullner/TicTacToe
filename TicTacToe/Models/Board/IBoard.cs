using TicTacToe.Enums;

namespace TicTacToe.Models.Board;

public interface IBoard
{
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public CellStateEnum[,] Cells { get; }
    public bool SetCellState(int rowIndex, int colIndex, CellStateEnum mark);
    public void Reset();
    
    public bool IsFull();
}