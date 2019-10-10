using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrator
{
    [Migration(5)]
    public class AddPersonal : Migration
    {
        public override void Up()
        {
            Create.Table("personal")
                 .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                 .WithColumn("FullName").AsString(256)
                 .WithColumn("Address").AsString(256)
                 .WithColumn("DOB").AsDateTime()
                 .WithColumn("PhoneNumber").AsString(20);
        }
        public override void Down()
        {
            Delete.Table("personal");
        }

        
    }
}
