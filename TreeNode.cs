using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	/// <summary>
	///  Representa un nodo en un treelist
	/// </summary>
	public class TreeNode<T> : ITreeNode<T>
	{
		public TreeList<T>.EnumOpcionOrden OrdenEnumeración = TreeList<T>.EnumOpcionOrden.PadrePrimero;
		TreePath<T> _obj;
		List<ITreeNode<T>> _succ;

		/// <summary>
		/// Initializes a new instance of the TreeNode class.
		/// </summary>
		/// <param name="obj">Objeto de este nodo</param>
		public TreeNode (T[] obj)
		{
			_succ = new List<ITreeNode<T>> ();
			_obj = (TreePath<T>)obj;
		}

		/// <summary>
		/// Initializes a new instance of the TreeLista class.
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
		/// Initializes a new instance of the TreeLista class.
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
			return new TreeEnumerator<T> (_succ, Objeto);

		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return new TreeEnumerator<T> (_succ, Objeto);
		}

		#endregion

		/// <summary>
		/// Devuevle la lista de sucesores.
		/// </summary>
		/// <value>The get succ.</value>
		public List<ITreeNode<T>> Sucesor {
			get {
				return _succ;
			}
		}

		public TreePath<T> Objeto {
			get {
				return _obj ?? new TreePath<T> (0);
			}
		}

		/// <summary>
		/// Devuelve el árbol correspondiente a una suceción de nodos en el árbol
		/// </summary>
		/// <returns>The tree from.</returns>
		/// <param name="stem">Stem.</param>
		public ITreeNode<T> ÁrbolDesde (TreePath<T> stem)
		{
			if (stem.Length == 0) {
				return this;
			} else {
				ITreeNode<T> suc = this.FindSucc (stem [0]);
				var substem = new T[stem.Length - 1];
				stem.CopyTo (substem, 1);
				return suc.ÁrbolDesde ((TreePath<T>)substem);
			}
		}

		public int Count {
			get {
				int ret = 1;
				foreach (var x in Sucesor) {
					ret += x.Count;
				}
				return ret;
			}
		}

		#endregion
	}
}

