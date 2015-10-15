using NUnit.Framework;
using ListasExtra;

namespace Pruebas
{
	[TestFixture]
	public class TestListaCíclica
	{
		[Test]
		public void Probar()
		{
			var testList = new ListaCíclica<int>();
			for (int i = 0; i < 10; i++)
			{
				testList.Add(i);
			}
			Assert.AreEqual(0, testList.Siguiente);
			for (int i = 10; i < 20; i++)
			{
				testList.Add(i);
			}
			Assert.AreEqual(1, testList.Siguiente);
			Assert.AreEqual(20, testList.Count);


		}
	}
}

