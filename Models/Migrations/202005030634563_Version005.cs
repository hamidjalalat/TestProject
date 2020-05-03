namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version005 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Factors", "approved", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Factors", "approved", c => c.Boolean(nullable: false));
        }
    }
}
