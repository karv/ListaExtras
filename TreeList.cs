using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Lista en árbol.
	/// Note que siempre {} \in this
	public class TreeList<T> : TreeNode<T>, IEnumerable<TreePath<T>>
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
			getSucc.Clear ();
		}



		public bool Contains (T[] item)
		{
			ITreeNode<T> iter = this;
			foreach (var x in item) {
				if (!iter.getSucc.Exists (y => y.objeto.Equals (item)))
					return false;
				else
					iter = iter.getSucc.Find (y => y.objeto.Equals (item));
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

		public bool Remove (TreePath<T> item)
		{
			throw new NotImplementedException ();
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

		bool IsReadOnly {
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
			return new Treelike.TreeEnumerator<T> (getSucc, new TreePath<T> (objeto));
		}

		System.Collections.Generic.IEnumerator<TreePath<T>> System.Collections.Generic.IEnumerable<TreePath<T>>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, new TreePath<T> (objeto));
		}

	}
}

