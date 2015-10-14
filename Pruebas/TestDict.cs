using NUnit.Framework;
using ListasExtra.Extensiones;
using System.Collections.Generic;

namespace Pruebas
{
	[TestFixture]
	public class TestDict
	{
		[Test]
		public void TestClonar()
		{
			var diagonal = new Dictionary<int, int>();
			for (int i = 0; i < 100; i++)
			{
				diagonal[i] = i;
			}
			var clon = diagonal.Clonar();
			Assert.AreEqual(clon.Count, 100);
			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual(clon[i], i);
			}
		}
	}
}

