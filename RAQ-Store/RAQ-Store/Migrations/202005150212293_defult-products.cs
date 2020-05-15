namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defultproducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "number_of_products", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "number_of_products", c => c.Int());
        }
    }
}
