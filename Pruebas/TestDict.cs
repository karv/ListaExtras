using NUnit.Framework;
using ListasExtra.Extensiones;
using System.Collections.Generic;

namespace Pruebas
{
	[TestFixture]
	public class TestDict
	{
		[Test]
		public void TestClonar ()
		{
			var diagonal = new Dictionary<int, int> ();
			for (int i = 0; i < 100; i++)
			{
				diagonal [i] = i;
			}
			var clon = diagonal.Clonar ();
			Assert.AreEqual (clon.Count, 100);
			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual (clon [i], i);
			}

			Assert.False (ReferenceEquals (diagonal, clon));
		}

		[Test]
		public void TestOper ()
		{
			var dict1 = new Dictionary<int, int> ();
			var dict2 = new Dictionary<int, int> ();

			for (int i = 0; i < 3; i++)
			{
				dict1.Add (i, i);
				dict2.Add (i, 2 * i);
			}
			Assert.AreEqual (dict1.Suma (), 3);
			var dict1Int = dict1.Inverso ();
			for (int i = 0; i < 3; i++)
			{
				Assert.AreEqual (-dict1 [i], dict1Int [i]);
			}

			var dict3 = IDictionaryExt.Restar (dict2, dict1);
			Assert.True (IDictionaryExt.IgualPorEntrada (dict3, dict1));

			Assert.True (IDictionaryExt.IgualPorEntrada (dict1.Veces (2), dict2));
		}
	}
}