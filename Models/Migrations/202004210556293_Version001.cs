namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FactorDetails", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FactorDetails", "Date");
        }
    }
}
