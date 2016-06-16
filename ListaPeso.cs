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
	[Serializable]
	public class ListaPeso<T, TVal> : IDictionary<T, TVal>, IEquatable<IDictionary<T, TVal>>
	{
		#region Accesor

		public TVal this [T key]
		{
			get
			{
				var ret = Entrada (key);
				return ret == null ? Nulo : ret.Value;
			}
			set
			{
				TVal val;
				if (Model.TryGetValue (key, out val))
				{
					Model [key] = value;
					AlCambiarValor?.Invoke (
						this,
						new CambioElementoEventArgs<T, TVal> (
							key,
							val,
							value));
				}
				else
				{
					// Si es entrada nueva, se agrega.
					Model.Add (key, value);
					AgregandoEntrada (key, value);
					AlCambiarValor?.Invoke (
						this,
						new CambioElementoEventArgs<T, TVal> (
							key,
							Nulo,
							Model [key]));
				}
			}
		}

		#endregion

		#region Internos

		[DataMember]
		protected IDictionary<T, TVal> Model { get; }

		/// <summary>
		/// La operación suma.
		/// </summary>
		[DataMember]
		public Func<TVal, TVal, TVal> Suma;
		/// <summary>
		/// La operación inverso, si la tiene.
		/// </summary>
		[DataMember]
		public Func<TVal, TVal> Inv;

		/// <summary>
		/// Devuelve o establece cuál es el objeto nulo (cero) del grupoide; o bien, el velor prederminado de cada entrada T del dominio.
		/// </summary>
		[DataMember]
		public TVal Nulo { get; set; }

		/// <summary>
		/// Devuelve la entrada correspondiente a un Key
		/// </summary>
		/// <param name="entrada">Key de la entrada</param>
		public ReadonlyPair<T, TVal> Entrada (T entrada)
		{
			TVal ret;
			return TryGetValue (entrada, out ret) ? 
				new ReadonlyPair<T, TVal> (entrada, ret) : 
				null;
		}

		#endregion

		#region IDictionary

		public IEqualityComparer<T> Comparador
		{
			get
			{
				return (Model as Dictionary<T, TVal>)?.Comparer;
			}
		}

		public void Add (T key, TVal value)
		{
			Model.Add (key, value);
		}

		public bool Remove (T key)
		{
			return Model.Remove (key);
		}

		public bool ContainsKey (T key)
		{
			return Model.ContainsKey (key);
		}

		public bool TryGetValue (T key, out TVal value)
		{
			return Model.TryGetValue (key, out value);
		}

		public ICollection<T> Keys
		{
			get
			{
				return Model.Keys;
			}
		}

		public ICollection<TVal> Values
		{
			get
			{
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

		public void CopyTo (KeyValuePair<T, TVal> [] array, int arrayIndex)
		{
			Model.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<T, TVal> item)
		{
			return Remove (item);
		}

		public int Count
		{
			get
			{
				return Model.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
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
			foreach (T x in Keys)
			{
				tot = Suma (tot, this [x]);
			}
			return tot;
		}

		/// <summary>
		/// Devuelve una copia del soporte.
		/// </summary>
		/// <returns>Un ISet que contiene a cada elemento del soporte.</returns>
		public ISet<T> Soporte ()
		{
			return new HashSet<T> (Model.Keys);
		}

		#endregion

		#region Orden y máximos

		/// <summary>
		/// Obtiene la entrda cuyo valor es máximo.
		/// </summary>
		/// <returns></returns>		
		public T ObtenerMáximo (Func<TVal, TVal, bool> comparador)
		{
			if (!Any ())
			{
				return default(T);
			}
			else
			{
				T tmp = Keys.ToArray () [0];
				foreach (T x in Keys)
				{
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
		public event EventHandler<CambioElementoEventArgs<T, TVal>> AlCambiarValor;

		/// <summary>
		/// Ocurre cuando se agrega una nueva entrada(key) al diccionario
		/// </summary>
		public event EventHandler<CambioElementoEventArgs<T, TVal>> AlAgregarEntrada;

		#endregion


		#region ctor

		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="operSuma">Operador suma inicial.</param>
		/// <param name="objetoNulo">Objeto cero inicial.</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IEqualityComparer<T> comparador)
			: this (comparador)
		{
			Suma = operSuma;
			Nulo = objetoNulo;
		}

		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo)
			: this (null)
		{
			Suma = operSuma;
			Nulo = objetoNulo;
		}

		/// <summary>
		/// Inicializa una instancia de la clase a partir de un modelo de IDiccionary.
		/// </summary>
		/// <param name="operSuma">Operador suma inicial.</param>
		/// <param name="objetoNulo">Objeto cero inicial.</param>
		/// <param name="modelo">Modelo</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IDictionary<T, TVal> modelo)
		{
			Suma = operSuma;
			Nulo = objetoNulo;
			Model = modelo;
		}

		protected ListaPeso (IEqualityComparer<T> comparador = null)
		{
			var Comparador = comparador ?? EqualityComparer<T>.Default;
			Model = new Dictionary<T, TVal> (Comparador);
		}

		/// <summary>
		/// Cuando se agrega un nuevo key al diccionario, 
		/// se ejecuta esta función.
		/// Además éste método invoca a su respectivo evento.
		/// </summary>
		/// <param name="key">Key agregada</param>
		/// <param name="value">Valor de esta key.</param>
		protected virtual void AgregandoEntrada (T key, TVal value)
		{
			AlAgregarEntrada?.Invoke (
				this,
				new CambioElementoEventArgs<T, TVal> (
					key,
					Nulo,
					value));
		}

		#endregion

		#region Lista

		/// <summary>
		/// Revisa si existe un objeto con las con las condiciones dadas.
		/// </summary>
		/// <param name="pred">Predicado a exaluar.</param>
		/// <returns>Devuelve true si existe un objeto que cumple Pred.</returns>
		public bool Any (Func<T, TVal, bool> pred)
		{
			foreach (var x in Keys)
			{
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

		#region IEquatable

		public bool Equals (IDictionary<T, TVal> other)
		{
			var supp = Soporte ();
			supp.UnionWith (other.Keys);
			foreach (var x in supp)
			{
				if (other.ContainsKey (x))
				{
					if (!this [x].Equals (other [x]))
						return false;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		#endregion

		#region Generales

		public override string ToString ()
		{
			string ret = "";
			foreach (var item in Model)
			{
				ret += string.Format ("{0} -> {1}\n", item.Key, item.Value);
			}
			return ret;
		}

		#endregion

		#region Operacional

		/// <summary>
		/// Devuelve la lista inversa a esta instancia.
		/// </summary>
		/// <returns></returns>
		public ListaPeso<T, TVal> Inverso ()
		{
			if (Inv == null)
				throw new NullReferenceException ("No está definito Inv");
			var ret = new ListaPeso<T, TVal> (Suma, Nulo, Comparador);
			ret.Inv = Inv;
			foreach (var x in Keys)
			{
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
		ListaPeso<T, TVal> SumarA (IDictionary<T, TVal> sumando)
		{
			var ret = (ListaPeso<T, TVal>)MemberwiseClone ();
			foreach (T x in sumando.Keys)
			{
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
		protected static ListaPeso<T, TVal> Sumar (ListaPeso<T, TVal> left,
		                                           IDictionary<T, TVal> right)
		{
			var ret = new ListaPeso<T, TVal> (left.Suma, left.Nulo, left.Comparador);

			foreach (var x in left)
			{
				ret [x.Key] = x.Value;
			}
			foreach (var x in right)
			{
				ret [x.Key] = ret.Suma (ret [x.Key], x.Value);
			}
			return ret;
		}

		public static ListaPeso<T, TVal> operator + (ListaPeso<T, TVal> left,
		                                             IDictionary<T, TVal> right)
		{
			return Sumar (left, right);
		}

		public static ListaPeso<T, TVal> operator - (ListaPeso<T, TVal> x)
		{
			return x.Inverso ();
		}

		public static ListaPeso<T, TVal> operator - (ListaPeso<T, TVal> left,
		                                             IDictionary<T, TVal> right)
		{
			var ret = left.MemberwiseClone () as ListaPeso<T, TVal>;
			foreach (var x in right)
			{
				ret [x.Key] = ret.Suma (ret.Inv (x.Value), ret [x.Key]);
			}
			return ret;
		}

		#endregion

		#region Serializable

		public void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			var iser = Model as ISerializable;
			iser.GetObjectData (info, context);
		}

		#endregion
	}

	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T1">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="T2">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="TVal">Tipo de valor</typeparam>
	[Serializable]
	public class ListaPeso<T1, T2, TVal> 
		: ListaPeso<Tuple<T1, T2>, TVal>
	{
		/// <summary>
		/// Initializes a new instance of the ListaPeso class.
		/// </summary>
		/// <param name="operSuma">Suma</param>
		/// <param name="objetoNulo">Objeto nulo</param>
		/// <param name="modelo">Modelo de diccionario</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IDictionary<Tuple<T1, T2>, TVal> modelo)
			: base (operSuma,
			        objetoNulo,
			        modelo)
		{
		}

		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo)
			: base (operSuma,
			        objetoNulo, new TupleComparador<T1, T2> ())
		{
		}

		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IEqualityComparer<Tuple<T1, T2>> comparador)
			: base (operSuma, objetoNulo, comparador)
		{
		}

		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IEqualityComparer<T1> comparador1,
		                  IEqualityComparer<T2> comparador2)
			: base (operSuma,
			        objetoNulo,
			        new TupleComparador<T1, T2> (
				        comparador1,
				        comparador2))
		{
		}

		public ListaPeso (IEqualityComparer<Tuple<T1, T2>> comparador)
			: base (comparador)
		{
		}

		public TVal this [T1 x, T2 y]
		{
			get
			{
				return base [new Tuple<T1, T2> (x, y)];
			}
			set
			{
				base [new Tuple<T1, T2> (x, y)] = value;
			}
		}

	}

	[Serializable]
	public class ListaPeso<T> : ListaPeso<T, Single>, IComparable<IDictionary<T, Single>>
	{
		public ListaPeso (IDictionary<T, float> modelo)
			: base ((x, y) => x + y, 0, modelo)
		{
		}

		public ListaPeso ()
			: base ((x, y) => x + y, 0)
		{
		}

		public ListaPeso (IEqualityComparer<T> comparador)
			: base ((x, y) => x + y,
			        0,
			        comparador)
		{
		}

		public static bool operator <= (ListaPeso<T> left,
		                                IDictionary<T, float> right)
		{
			foreach (var x in left.Keys)
			{
				if (left [x] > right [x])
					return false;
			}
			return true;
		}

		public static bool operator >= (ListaPeso<T> left,
		                                IDictionary<T, float> right)
		{
			foreach (var x in right.Keys)
			{
				if (left [x] < right [x])
					return false;
			}
			foreach (var x in left.Keys)
			{
				if (left [x] < right [x])
					return false;
			}
			return true;
		}

		public static ListaPeso<T> operator + (ListaPeso<T> left,
		                                       IDictionary<T, float> right)
		{
			var ret = new ListaPeso<T> ();
				
			foreach (var x in left)
			{
				ret [x.Key] = x.Value;
			}
			foreach (var x in right)
			{
				ret [x.Key] = ret.Suma (ret [x.Key], x.Value);
			}
			return ret;

		}

		public static ListaPeso<T> operator * (ListaPeso<T> left, float right)
		{
			var ret = new ListaPeso<T> ();
			foreach (var x in left)
			{
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
			foreach (var x in soporte)
			{
				ret += left [x] * right [x];
			}

			return ret;
		}

		public new void Add (T key, float value)
		{
			this [key] += value;
		}

		#region IComparable

		public int CompareTo (IDictionary<T, float> other)
		{
			return this <= other ? -1 : this >= other ? 1 : 0;
		}

		#endregion

	}

	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T1">Tipo de parámetro de la funcion</typeparam>
	/// <typeparam name="T2">Tipo de parámetro de la funcion</typeparam>
	[Serializable]
	public class ListaPesoFloat<T1, T2> : ListaPeso<T1, T2, float>
	{
		public ListaPesoFloat ()
			: base ((x, y) => x + y, 0)
		{
		}

		public ListaPesoFloat (IDictionary<Tuple<T1, T2>, float> modelo)
			: base ((x, y) => x + y, 0, modelo)
		{
		}


		public ListaPesoFloat (IEqualityComparer<Tuple<T1, T2>> comparador)
			: base ((x, y) => x + y, 
			        0,
			        comparador)
		{
		}


		public ListaPesoFloat (IEqualityComparer<T1> comparador1,
		                       IEqualityComparer<T2> comparador2)
			: base ((x, y) => x + y, 
			        0,
			        comparador1,
			        comparador2)
		{
		}
	}
}