namespace ProjectContentManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstLastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecondLastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Position", c => c.String());
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "SecondLastName");
            DropColumn("dbo.AspNetUsers", "FirstLastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
