using System;
using System.Collections.Generic;

namespace ListasExtra
{
	/// <summary>
	/// Par no ordenado.
	/// </summary>
	[Serializable]
	public struct ParNoOrdenado<T> : IEquatable<ParNoOrdenado<T>> 
		where T : IEquatable<T>
	{
		T A { get; }

		T B { get; }

		IEqualityComparer<T> comparador;

		/// <summary>
		/// Initializes a new instance of this struct.
		/// </summary>
		/// <param name="a">Entrada cero</param>
		/// <param name="b">Entrada uno</param>
		public ParNoOrdenado (T a, T b, IEqualityComparer<T> compara = null)
		{
			A = a;
			B = b;
			comparador = compara ?? EqualityComparer<T>.Default;
		}

		/// <summary>
		/// Revisa si este par contiene un elemento dado.
		/// </summary>
		public bool Contiene (T x)
		{
			return comparador.Equals (x, A) || comparador.Equals (x, B);
		}

		/// <summary>
		/// Devuelve el único elemento que no es uno dado.
		/// </summary>
		public T Excepto (T x)
		{
			if (comparador.Equals (A, x))
				return B;
			if (comparador.Equals (B, x))
				return A;
			throw new Exception ("No existe el ÚNICO punto distinto de el punto dado.");
		}

		/// <summary>
		/// Devuelve la primera o segunda entrada de este par
		/// </summary>
		/// <param name="i">Entrada, base cero</param>
		public T this [int i]
		{
			get
			{
				if (i == 0)
					return A;
				if (i == 1)
					return B;
				throw new IndexOutOfRangeException ();
			}
		}

		/// <summary>
		/// Determines whether the specified ParOrdenado is equal to the current ParOrdenado"/>.
		/// </summary>
		/// <param name="other">The ParOrdenado  to compare with the current ParOrdenado .</param>
		/// <returns><c>true</c> if the specified ParOrdenado is equal to the current
		/// ParOrdenado; otherwise, <c>false</c>.</returns>
		public bool Equals (ParNoOrdenado<T> other)
		{
			var numn = numNoNulos;

			if (numn != other.numNoNulos)
				return false;
			
			switch (numn)
			{
				case 0: // Aquí nunca debería entrar
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

		/// <summary>
		/// El número de nulos
		/// </summary>
		/// <value>The number nulos.</value>
		int numNoNulos
		{
			get
			{ 
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
		T noNulo
		{
			get
			{
				if (A == null)
					return B;
				if (B == null)
					return A;

				throw new Exception ("No existe el único no nulo en el par " + this);
			}
		}

		/// <summary>
		/// Devuelve un conjunto que representa a este par.
		/// </summary>
		public Set.Set<T> AsSet ()
		{
			var ret = new Set.Set<T> ();
			ret.Add (A);
			ret.Add (B);
			return ret;
		}

		/// <summary>
		/// Enumera los elementos del par.
		/// </summary>
		public IEnumerable<T> Enumererar ()
		{
			yield return A;
			yield return B;
		}

		public override string ToString ()
		{
			return string.Format ("({0}, {1})", A, B);
		}
	}
}