using System;
using NUnit.Framework;
using ListasExtra;
using ListasExtra.Set;
using ListasExtra.Treelike;

namespace Test
{
	[TestFixture]
	public class TestUnit
	{

		[Test]
		public void ProbarTree()
		{
			Tree<char> x = new Tree<char>();

			x.Add("nhue".ToCharArray());
			x.Add("bhua".ToCharArray());
			System.Diagnostics.Debug.WriteLine(x.ToArray());
		}

		[TestCase]
		public void ProbarStrTree()
		{
			string[] a = {"fdfe", "fsgf"};
			StringTree t = new StringTree(a);
			foreach (string x in t.ToArray()) {
				Console.WriteLine (x);
			}

		}

		[TestCase]
		public void ProbarSet()
		{
			Set<int> x = new Set<int>();
			for (int i = 0; i < 5235; i++)
			{
				x.Add(i);
			}
			int t = x.Next;
		
		}
	}
}

