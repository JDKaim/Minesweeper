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
                throw new NotImplementedException();
            }
        }

        public bool IsWon
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsLost
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Game(int rows, int columns, int mines)
        {
            this.Board = new Board(rows, columns);

            List<Cell> cells = new List<Cell>();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    cells.Add(this.Board.GetAt(row, column));
                }
            }

            Random random = new Random();
            for(int mine = 0; mine < mines; mine++)
            {
                int index = random.Next(0, cells.Count);
                cells[index].IsMine = true;
                cells.RemoveAt(index);
            }

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
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
            throw new NotImplementedException();
        }

        public void SetFlag(int row, int column)
        {
            throw new NotImplementedException();
        }

        public void ClearFlag(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
