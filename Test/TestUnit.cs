using System;
using NUnit.Framework;
using ListasExtra;

namespace Test
{
	[TestFixture]
	public class TestUnit
	{
		public TestUnit()
		{

		}

		[Test]
		public void ProbarSet()
		{
			ListasExtra.Set.Set<int> x = new ListasExtra.Set.Set<int>();
			x.Add(12);
			int t = x.Next;
		}
	}
}

