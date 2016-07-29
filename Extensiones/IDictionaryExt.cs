using System.Collections.Generic;
using System;

namespace ListasExtra.Extensiones
{
	/// <summary>
	/// Extensión de dicionarios
	/// </summary>
	public static class IDictionaryExt
	{
		/// <summary>
		/// Clona un IDictionary a un Dictionary
		/// </summary>
		/// <param name="clonando">Clonando.</param>
		/// <typeparam name="TKey">The 1st type parameter.</typeparam>
		/// <typeparam name="TVal">The 2nd type parameter.</typeparam>
		public static Dictionary<TKey, TVal> Clonar<TKey, TVal> (this IDictionary<TKey, TVal> clonando)
		{
			return new Dictionary<TKey, TVal> (clonando);
		}

		#region Operacional

		/// <summary>
		/// Devuelve la suma de los valores de un diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <typeparam name="TKey">Tipo de Key del diccionario</typeparam>
		public static double Suma<TKey> (this IDictionary<TKey, double> dict)
		{
			double ret = 0;
			foreach (var x in dict.Values)
				ret += x;
			return ret;
		}

		/// <summary>
		/// Devuelve la suma de los valores de un diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <typeparam name="TKey">Tipo de Key del diccionario</typeparam>
		public static float Suma<TKey> (this IDictionary<TKey, float> dict)
		{
			float ret = 0;
			foreach (var x in dict.Values)
				ret += x;
			return ret;
		}

		/// <summary>
		/// Devuelve la suma de los valores de un diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <typeparam name="TKey">Tipo de Key del diccionario</typeparam>
		public static int Suma<TKey> (this IDictionary<TKey, int> dict)
		{
			var ret = 0;
			foreach (var x in dict.Values)
				ret += x;
			return ret;
		}

		/// <summary>
		/// Devuelve un diccionario con los valores de las entradas como inverso aditivo
		/// </summary>
		public static Dictionary<TKey, double> Inverso<TKey> (this IDictionary<TKey, double> dict)
		{
			var ret = new Dictionary<TKey, double> ();
			foreach (var x in dict)
				ret.Add (x.Key, -x.Value);
			return ret;
		}

		/// <summary>
		/// Devuelve un diccionario con los valores de las entradas como inverso aditivo
		/// </summary>
		public static Dictionary<TKey, float> Inverso<TKey> (this IDictionary<TKey, float> dict)
		{
			var ret = new Dictionary<TKey, float> ();
			foreach (var x in dict)
				ret.Add (x.Key, -x.Value);
			return ret;
		}

		/// <summary>
		/// Devuelve un diccionario con los valores de las entradas como inverso aditivo
		/// </summary>
		public static Dictionary<TKey, int> Inverso<TKey> (this IDictionary<TKey, int> dict)
		{
			var ret = new Dictionary<TKey, int> ();
			foreach (var x in dict)
				ret.Add (x.Key, -x.Value);
			return ret;
		}

		/// <summary>
		/// Suma una entrada a un(este) diccionario.
		/// </summary>
		/// <param name="dict">Dict.</param>
		/// <param name="key">Key del sumando.</param>
		/// <param name="val">Valor del sumando.</param>
		public static void Sumarse <TKey> (this IDictionary<TKey, double> dict,
		                                   TKey key,
		                                   double val)
		{
			if (dict.ContainsKey (key))
				dict [key] += val;
			else
				dict.Add (key, val);
		}

		/// <summary>
		/// Suma una entrada a un(este) diccionario.
		/// </summary>
		/// <param name="dict">Dict.</param>
		/// <param name="key">Key del sumando.</param>
		/// <param name="val">Valor del sumando.</param>
		public static void Sumarse <TKey> (this IDictionary<TKey, float> dict,
		                                   TKey key,
		                                   float val)
		{
			if (dict.ContainsKey (key))
				dict [key] += val;
			else
				dict.Add (key, val);
		}

		/// <summary>
		/// Suma una entrada a un(este) diccionario.
		/// </summary>
		/// <param name="dict">Dict.</param>
		/// <param name="key">Key del sumando.</param>
		/// <param name="val">Valor del sumando.</param>
		public static void Sumarse <TKey> (this IDictionary<TKey, int> dict,
		                                   TKey key,
		                                   int val)
		{
			if (dict.ContainsKey (key))
				dict [key] += val;
			else
				dict.Add (key, val);
		}

		/// <summary>
		/// Suma dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, double> Sumar<TKey> (IDictionary<TKey, double> left,
		                                                    IDictionary<TKey, double> right)
		{
			var ret = new Dictionary<TKey, double> (left);
			foreach (var x in right)
				ret.Sumarse (x.Key, x.Value);
			return ret;
		}

