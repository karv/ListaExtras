using System.Collections.Generic;
using System;

namespace ListasExtra.Treelike
{
	[Obsolete]
	class TreeEnumerator<T> : ListasExtra.Enumerator.SerialEnumerator<TreePath<T>>, IEnumerator<TreePath<T>>
	{
		public enum EnumOpcionOrden
		{
			PadrePrimero,
			HijosPrimero
		}

		public EnumOpcionOrden Orden = EnumOpcionOrden.PadrePrimero;
		/// <summary>
		/// Devuelve o establece si este nodo ya se enumeró.
		/// </summary>
		bool Myself;
		readonly TreePath<T> _root;
		bool EnPadre;

		public TreeEnumerator (IEnumerable<IEnumerable<TreePath<T>>> enums, TreePath<T> root) : base (enums)
		{
			_root = root;
		}

		new public bool MoveNext ()
		{
			if (Orden == EnumOpcionOrden.PadrePrimero && !Myself) {
				Myself = true;
				EnPadre = true;
				return true;
			}
			EnPadre = false;

			bool baseRet = base.MoveNext ();
			if (Orden == EnumOpcionOrden.HijosPrimero && !baseRet)
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

