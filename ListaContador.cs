using System;

namespace ListasExtra
{
	/// <summary>
	/// Es sólo una listaPeso de enteros largos.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ListaContador<T> : ListaPeso<T, long>
	{
		public ListaContador ()
			: base ((x, y) => x + y, 0)
		{
		}

		public long CountIf (Func<T, bool> Selector)
		{
			long ret = 0;
			foreach (var x in Keys) {
				if (Selector.Invoke (x)) {
					ret += this [x];
				}
			}
			return ret;

		}
	}
}

