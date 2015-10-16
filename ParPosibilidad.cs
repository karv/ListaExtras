using System;
using System.Linq;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Representa un par ordenado de posibiildad
	/// </summary>
	public class ParPosibilidad<T> 
		where T : IEquatable<T>
	{
		protected T A { get; }

		protected IEnumerable<T> B { get; }

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

		public override string ToString ()
		{
			return string.Format ("[ParPosibilidad]\nFijo: {0}\nPosibles: {1}", A, B);
		}
	}
}