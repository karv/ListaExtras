using System;
using ListasExtra;
using System.Diagnostics;


namespace Test
{
	class MainClass
	{
		static void TestSet()
		{
			//ListasExtra.Set.Set<int> mySet = new ListasExtra.Set.Set<int> ();
		}

		static void TestLock()
		{
			ListasExtra.Lock.ListaPesoBloqueable<int, int> bv = new ListasExtra.Lock.ListaPesoBloqueable<int, int>(((x, y) => x + y), 0);
		
			bv[3] = 1;
			bv[4] = 1;
			foreach (var x in bv)
			{
				bv.Add(5, 1);
			}

			System.Console.WriteLine("");

			//ListasExtra.Lock.LockEnumerator
		}

		static void TestListaPeso()
		{
			var lp = new ListaPeso<int>();
			for (int i = 0; i < 100; i++)
			{
				lp[i] = 99 - i;
			}
			Debug.WriteLine(lp);
		}

		public static void TestTree()
		{
			ListasExtra.Treelike.TreeList<int> tl = new ListasExtra.Treelike.TreeList<int>();
			int[] o = { 1, 2, 3 };
			Console.WriteLine(tl.Count);
			tl.Add(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
			tl.Remove(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
		}

		public static void Main(string[] args)
		{
			TestListaPeso();
		}
	}
}
