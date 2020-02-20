using System;
using Minesweeper.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Web.Models;

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

        public ActionResult Leaderboard(int rows, int columns, int mines)
        {
            using (ApplicationDbContext db = ApplicationDbContext.Create())
            {
                var query = db.CompletedGames.Include(item => item.User).Where((item) => (item.Rows == rows) && (item.Columns == columns) && (item.Mines == mines));
                query = query.OrderBy(item => item.Elapsed);
                query = query.Take(10);

                List<CompletedGame> games = query.ToList();

                return this.View(new LeaderboardViewModel(rows, columns, mines, games));
            }
        }
    }
}