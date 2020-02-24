using System;
using Minesweeper.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Minesweeper.Web.Models;
using Microsoft.AspNet.Identity;

namespace Minesweeper.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUser LoggedInUser
        {
            get
            {
                return UserManager.FindById(User.Identity.GetUserId());
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public GameController()
        {
        }

        public GameController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Show()
        {
            Game game = this.Session["Game"] as Game;
            if (game == null)
            {
                return this.RedirectToAction("NewMedium");
            }
            DateTime created = (DateTime)this.Session["GameStarted"];
            return View(new GameViewModel(game, (double)this.Session["Elapsed"]));
        }

        public ActionResult NewEasy()
        {
            return StartGame(8, 8, 6);
        }

        public ActionResult NewMedium()
        {
            return StartGame(10, 10, 10);
        }

        public ActionResult NewHard()
        {
            return StartGame(15, 15, 30);
        }
        public ActionResult NewExpert()
        {
            return StartGame(20, 20, 70);
        }

        public ActionResult Custom()
        {
            return this.View();
        }


        public ActionResult StartGame(int rows, int columns, int mines)
        {
            this.Session["Game"] = new Game(rows, columns, mines);
            this.Session["GameStarted"] = DateTime.UtcNow;
            this.Session["Elapsed"] = 0.0;
            return this.RedirectToAction("Show");
        }

        public ActionResult Mark(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            if (game.IsGameOver)
            {
                return this.RedirectToAction("Show");
            }

            game.Mark(row, column);
            this.Session["Elapsed"] = (DateTime.UtcNow - (DateTime)this.Session["GameStarted"]).TotalMilliseconds;

            if (game.IsWon)
            {
                this.LoggedInUser.CompletedGames.Add(
                    new CompletedGame()
                    {
                        Columns = game.Board.Columns,
                        Rows = game.Board.Rows,
                        Mines = game.Mines,
                        Created = (DateTime)this.Session["GameStarted"],
                        Elapsed = (double)this.Session["Elapsed"]
                    });
                this.UserManager.Update(this.LoggedInUser);
            }

            return this.RedirectToAction("Show");
        }

        public ActionResult RevealSurroundings(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            if (game.IsGameOver)
            {
                return this.RedirectToAction("Show");
            }

            game.RevealSurroundings(row, column);
            this.Session["Elapsed"] = (DateTime.UtcNow - (DateTime)this.Session["GameStarted"]).TotalMilliseconds;

            if (game.IsWon)
            {
                this.LoggedInUser.CompletedGames.Add(
                    new CompletedGame()
                    {
                        Columns = game.Board.Columns,
                        Rows = game.Board.Rows,
                        Mines = game.Mines,
                        Created = (DateTime)this.Session["GameStarted"],
                        Elapsed = (double)this.Session["Elapsed"]
                    });
                this.UserManager.Update(this.LoggedInUser);
            }

            return this.RedirectToAction("Show");
        }

        public ActionResult Flag(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            if (!game.IsGameOver)
            {
                game.SetFlag(row, column);
                this.Session["Elapsed"] = (DateTime.UtcNow - (DateTime)this.Session["GameStarted"]).TotalMilliseconds;
            }

            return this.RedirectToAction("Show");
        }

        public ActionResult Clear(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            if (!game.IsGameOver)
            {
                game.ClearFlag(row, column);
                this.Session["Elapsed"] = (DateTime.UtcNow - (DateTime)this.Session["GameStarted"]).TotalMilliseconds;
            }

            return this.RedirectToAction("Show");
        }
    }
}