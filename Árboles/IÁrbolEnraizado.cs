using System.Collections.Generic;

namespace ListasExtra.Árboles
{
	/// <summary>
	/// Representa una estructora de objetos con sucesores definidos.
	/// </summary>
	public interface IÁrbolEnraizado<T>
	{
		/// <summary>
		/// Devuelve el nodo del objeto éste.
		/// </summary>
		/// <value>The nodo.</value>
		T Nodo { get; }

		/// <summary>
		/// Devuelve los sucesores de este objeto
		/// </summary>
		IEnumerable<IÁrbolEnraizado<T>> Sucesores { get; }
	}
}