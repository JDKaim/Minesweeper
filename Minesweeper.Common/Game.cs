using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper.Common
{
    public class Game
    {
        public readonly Board Board;
        public readonly int Mines;

        public bool IsGameOver
        {
            get
            {
                return this.IsWon || this.IsLost;
            }
        }

        public bool IsWon
        {
            get
            {
                for (int row = 0; row < this.Board.Rows; row++)
                {
                    for (int column = 0; column < this.Board.Columns; column++)
                    {
                        Cell cell = Board.GetAt(row, column);
                        if (cell.IsMine) { continue; }
                        switch (cell.State)
                        {
                            case CellState.Pristine:
                            case CellState.Flagged:
                                return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool IsLost
        {
            get
            {
                for (int row = 0; row < this.Board.Rows; row++)
                {
                    for (int column = 0; column < this.Board.Columns; column++)
                    {
                        Cell cell = Board.GetAt(row, column);
                        if (!cell.IsMine) { continue; }
                        if (cell.State == CellState.Revealed) { return true; }
                    }
                }
                return false;
            }
        }

        public Game(int rows, int columns, int mines)
        {
            this.Board = new Board(rows, columns);

            if (mines < 0) { throw new Exception("Mine amount must zero or greater."); }
            if (mines > (rows * columns)) { throw new Exception("Mine amount must be less than game board room."); }

            List<Cell> cells = new List<Cell>();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    cells.Add(this.Board.GetAt(row, column));
                }
            }

            Random random = new Random();
            for (int mine = 0; mine < mines; mine++)
            {
                int index = random.Next(0, cells.Count);
                cells[index].IsMine = true;
                cells.RemoveAt(index);
            }

            this.UpdateNumbers();
        }

        public void UpdateNumbers()
        {
            for (int row = 0; row < this.Board.Rows; row++)
            {
                for (int column = 0; column < this.Board.Columns; column++)
                {
                    Cell cell = Board.GetAt(row, column);
                    if (cell.IsMine) { continue; }

                    int mineCount = 0;
                    if (this.Board.IsOnBoard((row - 1), (column - 1)) && this.Board.GetAt((row - 1), (column - 1)).IsMine)
                    {
                        mineCount++;
                    }
                    if (this.Board.IsOnBoard((row - 1), (column)) && this.Board.GetAt((row - 1), (column)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row - 1), (column + 1)) && this.Board.GetAt((row - 1), (column + 1)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row), (column - 1)) && this.Board.GetAt((row), (column - 1)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row), (column + 1)) && this.Board.GetAt((row), (column + 1)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row + 1), (column - 1)) && this.Board.GetAt((row + 1), (column - 1)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row + 1), (column)) && this.Board.GetAt((row + 1), (column)).IsMine)
                    {
                        mineCount += 1;
                    }
                    if (this.Board.IsOnBoard((row + 1), (column + 1)) && this.Board.GetAt((row + 1), (column + 1)).IsMine)
                    {
                        mineCount += 1;
                    }
                    cell.SurroundingMines = mineCount;
                }
            }
        }

        public void Mark(int row, int column)
        {
            if (!this.Board.IsOnBoard(row, column)) { return; }

            Cell cell = Board.GetAt(row, column);

            if (cell.State != CellState.Pristine) { return; }

            cell.State = CellState.Revealed;
            if (cell.IsMine) { return; }

            if (cell.SurroundingMines == 0)
            {
                Mark((row + 1), column);
                Mark((row - 1), column);
                Mark((row + 1), (column + 1));
                Mark((row + 1), (column - 1));
                Mark((row - 1), (column + 1));
                Mark((row - 1), (column - 1));
                Mark(row, (column + 1));
                Mark(row, (column - 1));
            }
        }

        public void SetFlag(int row, int column)
        {
            Cell cell = Board.GetAt(row, column);
            if (cell.State == CellState.Pristine) { cell.State = CellState.Flagged; }
        }

        public void ClearFlag(int row, int column)
        {
            Cell cell = Board.GetAt(row, column);
            if (cell.State == CellState.Flagged) { cell.State = CellState.Pristine; }
        }

    }
}
