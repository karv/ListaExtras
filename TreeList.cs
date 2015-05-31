using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Lista en árbol.
	/// Note que siempre {} \in this
	public class TreeList<T> : TreeNode<T>, ICollection<TreePath<T>>
	/// </summary>
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
			Add ((TreePath<T>)item); 
		}

		public void Add (TreePath<T> item) //TODO probar.
		{
			ITreeNode<T> iter = this;
			TreePath<T> objz;
			for (int i = 1; i <= item.Length; i++) {
				objz = item.getSecciónInicial (i);
				if (Contains (objz))
					iter = getTreeFrom (objz);
				else {
					ITreeNode<T> Agrega = new TreeNode<T> ((T[])objz);
					iter.getSucc.Add (Agrega);
					iter = Agrega;
				}
			}
		}

		public void Add (T item)
		{			
			T[] Adding = new T[objeto.Length + 1];

			objeto.CopyTo (Adding, 0);
			Adding [objeto.Length] = item;
			Add ((TreePath<T>)Adding); 
		}

		public void Add (ITreeNode<T> item)
		{
			getSucc.Add (item);
		}

		public void Clear ()
		{
			getSucc.Clear ();
		}

		public bool Contains (T[] item)
		{
			return Contains ((TreePath<T>)item); 
		}

		public bool Contains (TreePath<T> item)
		{
			ITreeNode<T> iter = this;
			int i = 0;
			foreach (var x in item) {
				if (!iter.getSucc.Exists (y => y.objeto.Equals (item.getSecciónInicial (++i))))
					return false;
				else
					iter = iter.getSucc.Find (y => y.objeto.Equals (item.getSecciónInicial (i)));
			}
			return true;
		}

		public void CopyTo (TreePath<T>[] array, int arrayIndex)
		{			
			// TODO por probar
			for (int i = 0; i < arrayIndex; i++) {
				array [i] = getSucc [arrayIndex + i].objeto;
			}
		}

		public bool Remove (T[] item)
		{
			return Remove ((TreePath<T>)item); 
		}

		public bool Remove (TreePath<T> item)
		{
			ITreeNode<T> iter = this;
			int ctr = item.Length;

			if (ctr == 0)
				getSucc.Clear ();
			
			foreach (var x in item) {
				if (ctr == 1) { // Si x = last(item)
					return (iter.getSucc.RemoveAll (y => y.objeto.Equals (item)) > 0);
				}

				if (!iter.getSucc.Exists (y => y.objeto.Equals (item.getSecciónInicial (item.Length - ctr + 1))))
					return false;
				else {
					iter = iter.getSucc.Find (y => y.objeto.Equals (item.getSecciónInicial (item.Length - ctr + 1)));
					ctr--;
				}
					
			}
			return true;
		}

		public int Count { //TEST
			get {
				int ret = 1;
				foreach (var x in getSucc) {
					ret += getSucc.Count;
				}
				return ret;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
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

		System.Collections.Generic.IEnumerator<TreePath<T>> System.Collections.Generic.IEnumerable<TreePath<T>>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, objeto);
		}

	}
}

