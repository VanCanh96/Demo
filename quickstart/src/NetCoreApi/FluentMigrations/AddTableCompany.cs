using FluentMigrator;
using FluentMigrator.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrations
{
    [Migration(3)]
    public class AddTableCompany : Migration
    {
        public override void Down()
        {
            Delete.Table("company");
        }

        public override void Up()
        {
            Create.Table("company")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(60).Nullable()
                .WithColumn("Address").AsString(200).Nullable();

        }
    }
}
