namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenre1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Genres", "timer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "timer", c => c.Int(nullable: false));
        }
    }
}
