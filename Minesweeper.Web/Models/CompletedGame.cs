using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Minesweeper.Web.Models
{
    public class CompletedGame
    {
        public int Id { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
        public DateTime Created { get; set; }
        public double Elapsed { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Mines { get; set; }
        public int Moves { get; set; }
    }
}