using System;
using System.Collections.Generic;
using System.Linq;
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
				return TryGetValue (Key, out ret) ? ret : Nulo;
			}
			set {
				// Encontrar la Key buscada.
				foreach (var x in Keys.ToList()) {
					if (_Comparador (x, Key)) {
						V prev = _model [x];
						_model [x] = value;
						AlCambiarValor?.Invoke (this, new CambioElementoEventArgs<T, V> (Key, prev, _model [x]));
						return;
					}
				}

				// Si es entrada nueva, se agrega.
				_model.Add (Key, value);
				AlCambiarValor?.Invoke (this, new CambioElementoEventArgs<T, V> (Key, Nulo, _model [Key]));
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

		Func<T, T, bool> _Comparador = (x, y) => x.Equals (y);

		/// <summary>
		/// Devuelve o establece qué función sirve para saber si dos T's son idénticos para esta lista.
		/// Por default es x.Equals(y).
		/// </summary>
		public Func<T, T, bool> Comparador {
			get { return _Comparador; }
			set { _Comparador = value; }
		}


		/// <summary>
		/// Devuelve o establece cuál es el objeto nulo (cero) del grupoide; o bien, el velor prederminado de cada entrada T del dominio.
		/// </summary>
		public V Nulo {
			get;
			set;
		}

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
		public event EventHandler<CambioElementoEventArgs<T,V>> AlCambiarValor;

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
			_model = modelo ?? new Dictionary<T, V> ();
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

		public override string ToString ()
		{
			string ret = "";
			foreach (var item in _model) {
				ret += string.Format ("{0} -> {1}\n", item.Key, item.Value);
			}
			return ret;
		}

		#region Operacional

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
		public ListaPeso<T, V> Inverso ()
		{
			if (Inv == null)
				throw new NullReferenceException ("No está definito Inv");
			var ret = new ListaPeso<T, V> (Suma, Nulo);
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
			var ret = (ListaPeso<T, V>)MemberwiseClone ();
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
	public class ListaPeso<T> : ListaPeso<T, Single>
	{
		public ListaPeso (IDictionary<T, float> modelo = null)
			: base ((x, y) => x + y, 0, modelo)
		{
		}

		public static bool operator <= (ListaPeso<T> left, IDictionary<T, float> right)
		{
			foreach (var x in left.Keys) {
				if (left [x] > right [x])
					return false;
			}
			return true;
		}

		public static bool operator >= (ListaPeso<T> left, IDictionary<T, float> right)
		{
			foreach (var x in left.Keys) {
				if (left [x] > right [x])
					return false;
			}
			return true;
		}

		public static ListaPeso<T> operator + (ListaPeso<T> left, ListaPeso<T> right)
		{
			return (ListaPeso<T>)ListaPeso<T, float>.Sumar (left, right);
		}

		public static ListaPeso<T> operator * (ListaPeso<T> left, float right)
		{
			var ret = new ListaPeso<T> ();
			foreach (var x in left) {
				ret [x.Key] = x.Value * right;
			}
			return ret;
		}

		public static ListaPeso<T> operator * (float left, ListaPeso<T> right)
		{
			return right * left;
		}


		public new void Add (T key, float value)
		{
			this [key] += value;
		}

		#region Otros IDictionary

		#endregion

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
		public static ObjetoAcotado<Double> ToAcotado (this Double x)
		{
			var ret = new ObjetoAcotado<Double> (ComparadoresPred.EsMenor, Double.MinValue, Double.MaxValue, 0);
			ret.Valor = x;
			return ret;
		}
	}
}