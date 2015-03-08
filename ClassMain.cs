﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace ListasExtra
{
	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T">Dominio de la función.</typeparam>
	/// <typeparam name="V">Rango(co-dominio) de la función.</typeparam>
	[DataContract(Name = "ListaPeso", IsReference = true)]
	public class ListaPeso<T, V>
	{
		/// <summary>
		/// Representa una entrada (KeyValuePair) para esta clase.
		/// </summary>
		public struct Entrada
		{
			public T Key;
			public V Val;
		}

		public bool ContainsKey(T key)
		{
			return Data.ContainsKey(key);
		}

		[DataMember(Name = "List")]
		private Dictionary<T, V> _Data = new Dictionary<T, V>();

		/// <summary>
		/// Devuelve el tipo diccionario de la instancia.
		/// </summary>
		public Dictionary<T, V> Data
		{
			get
			{
				return _Data;
			}
		}

		public V this [T Key]
		{
			get
			{
				return Data.ContainsKey(Key) ? _Data[Key] : Nulo;
			}
			set
			{
				Set(Key, value);
			}
		}

		private V _NullV;

		/// <summary>
		/// Devuelve o establece cuál es el objeto nulo (cero) del grupoide; o bien, el velor prederminado de cada entrada T del dominio.
		/// </summary>
		public V Nulo
		{
			get
			{
				return _NullV;
			}
			set
			{
				_NullV = value;
			}
		}

		/// <summary>
		/// La operación suma.
		/// </summary>
		public Func<V, V, V> Suma;
		/// <summary>
		/// La operación inverso, si la tiene.
		/// </summary>
		public Func<V, V> Inv;
		//Estadísticos
		/// <summary>
		/// Devuelve la suma sobre las Keys, de sus respectivos Values.
		/// </summary>
		/// <returns>Devuelve el objeto resultante de aplicar la operación a cada valor.</returns>
		public V SumaTotal()
		{
			V tot = Nulo;
			foreach (T x in Data.Keys)
			{
				tot = Suma(tot, this[x]);
			}
			return tot;
		}
		//Ordenación y máximización
		/// <summary>
		/// Obtiene la entrda cuyo valor es máximo.
		/// </summary>
		/// <returns></returns>		
		public T ObtenerMáximo(Func<V, V, bool> Comparador)
		{
			if (!Data.Any())
				return default(T);
			else
			{
				T tmp = Data.Keys.ToArray()[0];
				foreach (T x in Data.Keys)
				{
					if (Comparador(this[x], this[tmp]))
						tmp = x;
				}
				return tmp;
			}
		}
		//Eventos
		/// <summary>
		/// Se llama cuando se cambia algún valor (creo que no sirve aún Dx).
		/// </summary>
		public event EventHandler CambioValor;
		//Constructor
		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="OperSuma">Operador suma inicial.</param>
		/// <param name="ObjetoNulo">Objeto cero inicial.</param>
		public ListaPeso(Func<V, V, V> OperSuma, V ObjetoNulo)
		{
			Suma = OperSuma;
			Nulo = ObjetoNulo;
		}

		public List<T> Keys
		{
			get
			{
				List<T> ret = new List<T>(_Data.Keys);
				return ret;
			}
		}

		/// <summary>
		/// Inicializa una instancia de la clase a partir de un valor inicial dado.
		/// </summary>
		/// <param name="OperSuma">Operador suma inicial.</param>
		/// <param name="ObjetoNulo">Objeto cero inicial.</param>
		/// <param name="InitDat">Data inicial.</param>
		public ListaPeso(Func<V, V, V> OperSuma, V ObjetoNulo, Dictionary<T, V> InitDat)
			: this(OperSuma, ObjetoNulo)
		{
			foreach (var x in InitDat)
				Add(x.Key, x.Value);
		}

		protected ListaPeso()
		{
		}

		/// <summary>
		/// Establece el valor de Obj.Key como Obj.Valor.
		/// </summary>
		/// <param name="Obj"></param>
		public virtual void Set(Entrada Obj)
		{
			//Está en la lista?
			if (Data.ContainsKey(Obj.Key))
			{
				Data[Obj.Key] = Obj.Val;

			}
			else
			{
				Data.Add(Obj.Key, Obj.Val);
			}

			if (Data[Obj.Key].Equals(Nulo))           // ¿No es Data[Obj.val]?
			{
				Data.Remove(Obj.Key);
			}
			if (CambioValor != null)
				CambioValor.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Establece el valor de una entrada: this(Key) = Val.
		/// </summary>
		/// <param name="Key"></param>
		/// <param name="Val"></param>
		public void Set(T Key, V Val)
		{
			Entrada E;
			E.Key = Key;
			E.Val = Val;
			this.Set(E);
		}

		/// <summary>
		/// Suma una entrada de la instancia.
		/// </summary>
		/// <param name="Key">Entrada que se le sumará.</param>
		/// <param name="Val">Comparador que se sumará a la entrada.</param>
		public void Add(T Key, V Val)
		{
			Set(Key, Suma(this[Key], Val));
		}

		/// <summary>
		/// Suma una entrada de la instancia.
		/// </summary>
		/// <param name="Obj">Info a sumar.</param>
		public void Add(Entrada Obj)
		{
			this.Add(Obj.Key, Obj.Val);
		}

		/// <summary>
		/// Hace la función instancia cero.
		/// </summary>
		public void Vaciar()
		{
			List<T> Keys = new List<T>();
			foreach (T x in Data.Keys)
				Keys.Add(x);
			foreach (var x in Keys)
				Set(x, Nulo);
		}

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
		public ListaPeso<T, V> Inverso()
		{
			if (Inv == null)
				throw new NullReferenceException("No está definito Inv");
			ListaPeso<T, V> ret = new ListaPeso<T, V>(Suma, Nulo);
			ret.Inv = Inv;
			foreach (var x in Data.Keys)
			{
				ret.Add(x, Inv(this[x]));
			}
			return ret;
		}

		/// <summary>
		/// Suma esta lista en otra.
		/// </summary>
		/// <param name="S">Lista sumando.</param>
		/// <returns></returns>
		public ListaPeso<T, V> SumarA(ListaPeso<T, V> S)
		{
			ListaPeso<T, V> ret = (ListaPeso<T, V>)this.MemberwiseClone();
			foreach (T x in S.Data.Keys)
			{
				ret.Add(x, S[x]);
			}
			return ret;
		}

		/// <summary>
		/// Suma dos ListaExtra.ListaPeso coordenada a coordenada.
		/// </summary>
		/// <param name="Left">Primer sumando.</param>
		/// <param name="Right">Segundo sumando.</param>
		/// <returns></returns>
		public static ListaPeso<T, V> Sumar(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Left.SumarA(Right);
		}

		public static ListaPeso<T, V> operator +(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Sumar(Left, Right);
		}

		public static ListaPeso<T, V> operator -(ListaPeso<T, V> x)
		{
			return x.Inverso();
		}

		public static ListaPeso<T, V> operator -(ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Left + -Right;
		}
	}

	[DataContract(Name = "ListaPeso", IsReference = true)]
	public class ListaPeso<T> : ListasExtra.ListaPeso<T, Single>
	{
		public ListaPeso()
			: base((x, y) => x + y, 0)
		{
		}

		public static bool operator <=(ListasExtra.ListaPeso<T> left, ListasExtra.ListaPeso<T> right)
		{

			foreach (var x in left.Keys)
			{
				if (left[x] > right[x])
					return false;
			}
			return true;
		}

		public static bool operator >=(ListasExtra.ListaPeso<T> left, ListasExtra.ListaPeso<T> right)
		{
			return right <= left;
		}
	}

	/// <summary>
	/// Es sólo una listaPeso de enteros largos.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ListaContador<T> : ListasExtra.ListaPeso<T, long>
	{
		public ListaContador()
			: base((x, y) => x + y, 0)
		{
		}

		public long CountIf(Func<T, bool> Selector)
		{
			long ret = 0;
			foreach (var x in Data.Keys)
			{
				if (Selector.Invoke(x))
				{
					ret += this[x];
				}
			}
			return ret;

		}
	}

	[DataContract(IsReference = true)]
	public class ObjetoAcotado<T>
	{
		public T CotaSup;
		public T CotaInf;
		private T _Valor;
		//private Comparer<T> Comparador;
		private Func<T, T, Boolean> _EsMenor;

		public Func<T, T, Boolean> EsMenor
		{
			get
			{
				return _EsMenor;
			}
			private set
			{
				_EsMenor = value;
			}
		}

		public T Valor
		{
			get
			{
				return _Valor;
			}
			set
			{

				_Valor = EsMenor(value, CotaSup) ? (EsMenor(value, CotaInf) ? CotaInf : value) : CotaSup;
				if (_Valor.Equals(CotaInf))
				{
					EventHandler Handler = LlegóMínimo;
					Handler(this, null);
				}
				if (_Valor.Equals(CotaSup))
				{
					EventHandler Handler = LlegóMáximo;
					Handler(this, null);
				}
			}
		}

		public ObjetoAcotado(Func<T, T, Boolean> Comparador)
		{
			EsMenor = Comparador;
		}

		public ObjetoAcotado(Func<T, T, Boolean> Comparador, T Min, T Max, T Inicial)
			: this(Comparador)
		{
			CotaInf = Min;
			CotaSup = Max;
			Valor = Inicial;
		}

		public override string ToString()
		{
			return Valor.ToString();
		}
		//Eventos
		public event EventHandler LlegóMínimo;
		public event EventHandler LlegóMáximo;
	}

	[DataContract(IsReference = true)]
	public static class ComparadoresPred
	{
		public static Boolean EsMenor(Double x, Double y)
		{
			return x < y;
		}

		public static Boolean EsMenor(Single x, Single y)
		{
			return x < y;
		}
	}

	[DataContract(IsReference = true)]
	public static class OperadoresPred
	{
		public static Double Suma(Double x, Double y)
		{
			return x + y;
		}

		public static long Suma(long x, long y)
		{
			return x + y;
		}

		public static ObjetoAcotado<Double> Suma(ObjetoAcotado<Double> x, ObjetoAcotado<Double> y)
		{
			ObjetoAcotado<Double> ret = new ObjetoAcotado<double>(x.EsMenor);
			ret.CotaSup = Math.Max(x.CotaSup, y.CotaSup);
			ret.CotaInf = Math.Min(x.CotaInf, y.CotaInf);
			ret.Valor = x.Valor + y.Valor;
			return ret;
		}
	}

	[DataContract(IsReference = true)]
	public static class ExtDouble
	{
		public static ListasExtra.ObjetoAcotado<Double> ToAcotado(this Double x)
		{
			ObjetoAcotado<Double> ret = new ObjetoAcotado<Double>(ComparadoresPred.EsMenor, Double.MinValue, Double.MaxValue, 0);
			ret.Valor = x;
			return ret;
		}
	}
}
