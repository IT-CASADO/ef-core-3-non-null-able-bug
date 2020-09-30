using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp1
{
	public class MyDbContext : DbContext
	{
		public DbSet<ImpactValue> ImpactValues { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			if (!optionsBuilder.IsConfigured)
			{
				var connection = @"Server=(localdb)\mssqllocaldb;Database=EFCoreBug.nonNULLable;Trusted_Connection=True;ConnectRetryCount=0";

				optionsBuilder.UseSqlServer(connection, options =>
				{
					options.CommandTimeout(300);
				});
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ImpactValueConfiguration());
		}
	}

	public class ImpactValueConfiguration : IEntityTypeConfiguration<ImpactValue>
	{
		public void Configure(EntityTypeBuilder<ImpactValue> builder)
		{
			builder.HasKey(i => new { i.ImpactId, i.ImpactValueTypeId, i.Date, i.ImpactPeriodId });

			builder.Property(i => i.Date)
				.IsRequired()
				.HasColumnType("DATE");

			builder.Property(i => i.ImpactValueTypeId)
				.IsRequired();

			builder.Property(i => i.ValidFrom)
				.IsRequired()
				.HasColumnType("datetime2(7)")
				.ValueGeneratedOnAddOrUpdate();

			builder.Property(i => i.ValidTo)
				.IsRequired()
				.HasColumnType("datetime2(7)")
				.ValueGeneratedOnAddOrUpdate();

			builder.ToTable("ImpactValue");
		}
	}


	public class ImpactValue
	{

		public Guid ImpactId { get; set; }
		public int ImpactValueTypeId { get; set; }
		public int ImpactPeriodId { get; set; }
		public DateTime Date { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidTo { get; set; }

		public ImpactValue(DateTime date)
		{
			ImpactId = Guid.NewGuid();
			ImpactValueTypeId = 1;
			ImpactPeriodId = 2;
			Date = date;
		}
	}
}
