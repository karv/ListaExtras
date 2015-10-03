using System;

namespace ListasExtra
{
	/// <summary>
	/// Par no ordenado.
	/// </summary>
	public struct ParNoOrdenado<T> : IEquatable<ParNoOrdenado<T>> 
		where T : IEquatable<T>
	{
		T A { get; }

		T B { get; }

		public ParNoOrdenado (T a, T b)
		{
			A = a;
			B = b;
		}

		/// <summary>
		/// Revisa si este par contiene un elemento dado.
		/// </summary>
		public bool Contiene (T x)
		{
			return A.Equals (x) || B.Equals (x);
		}

		/// <summary>
		/// Devuelve el único elemento que no es uno dado.
		/// </summary>
		public T Excepto (T x)
		{
			if (A.Equals (x))
				return B;
			if (B.Equals (x))
				return A;
			throw new Exception ("No existe el ÚNICO punto distinto de el punto dado.");
		}

		public T this [int i] {
			get {
				if (i == 0)
					return A;
				if (i == 1)
					return B;
				throw new IndexOutOfRangeException ();
			}
		}

		public bool Equals (ParNoOrdenado<T> other)
		{
			return (A.Equals (other.A) && B.Equals (other.B)) || A.Equals (other.B) && B.Equals (other.A);
		}

		public override string ToString ()
		{
			return string.Format ("({0}, {1})", A, B);
		}
	}
}

