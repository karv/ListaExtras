using System.Collections.Generic;

namespace ListasExtra.Extensiones
{
	public static class IDictionaryExt
	{
		public static IDictionary<TKey, TVal> Clonar<TKey, TVal> (this IDictionary<TKey, TVal> clonando)
		{
			return new Dictionary<TKey, TVal> (clonando);
		}
	}
}

