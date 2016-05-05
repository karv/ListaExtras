using System;
using System.Collections.Generic;
using System.Collections;

namespace ListasExtra.Cola
{
	/// <summary>
	/// Representa una cola de objetos FIFO
	/// </summary>
	public class Cola<T> : ICola<T>
	{
		public Cola ()
		{
			Lista = new List<T> ();
		}

		/// <param name="data">Cola inicial</param>
		public Cola (IEnumerable<T> data)
		{
			Lista = new List<T> (data);
		}

		/// <summary>
		/// La lista que representa a la cola
		/// </summary>
		protected readonly List<T> Lista;

		/// <summary>
		/// Toma el siguiente objeto en la cola, y lo elimina.
		/// </summary>
		/// <returns>Devuelve el último objeto de la cola.</returns>
		public T Tomar ()
		{
			var ret = Mirar;
			Lista.RemoveAt (0);
			return ret;
		}

		/// <summary>
		/// Agrega un objeto a la cola.
		/// </summary>
		/// <param name="obj">Objeto a agregar</param>
		public void Encolar (T obj)
		{
			Lista.Add (obj);
		}

		/// <summary>
		/// Devuelve el último objeto en la cola. No lo elimina
		/// </summary>
		/// <value>The mirar.</value>
		public T Mirar
		{
			get
			{
				if (Lista.Count == 0)
					throw new Exception ("Imposible, cola vacía.");
				return Lista [0];
			}
		}

		/// <summary>
		/// Número de objetos encolados
		/// </summary>
		/// <value>The count.</value>
		public ulong Count { get { return (ulong)Lista.Count; } }

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return Lista.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Lista.GetEnumerator ();
		}
	}
}