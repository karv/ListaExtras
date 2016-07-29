using NUnit.Framework;
using ListasExtra;
using System.Collections.Generic;
using System;

namespace Pruebas
{
	[TestFixture]
	public class TestListaProb
	{
		[Test]
		public void Test ()
		{
			var prob = new ListaProbabilidad<int> (z => z);
			var res = new Dictionary<int, double> (10);
			for (int i = 0; i < 10; i++)
			{
				prob.Add (i);
				res.Add (i, 0);
			}
			const int numPruebas = 100000;
			for (int i = 0; i < numPruebas; i++)
			{
				var ind = prob.Select ();
				res [ind]++;
			}

			Assert.AreEqual (10, prob.Count);

			const double expFactor = numPruebas / 45.0;

			// Imprimir resultados
			for (int i = 0; i < 10; i++)
			{
				var expect = expFactor * i;
				var errAbs = expect - res [i];
				var errRel = errAbs / expect;
				if (i > 0)
					Assert.True (Math.Abs (errRel) < 0.1);
				Console.WriteLine ("{0}\t\tE = {1}:\t\t{2}", res [i], expect, errRel);
			}
		}

		[Test]
		public void TestCtor ()
		{
			var lista = new int[] { 1, 5, 2, 3, 3 };
			var pesos = new ListaProbabilidad<int> (lista, z => z);
			Assert.AreEqual (lista.Length, pesos.Count);
		}

		[Test]
		public void TestEvents ()
		{
			var probs = new ListaProbabilidad<int> (z => z);

			probs.Add (3);
			probs.Remove (3);
		}

		[Test]
		public void TestExceptions ()
		{
			// Exception suma 0
			var probs = new ListaProbabilidad<int> (z => z);
			Assert.Throws (typeof (ListaProbabilidad<int>.NotMeasureException), new TestDelegate (delegate
			{
				probs.Select ();
			}));

			Assert.Throws (typeof (ListaProbabilidad<int>.NotMeasureException), new TestDelegate (delegate
			{
				probs.Add (-3);
				probs.Select ();
			}));

		}
	}
}