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
		public void ProbarTree()
		{
			Treelike.Tree<char> x = new Treelike.Tree<char>();

			x.Add("nhue".ToCharArray());
			x.Add("bhua".ToCharArray());
			System.Diagnostics.Debug.WriteLine(x.ToArray());
			Assert.IsTrue(false, "hh");
		}
	}
}

