using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Collections;

namespace ListasExtra
{
	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	/// <typeparam name="T">Dominio de la función.</typeparam>
	/// <typeparam name="TVal">Rango(co-dominio) de la función.</typeparam>
	[Serializable]
	public class ListaPeso<T, TVal> : IDictionary<T, TVal>, IEquatable<IDictionary<T, TVal>>, IStructuralEquatable, IStructuralComparable
	{
		#region Accesor

		/// <summary>
		/// Devuelve el valor correspondiete a una entrada dada
		/// </summary>
		/// <param name="key">Key de la entrada</param>
		public TVal this [T key]
		{
			get
			{
				TVal ret;
				return  Entrada (key, out ret) ? ret : Nulo;
			}
			set
			{
				TVal val;
				CambioElementoEventArgs<T, TVal> ret;
				if (Model.TryGetValue (key, out val))
				{
					Model [key] = value;
					ret = new CambioElementoEventArgs<T, TVal> (key, val, value);
				}
				else
				{
					// Si es entrada nueva, se agrega.
					Model.Add (key, value);
					AgregandoEntrada (key, value);
					ret = new CambioElementoEventArgs<T, TVal> (
						key,
						Nulo,
						value);
				}
				checkForRemoval (key);
				AlCambiarValor?.Invoke (this, ret);
			}
		}

		void checkForRemoval (T key)
		{
			var val = this [key];
			if (val.Equals (Nulo))
				Remove (key);
		}

		#endregion

		#region Internos

		/// <summary>
		/// Devuelve el modelo que diccionario que se usa.
		/// </summary>
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
		public bool Entrada (T entrada, out TVal ret)
		{
			foreach (var x in this)
				if (Comparador.Equals (x.Key, entrada))
				{
					ret = x.Value;
					return true;
				}
			ret = default(TVal);
			return false;
		}

		#endregion

		#region IDictionary

		/// <summary>
		/// Devuelve el comparador que se usa para las keys
		/// </summary>
		/// <value>The comparador.</value>
		public IEqualityComparer<T> Comparador
		{
			get
			{
				return (Model as Dictionary<T, TVal>)?.Comparer;
			}
		}

