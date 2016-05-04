using NUnit.Framework;
using ListasExtra;
using System;

namespace Pruebas
{
	[TestFixture]
	public class TestSerial
	{
		[Test]
		[Obsolete]
		public void TestListaPeso ()
		{
			var rr = new ListaPeso<int> ();
			for (int i = 0; i < 5000; i++)
				rr [i] = i;
			Store.BinarySerialization.WriteToBinaryFile ("SomeDictionary.bin", rr);
			var rrClone = Store.BinarySerialization.ReadFromBinaryFile <ListaPeso<int>> ("SomeDictionary.bin");
			Assert.False (ReferenceEquals (rr, rrClone));
			Assert.True (rr.Equals (rrClone));
		}

		[Test]
		public void TestListaCíclica ()
		{
			var cic = new ListaCíclica<int> ();
			for (int i = 0; i < 100; i++)
			{
				cic.Add (i);
			}
			Store.BinarySerialization.WriteToBinaryFile ("SomeCicList.bin", cic);
			var cicClone = Store.BinarySerialization.ReadFromBinaryFile <ListaCíclica<int>> ("SomeCicList.bin");
			Assert.False (ReferenceEquals (cic, cicClone));
			Assert.AreEqual (cic.Count, cicClone.Count);
		}

		[Test]
		public void TestListaProb ()
		{
			var lp = new ListaProbabilidad<int> (i => i);
			for (int i = 0; i < 100; i++)
			{
				lp.Add (i);
			}
			Store.BinarySerialization.WriteToBinaryFile ("SomeProb.bin", lp);
			var lp2 = Store.BinarySerialization.ReadFromBinaryFile <ListaProbabilidad<int>> ("SomeProb.bin");
			Assert.AreEqual (lp2.Peso (3), 3);
		}
	}
}