namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageNotNeedLang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MESSAGECENTER.TXT_MESSAGE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        SUBJECT = c.String(nullable: false, maxLength: 100),
                        READED = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SENDER = c.String(nullable: false, maxLength: 20),
                        SENDER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RECEIVER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PRI = c.Decimal(nullable: false, precision: 3, scale: 0),
                        CTX = c.String(),
                        RECEIVER = c.String(),
                        CREATE_ON = c.DateTime(nullable: false),
                        CREATE_BY_USER_NAME = c.String(maxLength: 20),
                        CREATE_BY_USER_ID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MODIFY_ON = c.DateTime(),
                        MODIFY_BY_USER_NAME = c.String(maxLength: 20),
                        MODIFY_BY_USER_ID = c.Decimal(precision: 18, scale: 2),
                        IS_DELETED = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("MESSAGECENTER.E_MAIL_MESSAGE", "LANG");
            DropColumn("MESSAGECENTER.QQ_MESSAGE", "LANG");
            DropColumn("MESSAGECENTER.SMS_MESSAGE", "LANG");
            DropColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "LANG");
        }
        
        public override void Down()
        {
            AddColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "LANG", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.SMS_MESSAGE", "LANG", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.QQ_MESSAGE", "LANG", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            AddColumn("MESSAGECENTER.E_MAIL_MESSAGE", "LANG", c => c.Decimal(nullable: false, precision: 3, scale: 0));
            DropTable("MESSAGECENTER.TXT_MESSAGE");
        }
    }
}
