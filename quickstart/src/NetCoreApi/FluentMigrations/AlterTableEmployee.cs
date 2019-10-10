using FluentMigrator;
using FluentMigrator.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.FluentMigrations
{
    [Migration(5)]
    public class AlterTableEmployee : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Delete.Column("School")
                .FromTable("employee");

            Alter.Table("employee")
                .AddColumn("CompanyId").AsInt64().Nullable();

            Create.ForeignKey("PK_Employee_Company")
                .FromTable("employee").ForeignColumn("CompanyId")
                .ToTable("company").PrimaryColumn("Id");
        }
    }
}
