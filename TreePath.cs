using System;
using System.Collections.Generic;
using System.Collections;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Representa un camino en un árbol.
	/// </summary>
	public class TreePath<T> : IEnumerable, IEquatable<TreePath<T>>
	{
		T[] _dat;

		public void CopyTo (T[] Array, int index)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Devuelve una sección inicial de este TreePath
		/// </summary>
		/// <returns>The sección inicial.</returns>
		/// <param name="ht">Altura de la sección inicial</param>
		public TreePath<T> getSecciónInicial (int ht)
		{
			if (Length < ht)
				throw new Exception ("No se puede calcular una sección inicial de longitud mayor que el objeto original.");


			TreePath<T> ret = new TreePath<T> (ht);
			for (int i = 0; i < ht; i++) {
				ret [i] = this [i];
			}

			return ret;
		}

		#region ctor

		public TreePath (T[] dat)
		{
			_dat = dat;
		}

		public TreePath (int cap)
		{
			_dat = new T[cap];
		}

		#endregion

		#region Común

		public T this [int i] {
			get {
				return _dat [i];
			}
			set {
				_dat [i] = value;
			}
		}

		public int Length {
			get {
				return _dat.Length;
			}
		}

		#endregion

		#region Convertidor

		public static implicit operator TreePath<T> (T[] array)
		{
			return new TreePath<T> (array);
		}

		public static implicit operator T[] (TreePath<T> path)
		{
			return path._dat;
		}

		#endregion

		#region IEquatable implementation

		bool IEquatable<TreePath<T>>.Equals (TreePath<T> other)
		{
			if (Length != other.Length)
				return false;
			for (int i = 0; i < Length; i++) {
				if (!this [i].Equals (other [i]))
					return false;
			}
			return true;
		}

		public static bool operator == (TreePath<T> left, TreePath<T> right)
		{
			if (left == null || right == null)
				return false;
			return ((IEquatable<TreePath<T>>)left).Equals (right);
		}

		public static bool operator != (TreePath<T> left, TreePath<T> right)
		{
			return !(left == right);
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _dat.GetEnumerator ();
		}

		#endregion
	}
}

