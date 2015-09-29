using System;
using NUnit.Framework;
using System.Collections.Generic;
using ListasExtra.Extensiones;

namespace Pruebas
{
	[TestFixture]
	public class IListExt
	{
		[Test]
		public void TestIListGetRandom ()
		{
			var list = new List<int> ();
			for (int i = 0; i < 1000; i++) {
				list.Add (i);
			}
			for (int j = 0; j < 1000; j++) {
				int i = list.Aleatorio ();
				Console.Write (i + " ");
				Assert.True (list.Remove (i));
			}
			Assert.AreEqual (0, list.Count);
		}

		[Test]
		public void TestCollectionRandom ()
		{
			var list = new List<int> ();
			for (int i = 0; i < 1000; i++) {
				list.Add (i);
			}
			for (int j = 0; j < 1000; j++) {
				int i = list.Aleatorio ();
				Console.Write (i + " ");
				Assert.True (list.Remove (i));
			}
			Assert.AreEqual (0, list.Count);
		}

		[Test]
		public void TestEmptyCollectionRandom ()
		{
			Assert.Throws<IndexOutOfRangeException> (
				new TestDelegate (
					delegate {
						ICollection<int> g = new List<int> ();
						g.Aleatorio ();
					}
				)
			);
		}
	}
}