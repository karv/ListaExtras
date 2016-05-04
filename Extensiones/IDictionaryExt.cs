using System.Collections.Generic;
using System;

namespace ListasExtra.Extensiones
{
	public static class IDictionaryExt
	{
		public static IDictionary<TKey, TVal> Clonar<TKey, TVal> (this IDictionary<TKey, TVal> clonando)
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

		#endregion
	}
}