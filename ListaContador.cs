using System;

namespace ListasExtra
{
	/// <summary>
	/// Es sólo una listaPeso de enteros largos.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	[Obsolete]
	public class ListaContador<T> : ListaPeso<T, long>
	{
		public ListaContador ()
			: base ((x, y) => x + y, 0)
		{
		}

		public long ContarSi (Func<T, bool> selector)
		{
			long ret = 0;
			foreach (var x in Keys)
			{
				if (selector.Invoke (x))
				{
					ret += this [x];
				}
			}
			return ret;

		}
	}
}