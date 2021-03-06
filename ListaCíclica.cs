﻿using System.Collections.Generic;
using System.Collections;
using System;

namespace ListasExtra
{
	/// <summary>
	/// Arreglo 1-dimensional de tamaño dinámico de índices cíclicos
	/// </summary>
	[Serializable]
	public class ListaCíclica<T> : List<T>, IEnumerable<T>, IEnumerable
	{
		int _internalZero;

		/// <summary>
		/// El valor de índice que está desfasada esta clase de la List base
		/// </summary>
		protected int InternalZero
		{
			get
			{
				return _internalZero;
			}
			set
			{
				if (Count == 0)
				{
					_internalZero = 0;
					return;
				}
				_internalZero = value % Count;
				if (_internalZero < 0)
					_internalZero += Count;
			}
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public ListaCíclica ()
		{
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="coll">Colección inicial</param>
		public ListaCíclica (IEnumerable<T> coll)
			: base (coll)
		{
		}

		/// <summary>
		/// Mueve la base cero del cursor un entero.
		/// </summary>
		/// <param name="index">Index.</param>
		public void Move (int index)
		{
			InternalZero += index;
		}

		/// <summary>
		/// Gets or sets the element with the specified i.
		/// </summary>
		/// <param name="i">The index.</param>
		public new T this [int i]
		{
			get
			{
				return base [(i + InternalZero) % Count];
			}
			set
			{
				base [(i + InternalZero) % Count] = value;
			}
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public new void RemoveAt (int index)
		{
			base.RemoveAt ((index + InternalZero) % Count);
			InternalZero = _internalZero;
		}

		/// <summary>
		/// Vacía esta lista y devuelve el cursor a cero
		/// </summary>
		public new void Clear ()
		{
			InternalZero = 0;
			base.Clear ();
		}

		/// <summary>
		/// Devuelve el siguiente elemento de la lista
		/// </summary>
		public T Siguiente
		{
			get
			{
				var ret = base [InternalZero];
				InternalZero++;
				return ret;
			}
		}

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="objeto">Objeto.</param>
		public new void Add (T objeto)
		{
			base.Insert (InternalZero, objeto);
			InternalZero++;
		}

		/// <summary>
		/// Insert the specified index and objeto.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="objeto">Objeto.</param>
		public new void Insert (int index, T objeto)
		{
			base.Insert ((index + InternalZero) % Count, objeto);
			if (index == 0 || InternalZero + index > Count)
				InternalZero++;
		}

		/// <summary>
		/// Mira el siguiente elemento de la lista, sin mover el cursor.
		/// </summary>
		public T Peek
		{
			get
			{
				return this [0];
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return this [i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return this [i];
			}
		}

		/// <summary>
		/// Se brinca n elementos
		/// </summary>
		/// <param name="n">número de elementos, acepta negativos</param>
		public void Skip (int n)
		{
			InternalZero += n;
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		public new IEnumerator GetEnumerator ()
		{
			return (this as IEnumerable).GetEnumerator ();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this"/>.
		/// </summary>
		public override string ToString ()
		{
			var ret = string.Format ("ListaCíclica:\n");
			for (int i = 0; i < Count; i++)
			{
				ret += ":" + this [i];
			}
			return ret;
		}
	}
}