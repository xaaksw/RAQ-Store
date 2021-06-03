﻿namespace RAQ_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imgChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "image", c => c.String(nullable: false));
        }
    }
}
