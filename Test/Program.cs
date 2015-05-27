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
			TestSet();
			Console.WriteLine("Hello World!");
		}
	}
}
