using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrations
{
    [Migration(4)]
    public class AlterTableCustomer : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("customer")
                .AddColumn("CompanyId").AsInt64().Nullable();

            Create.ForeignKey()
                .FromTable("customer").ForeignColumn("CompanyId")
                .ToTable("company").PrimaryColumn("Id");
        }
    }
}
