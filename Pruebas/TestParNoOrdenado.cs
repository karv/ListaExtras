using NUnit.Framework;
using ListasExtra;
using System;
using System.Collections.Generic;

namespace Pruebas
{
	[TestFixture]
	public class TestParNoOrdenado
	{
		[Test]
		public void TestCtor ()
		{
			var p = new ParNoOrdenado<int> (0, 1);
			Assert.True (p.Contiene (0));
			Assert.True (p.Contiene (1));
			Assert.False (p.Contiene (2));

			Assert.AreEqual (0, p.Excepto (1));
			Assert.AreEqual (1, p.Excepto (0));

			Assert.AreEqual (p, new ParNoOrdenado<int> (1, 0));
			Assert.Throws<Exception> (delegate
			{
				p.Excepto (2);
			});

			Assert.AreEqual (p [0], 0);
			Assert.AreEqual (p [1], 1);
			Assert.Throws<IndexOutOfRangeException> (delegate
			{
				p.Excepto (p [2]);
			});

			Assert.Throws<IndexOutOfRangeException> (delegate
			{
				p.Excepto (p [-1]);
			});

			Assert.True (p.Equals (new ParNoOrdenado<int> (1, 0)));
		}

		[Test]
		public void TestParPosibilidad ()
		{
			const int A = -1;
			var B = new List<int> ();
			var pp = new ParPosibilidad<int> (A, B);

			for (int i = 0; i < 100; i++)
			{
				B.Add (i);
			}

			Assert.True (pp.EsConsistenteCon (new ParNoOrdenado<int> (-1, 0)));
			Assert.True (pp.EsConsistenteCon (new ParNoOrdenado<int> (-1, 1)));
			Assert.False (pp.EsConsistenteCon (new ParNoOrdenado<int> (1, 0)));
			Assert.False (pp.EsConsistenteCon (new ParNoOrdenado<int> (1, 1)));
			Assert.False (pp.EsConsistenteCon (new ParNoOrdenado<int> (-1, -1)));
			Assert.False (pp.EsConsistenteCon (new ParNoOrdenado<int> (-1, 100)));

			Console.WriteLine (pp);
		}

		[Test]
		public void TestParConNulos ()
		{
			var par1 = new ParNoOrdenado<string> ("1", null);
			var par2 = new ParNoOrdenado<string> (null, "1");
			Assert.AreEqual (par1, par2);

			par1 = new ParNoOrdenado<string> (null, null);
			Assert.AreNotEqual (par1, par2);
			par2 = new ParNoOrdenado<string> (null, null);
			Assert.AreEqual (par1, par2);

			par1 = new ParNoOrdenado<string> ("1", "0");
			par2 = new ParNoOrdenado<string> ("0", "1");
			Assert.AreEqual (par1, par2);
			par2 = new ParNoOrdenado<string> ("1", "1");
			Assert.AreNotEqual (par1, par2);
		}
	}
}