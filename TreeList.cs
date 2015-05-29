using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	public class TreeList<T> : TreeNode<T>, IEnumerable<T[]>
	{
		public enum enumOpcionOrden
		{
			PadrePrimero,
			HijosPrimero
		}

		public TreeList () : base (null)
		{
		}

		public void Add (T[] item)
		{
			getSucc.Add (new TreeNode<T> (item));
		}

		public void Add (T item)
		{			
			T[] Adding = new T[objeto.Length + 1];
			objeto.CopyTo (Adding, 0);
			Adding [objeto.Length] = item;
			Add (Adding); 
		}

		public void Add (ITreeNode<T> item)
		{
			getSucc.Add (item);
		}

		public void Clear ()
		{
			throw new NotImplementedException ();
		}

		public bool Contains (T[] item)
		{
			throw new NotImplementedException ();
		}

		public void CopyTo (T[][] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (T[] item)
		{
			throw new NotImplementedException ();
		}

		public int Count {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsReadOnly {
			get {
				throw new NotImplementedException ();
			}
		}
		/*
		IEnumerator<T> IEnumerable<T[]>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, objeto);
		}
*/
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, objeto);
		}

		System.Collections.Generic.IEnumerator<T[]> System.Collections.Generic.IEnumerable<T[]>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, objeto);
		}

	}
}

