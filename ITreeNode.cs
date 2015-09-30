using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	public interface ITreeNode<T> : IEnumerable<TreePath<T>>
	{
		List<ITreeNode<T>> Sucesor { get; }

		ITreeNode<T> ÁrbolDesde (TreePath<T> stem);

		TreePath<T> Objeto { get; }

		int Count { get; }


	}

	public static class ExtITreeNode
	{
		public static ITreeNode<T> FindSucc<T> (this ITreeNode<T> tree, T obj)
		{
			foreach (var x in tree.Sucesor) {
				if (x.Objeto.Equals (obj))
					return x;
			}
			throw new Exception (string.Format ("No se encuentra el sucesor {0} en el árbol {1}.", obj, tree));
		}

		/// <summary>
		/// Revisa si este árbol tiene sucesores.
		/// </summary>
		/// <returns><c>true</c>, si el árbol no tiene sucesores, <c>false</c> otherwise.</returns>
		/// <param name="tree">Tree.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool EsNodoMuerto<T> (this ITreeNode<T> tree)
		{
			return (tree.Sucesor == null || tree.Sucesor.Count == 0);
		}

		/// <summary>
		/// Devuelve el último objeto en el nodo árbol
		/// </summary>
		/// <returns>The objeto.</returns>
		/// <param name="tree">Tree.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T TObjeto<T> (this ITreeNode<T> tree)
		{
			return tree.Objeto [tree.Objeto.Length - 1];

		}

	}
}