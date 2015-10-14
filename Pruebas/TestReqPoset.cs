using NUnit.Framework;
using ListasExtra.Poset;

namespace Pruebas
{
	[TestFixture]
	public class TestReqPoset
	{
		[Test]
		public void TestCase()
		{
			// Crear un árbol de dependencias
			const int numReq = 100;
			var reqs = new ReqPoset<int>[numReq];
			for (int i = 0; i < numReq; i++)
			{
				reqs[i] = new ReqPoset<int>(i);
				for (int j = 1; j < i; j++)
				{
					if (i % j == 0)
						reqs[i].Reqs.Add(reqs[j], 1);
				}
			}

			Assert.AreEqual(0, reqs[0].Reqs.Count);
			Assert.AreEqual(5, reqs[99].Reqs.Count);
			Assert.AreEqual(1, reqs[13].Reqs.Count);
		}
	}
}

