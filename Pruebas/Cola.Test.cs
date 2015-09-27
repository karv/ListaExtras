using System;
using NUnit.Framework;
using ListasExtra.Cola;
using System.Threading;
using System.Security.Cryptography;

namespace Pruebas
{
	[TestFixture]
	public class ColaTest
	{
		[Test]
		public void Probar ()
		{
			var cola = new Cola<int> ();
			for (int i = 0; i < 10; i++) {
				cola.Encolar (i);
			}
			Assert.AreEqual (0, cola.Mirar);
			Assert.AreEqual (0, cola.Tomar ());

			Assert.AreEqual (1, cola.Mirar);
			Assert.AreEqual (1, cola.Tomar ());

		}

		[Test]
		public void Ctor ()
		{
			int[] m = { 2, 5, 3, 6, 34, 2 };
			var cola = new Cola<int> (m);
			Assert.AreEqual (cola.Count, m.Length);
			foreach (var item in m) {
				Assert.AreEqual (item, cola.Tomar ());
			}
		}
	}
}

