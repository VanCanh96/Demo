using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrations
{
    [Migration(2)]
    public class AddTableEmployee : Migration
    {
        public override void Down()
        {
            Delete.Table("employee");
        }

        public override void Up()
        {
            Create.Table("employee")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60).Nullable()
                .WithColumn("Dob").AsDateTime().Nullable()
                .WithColumn("School").AsString(100).Nullable();
        }
    }
}
