using System;
using NUnit.Framework;
using ListasExtra;
using ListasExtra.Set;
using ListasExtra.Treelike;
using System.Collections.Generic;

namespace Test
{
	[TestFixture]
	public class TestUnit
	{

		[Test]
		public void ProbarTree()
		{
			Tree<char> x = new Tree<char>();

			x.Add("nhue".ToCharArray());
			x.Add("bhua".ToCharArray());
			System.Diagnostics.Debug.WriteLine(x.ToArray());
		}

		[Test]
		public void ProbarStrTree()
		{
			string[] a = {"fdfe", "fsgf"};
			StringTree t = new StringTree(a);
			foreach (string x in t.ToArray()) {
				Console.WriteLine (x);
			}
		}

		[Test]
		public void ProbarSet()
		{
			Set<int> x = new Set<int>();
			for (int i = 0; i < 5235; i++)
			{
				x.Add(i);
			}
			int t = x.Next;		
		}

		const int L = 100;
		const int Cant = 5000;
		const int NumIter = 500;

		List<int[]> lista;
		Tree<int> ST;
		[SetUp]
		public void VelocidadSetup()
		{
			Random r = new Random();


			lista = new List<int[]>();
			for (int i = 0; i < Cant; i++)
			{
				int maxi = r.Next(L);
				int[] a = new int[maxi];
				for (int j = 0; j < maxi; j++) {
					a[j] = r.Next(256);
				}
				lista.Add(a);
			}
			ST = new Tree<int>(lista);
		}
		[Test]
		public void VelocidadLista()
		{
			Random r = new Random();
			for (int i = 0; i < NumIter; i++)
			{
				int maxi = r.Next(L);
				int[] a = new int[maxi];
				for (int j = 0; j < maxi; j++) {
					a[j] = r.Next(256);
				}
				if (lista.Exists(x => TestUnit.MemberwiseEq(x, a)) != ST.Contains(a))
				{
					ST.Contains(a);
					Assert.AreEqual(lista.Exists(x => TestUnit.MemberwiseEq(x, a)), ST.Contains(a), 
					                "lista piensa que un elemento pertenece\n" +
						"ST no.");
					throw new Exception();
				}
			}
		}

		public static bool MemberwiseEq (int[] a, int[] b)
		{
			if (a.Length != b.Length)
				return false;
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
					return false;
			}
			return true;
		}

		[Test]
		public void VelocidadTree()
		{
			Random r = new Random();
			for (int i = 0; i < NumIter; i++)
			{
				int maxi = r.Next(L);
				int[] a = new int[maxi];
				for (int j = 0; j < maxi; j++) {
					a[j] = r.Next(256);
				}
				ST.Contains(a);
			}
		}
	}
}

