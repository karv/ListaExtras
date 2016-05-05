using ListasExtra.Árboles;
using Gtk;
using System;

namespace ListasExtra.Gtk
{
	public static class TreeLinker
	{
		/// <summary>
		/// Agrega una entrada a un TreeStore dado un IÁrbolEnraizado.
		/// </summary>
		/// <param name="store">Modelo de gtk.árbol donde copiar contenido</param>
		/// <param name="tree">Árbol a copiar contenido</param>
		/// <param name="encaje">Cómo se encaja el nodo del árbol en Store</param>
		/// <param name="iter">Iterador dentro del gtk.árbol</param>
		/// <typeparam name="T">Clase de nodos del árbol</typeparam>
		public static void Append<T> (this TreeStore store,
		                              IÁrbolEnraizado<T> tree,
		                              Func<IÁrbolEnraizado<T>, Array> encaje,
		                              TreeIter? iter = null)
		{
			TreeIter newIter;
			newIter = !iter.HasValue ? store.AppendValues (encaje (tree)) : store.AppendValues (
				iter.Value,
				encaje (tree));

			foreach (var tr in tree.Sucesores)
			{
				Append<T> (store, tr, encaje, newIter);
			}
		}
	}
}