using System;

namespace ListasExtra
{
	/// <summary>
	/// Objeto acotado.
	/// </summary>
	public class ObjetoAcotado<T>
	{
		//TODO conversión implícita entre T y esta clase
		public T CotaSup;
		public T CotaInf;
		T _Valor;

		public Func<T, T, Boolean> EsMenor { get; set; }
		//TODO hacer IComparable

		public T Valor {
			get {
				return _Valor;
			}
			set {

				_Valor = EsMenor (value, CotaSup) ? (EsMenor (value, CotaInf) ? CotaInf : value) : CotaSup;
				if (_Valor.Equals (CotaInf)) {
					EventHandler Handler = LlegóMínimo;
					Handler (this, null);
				}
				if (_Valor.Equals (CotaSup)) {
					EventHandler Handler = LlegóMáximo;
					Handler (this, null);
				}
			}
		}

		public ObjetoAcotado (Func<T, T, Boolean> comparador)
		{
			EsMenor = comparador;
		}

		public ObjetoAcotado (Func<T, T, Boolean> comparador, T min, T max, T inicial)
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
		public event EventHandler LlegóMínimo;
		public event EventHandler LlegóMáximo;
	}
}

