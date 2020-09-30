using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp1.Migrations
{
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"


					-- CREATE TABLE [dbo].[ImpactValue]
					CREATE TABLE [dbo].[ImpactValue](
						[ImpactId] [uniqueidentifier] NOT NULL,
						[ImpactValueTypeId] [int] NOT NULL,
						[ImpactPeriodId] [int] NOT NULL,
						[Date] [date] NOT NULL,
						[ValidFrom] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
						[ValidTo] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
						CONSTRAINT [PK_ImpactValue] PRIMARY KEY NONCLUSTERED 
					(
						[ImpactId] ASC,
						[ImpactValueTypeId] ASC,
						[Date] ASC,
						[ImpactPeriodId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
						PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
					) ON [PRIMARY]

					ALTER TABLE [dbo].[ImpactValue] ADD  CONSTRAINT [DF_ImpactValue_ValidFrom]  DEFAULT (CONVERT([datetime2],'0001-01-01')) FOR [ValidFrom]

					ALTER TABLE [dbo].[ImpactValue] ADD  CONSTRAINT [DF_ImpactValue_ValidTo]  DEFAULT (CONVERT([datetime2],'9999-12-31 23:59:59.9999999')) FOR [ValidTo]


					-- CREATE TABLE [dbo].[ImpactValueHistory]
					CREATE TABLE [dbo].[ImpactValueHistory](
						[ImpactId] [uniqueidentifier] NOT NULL,
						[ImpactValueTypeId] [int] NOT NULL,
						[ImpactPeriodId] [int] NOT NULL,
						[Date] [date] NOT NULL,
						[ValidFrom] [datetime2](7) NOT NULL,
						[ValidTo] [datetime2](7) NOT NULL,
					) ON [PRIMARY]

					ALTER TABLE [dbo].[ImpactValueHistory] ADD  CONSTRAINT [DF_ImpactValueHistory_ValidFrom]  DEFAULT (CONVERT([datetime2],'0001-01-01')) FOR [ValidFrom]

					ALTER TABLE [dbo].[ImpactValueHistory] ADD  CONSTRAINT [DF_ImpactValueHistory_ValidTo]  DEFAULT (CONVERT([datetime2],'9999-12-31 23:59:59.9999999')) FOR [ValidTo]


					-- CREATE INDEX ON TABLE [dbo].[ImpactValueHistory]
					CREATE NONCLUSTERED INDEX [IX_ImpactValueHistory] ON [dbo].[ImpactValueHistory]
					(
						[ValidFrom] ASC,
						[ValidTo] ASC,
						[ImpactId] ASC,
						[ImpactValueTypeId] ASC,
						[Date] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


					-- ENABLE SYSTEM_VERSIONING
					ALTER TABLE [dbo].[ImpactValue]
					SET 
							(SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[ImpactValueHistory] , DATA_CONSISTENCY_CHECK = ON ))
				


      ");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
		}
	}
}
