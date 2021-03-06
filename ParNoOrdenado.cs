﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace ListasExtra
{
	/// <summary>
	/// Par no ordenado inmutable.
	/// </summary>
	[Serializable]
	public struct ParNoOrdenado<T> : IStructuralEquatable
	{
		T A { get; }

		T B { get; }

		/// <summary>
		/// Initializes a new instance of this struct.
		/// </summary>
		/// <param name="a">Entrada cero</param>
		/// <param name="b">Entrada uno</param>
		/// <remarks>Dejar el comparador nulo hace que tome el valor Default según EqualityComparer </remarks>
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
			return Contiene (x, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Verifica si un objeto es un elemento de este par.
		/// </summary>
		/// <param name="item">Objeto.</param>
		/// <param name="comparador">Comparador.</param>
		public bool Contiene (T item, IEqualityComparer<T> comparador)
		{
			return comparador.Equals (item, A) || comparador.Equals (item, B);
		}

		/// <summary>
		/// Devuelve el único elemento que no es uno dado.
		/// </summary>
		public T Excepto (T x, IEqualityComparer<T> comparador)
		{
			if (comparador.Equals (A, x))
				return B;
			if (comparador.Equals (B, x))
				return A;
			throw new Exception ("No existe el ÚNICO punto distinto de el punto dado.");
		}

		/// <summary>
		/// Devuelve el único elemento que no es uno dado.
		/// </summary>
		public T Excepto (T x)
		{
			return Excepto (x, EqualityComparer<T>.Default);
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
		/// El número de nulos
		/// </summary>
		/// <value>The number nulos.</value>
		int numNoNulos
		{
			get
			{ 
				int ret = 0;
				if (!ReferenceEquals (A, null))
					ret++;
				if (!ReferenceEquals (B, null))
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
				if (ReferenceEquals (A, null))
					return B;
				if (ReferenceEquals (B, null))
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

		/// <summary>
		/// </summary>
		public override bool Equals (object obj)
		{
			return Equals (obj, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Verifica que este par sea igual estructuralmente a otro
		/// </summary>
		/// <param name="other">Otro par del mismo tipo</param>
		/// <param name="comparer">Comparer.</param>
		public bool Equals (object other, IEqualityComparer comparer)
		{
			// Analysis disable CanBeReplacedWithTryCastAndCheckForNull
			if (other is ParNoOrdenado<T>)
			{
				var otro = (ParNoOrdenado<T>)other;
				var numn = numNoNulos;

				if (numn != otro.numNoNulos)
					return false;

				switch (numn)
				{
					case 0: // Aquí nunca debería entrar
						return true;

					case 1:
						var nnulA = noNulo;
						var nnulB = otro.noNulo;
						return comparer.Equals (nnulA, nnulB);

					case 2:
						return (comparer.Equals (A, otro.A) && comparer.Equals (B, otro.B)) ||
						(comparer.Equals (A, otro.B) && comparer.Equals (B, otro.A));
				}
				Debug.Fail ("Valos inconsistente de ParNoOrdenado.numNoNulos", 
					"numNoNulos = " + numNoNulos);

			}
			return false;
			// Analysis restore CanBeReplacedWithTryCastAndCheckForNull
		}

		int IStructuralEquatable.GetHashCode (IEqualityComparer comparer)
		{
			return comparer.GetHashCode (A) + comparer.GetHashCode (B);
		}

		/// <summary>
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode ()
		{
			return EqualityComparer<ParNoOrdenado<T>>.Default.GetHashCode (this);
		}

		/// <summary>
		/// </summary>
		public override string ToString ()
		{
			return string.Format ("({0}, {1})", A, B);
		}
	}
}