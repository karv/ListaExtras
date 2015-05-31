using System;
using System.Collections.Generic;

namespace ListasExtra.Poset
{
	public interface IReqPoset<T>
	{
		/// <summary>
		/// Objeto vinculado
		/// </summary>
		/// <value>The objeto.</value>
		T objeto{ get; }

		/// <summary>
		/// Requicitos
		/// </summary>
		/// <value>The reqs.</value>
		IDictionary <IReqPoset<T>, float> reqs { get; }

		/// <summary>
		/// Revisa si este objeto es satisfacido por un diccionario.
		/// No debe eser recursivo
		/// </summary>
		/// <returns><c>true</c>, if satisface was loed, <c>false</c> otherwise.</returns>
		/// <param name="comparador">Comparador.</param>
		bool loSatisface (IDictionary <T, float> comparador);
	}

	public static class extIReqPoset
	{
		/// <summary>
		/// Revisa si este objeto es satisfacido por un diccionario.
		/// Es recusrivio
		/// </summary>
		/// <returns><c>true</c>, if satisface her was loed, <c>false</c> otherwise.</returns>
		/// <param name="reqr">Reqr.</param>
		/// <param name="comparador">Comparador.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool loSatisfaceHer<T> (this IReqPoset<T> reqr, IDictionary<T, float> comparador)
		{
			if (!reqr.loSatisface (comparador))
				return false;
			foreach (var x in reqr.reqs) {
				if (!x.Key.loSatisface (comparador))
					return false;
			}
			return true;
		}
	}
}

