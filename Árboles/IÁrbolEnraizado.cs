using System;
using System.Collections.Generic;

namespace ListasExtra.Árboles
{
	/// <summary>
	/// Representa una estructora de objetos con sucesores definidos.
	/// </summary>
	public interface IÁrbolEnraizado<T>
	{
		T Nodo { get; }

		IEnumerable<IÁrbolEnraizado<T>> Sucesores { get; }
	}
}