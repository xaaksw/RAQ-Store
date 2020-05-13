namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductFromTheSameType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cart", "product_id", "dbo.Product");
            DropPrimaryKey("dbo.Cart");
            AddPrimaryKey("dbo.Cart", new[] { "product_id", "added_at" });
            AddForeignKey("dbo.Cart", "product_id", "dbo.Product", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cart", "product_id", "dbo.Product");
            DropPrimaryKey("dbo.Cart");
            AddPrimaryKey("dbo.Cart", "product_id");
            AddForeignKey("dbo.Cart", "product_id", "dbo.Product", "id");
        }
    }
}
