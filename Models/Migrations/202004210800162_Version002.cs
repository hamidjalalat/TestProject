namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version002 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FactorDetails", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FactorDetails", "Date", c => c.DateTime(nullable: false));
        }
    }
}
