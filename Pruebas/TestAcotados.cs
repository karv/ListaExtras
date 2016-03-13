using NUnit.Framework;
using ListasExtra.Acotados;
using System;

namespace Pruebas
{
	[TestFixture]
	public class TestAcotados
	{
		readonly Random r = new Random();

		[Test]
		public void TestFloat()
		{
			var s = new Float();
			var t = new Float();
			s.Valor = (float)r.NextDouble();
			t.Valor = (float)r.NextDouble();

			Assert.AreEqual(s.Valor + t.Valor, s + t);

			var w = new Float(0, 0, 1);
			w.Valor = w + 2;
			Assert.AreEqual(1, (float)w);
			w.Valor -= 3;
			Assert.AreEqual(0, (float)w);
		}
	}
}