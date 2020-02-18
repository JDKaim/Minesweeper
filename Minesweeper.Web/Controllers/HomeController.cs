using System;
using Minesweeper.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Show()
        {
            Game game = this.Session["Game"] as Game;
            if (game == null)
            {
                return this.RedirectToAction("NewMedium");
            }
            DateTime created = (DateTime)this.Session["GameStarted"];
            return View(new GameViewModel(game, (DateTime.UtcNow - created).TotalMilliseconds));
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
            return StartGame(15,15,30);
        }

        private ActionResult StartGame(int rows, int columns, int mines)
        {
            this.Session["Game"] = new Game(rows, columns, mines);
            this.Session["GameStarted"] = DateTime.UtcNow;
            return this.RedirectToAction("Show");
        }

        public ActionResult Mark(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            game.Mark(row, column);
            return this.RedirectToAction("Show");
        }

        public ActionResult Flag(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            game.SetFlag(row, column);
            return this.RedirectToAction("Show");
        }

        public ActionResult Clear(int row, int column)
        {
            Game game = this.Session["Game"] as Game;
            game.ClearFlag(row, column);
            return this.RedirectToAction("Show");
        }
    }
}