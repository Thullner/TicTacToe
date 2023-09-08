using TicTacToe.Enums;

namespace TicTacToe.Models.Board;

public class Board : IBoard
{
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    
    public CellStateEnum[,] Cells { get; private set; }

    public Board(int numberOfRows, int numberOfColumns)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new Exception("Board must have at least one row and one column.");
        
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
        
        Cells = new CellStateEnum[NumberOfRows, numberOfColumns];
    }
    
    public bool SetCellState(int rowIndex, int colIndex, CellStateEnum cellState)
    {
        if (!ValidRowIndex(rowIndex) || !ValidColumnIndex(colIndex) || Cells[rowIndex, colIndex] != CellStateEnum.Empty)
        {
            return false;
        }

        Cells[rowIndex, colIndex] = cellState;
        return true;
    }
    
    public void Reset()
    {
        GenerateBoard();
    }
    
    public bool IsFull()
    {
        return Cells.Cast<CellStateEnum>().All(cell => cell != CellStateEnum.Empty);
    }
    
    private void GenerateBoard()
    {
        if (NumberOfRows < 1 || NumberOfColumns < 1)
            throw new Exception("Board must have at least one row and one column.");
        
        Cells = new CellStateEnum[NumberOfRows, NumberOfColumns];
    }
    
    private bool ValidRowIndex(int rowIndex)
    {
        return rowIndex >= 0 && rowIndex < NumberOfRows;
    }
    
    private bool ValidColumnIndex(int colIndex)
    {
        return colIndex >= 0 && colIndex < NumberOfColumns;
    }
}