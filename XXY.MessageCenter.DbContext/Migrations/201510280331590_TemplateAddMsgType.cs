namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemplateAddMsgType : DbMigration
    {
        public override void Up()
        {
            AddColumn("MESSAGECENTER.TEMPLATE", "MSG_TYPE", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            CreateIndex("MESSAGECENTER.TEMPLATE", "MSG_TYPE");
        }
        
        public override void Down()
        {
            DropIndex("MESSAGECENTER.TEMPLATE", new[] { "MSG_TYPE" });
            DropColumn("MESSAGECENTER.TEMPLATE", "MSG_TYPE");
        }
    }
}
