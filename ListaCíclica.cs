using System.Collections.Generic;
using System.Collections;

namespace ListasExtra
{
	public class ListaCíclica<T>:List<T>, IEnumerable<T>, IEnumerable //TEST
	{
		int _internalZero;

		int InternalZero {
			get {
				return _internalZero;
			}
			set {
				_internalZero = value % Count;
				if (_internalZero < 0)
					_internalZero += Count;
			}
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public ListaCíclica () : base ()
		{
		}

		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		/// <param name="coll">Colección inicial</param>
		public ListaCíclica (IEnumerable<T> coll) : base (coll) //TEST
		{
		}

		/// <summary>
		/// Gets or sets the element with the specified i.
		/// </summary>
		/// <param name="i">The index.</param>
		public new T this [int i] {
			get {
				return base [(i + InternalZero) % Count];
			}
			set {
				base [(i + InternalZero) % Count] = value;
			}
		}

		/// <summary>
		/// Devuelve el siguiente elemento de la lista
		/// </summary>
		public T Siguiente {
			get {
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
		public T Peek {
			get {
				return this [0];
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			for (int i = 0; i < Count; i++) {
				yield return this [i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			for (int i = 0; i < Count; i++) {
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

		public override string ToString ()
		{
			var ret = string.Format ("ListaCíclica:\n");
			for (int i = 0; i < Count; i++) {
				ret += ":" + this [i];
			}
			return ret;
		}
	}
}