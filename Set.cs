using System;
using System.Collections.Generic;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto de elementos sin un control sobre el orden.
	/// </summary>
	/// <typeparam name="T">Tipo de objetos</typeparam>
	public class Set<T> : List<T>
	{
		Random r = new Random ();

		/// <summary>
		/// Agrega un objeto al conjunto.
		/// </summary>
		/// <param name="x"></param>
		public new void Add (T x)
		{
			base.Insert (r.Next (Count + 1), x);
		}

		/// <summary>
		/// Regresa un arreglo con los objetos en el conjunto, sin repetición.
		/// </summary>
		/// <returns></returns>
		public new T[] ToArray ()
		{
			return base.ToArray ();
		}

		/// <summary>
		/// Devuelve un elemento de este conjunto.
		/// </summary>
		public T Next {
			get {
				return base [r.Next (Count)];
			}
		}
	}
}