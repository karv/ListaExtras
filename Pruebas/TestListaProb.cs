using NUnit.Framework;
using ListasExtra;
using System.Runtime.Remoting.Metadata;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

namespace Pruebas
{
	[TestFixture]
	public class TestListaProb
	{
		[Test]
		public void Test()
		{
			var prob = new ListaProbabilidad<int>(z => z);
			var res = new Dictionary<int, double>(10);
			for (int i = 0; i < 10; i++)
			{
				prob.Add(i);
				res.Add(i, 0);
			}
			const int numPruebas = 100000;
			for (int i = 0; i < numPruebas; i++)
			{
				var ind = prob.Select();
				res[ind]++;
			}

			Assert.AreEqual(10, prob.Count);
			Assert.AreEqual(45, prob.Suma());

			const double expFactor = numPruebas / 45.0;

			// Imprimir resultados
			for (int i = 0; i < 10; i++)
			{
				var expect = expFactor * i;
				var errAbs = expect - res[i];
				var errRel = errAbs / expect;
				if (i > 0)
					Assert.True(Math.Abs(errRel) < 0.1);
				Console.WriteLine("{0}\t\tE = {1}:\t\t{2}", res[i], expect, errRel);
			}
		}
	}
}