using System;
using System.Collections.Generic;

namespace ListasExtra
{
	public class TupleComparador<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
	{
		public IEqualityComparer<T1> Item1Comparer { get; }

		public IEqualityComparer<T2> Item2Comparer { get; }

		public TupleComparador (IEqualityComparer<T1> compa1 = null,
		                        IEqualityComparer<T2> compa2 = null)
		{
			Item1Comparer = compa1 ?? EqualityComparer<T1>.Default;
			Item2Comparer = compa2 ?? EqualityComparer<T2>.Default;
		}

		public bool Equals (Tuple<T1, T2> x, Tuple<T1, T2> y)
		{
			return 
				Item1Comparer.Equals (x.Item1, y.Item1) &&
			Item2Comparer.Equals (x.Item2, y.Item2);
		}

		public int GetHashCode (Tuple<T1, T2> obj)
		{
			return Item1Comparer.GetHashCode (obj.Item1) + Item2Comparer.GetHashCode (obj.Item2);
		}
	}
}