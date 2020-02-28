namespace Minesweeper.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMoves : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompletedGames", "Moves", c => c.Int(nullable: false, defaultValue: 0));

            this.Sql("UPDATE dbo.CompletedGames SET Moves = Rows * Columns");
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompletedGames", "Moves");
        }
    }
}