		/// <summary>
		/// Suma dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, float> Sumar<TKey> (IDictionary<TKey, float> left,
		                                                   IDictionary<TKey, float> right)
		{
			var ret = new Dictionary<TKey, float> (left);
			foreach (var x in right)
				ret.Sumarse (x.Key, x.Value);
			return ret;
		}

		/// <summary>
		/// Suma dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, int> Sumar<TKey> (IDictionary<TKey, int> left,
		                                                 IDictionary<TKey, int> right)
		{
			var ret = new Dictionary<TKey, int> (left);
			foreach (var x in right)
				ret.Sumarse (x.Key, x.Value);
			return ret;
		}

		/// <summary>
		/// Resta dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, double> Restar<TKey> (IDictionary<TKey, double> left,
		                                                     IDictionary<TKey, double> right)
		{
			return Sumar (left, right.Inverso ());
		}

		/// <summary>
		/// Resta dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, float> Restar<TKey> (IDictionary<TKey, float> left,
		                                                    IDictionary<TKey, float> right)
		{
			return Sumar (left, right.Inverso ());
		}

		/// <summary>
		/// Resta dos diccionarios por entrada
		/// </summary>
		public static Dictionary<TKey, int> Restar<TKey> (IDictionary<TKey, int> left,
		                                                  IDictionary<TKey, int> right)
		{
			return Sumar (left, right.Inverso ());
		}

		/// <summary>
		/// Revisa si un diccionario es menor en cada entrada que otro
		/// </summary>
		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		/// <param name="defEntry">Valor de las entradas que no aparezcan</param>
		/// <typeparam name="TKey">Tipo de Key</typeparam>
		/// <typeparam name="TVal">Tipo de Value, debe ser IComparable</typeparam>
		public static bool MenorPorEntrada<TKey, TVal> (IDictionary<TKey, TVal> left,
		                                                IDictionary<TKey, TVal> right, 
		                                                TVal defEntry = default(TVal))
			where TVal : IComparable<TVal>
		{
			foreach (var x in left.Keys)
			{
				TVal tmp;
				var rightVal = right.TryGetValue (x, out tmp) ? tmp : defEntry;
				if (left [x].CompareTo (rightVal) == 1)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Revisa si un diccionario es idéntico en cada entrada que otro
		/// </summary>
		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		/// <param name="defEntry">Valor de las entradas que no aparezcan</param>
		/// <typeparam name="TKey">Tipo de Key</typeparam>
		/// <typeparam name="TVal">Tipo de Value, debe ser IEquatable</typeparam>
		public static bool IgualPorEntrada <TKey, TVal> (IDictionary<TKey, TVal> left,
		                                                 IDictionary<TKey, TVal> right, 
		                                                 TVal defEntry = default(TVal))
			where TVal : IEquatable<TVal>
		{
			var supp = new HashSet<TKey> (left.Keys);
			supp.UnionWith (right.Keys);

			foreach (var x in supp)
			{
				TVal tmp;
				var leftv = left.TryGetValue (x, out tmp) ? tmp : defEntry;
				var rightv = right.TryGetValue (x, out tmp) ? tmp : defEntry;

				if (!leftv.Equals (rightv))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Devuelve un nuevo diccionario que consiste en el producto escalar con un entero y este diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <param name="veces">Factor</param>
		public static Dictionary<TKey, int> Veces<TKey> (this IDictionary<TKey, int> dict,
		                                                 int veces)
		{
			var ret = new Dictionary<TKey, int> ();
			foreach (var x in dict)
				ret.Add (x.Key, x.Value * veces);
			return ret;
		}

		/// <summary>
		/// Devuelve un nuevo diccionario que consiste en el producto escalar con un flotante y este diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <param name="veces">Factor</param>
		public static Dictionary<TKey, float> Veces<TKey> (this IDictionary<TKey, float> dict,
		                                                   float veces)
		{
			var ret = new Dictionary<TKey, float> ();
			foreach (var x in dict)
				ret.Add (x.Key, x.Value * veces);
			return ret;
		}

		/// <summary>
		/// Devuelve un nuevo diccionario que consiste en el producto escalar con un double y este diccionario.
		/// </summary>
		/// <param name="dict">Diccionario</param>
		/// <param name="veces">Factor</param>
		public static Dictionary<TKey, double> Veces<TKey> (this IDictionary<TKey, double> dict,
		                                                    double veces)
		{
			var ret = new Dictionary<TKey, double> ();
			foreach (var x in dict)
				ret.Add (x.Key, x.Value * veces);
			return ret;
		}

		#endregion
	}
}