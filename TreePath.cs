using System;
using System.Collections;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Representa un camino en un árbol.
	/// </summary>
	public class TreePath<T> : IEnumerable, IEquatable<TreePath<T>>
	{
		readonly T[] _dat;

		public void CopyTo (T[] array, int index)
		{
			_dat.CopyTo (array, index);
		}

		/// <summary>
		/// Devuelve una sección inicial de este TreePath
		/// </summary>
		/// <returns>The sección inicial.</returns>
		/// <param name="ht">Altura de la sección inicial</param>
		public TreePath<T> SecciónInicial (int ht)
		{
			if (Length < ht)
				throw new Exception ("No se puede calcular una sección inicial de longitud mayor que el objeto original.");


			var ret = new TreePath<T> (ht);
			for (int i = 0; i < ht; i++) {
				ret [i] = this [i];
			}

			return ret;
		}

		public T Último {
			get {
				return _dat [_dat.Length - 1];
			}
		}

		public override string ToString ()
		{
			return string.Concat (_dat);
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

		public static explicit operator TreePath<T> (T[] array)
		{
			return new TreePath<T> (array);
		}

		public static explicit operator T[] (TreePath<T> path)
		{
			return path._dat;
		}

		#endregion

		#region IEquatable implementation

		public bool Equals (TreePath<T> other)
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

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _dat.GetEnumerator ();
		}

		#endregion
	
	}
}

