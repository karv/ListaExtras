using System;

namespace ListasExtra
{
	/// <summary>
	/// Es sólo una listaPeso de enteros largos.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ListaContador<T> : ListaPeso<T, long>
	{
		/// <summary>
		/// </summary>
		public ListaContador ()
			: base ((x, y) => x + y, 0)
		{
		}

		/// <summary>
		/// Suma los valores de las entradas seleccionadas por un delegado.
		/// </summary>
		/// <returns>La suma cherrypicked</returns>
		/// <param name="selector">Selector de sumandos</param>
		public long ContarSi (Func<T, bool> selector)
		{
			long ret = 0;
			foreach (var x in Keys)
				if (selector.Invoke (x))
					ret += this [x];
			return ret;
		}
	}
}