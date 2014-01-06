using System.Data.Entity.Migrations;

namespace Sandbox.SOA.Services.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifier = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(c => c.Identifier, unique: true);

            CreateTable(
                "dbo.PersonAddressDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Name = c.String(maxLength:30),
                        Line1 = c.String(),
                        Line2 = c.String(),
                        Town = c.String(),
                        County = c.String(),
                        Postcode = c.String(),
                        Country = c.String(),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonDatas", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(c => new {c.Person_Id, c.Name}, unique: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PersonAddressDatas", "Person_Id", "dbo.PersonDatas");
            DropIndex("dbo.PersonAddressDatas", new[] {"Person_Id"});
            DropTable("dbo.PersonAddressDatas");
            DropTable("dbo.PersonDatas");
        }
    }
}