using System;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Objeto acotado.
	/// </summary>
	[Obsolete ("Es difícil usar, hacerlo manualmente")]
	public class ObjetoAcotado<T>
	{
		public T CotaSup;
		public T CotaInf;
		T _valor;

		public IComparer<T> Comparador { get; }

		bool EsMenor (T x, T y)
		{
			return Comparador.Compare (x, y) == -1;
		}

		public T Valor {
			get {
				return _valor;
			}
			set {
				_valor = EsMenor (value, CotaSup) ? (EsMenor (value, CotaInf) ? CotaInf : value) : CotaSup;
				if (_valor.Equals (CotaInf))
					AlLlegarMínimo?.Invoke ();
				
				if (_valor.Equals (CotaSup))
					AlLlegarMáximo?.Invoke ();

				AlCambiar.Invoke ();
			}
		}

		public ObjetoAcotado (IComparer<T> comparador)
		{
			Comparador = comparador;
		}

		public ObjetoAcotado (IComparer<T> comparador, T min, T max, T inicial = default(T))
			: this (comparador)
		{
			CotaInf = min;
			CotaSup = max;
			Valor = inicial;
		}

		public override string ToString ()
		{
			return Valor.ToString ();
		}
		//Eventos
		public event Action AlLlegarMínimo;
		public event Action AlLlegarMáximo;
		public event Action AlCambiar;

		// operadores
		public static implicit operator T (ObjetoAcotado<T> obj)
		{
			return obj.Valor;
		}
	}
}