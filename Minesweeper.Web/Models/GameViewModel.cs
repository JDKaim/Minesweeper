using Minesweeper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Web
{
    public class GameViewModel
    {
        public readonly Game Game;
        public readonly double Elapsed;

        public GameViewModel(Game game, double elapsed)
        {
            this.Game = game;
            this.Elapsed = elapsed;
        }
    }
}