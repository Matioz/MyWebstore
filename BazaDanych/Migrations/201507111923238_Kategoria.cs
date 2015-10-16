namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kategoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProducerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Colors = c.Int(nullable: false),
                        Sizes = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProducerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ProducerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.Pictures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "ProducerId" });
            DropIndex("dbo.Pictures", new[] { "ProductId" });
            DropTable("dbo.Producers");
            DropTable("dbo.Products");
            DropTable("dbo.Pictures");
            DropTable("dbo.Categories");
        }
    }
}
