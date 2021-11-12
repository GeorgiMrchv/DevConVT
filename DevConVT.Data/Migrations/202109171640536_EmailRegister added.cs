namespace DevConVT.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailRegisteradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventRegistrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventRegistrations");
        }
    }
}
