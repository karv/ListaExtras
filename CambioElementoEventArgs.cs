using System;

namespace ListasExtra
{
	/// <summary>
	/// Argumento de un evento de cambio de valor de un diccionario
	/// </summary>
	public class CambioElementoEventArgs<TKey, TVal> : EventArgs
	{
		/// <summary>
		/// Key que cambia
		/// </summary>
		public readonly TKey Key;
		/// <summary>
		/// Valor previo al cambio
		/// </summary>
		public readonly TVal Previo;
		/// <summary>
		/// Nuevo valor
		/// </summary>
		public readonly TVal NuevoValor;

		/// <summary>
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="prev">Anterior.</param>
		/// <param name="post">Nuevo.</param>
		public CambioElementoEventArgs (TKey key, TVal prev, TVal post)
		{
			Key = key;
			Previo = prev;
			NuevoValor = post;
		}
	}
}