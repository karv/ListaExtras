using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	public interface ITreeList<T>: ICollection<T[]>
	{
		/// <summary>
		/// Devuelve true si se enumera el pseudonodo actual.
		/// </summary>
		/// <value><c>true</c> if enumera actual; otherwise, <c>false</c>.</value>
		bool enumeraActual{ get; }

		/// <summary>
		/// Devuelve true sólo si este nodo es la raíz del árbol.
		/// </summary>
		/// <value><c>true</c> if is root; otherwise, <c>false</c>.</value>
		bool isRoot{ get; }

		/// <summary>
		/// Devuelve el objeto del nodo.
		/// </summary>
		/// <value>The nodo.</value>
		T nodo{ get; }

		/// <summary>
		/// Devuelve la base del árbol.
		/// </summary>
		/// <value>The get base.</value>
		T[] getBase { get; }

		/// <summary>
		/// Devuelve la lista de sucesores de este nodo.
		/// </summary>
		/// <value>The succ.</value>
		IEnumerable<ITreeList<T>> succ { get; }
	}
}

