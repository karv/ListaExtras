using System.Collections.Generic;

namespace ListasExtra.Cola
{
	/// <summary>
	/// Representa una cola FIFO
	/// </summary>
	public interface ICola<T> : IEnumerable<T>
	{
		/// <summary>
		/// Toma el siguiente objeto en la cola, y lo elimina.
		/// </summary>
		/// <returns>Devuelve el último objeto de la cola.</returns>
		T Tomar ();

		/// <summary>
		/// Devuelve el último objeto en la cola. No lo elimina
		/// </summary>
		T Mirar{ get; }

		/// <summary>
		/// Agrega un objeto a la cola.
		/// </summary>
		/// <param name="obj">Objeto a agregar</param>
		void Encolar (T obj);

		ulong Count{ get; }
	}
}

