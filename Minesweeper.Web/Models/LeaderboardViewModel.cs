using Minesweeper.Common;
using Minesweeper.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Web
{
    public class LeaderboardViewModel
    {
        public readonly int Rows;
        public readonly int Columns;
        public readonly int Mines;
        public readonly List<CompletedGame> CompletedGames;
        public LeaderboardViewModel(int rows, int columns, int mines, List<CompletedGame> completedGames)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Mines = mines;
            this.CompletedGames = completedGames;
        }
    }
}