		/// <summary>
		/// Agrega una entrada
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void Add (T key, TVal value)
		{
			Model.Add (key, value);
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Elimina una entrada del diccionario, dado su key
		/// </summary>
		public bool Remove (T key)
		{
			return Model.Remove (key);
		}

		/// <Docs>The key to locate in the current instance.</Docs>
		/// <para>Determines whether the current instance contains an entry with the specified key.</para>
		/// <summary>
		/// Determina si contiene un key dado.
		/// </summary>
		public bool ContainsKey (T key)
		{
			return Model.ContainsKey (key);
		}

		/// <Docs>To be added.</Docs>
		/// <summary>
		/// Intenta devolver el valor de una key correspondiente
		/// </summary>
		/// <returns><c>true</c>, si la entrada existe y devuelve algo, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Valor de regreso.</param>
		public bool TryGetValue (T key, out TVal value)
		{
			return Model.TryGetValue (key, out value);
		}

		/// <summary>
		/// Devuelve la colección de las keys
		/// </summary>
		public ICollection<T> Keys
		{
			get
			{
				return Model.Keys;
			}
		}

		/// <summary>
		/// Devuelve la colección de los valores
		/// </summary>
		public ICollection<TVal> Values
		{
			get
			{
				return Model.Values;
			}
		}

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Agrega una entrada
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (KeyValuePair<T, TVal> item)
		{
			Model.Add (item);
		}

		/// <summary>
		/// Vacía el diccionario
		/// </summary>
		public void Clear ()
		{
			Model.Clear ();
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Revisa si existe una entrada
		/// </summary>
		public bool Contains (KeyValuePair<T, TVal> item)
		{
			return Model.Contains (item);
		}

		/// <summary>
		/// Copia este diccionario a un arreglo
		/// </summary>
		public void CopyTo (KeyValuePair<T, TVal> [] array, int arrayIndex)
		{
			Model.CopyTo (array, arrayIndex);
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Elimina una entrada dada
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove (KeyValuePair<T, TVal> item)
		{
			return Remove (item);
		}

		/// <summary>
		/// Devuelve el número de entradas del dicionario.
		/// </summary>
		public int Count
		{
			get
			{
				return Model.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly
		{
			get
			{
				return Model.IsReadOnly;
			}
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
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
		/// <param name="comparador">Comparador.</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IEqualityComparer<T> comparador)
			: this (comparador)
		{
			Suma = operSuma;
			Nulo = objetoNulo;
		}

		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="operSuma">Oper suma.</param>
		/// <param name="objetoNulo">Objeto nulo.</param>
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

		/// <summary>
		/// Inicializa una instancia de la clase.
		/// </summary>
		/// <param name="comparador">Comparador.</param>
		protected ListaPeso (IEqualityComparer<T> comparador = null)
		{
			var cprd = comparador ?? EqualityComparer<T>.Default;
			Model = new Dictionary<T, TVal> (cprd);
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

		/// <summary>
		/// Determina si este diccionario coincide con otro del mismo tipo
		/// </summary>
		/// <param name="other">The other to compare with the current dictionary.</param>
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

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current dictionary.
		/// </summary>
		public override string ToString ()
		{
			string ret = "";
			foreach (var item in Model)
			{
				ret += string.Format ("{0} -> {1}\n", item.Key, item.Value);
			}
			return ret;
		}

		bool _eliminarValoresNull;

		/// <summary>
		/// Determina si las entradas cuyas <c>value</c> es igual a <c>Nulo</c> deben de ser eliminadas del modelo.
		/// </summary>
		/// <value><c>true</c> if eliminar valores null; otherwise, <c>false</c>.</value>
		public bool EliminarValoresNull // TEST
		{
			get
			{
				return _eliminarValoresNull;
			}
			set
			{
				_eliminarValoresNull = value;
				if (value)
					eliminarEntradasNulas ();
			}
		}

		void eliminarEntradasNulas ()
		{
			var removing = this.Where (z => z.Value.Equals (Nulo));
			foreach (var r in removing)
				Remove (r);
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

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		public static ListaPeso<T, TVal> operator + (ListaPeso<T, TVal> left,
		                                             IDictionary<T, TVal> right)
		{
			return Sumar (left, right);
		}

		/// <param name="x">other</param>
		public static ListaPeso<T, TVal> operator - (ListaPeso<T, TVal> x)
		{
			return x.Inverso ();
		}

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
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

		#region Structural

		// TEST: region
		/// <summary>
		/// Compares the current ListaPeso with some other dictionary using a IEqualityComparer for its elements
		/// </summary>
		/// <param name="other">Other.</param>
		/// <param name="comparer">Comparer.</param>
		public bool Equals (object other, IEqualityComparer comparer)
		{
			var otherDict = other as IDictionary<T, TVal>;
			if (otherDict == null)
				return false;
			var keys = new HashSet<T> ();
			keys.UnionWith (Keys);
			keys.UnionWith (otherDict.Keys);
			foreach (var x in keys)
			{
				TVal otherVal;
				// Regresar false si falla en alguna entrada.
				if (!otherDict.TryGetValue (x, out otherVal) && comparer.Equals (
					    this [x],
					    otherVal))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns>The hash code.</returns>
		/// <param name="comparer">Comparer.</param>
		public int GetHashCode (IEqualityComparer comparer)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determina el orden que deben tener dos objetos bajo un comparador
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="other">Other.</param>
		/// <param name="comparer">Comparer.</param>
		public int CompareTo (object other, IComparer comparer)
		{
			throw new NotImplementedException ();
		}

		#endregion

		#region Serializable

		/// <summary>
		/// Gets the object data.
		/// </summary>
		/// <param name="info">Info.</param>
		/// <param name="context">Context.</param>
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

		/// <summary>
		/// </summary>
		/// <param name="operSuma">Oper suma.</param>
		/// <param name="objetoNulo">Objeto nulo.</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo)
			: base (operSuma,
			        objetoNulo, new TupleComparador<T1, T2> ())
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="operSuma">Oper suma.</param>
		/// <param name="objetoNulo">Objeto nulo.</param>
		/// <param name="comparador">Comparador.</param>
		public ListaPeso (Func<TVal, TVal, TVal> operSuma,
		                  TVal objetoNulo,
		                  IEqualityComparer<Tuple<T1, T2>> comparador)
			: base (operSuma, objetoNulo, comparador)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="operSuma">Oper suma.</param>
		/// <param name="objetoNulo">Objeto nulo.</param>
		/// <param name="comparador1">Comparador del primer valor de Key.</param>
		/// <param name="comparador2">Comparador del segundo valor de Key.</param>
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

		/// <summary>
		/// </summary>
		/// <param name="comparador">Comparador.</param>
		public ListaPeso (IEqualityComparer<Tuple<T1, T2>> comparador)
			: base (comparador)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="x">Primer valor.</param>
		/// <param name="y">Segundo valor.</param>
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

	/// <summary>
	/// Representa una lista tipo Dictionary (o mejor aún una función de soporte finito) con operaciones de grupoide.
	/// </summary>
	[Serializable]
	public class ListaPeso<T> : ListaPeso<T, Single>, IComparable<IDictionary<T, Single>>
	{
		/// <summary>
		/// </summary>
		/// <param name="modelo">Modelo.</param>
		public ListaPeso (IDictionary<T, float> modelo)
			: base ((x, y) => x + y, 0, modelo)
		{
		}

		/// <summary>
		/// </summary>
		public ListaPeso ()
			: base ((x, y) => x + y, 0)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="comparador">Comparador.</param>
		public ListaPeso (IEqualityComparer<T> comparador)
			: base ((x, y) => x + y,
			        0,
			        comparador)
		{
		}

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
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

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
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

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
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

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		public static ListaPeso<T> operator * (ListaPeso<T> left, float right)
		{
			var ret = new ListaPeso<T> ();
			foreach (var x in left)
			{
				ret [x.Key] = x.Value * right;
			}
			return ret;
		}

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		public static ListaPeso<T> operator * (float left, ListaPeso<T> right)
		{
			return right * left;
		}

		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
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

		/// <summary>
		/// Suma, en una entrada dada, un valor dado.
		/// </summary>
		/// <param name="key">Entrada.</param>
		/// <param name="value">Valor.</param>
		public new void Add (T key, float value)
		{
			this [key] += value;
		}

		#region IComparable

		/// <Docs>To be added.</Docs>
		/// <para>Returns the sort order of the current instance compared to the specified object.</para>
		/// <summary>
		/// </summary>
		/// <param name="other">Otro diccionario</param>
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
		/// <summary>
		/// </summary>
		public ListaPesoFloat ()
			: base ((x, y) => x + y, 0)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="modelo">Modelo.</param>
		public ListaPesoFloat (IDictionary<Tuple<T1, T2>, float> modelo)
			: base ((x, y) => x + y, 0, modelo)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="comparador">Comparador.</param>
		public ListaPesoFloat (IEqualityComparer<Tuple<T1, T2>> comparador)
			: base ((x, y) => x + y, 
			        0,
			        comparador)
		{
		}


		/// <summary>
		/// </summary>
		/// <param name="comparador1">Comparador1.</param>
		/// <param name="comparador2">Comparador2.</param>
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