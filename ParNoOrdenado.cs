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
			var numn = NumNulos;

			if (numn != other.NumNulos)
				return false;
			
			switch (numn) {
			case 0:
				return true;

			case 1:
				var nnulA = noNulo;
				var nnulB = other.noNulo;
				return nnulA.Equals (nnulB);

			case 2:
				return (A.Equals (other.A) && B.Equals (other.B)) || A.Equals (other.B) && B.Equals (other.A);

			default:
				throw new Exception ("WTF?");
			}
		}

		// Analysis disable CompareNonConstrainedGenericWithNull
		/// <summary>
		/// El número de nulos
		/// <value>The number nulos.</value>
		int NumNulos {
			get { 
				int ret = 0;
				if (A != null)
					ret++;
				if (B != null)
					ret++;
				return ret;
			}
		}

		/// <summary>
		///  Devuelve el único elemento no nulo.
		/// </summary>
		/// <value>The no nulo.</value>
		T noNulo {
			get {
				if (A == null)
					return B;
				if (B == null)
					return A;

				throw new Exception ("No existe el único no nulo en el par " + this);
			}
		}
		// Analysis restore CompareNonConstrainedGenericWithNull


		public override string ToString ()
		{
			return string.Format ("({0}, {1})", A, B);
		}
	}
}

