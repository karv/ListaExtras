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



		public void Add (TreePath<T> item) //TODO probar.
		{
			ITreeNode<T> iter = this;
			T[] objz;
			for (int i = 1; i < item.Length; i++) {
				objz = item.getSecciónInicial (i);
				if (Contains (objz))
					iter = getTreeFrom (objz);
				else {
					ITreeNode<T> Agrega = new TreeNode<T> (objz);
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

		public bool Contains (TreePath<T> item)
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
			throw new NotImplementedException ();
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

				if (!iter.getSucc.Exists (y => y.objeto.Equals (item)))
					return false;
				else {
					ctr--;
					iter = iter.getSucc.Find (y => y.objeto.Equals (item));
				}
					
			}
			return true;
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
			return new Treelike.TreeEnumerator<T> (getSucc, new TreePath<T> (objeto));
		}

		System.Collections.Generic.IEnumerator<TreePath<T>> System.Collections.Generic.IEnumerable<TreePath<T>>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (getSucc, new TreePath<T> (objeto));
		}

	}
}

