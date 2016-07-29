using System;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Comparador de tuplas
	/// </summary>
	public class TupleComparador<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
	{
		/// <summary>
		/// Comparador de la primera entrada
		/// </summary>
		/// <value>The item1 comparer.</value>
		public IEqualityComparer<T1> Item1Comparer { get; }

		/// <summary>
		/// Comparador de la segunda entrada
		/// </summary>
		/// <value>The item2 comparer.</value>
		public IEqualityComparer<T2> Item2Comparer { get; }

		/// <summary>
		/// </summary>
		/// <param name="compa1">Compa1.</param>
		/// <param name="compa2">Compa2.</param>
		public TupleComparador (IEqualityComparer<T1> compa1 = null,
		                        IEqualityComparer<T2> compa2 = null)
		{
			Item1Comparer = compa1 ?? EqualityComparer<T1>.Default;
			Item2Comparer = compa2 ?? EqualityComparer<T2>.Default;
		}

		/// <summary>
		/// Compara dos tuplas por igualdad
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool Equals (Tuple<T1, T2> x, Tuple<T1, T2> y)
		{
			return 
				Item1Comparer.Equals (x.Item1, y.Item1) &&
			Item2Comparer.Equals (x.Item2, y.Item2);
		}

		/// <Docs>The object for which the hash code is to be returned.</Docs>
		/// <para>Returns a hash code for the specified object.</para>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">Object.</param>
		public int GetHashCode (Tuple<T1, T2> obj)
		{
			return Item1Comparer.GetHashCode (obj.Item1) + Item2Comparer.GetHashCode (obj.Item2);
		}
	}
}