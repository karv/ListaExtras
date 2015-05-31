using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	///  Representa un nodo en un treelist
	/// </summary>
	public class TreeNode<T> : ITreeNode<T>
	{
		public TreeList<T>.enumOpcionOrden ordenEnumeración = TreeList<T>.enumOpcionOrden.PadrePrimero;
		TreePath<T> _obj;
		List<ITreeNode<T>> _succ;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Treelike.TreeNode"/> class.
		/// </summary>
		/// <param name="obj">Objeto de este nodo</param>
		public TreeNode (T[] obj)
		{
			_succ = new List<ITreeNode<T>> ();
			_obj = (TreePath<T>)obj;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Treelike.TreeLista`1"/> class.
		/// </summary>
		/// <param name="obj">Objeto de este nodo</param>
		/// <param name="succ">Lista de sucesores</param>
		public TreeNode (T[] obj, IEnumerable<T> succ) : this (obj)
		{
			T[] tmpName;
			foreach (var x in succ) {
				tmpName = new T[obj.Length + 1];
				obj.CopyTo (tmpName, 0);
				tmpName [obj.Length] = x;
					
				_succ.Add (new TreeNode<T> (tmpName));
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Treelike.TreeLista`1"/> class.
		/// </summary>
		/// <param name="obj">Objeto de este nodo</param>
		/// <param name="succ">Lista de sucesores</param>
		public TreeNode (T[] obj, IEnumerable<ITreeNode<T>> succ)
		{
			_obj = (TreePath<T>)obj;
			_succ = (List<ITreeNode<T>>)succ;
		}

		#region ITreeNode implementation

		#region IEnumerator

		IEnumerator<TreePath<T>> IEnumerable<TreePath<T>>.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (_succ, objeto);

		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return new Treelike.TreeEnumerator<T> (_succ, objeto);
		}

		#endregion

		/// <summary>
		/// Devuevle la lista de sucesores.
		/// </summary>
		/// <value>The get succ.</value>
		public List<ITreeNode<T>> getSucc {
			get {
				return _succ;
			}
		}

		public TreePath<T> objeto {
			get {
				return _obj == null ? new TreePath<T> (0) : _obj;
			}
		}

		/// <summary>
		/// Devuelve el árbol correspondiente a una suceción de nodos en el árbol
		/// </summary>
		/// <returns>The tree from.</returns>
		/// <param name="stem">Stem.</param>
		public ITreeNode<T> getTreeFrom (TreePath<T> stem)
		{
			if (stem.Length == 0) {
				return this;
			} else {
				ITreeNode<T> suc = this.FindSucc (stem [0]);
				T[] substem = new T[stem.Length - 1];
				;
				stem.CopyTo (substem, 1);
				return suc.getTreeFrom ((TreePath<T>)substem);
			}
		}

		#endregion
	}
}

