using System;
using System.Linq;
using ConsoleApp1;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTestProject1
{
	public class UnitTest1
	{

		MyDbContext context;

		public UnitTest1()
		{
			context = new MyDbContext();

			if (context.Database.GetDbConnection().Database != "")
			{
				context.Database.EnsureDeleted();
				context.Database.Migrate();
			}
		}

		private void ResetContext()
		{
			context = new MyDbContext();

		}

		[Fact()]
		public void Test()
		{
			var recordsToCreate = 331;

			// arrange
			var data = Enumerable.Range(1, recordsToCreate).Select(i => new ImpactValue(DateTime.Now.Date.AddDays(i)));
			context.ImpactValues.AddRange(data);


			// act
			context.SaveChanges();
			ResetContext();

			// assert
			context.ImpactValues.Count().Should().Be(recordsToCreate);
		}
	}
}
