using NUnit.Framework;
using ListasExtra;
using System.Collections.Generic;

namespace Pruebas
{
	[TestFixture]
	public class TestListaCíclica
	{
		[Test]
		public void Probar ()
		{
			var testList = new ListaCíclica<int> ();
			for (int i = 0; i < 10; i++)
			{
				testList.Add (i);
			}
			Assert.AreEqual (0, testList.Siguiente);
			for (int i = 10; i < 20; i++)
			{
				testList.Add (i);
			}
			Assert.AreEqual (1, testList.Siguiente);
			Assert.AreEqual (20, testList.Count);

			testList.Insert (3, 20);
			Assert.AreEqual (2, testList.Siguiente);
			System.Console.WriteLine (testList);

			System.Console.WriteLine (testList [0]);
			foreach (var i in testList)
			{
				System.Console.Write (i + "|");
			}
		}

		[Test]
		public void ProbarCtor ()
		{
			var l = new List<int> ();
			for (int i = 0; i < 100; i++)
			{
				l.Add (i);
			}
			var test = new ListaCíclica<int> (l);
			Assert.AreEqual (100, test.Count);
			Assert.AreEqual (0, test.Siguiente);
			System.Console.WriteLine (test);
		}

		[Test]
		public void ProbarSkip ()
		{
			var test = new ListaCíclica<int> ();
			for (int i = 0; i < 100; i++)
			{
				test.Add (i);
			}
			test.Skip (5);
			Assert.AreEqual (5, test.Peek);
			test.Skip (-8);
			Assert.AreEqual (97, test.Peek);
		}

		[Test]
		public void ProbarRemover ()
		{
			var test = new ListaCíclica<int> ();
			test.Add (0);
			test.Add (1);
			test.Add (2);
			test.Move (1);
			Assert.AreEqual (1, test [0]);
			test.RemoveAt (0);
			Assert.AreEqual (2, test [0]);
			test.Clear ();
			test.Move (3);
			test.Add (3);
			Assert.AreEqual (3, test [0]);

			test.Clear ();
			test.Add (0);
			test.Add (1);
			test.Add (2);
			test.RemoveAt (0);
			test.RemoveAt (0);
			test.RemoveAt (0);
			test.Add (3);
		}
	}
}