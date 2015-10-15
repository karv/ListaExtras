using System.Collections.Generic;

namespace ListasExtra
{
	public class ListaCíclica<T>:List<T> //TEST
	{
		int _internalZero;

		int InternalZero {
			get {
				return _internalZero;
			}
			set {
				_internalZero = value % Count;
			}
		}

		public ListaCíclica () : base ()
		{
		}

		public ListaCíclica (IEnumerable<T> coll) : base (coll)
		{
		}

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
			Insert (InternalZero, objeto);
			InternalZero++;
		}
	}
}