using System;
using System.Collections.Generic;
using System.Linq;

namespace ListasExtra.Treelike
{
	/// <summary>
	/// Una colección de objetos T[] que se van acomodando según su posición en un árbol de suceciones de T. 
	/// </summary>
	public class TreeList<T>: ITreeListBackward<T>
	{
		#region Objetos

		/// <summary>
		/// devuelve o establece si el nodo actual del árbol se considera como parte del árbol.
		/// </summary>
		bool _enumeraActual = false;

		/// <summary>
		/// Revisa si este nodo es raíz
		/// </summary>
		/// <value><c>true</c> if this instance is root; otherwise, <c>false</c>.</value>
		public bool _isRoot {
			get {
				return _pred == null;
			}
		}

		readonly T _nodo;
		readonly List<TreeList<T>> _succ = new List<TreeList<T>> ();
		readonly ITreeList<T> _pred;

		#endregion

		#region Propio

		public T[] Stem {
			get {
				T[] ret;
				if (!_isRoot) {
					T[] iter = _pred.getBase;
					ret = new T[iter.Length];
					iter.CopyTo (ret, 0);
					ret [ret.Length - 1] = _nodo;
				} else {
					ret = new T[0];
				}
				return ret;
			}
		}

		public TreeList (T nNodo, TreeList<T> nPred)
		{
			_pred = nPred;
			_nodo = nNodo;
		}

		public TreeList ()
		{
			_pred = null;
		}

		/// <summary>
		/// Serializa el árbol en una lista
		/// </summary>
		/// <returns>The list.</returns>
		public List<T[]> ToList ()
		{
			List<T[]> ret = new List<T[]> ();
			AddToList (ret);
			return ret;
		}

		/// <summary>
		/// Agrega una copia serializada de este árbol a una lista
		/// </summary>
		/// <param name="lst">La lista</param>
		public void AddToList (List<T[]> lst)
		{
			if (_enumeraActual)
				lst.Add (Stem);
			foreach (var x in _succ) {
				x.AddToList (lst);
			}
		}

		/// <summary>
		/// Devuelve un arreglo enlistando una serialización de este árbol.
		/// </summary>
		/// <returns>The array.</returns>
		public T[][] ToArray ()
		{
			return ToList ().ToArray ();
		}

		TreeList<T> EncuentraSucc (T nodoSucc, bool Forzar = false)
		{
			TreeList<T> ret;
			ret = _succ.Find (x => x._nodo.Equals (nodoSucc));
			if (ret == null && Forzar) {
				ret = new TreeList<T> (nodoSucc, this);
				_succ.Add (ret);
			}
			return ret;
		}

		/// <summary>
		/// Agrega n objeto al árbol
		/// </summary>
		/// <param name="x">Objeto a agregar</param>
		public void Add (T[] x)
		{
			if (x.Length == 0) {
				_enumeraActual = true;
			} else {
				T[] y = new T[x.Length - 1];
				TreeList<T> AgregaEn = EncuentraSucc (x [0], true);
				for (int i = 0; i < y.Length; i++) {
					y [i] = x [i + 1];
				}
				AgregaEn.Add (y);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Treelike.Tree`1"/> class.
		/// </summary>
		/// <param name="coll">Colección inicial</param>
		public TreeList (IEnumerable<T[]> coll) : this ()
		{
			foreach (var x in coll) {
				Add (x);
			}
		}

		public bool Contains (T[] x)
		{
			if (x.Length == 0) {
				return this._enumeraActual;
			} else {
				T a = x [0];
				x = x.Skip (1).ToArray ();
				TreeList<T> iter = EncuentraSucc (a);
				return iter != null && iter.Contains (x);
			}
		}

		public override string ToString ()
		{
			return ToList ().ToString ();
		}

		#endregion

		#region ITreeList

		public ITreeList<T> pred {
			get {
				return _pred;
			}
		}

		public bool enumeraActual {
			get {
				return _enumeraActual;
			}
		}

		public bool isRoot {
			get {
				return _isRoot;
			}
		}

		public T nodo {
			get {
				return _nodo;
			}
		}

		public T[] getBase {
			get {
				throw new NotImplementedException ();
			}
		}

		public IEnumerable<ITreeList<T>> succ {
			get {
				return _succ;
			}
		}

		/// <summary>
		/// Elimina el contenido del árbol desde este nodo.
		/// </summary>
		public void Clear ()
		{
			_enumeraActual = false;
			_succ.Clear ();
		}

		public void CopyTo (T[][] array, int arrayIndex)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (T[] item)
		{
			bool ret;
			if (item.Length == 0) {
				ret = _enumeraActual;
				_enumeraActual = false;
				return ret;
			} else {
				T a = item [0];
				item = item.Skip (1).ToArray ();
				TreeList<T> iter = EncuentraSucc (a);
				return iter.Remove (item);
			}
		}

		public int Count {
			get {
				int ret = 0;
				if (_enumeraActual)
					ret++;
				foreach (var x in _succ) {
					ret += x.Count;
				}
				return ret;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public IEnumerator<T[]> GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

	/// <summary>
	/// Representa un árbol de strings que se puede enumerar.
	/// </summary>
	public class StringTree:Treelike.TreeList<char>
	{
		/// <summary>
		/// Agrega un objeto al árbol
		/// </summary>
		/// <param name="x">Objeto a agregar</param>
		public void Add (string x)
		{
			base.Add (x.ToCharArray ());
		}

		/// <summary>
		/// Serializa el árbol en una lista
		/// </summary>
		/// <returns>The list.</returns>
		public new List<string> ToList ()
		{
			char[][] ret2 = base.ToArray ();
			List<string> ret = new List<string> ();
			foreach (var x in ret2) {
				ret.Add (new string (x));
			}
			return ret;
		}

		/// <summary>
		/// Devuelve un arreglo enlistando una serialización de este árbol.
		/// </summary>
		/// <returns>The array.</returns>
		public new string[] ToArray ()
		{
			return ToList ().ToArray ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Treelike.StringTree"/> class.
		/// </summary>
		/// <param name="coll">La colección inicial.</param>
		public StringTree (IEnumerable<string> coll)
		{
			foreach (var x in coll) {
				Add (x);
			}
		}
	}
}
