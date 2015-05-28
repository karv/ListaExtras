using System;
using System.Collections.Generic;
using System.Linq;

namespace ListasExtra.Treelike
{
	public class TreeEndingList <T> :ITreeList<T>
	{
		T _nodo;

		public TreeEndingList (T obj)
		{
			_nodo = obj;
		}

		#region ITreeList implementation

		void ITreeList<T>.AddToList (ICollection<T[]> lst)
		{
			throw new NotImplementedException ();
		}

		ITreeList<T> ITreeList<T>.FindTree (T[] nodo)
		{
			throw new NotImplementedException ();
		}

		bool ITreeList<T>.enumeraActual {
			get {
				return true;
			}
		}

		bool ITreeList<T>.isRoot {
			get {
				throw new NotImplementedException ();
			}
		}

		T ITreeList<T>.nodo {
			get {
				return _nodo;
			}
		}

		T[] ITreeList<T>.getBase {
			get {
				throw new NotImplementedException ();
			}
		}

		ICollection<ITreeList<T>> ITreeList<T>.succ {
			get {
				return new List<ITreeList<T>> ();
			}
		}

		#endregion

		#region ICollection implementation

		void ICollection<T[]>.Add (T[] item)
		{
			throw new NotImplementedException ();
		}

		void ICollection<T[]>.Clear ()
		{
			throw new NotImplementedException ();
		}

		bool ICollection<T[]>.Contains (T[] item)
		{
			throw new NotImplementedException ();
		}

		void ICollection<T[]>.CopyTo (T[][] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		bool ICollection<T[]>.Remove (T[] item)
		{
			throw new NotImplementedException ();
		}

		int ICollection<T[]>.Count {
			get {
				throw new NotImplementedException ();
			}
		}

		bool ICollection<T[]>.IsReadOnly {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator<T[]> IEnumerable<T[]>.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

