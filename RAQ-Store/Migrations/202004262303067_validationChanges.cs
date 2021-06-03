namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Product", "description", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "description", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "name", c => c.String(nullable: false));
        }
    }
}
