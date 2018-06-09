namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'68e8da6c-e230-45b6-bf90-e8ee08750a28', N'admin@gmail.com', 0, N'AOpDRB/kU6y10mFTK7btYzOiWdyNB0VxXD92ZruyDRxJH8+AS1Y1J0ix+MoD4hwx2A==', N'5b68ec8c-fde9-4775-8185-459b8e1aab88', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7be4fd7f-d332-4a61-963b-2b90e4bdfd33', N'guest@gmail.com', 0, N'AF16zKBlLjpIyKup6EHZnAvXDdx8RweMKVwHm/jpfYWvpe2aizZwahZel8zRH4kPeg==', N'50c5893c-4367-46f9-9c7a-9c12a8c5debb', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a28adbd4-de92-483b-877f-4b117c3ce724', N'CanManageMovies')            
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'68e8da6c-e230-45b6-bf90-e8ee08750a28', N'a28adbd4-de92-483b-877f-4b117c3ce724')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
