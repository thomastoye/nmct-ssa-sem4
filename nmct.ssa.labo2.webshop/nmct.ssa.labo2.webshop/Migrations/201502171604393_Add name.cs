namespace nmct.ssa.labo2.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Name");
        }
    }
}
