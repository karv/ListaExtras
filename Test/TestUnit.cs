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
			char[][] y = x.ToArray();
			Console.WriteLine(y[0]);
		}
	}
}

