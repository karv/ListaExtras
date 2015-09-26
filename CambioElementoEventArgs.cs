using System;

namespace ListasExtra
{
	public class CambioElementoEventArgs<TKey, TVal> : EventArgs
	{
		
		public readonly TKey Key;
		/// <summary>
		/// Valor previo al cambio
		/// </summary>
		public readonly TVal Previo;
		/// <summary>
		/// Valor actual
		/// </summary>
		public readonly TVal Actual;

		public CambioElementoEventArgs (TKey key, TVal prev, TVal post)
		{
			Key = key;
			Previo = prev;
			Actual = post;
		}
	}
}

