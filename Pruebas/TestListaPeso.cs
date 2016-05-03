using NUnit.Framework;
using ListasExtra;
using System;

namespace Pruebas
{
	[TestFixture]
	public class TestListaPeso
	{
		[Test]
		public void TestCase ()
		{
			var lp = new ListaPeso<int> ();
			for (int i = 0; i < 100; i++)
			{
				lp [i] = 99 - i;
			}
			Console.WriteLine (lp);
			lp.AlCambiarValor += (sender, e) => 
				Console.WriteLine (string.Format (
				"lp[{0}] cambió de {1} a {2}",
				e.Key,
				e.Previo,
				e.Actual));
			lp [1] = 98;
		}

		[Test]
		public void TestMulti ()
		{
			var lp = new ListaPesoFloat<int, int> ();
			lp [3, 3] = 4;
			Assert.AreEqual (lp [3, 3], 4);
			lp [3, 3] = 0;
			Assert.AreEqual (0, lp [3, 3]);
		}

		[Test]
		public void TestSumaDuplicada ()
		{
			var lp = new ListaPeso<int> ();
			for (int i = 0; i < 100; i++)
			{
				lp [i] = 1;
			}
			var lp2 = new ListaPeso<int> ();
			lp2 [3] = 1;
			ListaPeso<int> sumado = lp + lp2;

			// Probar que ni lp ni lp2 han cambiado y que sumado vale lo que debe
			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual (1, lp [i]);
				Assert.AreEqual (i == 3 ? 1 : 0, lp2 [i]);
				Assert.AreEqual (i == 3 ? 2 : 1, sumado [i]);
			}

			ListaPeso<int> mult = sumado * 2f;
			// Probar que ni lp ni lp2 han cambiado y que sumado vale lo que debe
			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual (1, lp [i]);
				Assert.AreEqual (i == 3 ? 1 : 0, lp2 [i]);
				Assert.AreEqual (i == 3 ? 2 : 1, sumado [i]);
				Assert.AreEqual (i == 3 ? 4 : 2, mult [i]);
			}
		}

		[Test]
		public void TestDataContract ()
		{
			var lp = new ListaPeso<int> ();
			for (int i = 0; i < 100; i++)
			{
				lp [i] = i;
			}

			Store.DataContractStore<ListaPeso<int>>.Serialize ("test.xml", lp);
			var lpClone = Store.DataContractStore<ListaPeso<int>>.Deserialize ("test.xml");

			for (int i = 0; i < 100; i++)
			{
				Assert.AreEqual (lp [i], lpClone [i]);
			}
		}
	}
}