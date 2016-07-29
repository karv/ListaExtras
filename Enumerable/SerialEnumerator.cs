using System.Collections.Generic;

namespace ListasExtra.Enumerable
{
	/// <summary>
	/// Un enumerable que representa el producto lexicográfico de dos enumerables.
	/// </summary>
	public class SerialEnumerable<T> : IEnumerable<T>
	{
		/// <summary>
		/// El enumerador de enumeradores
		/// </summary>
		protected IEnumerable<IEnumerable<T>> Enum;

		/// <summary>
		/// </summary>
		/// <param name="enume">Enumerator</param>
		public SerialEnumerable (IEnumerable<IEnumerable<T>> enume)
		{
			Enum = enume;
		}

		IEnumerator<T> Enumerate ()
		{
			foreach (var x in Enum)
				foreach (var y in x)
					yield return y;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return Enumerate ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return Enumerate ();
		}
	}
}