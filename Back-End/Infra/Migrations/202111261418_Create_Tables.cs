

using FluentMigrator;
using System;

namespace PopCorn.Web.Infra.Migrations
{
    [Migration(202111261418)]
    public class _202111261418_Create_Tables : Migration
    {
        public override void Down()
        {
        
        }

        public override void Up()
        {
            Create.Table("Users")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("UserName").AsString(200)
                 .WithColumn("Role").AsString(20)
                 .WithColumn("Password").AsString(100)
                 .WithColumn("Email").AsString(100)
                 .WithColumn("DateCreate").AsDateTime();


            Insert.IntoTable("Users").Row(new { UserName = "Adm", Role = "Admin", Password = "manager", Email = "adm@printwayy.com.br", DateCreate = DateTime.Now });

            Create.Table("Rooms")
                .WithColumn("Id").AsString(200).PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("NumberOfSeats").AsInt32().NotNullable();


            Insert.IntoTable("Rooms").Row(new { Id = 1, Name = "Sala 01", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 2, Name = "Sala 02", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 3, Name = "Sala 03", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 4, Name = "Sala 04", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 5, Name = "Sala 05", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 6, Name = "Sala 06", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 7, Name = "Sala 07", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 8, Name = "Sala 08", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 9, Name = "Sala 09", NumberOfSeats = 200 });
            Insert.IntoTable("Rooms").Row(new { Id = 10, Name = "Sala 10", NumberOfSeats = 200 });



            Create.Table("Sessions")
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                    .WithColumn("SessionDate").AsDateTime().NotNullable()
                    .WithColumn("StartTime").AsDateTime().NotNullable()
                    .WithColumn("EndTime").AsDateTime().NotNullable()
                    .WithColumn("TicketValue").AsDouble().NotNullable()
                    .WithColumn("TypeAnimation").AsInt32().Nullable()
                    .WithColumn("TypeAudio").AsInt32().Nullable()
                    .WithColumn("MovieId").AsInt32().ForeignKey("Movies", "Id")
                    .WithColumn("RoomId").AsInt32().ForeignKey("Rooms", "Id");


            Create.Table("Movies")
                    .WithColumn("Id").AsInt32().PrimaryKey()
                    .WithColumn("Image").AsString(3000).Nullable()
                    .WithColumn("Title").AsString().NotNullable()
                    .WithColumn("Description").AsString().NotNullable()
                    .WithColumn("Duration").AsDateTime().NotNullable();

        }
    }
}
