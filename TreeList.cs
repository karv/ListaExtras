using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Lista en árbol.
	/// Note que siempre {} \in this
	/// </summary>
	public class TreeList<T> : TreeNode<T>, ICollection<TreePath<T>>
	{
		public enum EnumOpcionOrden
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

		public void Add (TreePath<T> item)
		{
			ITreeNode<T> iter = this;
			TreePath<T> objz;
			for (int i = 1; i <= item.Length; i++) {
				objz = item.SecciónInicial (i);
				if (Contains (objz))
					iter = ÁrbolDesde (objz);
				else {
					ITreeNode<T> Agrega = new TreeNode<T> ((T[])objz);
					iter.Sucesor.Add (Agrega);
					iter = Agrega;
				}
			}
		}

		public void Add (T item)
		{			
			T[] Adding = new T[Objeto.Length + 1];

			Objeto.CopyTo (Adding, 0);
			Adding [Objeto.Length] = item;
			Add ((TreePath<T>)Adding); 
		}

		public void Add (ITreeNode<T> item)
		{
			Sucesor.Add (item);
		}

		public void Clear ()
		{
			Sucesor.Clear ();
		}

		public bool Contains (T[] item)
		{
			return Contains ((TreePath<T>)item); 
		}
			
		// Analysis disable UnusedVariable.Compiler
		public bool Contains (TreePath<T> item)
		{
			ITreeNode<T> iter = this;
			int i = 0;
			foreach (var x in item) {//TODO obviamente no sirve, $x$ no se usa.
				if (!iter.Sucesor.Exists (y => y.Objeto.Equals (item.SecciónInicial (++i))))
					return false;
				iter = iter.Sucesor.Find (y => y.Objeto.Equals (item.SecciónInicial (i)));
			}
			return true;
		}
		// Analysis restore UnusedVariable.Compiler

		public void CopyTo (TreePath<T>[] array, int arrayIndex)
		{			
			// TODO por probar
			for (int i = 0; i < arrayIndex; i++) {
				array [i] = Sucesor [arrayIndex + i].Objeto;
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
				Sucesor.Clear ();
			
			// Analysis disable UnusedVariable.Compiler
			foreach (var x in item) {
				// Analysis restore UnusedVariable.Compiler
				if (ctr == 1) { // Si x = last(item)
					return (iter.Sucesor.RemoveAll (y => y.Objeto.Equals (item)) > 0);
				}

				if (!iter.Sucesor.Exists (y => y.Objeto.Equals (item.SecciónInicial (item.Length - ctr + 1))))
					return false;
				iter = iter.Sucesor.Find (y => y.Objeto.Equals (item.SecciónInicial (item.Length - ctr + 1)));
				ctr--;
					
			}
			return true;
		}


		public bool IsReadOnly {
			get {
				return false;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return new TreeEnumerator<T> (Sucesor, Objeto);
		}

		IEnumerator<TreePath<T>> IEnumerable<TreePath<T>>.GetEnumerator ()
		{
			return new TreeEnumerator<T> (Sucesor, Objeto);
		}

	}
}

