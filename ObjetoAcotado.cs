using System.Runtime.Serialization;
using System;

namespace ListasExtra
{
	[DataContract]
	public class ObjetoAcotado<T>
	{
		public T CotaSup;
		public T CotaInf;
		T _Valor;

		public Func<T, T, Boolean> EsMenor { get; set; }

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

