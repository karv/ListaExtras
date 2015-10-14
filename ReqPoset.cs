using System.Collections.Generic;

namespace ListasExtra.Poset
{
	public class ReqPoset<T>: IReqPoset<T>
	{
		public ReqPoset (T objeto) : this (objeto, new Dictionary<IReqPoset<T>, float> ())
		{
		}

		public ReqPoset (T objeto, IDictionary<IReqPoset<T>, float> reqs)
		{
			Objeto = objeto;
			Reqs = reqs;
		}

		public T Objeto { get; }

		public IDictionary<IReqPoset<T>, float> Reqs { get; }

		/// <summary>
		/// Revisa si este objeto es satisfacido por un diccionario.
		/// No es recursivo
		/// </summary>
		/// <returns>true si comparador satisface este objeto. false en caso contrario. </returns>
		/// <param name="comparador">Diccionatio con qué comparar.</param>
		public bool LoSatisface (IDictionary<T, float> comparador)
		{
			float val;
			foreach (var x in Reqs) {
				if (!comparador.TryGetValue (x.Key.Objeto, out val) || val < x.Value)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Es 'loSatisface' en su forma cualitativa.
		/// </summary>
		/// <returns><c>true</c>, si , <c>false</c> otherwise.</returns>
		/// <param name="comparador">Comparador.</param>
		public bool EsAbierto (ICollection<T> comparador)
		{
			foreach (var x in Reqs) {
				if (!comparador.Contains (x.Key.Objeto))
					return false;
			}
			return true;
		}

		public IDictionary<T, float> EnumerarRequerimientos ()
		{
			var ret = new Dictionary<T, float> ();
			foreach (var x in Reqs) {
				PickMax (ret, x.Key.Objeto, x.Value);
				PickMax (ret, x.Key.EnumerarRequerimientos ());
			}
			return ret;
		}

		static void PickMax (IDictionary<T, float> dict, T key, float nuevo)
		{
			if (dict.ContainsKey (key) && dict [key] < nuevo)
				dict [key] = nuevo;
		}

		static void PickMax (IDictionary<T, float> dict, IDictionary<T, float> comparer)
		{
			foreach (var x in comparer) {
				PickMax (dict, x.Key, x.Value);
			}
		}
	}
}