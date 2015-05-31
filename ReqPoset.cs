﻿using System;
using System.Collections.Generic;

namespace ListasExtra.Poset
{
	public class ReqPoset<T>: IReqPoset<T>
	{
		readonly T _objeto;

		public T objeto {
			get {
				return _objeto;
			}
		}

		IDictionary <IReqPoset<T>, float> _reqs;

		public IDictionary<IReqPoset<T>, float> reqs {
			get {
				return _reqs;
			}
		}

		public ReqPoset (T objeto)
		{
			_objeto = objeto;
			_reqs = new Dictionary <IReqPoset<T>, float> ();
		}

		public ReqPoset (T objeto, IDictionary<IReqPoset<T>, float> reqs) : this (objeto)
		{
			_reqs = reqs;
		}

		/// <summary>
		/// Revisa si este objeto es satisfacido por un diccionario.
		/// No es recursivo
		/// </summary>
		/// <returns>true si comparador satisface este objeto. false en caso contrario. </returns>
		/// <param name="comparador">Diccionatio con qué comparar.</param>
		public bool loSatisface (IDictionary<T, float> comparador)
		{
			float val;
			foreach (var x in _reqs) {
				if (!comparador.TryGetValue (x.Key.objeto, out val) || val < x.Value)
					return false;
			}
			return true;
		}
	}
}