using System;


namespace Test
{
	class MainClass
	{
		static void TestSet ()
		{
			ListasExtra.Set.Set<int> mySet = new ListasExtra.Set.Set<int> ();
		}

		public static void Main (string[] args)
		{
			ListasExtra.Treelike.TreeList<int> tl = new ListasExtra.Treelike.TreeList<int> ();
			int[] o = { 1, 2, 3 };
			Console.WriteLine (tl.Count);
			tl.Add (o);
			Console.WriteLine (tl.Contains (o));
			Console.WriteLine (tl.Count);
			tl.Remove (o);
			Console.WriteLine (tl.Contains (o));
			Console.WriteLine (tl.Count);
			TestSet ();
			Console.WriteLine ("Hello World!");
		}
	}
}
