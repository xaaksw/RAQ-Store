namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        number_of_products = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        price = c.Double(),
                        image = c.String(nullable: false),
                        description = c.String(nullable: false),
                        category_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Category", t => t.category_id)
                .Index(t => t.category_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "category_id", "dbo.Category");
            DropIndex("dbo.Product", new[] { "category_id" });
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
