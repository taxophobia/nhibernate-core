﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Id;
using NHibernate.Transaction;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1326
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		public override string BugNumber
		{
			get { return "NH1326"; }
		}


		[Test]
		public async Task ShouldThrowIfCallingDisconnectInsideTransactionAsync()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Assert.Throws<InvalidOperationException>(() => s.Disconnect(), "Disconnect cannot be called while a transaction is in progress.");
				await (tx.CommitAsync());
			}
		}
	}
}