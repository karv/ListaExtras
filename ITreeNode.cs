using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	public interface ITreeNode<T> : IEnumerable<T[]>
	{
		System.Collections.Generic.List<ITreeNode<T>> getSucc { get; }

		ITreeNode<T> getTreeFrom (T[] stem);

		T[] objeto { get; }
	}

	public static class ExtITreeNode
	{
		public static ITreeNode<T> FindSucc<T> (this ITreeNode<T> tree, T obj)
		{
			foreach (var x in tree.getSucc) {
				if (x.objeto.Equals (obj))
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
		public static bool esNodoMuerto<T> (this ITreeNode<T> tree)
		{
			return (tree.getSucc == null || tree.getSucc.Count == 0);
		}
	}
}