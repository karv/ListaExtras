using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Representa un camino en un árbol.
	/// </summary>
	public class TreePath<T> : IEnumerable<T>, IEquatable<TreePath<T>>
	{
		T[] _dat;

		public TreePath (T[] dat)
		{
			_dat = dat;
		}

		public TreePath ()
		{
		}

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

		public static implicit operator TreePath<T> (T[] array)
		{
			return new TreePath<T> (array);
		}

		public static implicit operator T[] (TreePath<T> path)
		{
			return path._dat;
		}

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

		#endregion

		#region IEnumerable implementation

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return (IEnumerator<T>)_dat.GetEnumerator ();
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

