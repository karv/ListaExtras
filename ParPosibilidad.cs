using System;
using System.Linq;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Representa un par ordenado de posibiildad
	/// </summary>
	[Serializable]
	public class ParPosibilidad<T> 
		where T : IEquatable<T>
	{
		/// <summary>
		/// El objeto de probabilidad
		/// </summary>
		protected T A { get; }

		/// <summary>
		/// La lista de objetos que pueden ser con A
		/// </summary>
		protected IEnumerable<T> B { get; }

		/// <param name="a">Objeto</param>
		/// <param name="b">Posibilidades</param>
		public ParPosibilidad (T a, IEnumerable<T> b)
		{
			A = a;
			B = b;
		}

		/// <summary>
		/// Revisa si es consistente que este par sea idéntico a otro
		/// </summary>
		public bool EsConsistenteCon (ParNoOrdenado<T> par)
		{
			return B.Any (x => par.Equals (new ParNoOrdenado<T> (A, x)));
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current class/>.
		/// </summary>
		public override string ToString ()
		{
			return string.Format ("[ParPosibilidad]\nFijo: {0}\nPosibles: {1}", A, B);
		}
	}
}