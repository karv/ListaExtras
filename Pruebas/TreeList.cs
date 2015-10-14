using System;
using ListasExtra.Treelike;
using NUnit.Framework;
using System.Collections.Generic;
using ListasExtra.Extensiones;

namespace Pruebas
{
	[TestFixture]
	public class TreeList
	{
		[Test]
		public void Test()
		{
			var tl = new TreeList<int>();
			int[] o = { 1, 2, 3 };
			Console.WriteLine(tl.Count);
			tl.Add(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
			tl.Remove(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
		}

	}
}

