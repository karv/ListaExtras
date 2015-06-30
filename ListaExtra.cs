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
	[DataContract (Name = "ListaPeso")]
	public class ListaPeso<T, V> : IDictionary<T, V>
	{
		#region Accesor

		public V this [T Key] {
			get {
				V ret;
				if (TryGetValue (Key, out ret)) {
					return ret;
				}
				return Nulo;
			}
			set {
				// Encontrar la Key buscada.
				foreach (var x in Keys.ToList()) {
					if (_Comparador (x, Key)) {
						_model [x] = value;
						if (CambioValor != null)
							CambioValor.Invoke (this, new EventArgs ());
						return;
					}
				}

				// Si es entrada nueva, se agrega.
				if (CambioValor != null)
					CambioValor.Invoke (this, new EventArgs ());
				_model.Add (Key, value);
			}
		}

		#endregion

		#region Internos

		IDictionary<T, V> _model;

		/// <summary>
		/// La operación suma.
		/// </summary>
		public Func<V, V, V> Suma;
		/// <summary>
		/// La operación inverso, si la tiene.
		/// </summary>
		public Func<V, V> Inv;

		private Func<T, T, bool> _Comparador = (x, y) => x.Equals (y);

		/// <summary>
		/// Devuelve o establece qué función sirve para saber si dos T's son idénticos para esta lista.
		/// Por default es x.Equals(y).
		/// </summary>
		public Func<T, T, bool> Comparador {
			get { return _Comparador; }
			set { _Comparador = value; }
		}

		private V _NullV;

		/// <summary>
		/// Devuelve o establece cuál es el objeto nulo (cero) del grupoide; o bien, el velor prederminado de cada entrada T del dominio.
		/// </summary>
		public V Nulo {
			get {
				return _NullV;
			}
			set {
				_NullV = value;
			}
		}

		// TODO
		/// <summary>
		/// Determina si las entradas se eliminan al llegar su valor a su valor nulo.
		/// </summary>
		public bool borrarEnNull = false;

		public ReadonlyPair<T, V> getEntrada (T entrada)
		{
			foreach (var x in this) {
				if (x.Key.Equals (entrada)) {
					return new ReadonlyPair<T, V> (x);
				}
			}
			return null;
		}

		#endregion

		#region IDictionary

		public void Add (T key, V value)
		{
			_model.Add (key, value);
		}

		public bool Remove (T key)
		{
			return _model.Remove (key);
		}

		public bool TryGetValue (T key, out V value)
		{
			return _model.TryGetValue (key, out value);
		}

		public ICollection<T> Keys {
			get {
				return _model.Keys;
			}
		}

		public ICollection<V> Values {
			get {
				return _model.Values;
			}
		}

		public void Add (KeyValuePair<T, V> item)
		{
			_model.Add (item);
		}

		public void Clear ()
		{
			_model.Clear ();
		}

		public bool Contains (KeyValuePair<T, V> item)
		{
			return _model.Contains (item);
		}

		public void CopyTo (KeyValuePair<T, V>[] array, int arrayIndex)
		{
			_model.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<T, V> item)
		{
			return Remove (item);
		}

		public int Count {
			get {
				return _model.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return _model.IsReadOnly;
			}
		}

		public IEnumerator<KeyValuePair<T, V>> GetEnumerator ()
		{
			return _model.GetEnumerator ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _model.GetEnumerator ();
		}

		#endregion

		#region Estadísticos

		/// <summary>
		/// Devuelve la suma sobre las Keys, de sus respectivos Values.
		/// </summary>
		/// <returns>Devuelve el objeto resultante de aplicar la operación a cada valor.</returns>
		public V SumaTotal ()
		{
			V tot = Nulo;
			foreach (T x in Keys) {
				tot = Suma (tot, this [x]);
			}
			return tot;
		}

		#endregion

		#region Orden y máximos

		/// <summary>
		/// Obtiene la entrda cuyo valor es máximo.
		/// </summary>
		/// <returns></returns>		
		public T ObtenerMáximo (Func<V, V, bool> Comparador)
		{
			if (!Any ()) {
				return default(T);
			} else {
				T tmp = Keys.ToArray () [0];
				foreach (T x in Keys) {
					if (Comparador (this [x], this [tmp]))
						tmp = x;
				}
				return tmp;
			}
		}

		#endregion

		#region Eventos

		/// <summary>
		/// Se llama cuando se cambia algún valor.
		/// </summary>
		public event EventHandler CambioValor;
		//TEST

		#endregion

		#region ctor

		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="OperSuma">Operador suma inicial.</param>
		/// <param name="ObjetoNulo">Objeto cero inicial.</param>
		ListaPeso (Func<V, V, V> OperSuma, V ObjetoNulo)
		{
			Suma = OperSuma;
			Nulo = ObjetoNulo;
		}

		/// <summary>
		/// Inicializa una instancia de la clase a partir de un modelo de IDIccionary.
		/// </summary>
		/// <param name="operSuma">Operador suma inicial.</param>
		/// <param name="objetoNulo">Objeto cero inicial.</param>
		/// <param name="modelo">Modelo</param>
		public ListaPeso (Func<V, V, V> operSuma, V objetoNulo, IDictionary<T, V> modelo = null)
			: this (operSuma, objetoNulo)
		{
			if (modelo == null)
				_model = new Dictionary<T, V> ();
			else
				_model = modelo;
		}

		protected ListaPeso ()
		{
		}

		#endregion

		#region Lista

		public bool ContainsKey (T Key)
		{
			foreach (var x in Keys) {
				if (_Comparador (x, Key))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Revisa si existe un objeto con las con las condiciones dadas.
		/// </summary>
		/// <param name="Pred">Predicado a exaluar.</param>
		/// <returns>Devuelve true si existe un objeto que cumple Pred.</returns>
		public bool Any (Func<T, V, bool> Pred)
		{
			foreach (var x in Keys) {
				if (Pred (x, this [x]))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Revisa si existe un objeto en esta lista.
		/// </summary>
		/// <returns>Devuelve true si existe algo.</returns>
		public bool Any ()
		{
			return Count > 0;
		}

		#endregion

		#region Operacional

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
		public ListaPeso<T, V> Inverso ()
		{
			if (Inv == null)
				throw new NullReferenceException ("No está definito Inv");
			ListaPeso<T, V> ret = new ListaPeso<T, V> (Suma, Nulo);
			ret.Inv = Inv;
			foreach (var x in Keys) {
				ret.Add (x, Inv (this [x]));
			}
			return ret;
		}

		/// <summary>
		/// Suma esta lista en otra.
		/// </summary>
		/// <param name="S">Lista sumando.</param>
		/// <returns></returns>
		public ListaPeso<T, V> SumarA (ListaPeso<T, V> S)
		{
			ListaPeso<T, V> ret = (ListaPeso<T, V>)this.MemberwiseClone ();
			foreach (T x in S.Keys) {
				ret.Add (x, S [x]);
			}
			return ret;
		}

		/// <summary>
		/// Suma dos ListaExtra.ListaPeso coordenada a coordenada.
		/// </summary>
		/// <param name="Left">Primer sumando.</param>
		/// <param name="Right">Segundo sumando.</param>
		/// <returns></returns>
		public static ListaPeso<T, V> Sumar (ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Left.SumarA (Right);
		}

		public static ListaPeso<T, V> operator + (ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Sumar (Left, Right);
		}

		public static ListaPeso<T, V> operator - (ListaPeso<T, V> x)
		{
			return x.Inverso ();
		}

		public static ListaPeso<T, V> operator - (ListaPeso<T, V> Left, ListaPeso<T, V> Right)
		{
			return Left + -Right;
		}

		#endregion
	}

	[DataContract (Name = "ListaPeso")]
	public class ListaPeso<T> : ListasExtra.ListaPeso<T, Single>
	{
		public ListaPeso ()
			: base ((x, y) => x + y, 0)
		{
		}

		public static bool operator <= (ListasExtra.ListaPeso<T> left, ListasExtra.ListaPeso<T> right)
		{

			foreach (var x in left.Keys) {
				if (left [x] > right [x])
					return false;
			}
			return true;
		}

		public static bool operator >= (ListasExtra.ListaPeso<T> left, ListasExtra.ListaPeso<T> right)
		{
			return right <= left;
		}

		public static ListaPeso<T> operator + (ListasExtra.ListaPeso<T> left, ListasExtra.ListaPeso<T> right)
		{
			return (ListaPeso<T>)ListaPeso<T, float>.Sumar (left, right);
		}

		public new void Add (T key, float value)
		{
			this [key] += value;
		}

	}

	/// <summary>
	/// Es sólo una listaPeso de enteros largos.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ListaContador<T> : ListasExtra.ListaPeso<T, long>
	{
		public ListaContador ()
			: base ((x, y) => x + y, 0)
		{
		}

		public long CountIf (Func<T, bool> Selector)
		{
			long ret = 0;
			foreach (var x in Keys) {
				if (Selector.Invoke (x)) {
					ret += this [x];
				}
			}
			return ret;

		}
	}

	[DataContract]
	public class ObjetoAcotado<T>
	{
		public T CotaSup;
		public T CotaInf;
		private T _Valor;
		//private Comparer<T> Comparador;
		private Func<T, T, Boolean> _EsMenor;

		public Func<T, T, Boolean> EsMenor {
			get {
				return _EsMenor;
			}
			private set {
				_EsMenor = value;
			}
		}

		public T Valor {
			get {
				return _Valor;
			}
			set {

				_Valor = EsMenor (value, CotaSup) ? (EsMenor (value, CotaInf) ? CotaInf : value) : CotaSup;
				if (_Valor.Equals (CotaInf)) {
					EventHandler Handler = LlegóMínimo;
					Handler (this, null);
				}
				if (_Valor.Equals (CotaSup)) {
					EventHandler Handler = LlegóMáximo;
					Handler (this, null);
				}
			}
		}

		public ObjetoAcotado (Func<T, T, Boolean> Comparador)
		{
			EsMenor = Comparador;
		}

		public ObjetoAcotado (Func<T, T, Boolean> Comparador, T Min, T Max, T Inicial)
			: this (Comparador)
		{
			CotaInf = Min;
			CotaSup = Max;
			Valor = Inicial;
		}

		public override string ToString ()
		{
			return Valor.ToString ();
		}
		//Eventos
		public event EventHandler LlegóMínimo;
		public event EventHandler LlegóMáximo;
	}

	public static class ComparadoresPred
	{
		public static Boolean EsMenor (Double x, Double y)
		{
			return x < y;
		}

		public static Boolean EsMenor (Single x, Single y)
		{
			return x < y;
		}
	}

	public static class OperadoresPred
	{
		public static Double Suma (Double x, Double y)
		{
			return x + y;
		}

		public static long Suma (long x, long y)
		{
			return x + y;
		}

		public static ObjetoAcotado<Double> Suma (ObjetoAcotado<Double> x, ObjetoAcotado<Double> y)
		{
			ObjetoAcotado<Double> ret = new ObjetoAcotado<double> (x.EsMenor);
			ret.CotaSup = Math.Max (x.CotaSup, y.CotaSup);
			ret.CotaInf = Math.Min (x.CotaInf, y.CotaInf);
			ret.Valor = x.Valor + y.Valor;
			return ret;
		}
	}

	public static class ExtDouble
	{
		public static ListasExtra.ObjetoAcotado<Double> ToAcotado (this Double x)
		{
			ObjetoAcotado<Double> ret = new ObjetoAcotado<Double> (ComparadoresPred.EsMenor, Double.MinValue, Double.MaxValue, 0);
			ret.Valor = x;
			return ret;
		}
	}
}