using System;


namespace Test
{
	class MainClass
	{
		static void TestSet()
		{
			ListasExtra.Set.Set<int> mySet = new ListasExtra.Set.Set<int>();
		}

		public static void Main(string[] args)
		{
			ListasExtra.Treelike.TreeList<int> tl = new ListasExtra.Treelike.TreeList<int>();
			int[] o = { 1, 2, 3 };
			tl.Add(o);
			Console.WriteLine(tl.Contains(o));
			tl.Remove(o);
			Console.WriteLine(tl.Contains(o));

			TestSet();
			Console.WriteLine("Hello World!");
		}
	}
}
