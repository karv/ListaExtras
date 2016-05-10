using System;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Una lista de probabilidades
	/// </summary>
	[Serializable]
	public class ListaProbabilidad<T> : ICollection<T>
	{
		/// <param name="peso">Función que asigna peso a cada elemento</param>
		public ListaProbabilidad (Func<T, double> peso)
		{
			Randomizer = new Random ();
			Peso = peso;
		}

		/// <param name="peso">Función que asigna peso a cada elemento</param>
		/// <param name="rand">Fuente de entropía</param>
		public ListaProbabilidad (Func<T, double> peso, Random rand)
		{
			Randomizer = rand;
			Peso = peso;
		}

		/// <param name="lista">Colección inicial</param>
		/// <param name="peso">Función que asigna peso a cada elemento</param>
		/// <param name="rand">Fuente de entropía</param>
		public ListaProbabilidad (IEnumerable<T> lista,
		                          Func<T, double> peso,
		                          Random rand = null)
			: this (peso)
		{
			if (rand == null)
				rand = new Random ();
			Randomizer = rand;

			foreach (var item in lista)
			{
				Add (item);
			}
		}

		/// <summary>
		/// El generador de pseudoaleatorios.
		/// </summary>
		public Random Randomizer { get; }

		/// <summary>
		/// Función de peso
		/// </summary>
		public Func<T, double> Peso { get; }

		readonly List<T> _data = new List<T> ();

		/// <summary>
		/// Agrega un objeto con peso
		/// </summary>
		public void Add (T obj)
		{
			_data.Add (obj);
			OnAdd?.Invoke (obj);
		}

		/// <summary>
		/// Elimina un objeto
		/// </summary>
		public bool Remove (T obj)
		{
			if (_data.Remove (obj))
			{
				OnRemove?.Invoke (obj);
				return true;
			}
			return false;
				
		}

		/// <summary>
		/// Selecciona aleatoriamente un objeto
		/// </summary>
		public T Select ()
		{
			if (Count == 0)
				throw new NotMeasureException ();

			var norm = NormalizedData ();
			var st = Randomizer.NextDouble ();
			foreach (var d in norm)
			{
				st -= d.Value;
				if (st <= 0)
					return d.Key;
			}
			throw new Exception ("¿Qué pasó aquí?");
		}

		/// <summary>
		/// Devuelve el número de elementos.
		/// </summary>
		public int Count
		{
			get
			{
				return _data.Count;
			}
		}

		/// <summary>
		/// Devuelve la suma de los pesos de sus elementos
		/// </summary>
		protected double Suma ()
		{
			var _dataSnapshot = AsDictionary ();
			double suma = 0;
			foreach (var x in _dataSnapshot)
			{
				if (x.Value < 0)
					throw new NotMeasureException (string.Format (
						"El peso de {0} actualmente tiene un valor negativo {1}.",
						x.Key,
						x.Value));
				suma += x.Value;
			}

			return suma;
		}

		/// <summary>
		/// Normaliza el peso para convertirlo en probabilidad
		/// </summary>
		/// <returns>The data.</returns>
		protected Dictionary<T, double> NormalizedData ()
		{
			var suma = Suma ();
			if (suma == 0)
				throw new NotMeasureException ();
			var _dataSnapshot = AsDictionary ();
			var ret = new Dictionary<T, double> ();
			foreach (var x in _dataSnapshot)
			{
				ret.Add (x.Key, x.Value / suma);
			}

			return ret;
		}

		/// <summary>
		/// Devuelve un dicionario asignando a cada elemento de esta lista su valor actual de peso.
		/// </summary>
		public Dictionary<T, double> AsDictionary ()
		{
			var ret = new Dictionary<T, double> (_data.Count);
			foreach (var x in _data)
			{
				ret.Add (x, Peso (x));
			}
			return ret;
		}

		#region ICollection

		public void Clear ()
		{
			_data.Clear ();
		}

		public bool Contains (T item)
		{
			return _data.Contains (item);
		}

		public void CopyTo (T [] array, int arrayIndex)
		{
			_data.CopyTo (array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return _data.GetEnumerator ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _data.GetEnumerator ();
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region Eventos

		public event Action<T> OnRemove;
		public event Action<T> OnAdd;

		#endregion

		/// <summary>
		/// Exception cuando se intenta usar una medida como probabilidad cuando no lo es.
		/// </summary>
		[Serializable]
		public class NotMeasureException : Exception
		{
			/// <summary>
			/// Initializes a new instance of this class.
			/// </summary>
			public NotMeasureException ()
			{
			}

			/// <param name="message">Message.</param>
			public NotMeasureException (string message)
				: base (message)
			{
			}

			/// <param name="message">Message.</param>
			/// <param name="inner">Inner.</param>
			public NotMeasureException (string message, Exception inner)
				: base (message, inner)
			{
			}

			/// <param name="info">Info.</param>
			/// <param name="context">Context.</param>
			protected NotMeasureException (System.Runtime.Serialization.SerializationInfo info,
			                               System.Runtime.Serialization.StreamingContext context)
				: base (info,
				        context)
			{
			}
		}
	}
}