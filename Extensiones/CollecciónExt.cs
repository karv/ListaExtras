using System;
using System.Collections.Generic;

namespace ListasExtra.Extensiones
{
	public static class CollecciónExt
	{
		static readonly Random random = new Random ();

		/// <summary>
		/// Devuelve un elemento aleatorio de una lista.
		/// </summary>
		/// <param name="lista">Lista.</param>
		/// <param name="r">The red component.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Aleatorio<T> (this IList<T> lista, Random r = null)
		{
			if (lista == null)
				throw new NullReferenceException ();
			if (lista.Count == 0)
				throw new IndexOutOfRangeException ("No se puede tomar un elemento de una lista vacía.");
			
			r = r ?? random;
			return lista [r.Next (lista.Count)];
		}

		public static T Aleatorio<T> (this ICollection<T> coll, Random r = null)
		{
			if (coll == null)
				throw new NullReferenceException ();
			if (coll.Count == 0)
				throw new IndexOutOfRangeException ("No se puede tomar un elemento de una colección vacía.");

			r = r ?? random;
			int currrnd = coll.Count;
			foreach (var x in coll) {
				if (r.Next (currrnd--) == 0) {
					return x;
				}
			}
			throw new Exception ("¡¡¡Ocurrió algo raro!!!");
		}
	}
}

