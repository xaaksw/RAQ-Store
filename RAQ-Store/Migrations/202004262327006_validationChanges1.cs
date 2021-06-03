namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationChanges1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "category_id", "dbo.Category");
            DropIndex("dbo.Product", new[] { "category_id" });
            AlterColumn("dbo.Product", "category_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "category_id");
            AddForeignKey("dbo.Product", "category_id", "dbo.Category", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "category_id", "dbo.Category");
            DropIndex("dbo.Product", new[] { "category_id" });
            AlterColumn("dbo.Product", "category_id", c => c.Int());
            CreateIndex("dbo.Product", "category_id");
            AddForeignKey("dbo.Product", "category_id", "dbo.Category", "id");
        }
    }
}
