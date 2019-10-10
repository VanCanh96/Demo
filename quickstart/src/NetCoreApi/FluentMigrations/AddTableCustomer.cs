using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrations
{
    [Migration(1)]
    public class AddTableCustomer : Migration
    {
        public override void Down()
        {
            Delete.Table("customer");
        }

        public override void Up()
        {
            Create.Table("customer")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Dob").AsDateTime();
        }
    }
}
