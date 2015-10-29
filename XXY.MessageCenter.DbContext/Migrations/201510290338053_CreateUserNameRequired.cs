namespace XXY.MessageCenter.DbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("MESSAGECENTER.E_MAIL_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("MESSAGECENTER.QQ_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("MESSAGECENTER.SMS_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("MESSAGECENTER.TEMPLATE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("MESSAGECENTER.WE_CHAT_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("MESSAGECENTER.TXT_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("MESSAGECENTER.TEMPLATE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("MESSAGECENTER.SMS_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("MESSAGECENTER.QQ_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("MESSAGECENTER.E_MAIL_MESSAGE", "CREATE_BY_USER_NAME", c => c.String(maxLength: 20));
        }
    }
}
