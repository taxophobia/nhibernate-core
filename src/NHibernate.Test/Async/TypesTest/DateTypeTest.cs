﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections.Generic;
using NHibernate.Dialect;
using NHibernate.Type;
using NUnit.Framework;
using System;

namespace NHibernate.Test.TypesTest
{
	using System.Threading.Tasks;
	using System.Threading;

	[TestFixture]
	public class DateTypeFixtureAsync : AbstractDateTimeTypeFixtureAsync
	{
		protected override string TypeName => "Date";
		protected override AbstractDateTimeType Type => NHibernateUtil.Date;
		protected override bool RevisionCheck => false;
		protected override long DateAccuracyInTicks => TimeSpan.TicksPerDay;

		protected override DateTime GetTestDate(DateTimeKind kind)
			=> base.GetTestDate(kind).Date;

		protected override DateTime GetSameDate(DateTime original)
		{
			var date = base.GetSameDate(original);
			return date.AddHours(date.Hour < 12 ? 12 : -12);
		}

		[Test]
		public Task ReadWriteNormalAsync()
		{
			try
			{
				var expected = DateTime.Today;

				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task ReadWriteMinAsync()
		{
			try
			{
				var expected = Sfi.ConnectionProvider.Driver.MinDate;

				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		[Test]
		public Task ReadWriteYear750Async()
		{
			try
			{
				var expected = new DateTime(750, 5, 13);
				if (Sfi.ConnectionProvider.Driver.MinDate > expected)
				{
					Assert.Ignore($"The driver does not support dates below {Sfi.ConnectionProvider.Driver.MinDate:O}");
				}
				return ReadWriteAsync(expected);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		private async Task ReadWriteAsync(DateTime expected, CancellationToken cancellationToken = default(CancellationToken))
		{
			// Add an hour to check it is correctly ignored once read back from db.
			var basic = new DateTimeClass { Id = AdditionalDateId, Value = expected.AddHours(1) };
			using (var s = OpenSession())
			{
				await (s.SaveAsync(basic, cancellationToken)); 
				await (s.FlushAsync(cancellationToken));
			}
			using (var s = OpenSession())
			{
				basic = await (s.GetAsync<DateTimeClass>(AdditionalDateId, cancellationToken));
				Assert.That(basic.Value, Is.EqualTo(expected));
				await (s.DeleteAsync(basic, cancellationToken));
				await (s.FlushAsync(cancellationToken));
			}
		}

		// They furthermore cause some issues with Sql Server Ce, which switch DbType.Date for DbType.DateTime
		// on its parameters.
		[Ignore("Test relevant for datetime, not for date.")]
		public override Task SaveUseExpectedSqlTypeAsync()
		{
			return Task.CompletedTask;
		}

		[Ignore("Test relevant for datetime, not for date.")]
		public override Task UpdateUseExpectedSqlTypeAsync()
		{
			return Task.CompletedTask;
		}

		[Ignore("Test relevant for datetime, not for date.")]
		public override Task QueryUseExpectedSqlTypeAsync()
		{
			return Task.CompletedTask;
		}
	}
}