using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	class TreeEnumerator<T> : ListasExtra.Enumerator.SerialEnumerator<TreePath<T>>, IEnumerator<TreePath<T>>
	{
		public enum enumOpcionOrden
		{
			PadrePrimero,
			HijosPrimero
		}

		public enumOpcionOrden orden = enumOpcionOrden.PadrePrimero;
		/// <summary>
		/// Devuelve o establece si este nodo ya se enumeró.
		/// </summary>
		bool Myself = false;
		TreePath<T> _root;
		bool EnPadre = false;

		public TreeEnumerator (IEnumerable<IEnumerable<TreePath<T>>> enums, TreePath<T> root) : base (enums)
		{
			_root = root;
		}

		new public bool MoveNext ()
		{
			if (orden == enumOpcionOrden.PadrePrimero && !Myself) {
				Myself = true;
				EnPadre = true;
				return true;
			}
			EnPadre = false;

			bool baseRet = base.MoveNext ();
			if (orden == enumOpcionOrden.HijosPrimero && !baseRet)
				return true;
			return baseRet;
		}

		new public void Reset ()
		{
			Myself = false;
			EnPadre = false;
			base.Reset ();
		}

		TreePath<T> IEnumerator<TreePath<T>>.Current {
			get {
				return EnPadre ? _root : (TreePath<T>)(base.Current);
			}
		}

		new public object Current {
			get {
				return EnPadre ? _root : base.Current;
			}
		}

	}

}

