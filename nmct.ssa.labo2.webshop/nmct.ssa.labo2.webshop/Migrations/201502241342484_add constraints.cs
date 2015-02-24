namespace nmct.ssa.labo2.webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Devices", "Picture", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "Picture", c => c.String());
            AlterColumn("dbo.Devices", "Name", c => c.String());
        }
    }
}
