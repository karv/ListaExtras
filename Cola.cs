using System;
using System.Collections.Generic;
using System.Collections;

namespace ListasExtra.Cola
{
	public class Cola<T>: ICola<T>
	{
		public Cola ()
		{
			list = new List<T> ();
		}

		public Cola (IEnumerable<T> data)
		{
			list = new List<T> (data);
		}

		readonly List<T> list;

		/// <summary>
		/// Toma el siguiente objeto en la cola, y lo elimina.
		/// </summary>
		/// <returns>Devuelve el último objeto de la cola.</returns>
		public T Tomar ()
		{
			var ret = Mirar;
			list.RemoveAt (0);
			return ret;
		}

		/// <summary>
		/// Agrega un objeto a la cola.
		/// </summary>
		/// <param name="obj">Objeto a agregar</param>
		public void Encolar (T obj)
		{
			list.Add (obj);
		}

		/// <summary>
		/// Devuelve el último objeto en la cola. No lo elimina
		/// </summary>
		/// <value>The mirar.</value>
		public T Mirar {
			get {
				if (list.Count == 0)
					throw new Exception ("Imposible, cola vacía.");
				return list [0];
			}
		}

		public ulong Count { get { return (ulong)list.Count; } }

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return list.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return list.GetEnumerator ();
		}
	}
}

