using System;
using NUnit.Framework;
using ListasExtra.Set;

namespace Pruebas
{
	[TestFixture]
	public class TestSet
	{
		[Test]
		public void TestPick ()
		{
			var con = new Set<int> ();
			con.Add (1);
			con.Add (2);
			con.Add (3);

			var picked = con.Pick ();
			Assert.True (1 <= picked && picked < 4);
		}

		[Test]
		public void TestPickRemove ()
		{
			var con = new Set<int> ();
			con.Add (1);
			con.Add (2);

			var p0 = con.PickRemove ();
			Assert.Greater (p0, 0);

			var p1 = con.PickRemove ();
			Assert.Greater (p1, 0);

			Assert.IsEmpty (con);
			Assert.AreNotEqual (p0, p1);
		}
	}
}