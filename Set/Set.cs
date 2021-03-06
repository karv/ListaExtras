﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto de elementos sin un control sobre el orden.
	/// </summary>
	/// <typeparam name="T">Tipo de objetos</typeparam>
	[DebuggerDisplayAttribute ("Count = {Count}")]
	public class Set<T> : HashSet<T>, ISet<T>
	{
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public Set ()
		{
		}

		/// <param name="inicial">Inicial.</param>
		public Set (IEnumerable<T> inicial)
			: base (inicial)
		{
		}

		/// <summary>
		/// Toma un elemento
		/// </summary>
		public T Pick ()
		{
			using (var en = GetEnumerator ())
				if (en.MoveNext ())
					return en.Current;
			throw new EmptySetException ("No se puede tomar un elemento de un conjunto vacío.");
		}

		/// <summary>
		/// Toma un elemento y lo remueve
		/// </summary>
		/// <returns>The remove.</returns>
		public T PickRemove ()
		{
			var ret = Pick ();
			Remove (ret);
			return ret;
		}

		/// <summary>
		/// Revisa si este Set es igual (conjuntista) a un IEnumerable del mismo tipo.
		/// </summary>
		/// <param name="other">Other.</param>
		/// <param name="comparer">Comparer.</param>
		public bool Equals (object other,
		                    System.Collections.IEqualityComparer comparer)
		{
			if (comparer.GetHashCode (other) != comparer.GetHashCode (this))
				return false;

			// TODO: ¿Y lo comparer-oso?
			if (other is IEnumerable<T>)
				return SetEquals ((IEnumerable<T>)other);

			return false;
		}

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns>The hash code.</returns>
		/// <param name="comparer">Comparer.</param>
		public int GetHashCode (System.Collections.IEqualityComparer comparer)
		{
			return this.Sum (z => comparer.GetHashCode (z));
		}
	}
}