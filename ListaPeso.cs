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
	/// <typeparam name="TVal">Rango(co-dominio) de la función.</typeparam>
	[DataContract (Name = "ListaPeso")]
	public class ListaPeso<T, TVal> : IDictionary<T, TVal>
	{
		#region Accesor

		public TVal this [T key] {
			get {
				TVal ret;
				return TryGetValue (key, out ret) ? ret : Nulo;
			}
			set {
				// Encontrar la Key buscada.
				foreach (var x in Keys.ToList()) {
					if (_Comparador (x, key)) {
						TVal prev = Model [x];
						Model [x] = value;
						AlCambiarValor?.Invoke (this, new CambioElementoEventArgs<T, TVal> (key, prev, Model [x]));
						return;
					}
				}

				// Si es entrada nueva, se agrega.
				Model.Add (key, value);
				AlCambiarValor?.Invoke (this, new CambioElementoEventArgs<T, TVal> (key, Nulo, Model [key]));
			}
		}

		#endregion

		#region Internos

		protected IDictionary<T, TVal> Model { get; }

		/// <summary>
		/// La operación suma.
		/// </summary>
		public Func<TVal, TVal, TVal> Suma;
		/// <summary>
		/// La operación inverso, si la tiene.
		/// </summary>
		public Func<TVal, TVal> Inv;

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
		public TVal Nulo {
			get;
			set;
		}

		public ReadonlyPair<T, TVal> Entrada (T entrada)
		{
			foreach (var x in this) {
				if (x.Key.Equals (entrada)) {
					return new ReadonlyPair<T, TVal> (x);
				}
			}
			return null;
		}

		#endregion

		#region IDictionary

		public void Add (T key, TVal value)
		{
			Model.Add (key, value);
		}

		public bool Remove (T key)
		{
			return Model.Remove (key);
		}

		public bool TryGetValue (T key, out TVal value)
		{
			return Model.TryGetValue (key, out value);
		}

		public ICollection<T> Keys {
			get {
				return Model.Keys;
			}
		}

		public ICollection<TVal> Values {
			get {
				return Model.Values;
			}
		}

		public void Add (KeyValuePair<T, TVal> item)
		{
			Model.Add (item);
		}

		public void Clear ()
		{
			Model.Clear ();
		}

		public bool Contains (KeyValuePair<T, TVal> item)
		{
			return Model.Contains (item);
		}

		public void CopyTo (KeyValuePair<T, TVal>[] array, int arrayIndex)
		{
			Model.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<T, TVal> item)
		{
			return Remove (item);
		}

		public int Count {
			get {
				return Model.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return Model.IsReadOnly;
			}
		}

		public IEnumerator<KeyValuePair<T, TVal>> GetEnumerator ()
		{
			return Model.GetEnumerator ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return Model.GetEnumerator ();
		}

		#endregion

		#region Estadísticos

		/// <summary>
		/// Devuelve la suma sobre las Keys, de sus respectivos Values.
		/// </summary>
		/// <returns>Devuelve el objeto resultante de aplicar la operación a cada valor.</returns>
		public TVal SumaTotal ()
		{
			TVal tot = Nulo;
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
		public T ObtenerMáximo (Func<TVal, TVal, bool> comparador)
		{
			if (!Any ()) {
				return default(T);
			} else {
				T tmp = Keys.ToArray () [0];
				foreach (T x in Keys) {
					if (comparador (this [x], this [tmp]))
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
		public event EventHandler<CambioElementoEventArgs<T,TVal>> AlCambiarValor;

		#endregion

		#region ctor

		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="operSuma">Operador suma inicial.</param>
		/// <param name="objetoNulo">Objeto cero inicial.</param>
		protected ListaPeso (Func<TVal, TVal, TVal> operSuma, TVal objetoNulo)
		{
			Suma = operSuma;
			Nulo = objetoNulo;
		}

		/// <summary>
		/// Inicializa una instancia de la clase a partir de un modelo de IDIccionary.
		/// </summary>
		/// <param name="operSuma">Operador suma inicial.</param>
		/// <param name="objetoNulo">Objeto cero inicial.</param>
		/// <param name="modelo">Modelo</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma, TVal objetoNulo, IDictionary<T, TVal> modelo = null)
			: this (operSuma, objetoNulo)
		{
			Model = modelo ?? new Dictionary<T, TVal> ();
		}

		protected ListaPeso ()
		{
		}

		#endregion

		#region Lista

		public bool ContainsKey (T key)
		{
			foreach (var x in Keys) {
				if (_Comparador (x, key))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Revisa si existe un objeto con las con las condiciones dadas.
		/// </summary>
		/// <param name="pred">Predicado a exaluar.</param>
		/// <returns>Devuelve true si existe un objeto que cumple Pred.</returns>
		public bool Any (Func<T, TVal, bool> pred)
		{
			foreach (var x in Keys) {
				if (pred (x, this [x]))
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

		/// <summary>
		/// Devuelve una copia del soporte.
		/// </summary>
		/// <returns>Un ISet que contiene a cada elemento del soporte.</returns>
		public ISet<T> Soporte ()
		{
			return new HashSet<T> (Model.Keys);
		}

		public override string ToString ()
		{
			string ret = "";
			foreach (var item in Model) {
				ret += string.Format ("{0} -> {1}\n", item.Key, item.Value);
			}
			return ret;
		}

		#region Operacional

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
		public ListaPeso<T, TVal> Inverso ()
		{
			if (Inv == null)
				throw new NullReferenceException ("No está definito Inv");
			var ret = new ListaPeso<T, TVal> (Suma, Nulo);
			ret.Inv = Inv;
			foreach (var x in Keys) {
				ret.Add (x, Inv (this [x]));
			}
			return ret;
		}

		/// <summary>
		/// Suma esta lista en otra.
		/// </summary>
		/// <param name="sumando">Lista sumando.</param>
		/// <returns></returns>
		[Obsolete]
		ListaPeso<T, TVal> SumarA (IDictionary<T, TVal> sumando) //TEST
		{
			var ret = (ListaPeso<T, TVal>)MemberwiseClone ();
			foreach (T x in sumando.Keys) {
				ret [x] = Suma (ret [x], sumando [x]);
			}
			return ret;
		}

		/// <summary>
		/// Suma dos ListaExtra.ListaPeso coordenada a coordenada.
		/// </summary>
		/// <param name="left">Primer sumando.</param>
		/// <param name="right">Segundo sumando.</param>
		/// <returns></returns>
		protected static ListaPeso<T, TVal> Sumar (ListaPeso<T, TVal> left, IDictionary<T, TVal> right)
		{
			var ret = new ListaPeso<T, TVal> (left.Suma, left.Nulo, null);

			foreach (var x in left) {
				ret [x.Key] = x.Value;
			}
			foreach (var x in right) {
				ret [x.Key] = ret.Suma (ret [x.Key], x.Value);
			}
			return ret;
		}

		public static ListaPeso<T, TVal> operator + (ListaPeso<T, TVal> left, IDictionary<T, TVal> right)
		{
			return Sumar (left, right);
		}

		public static ListaPeso<T, TVal> operator - (ListaPeso<T, TVal> x)
		{
			return x.Inverso ();
		}

		public static ListaPeso<T, TVal> operator - (ListaPeso<T, TVal> left, IDictionary<T, TVal> right)
		{
			var ret = left.MemberwiseClone () as ListaPeso<T, TVal>;
			foreach (var x in right) {
				ret [x.Key] = ret.Suma (ret.Inv (x.Value), ret [x.Key]);
			}
			return ret;
		}

		#endregion
	}

	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T1">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="T2">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="TVal">Tipo de valor</typeparam>
	public class ListaPeso<T1, T2, TVal> 
		:ListaPeso<Tuple<T1, T2>, TVal>
	{

		/// <summary>
		/// Initializes a new instance of the ListaPeso class.
		/// </summary>
		/// <param name="operSuma">Suma</param>
		/// <param name="objetoNulo">Objeto nulo</param>
		/// <param name="modelo">Modelo de diccionario</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma, TVal objetoNulo, IDictionary<Tuple<T1, T2>, TVal> modelo = null)
			: base (operSuma, objetoNulo, modelo)
		{
		}

		public TVal this [T1 x, T2 y] {
			get {
				return base [new Tuple<T1, T2> (x, y)];
			}
			set {
				base [new Tuple<T1, T2> (x, y)] = value;
			}
		}

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

		public static ListaPeso<T> operator + (ListaPeso<T> left, IDictionary<T, float> right)
		{
			var ret = new ListaPeso<T> ();
				
			foreach (var x in left) {
				ret [x.Key] = x.Value;
			}
			foreach (var x in right) {
				ret [x.Key] = ret.Suma (ret [x.Key], x.Value);
			}
			return ret;

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

		public static float operator * (ListaPeso<T> left, ListaPeso<T> right)
		{
			ISet<T> soporte = left.Soporte ();
			soporte.UnionWith (right.Soporte ());

			var ret = 0f;
			foreach (var x in soporte) {
				ret += left [x] * right [x];
			}

			return ret;
		}

		public new void Add (T key, float value)
		{
			this [key] += value;
		}


	}


	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T1">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="T2">Tipo de parámetro de la funcion</typeparam>
	public class ListaPesoFloat<T1, T2> : ListaPeso<T1, T2, float>
	{
		public ListaPesoFloat () :
			base ((x, y) => x + y, 0)
		{
		}
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