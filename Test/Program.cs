using System;
using ListasExtra;
using System.Diagnostics;
using ListasExtra.Treelike;


namespace Test
{
	class MainClass
	{
		static void TestSet()
		{
			// TODO
		}

		static void TestLock()
		{
			var bv = new ListasExtra.Lock.ListaPesoBloqueable<int, int>(((x, y) => x + y), 0);
		
			bv[3] = 1;
			bv[4] = 1;
			// Analysis disable UnusedVariable.Compiler
			foreach (var x in bv)
			{
				bv.Add(5, 1);
			}
			// Analysis restore UnusedVariable.Compiler

			Console.WriteLine("");

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
			lp.AlCambiarValor += (sender, e) => 
				Debug.WriteLine(string.Format("lp[{0}] cambió de {1} a {2}", e.Key, e.Previo, e.Actual));
			lp[1] = 98;
		}

		public static void TestTree()
		{
			var tl = new TreeList<int>();
			int[] o = { 1, 2, 3 };
			Console.WriteLine(tl.Count);
			tl.Add(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
			tl.Remove(o);
			Console.WriteLine(tl.Contains(o));
			Console.WriteLine(tl.Count);
		}

		public static void Main()
		{
			TestListaPeso();
		}
	}
}