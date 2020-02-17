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
                return this.RedirectToAction("New");
            }
            return View(game);
        }

        public ActionResult New()
        {
            this.Session["Game"] = new Game(10,10,10);
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