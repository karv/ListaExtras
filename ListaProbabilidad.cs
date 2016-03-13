using System;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Una lista de probabilidades
	/// </summary>
	public class ListaProbabilidad<T>
	{
		public ListaProbabilidad (Func<T, double> peso)
		{
			Randomizer = new Random ();
			Peso = peso;
		}

		public ListaProbabilidad (Func<T, double> peso, Random rand)
		{
			Randomizer = rand;
			Peso = peso;
		}

		/// <summary>
		/// El generador de pseudoaleatorios.
		/// </summary>
		public Random Randomizer { get; }

		/// <summary>
		/// Función de peso
		/// </summary>
		public Func<T, double> Peso { get; }

		readonly Dictionary<T, double> _data = new Dictionary<T, double> ();

		/// <summary>
		/// Agrega un objeto con peso
		/// </summary>
		public void Add (T obj)
		{
			_data.Add (obj, Peso (obj));
		}

		/// <summary>
		/// Elimina un objeto
		/// </summary>
		public void Remove (T obj)
		{
			_data.Remove (obj);
		}

		/// <summary>
		/// Selecciona aleatoriamente un objeto
		/// </summary>
		public T Select ()
		{
			if (Count == 0)
				throw new NotMeasureException ();

			var norm = _normalizedData ();
			var st = Randomizer.NextDouble ();
			foreach (var d in norm) {
				st -= d.Value;
				if (st <= 0)
					return d.Key;
			}
			throw new Exception ("¿Qué pasó aquí?");
		}

		public int Count {
			get {
				return _data.Count;
			}
		}

		public double Suma ()
		{
			var _dataSnapshot = new Dictionary<T, double> (_data);
			double suma = 0;
			foreach (var x in _dataSnapshot) {
				suma += x.Value;
			}

			return suma;
		}

		Dictionary<T, double> _normalizedData ()
		{
			var suma = Suma ();
			if (suma == 0)
				throw new NotMeasureException ();
			var _dataSnapshot = new Dictionary<T, double> (_data);
			var ret = new Dictionary<T, double> ();
			foreach (var x in _dataSnapshot) {
				ret.Add (x.Key, x.Value / suma);
			}

			return ret;
		}


		[Serializable]
		/// <summary>
		/// Exception cuando se intenta usar una medida como probabilidad cuando no lo es.
		/// </summary>
		public class NotMeasureException : Exception
		{
			public NotMeasureException ()
			{
			}

			public NotMeasureException (string message)
				: base (message)
			{
			}

			public NotMeasureException (string message, Exception inner)
				: base (message, inner)
			{
			}

			protected NotMeasureException (System.Runtime.Serialization.SerializationInfo info,
			                               System.Runtime.Serialization.StreamingContext context)
				: base (info,
				        context)
			{
			}
		}
	}
}