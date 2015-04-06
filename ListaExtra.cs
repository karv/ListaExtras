using System;
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
	public class ListaPeso<T, V> : Dictionary<T, V>
	{
		public new V this [T Key]
		{
			get
			{
				foreach (var x in this)
				{
					if (_Comparador(x.Key, Key))
						return x.Value;
				}
				return Nulo;
			}
			set
			{
				// Encontrar la Key buscada.
				foreach (var x in Keys)
				{
					if (_Comparador(x, Key))
					{
						base[x] = value;
						if (CambioValor != null)
							CambioValor.Invoke(this, new EventArgs());
						return;
					}
				}

				// Si es entrada nueva, se agraga.
				if (CambioValor != null)
					CambioValor.Invoke(this, new EventArgs());
				base.Add(Key, value);
			}
		}

		private Func<T, T, bool> _Comparador = (x, y) => x.Equals(y);

		/// <summary>
		/// Devuelve o establece qué función sirve para saber si dos T's son idénticos para esta lista.
		/// Por default es x.Equals(y).
		/// </summary>
		public Func<T, T, bool> Comparador
		{
			get { return _Comparador; }
			set { _Comparador = value; }
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
		/// Revisa si existe un objeto con las con las condiciones dadas.
		/// </summary>
		/// <param name="Pred">Predicado a exaluar.</param>
		/// <returns>Devuelve true si existe un objeto que cumple Pred.</returns>
		public bool Any(Func<T, V, bool> Pred)
		{
			foreach (var x in Keys)
			{
				if (Pred(x, this[x]))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Revisa si existe un objeto en esta lista.
		/// </summary>
		/// <returns>Devuelve true si existe algo.</returns>
		public bool Any()
		{
			return Count > 0;
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
			foreach (T x in Keys)
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
			if (!Any())
			{
				return default(T);
			}
			else
			{
				T tmp = Keys.ToArray()[0];
				foreach (T x in Keys)
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

		public new bool ContainsKey(T Key)
		{
			foreach (var x in Keys)
			{
				if (_Comparador(x, Key))
					return true;
			}
			return false;
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
			foreach (var x in Keys)
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
			foreach (T x in S.Keys)
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
			foreach (var x in Keys)
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
namespace Treelike
{
	/// <summary>
	/// Una colección de objetos T[] que se van acomodando según su posición en un árbol de suceciones de T. 
	/// </summary>
	public class Tree<T>
	{
		/// <summary>
		/// devuelve o establece si el nodo actual del árbol se considera como parte del árbol.
		/// </summary>
		bool EnumeraActual = false;

		/// <summary>
		/// Revisa si este nodo es raíz
		/// </summary>
		/// <value><c>true</c> if this instance is root; otherwise, <c>false</c>.</value>
		public bool IsRoot
		{
			get
			{
				return Pred == null;
			}
		}

		readonly T nodo;
		readonly List<Tree<T>> Succ = new List<Tree<T>>();
		readonly Tree<T> Pred;

		public T[] Stem
		{
			get
			{
				T[] ret;
				if (!IsRoot)
				{
					T[] iter = Pred.Stem;
					ret = new T[iter.Length + 1];
					iter.CopyTo(ret, 0);
					ret[ret.Length - 1] = nodo;
				}
				else
				{
					ret = new T[0];
				}
				return ret;
			}
		}

		public Tree(T nNodo, Tree<T> nPred)
		{
			Pred = nPred;
			nodo = nNodo;
		}

		public Tree()
		{
			Pred = null;
		}

		/// <summary>
		/// Serializa el árbol en una lista
		/// </summary>
		/// <returns>The list.</returns>
		public List<T[]> ToList()
		{
			List<T[]> ret = new List<T[]>();
			AddToList(ret);
			return ret;
		}

		/// <summary>
		/// Agrega una copia serializada de este árbol a una lista
		/// </summary>
		/// <param name="lst">La lista</param>
		public void AddToList(List<T[]> lst)
		{
			if (EnumeraActual)
				lst.Add(Stem);
			foreach (var x in Succ)
			{
				x.AddToList(lst);
			}
		}

		/// <summary>
		/// Devuelve un arreglo enlistando una serialización de este árbol.
		/// </summary>
		/// <returns>The array.</returns>
		public T[][] ToArray()
		{
			return ToList().ToArray();
		}

		public Tree<T> EncuentraSucc(T nodoSucc, bool Forzar = false)
		{
			Tree<T> ret;
			ret = Succ.Find(x => x.nodo.Equals(nodoSucc));
			if (ret == null && Forzar)
			{
				ret = new Tree<T>(nodoSucc, this);
				Succ.Add(ret);
			}
			return ret;
		}

		/// <summary>
		/// Agrega n objeto al árbol
		/// </summary>
		/// <param name="x">Objeto a agregar</param>
		public void Add(T[] x) //Esto quedaría genial en haskell
		{
			if (x.Length == 0)
			{
				EnumeraActual = true;
			}
			else
			{
				T[] y = new T[x.Length - 1];
				Tree<T> AgregaEn = EncuentraSucc(x[0], true);
				for (int i = 0; i < y.Length; i++)
				{
					y[i] = x[i + 1];
				}
				AgregaEn.Add(y);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Treelike.Tree`1"/> class.
		/// </summary>
		/// <param name="coll">Colección inicial</param>
		public Tree(IEnumerable<T[]> coll):this()
		{
			foreach (var x in coll)
			{
				Add(x);
			}
		}

		public override string ToString()
		{
			return ToList().ToString();
		}
	}

	/// <summary>
	/// Representa un árbol de strings que se puede enumerar.
	/// </summary>
	public class StringTree:Treelike.Tree<char>
	{
		/// <summary>
		/// Agrega un objeto al árbol
		/// </summary>
		/// <param name="x">Objeto a agregar</param>
		public void Add(string x)
		{
			base.Add(x.ToCharArray());
		}

		/// <summary>
		/// Serializa el árbol en una lista
		/// </summary>
		/// <returns>The list.</returns>
		public new List<string> ToList()
		{
			char[][] ret2 = base.ToArray();
			List<string> ret = new List<string>();
			foreach (var x in ret2)
			{
				ret.Add(x.ToString());
			}
			return ret;
		}

		/// <summary>
		/// Devuelve un arreglo enlistando una serialización de este árbol.
		/// </summary>
		/// <returns>The array.</returns>
		public new string[] ToArray()
		{
			return ToList().ToArray();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Treelike.StringTree"/> class.
		/// </summary>
		/// <param name="coll">La colección inicial.</param>
		public StringTree(IEnumerable<string> coll)
		{
			foreach (var x in coll)
			{
				Add(x);
			}
		}
	}
}
