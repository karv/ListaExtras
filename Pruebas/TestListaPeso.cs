using NUnit.Framework;
using ListasExtra;
using System;

namespace Pruebas
{
	[TestFixture]
	public class TestListaPeso
	{
		[Test]
		public void TestCase()
		{
			var lp = new ListaPeso<int>();
			for (int i = 0; i < 100; i++)
			{
				lp[i] = 99 - i;
			}
			Console.WriteLine(lp);
			lp.AlCambiarValor += (sender, e) => 
				Console.WriteLine(string.Format("lp[{0}] cambió de {1} a {2}", e.Key, e.Previo, e.Actual));
			lp[1] = 98;

		}
	}
}

