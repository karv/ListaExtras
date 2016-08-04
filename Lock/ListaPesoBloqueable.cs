using System;
using System.Collections.Generic;
using ListasExtra;

namespace ListasExtra.Lock
{
	/// <summary>
	/// Es una listapeso en el que se puede editar mientras se realiza una iteración 'foreach'.
	/// </summary>
	public class ListaPesoBloqueable<TKey, TVal> : ListaPeso<TKey, TVal>, IListBloqueable<KeyValuePair<TKey, TVal>>
	{
		/// <summary>
		/// </summary>
		/// <param name="operSuma">Oper suma.</param>
		/// <param name="objetoNulo">Objeto nulo.</param>
		public ListaPesoBloqueable (Func<TVal, TVal, TVal> operSuma, TVal objetoNulo)
			: base (operSuma, objetoNulo)
		{
		}

		/// <summary>
		/// Incorpora las promesas a la lista.
		/// </summary>
		public void Commit ()
		{
			foreach (var x in Promesas)
			{
				this [x.Key] = Suma (base [x.Key], x.Value);
			}
			Promesas.Clear ();
		}

		/// <summary>
		/// Agrega un valor
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="val">Value.</param>
		public new void Add (TKey key, TVal val)
		{
			if (Bloqueado)
				Promesas.Add (new KeyValuePair<TKey, TVal> (key, val));
			else
				base [key] = Suma (base [key], val);
		}

		#region Locking

		/// <summary>
		/// Ocurre cuando se libera el seguro.
		/// </summary>
		public event EventHandler OnRelease;

		bool _locked;

		/// <summary>
		/// Devuelve o establece el valor de bloqueo.
		/// Si es true, la enumeración no se modificará y se actualizará hasta que se libere el seguro o se invoque Commit
		/// </summary>
		public bool Bloqueado
		{
			get
			{
				return _locked;
			}
			set
			{
				bool released = _locked && !value;
				_locked = value;

				if (released)
				{
					Commit ();

					if (OnRelease != null)
						OnRelease.Invoke (this, EventArgs.Empty);
				}
			}
		}

		IList<KeyValuePair<TKey, TVal>> Promesas = new List<KeyValuePair<TKey, TVal>> ();

		/// <summary>
		/// Devuelve o establece el valor en una entrada
		/// </summary>
		/// <param name="key">Key.</param>
		public new TVal this [TKey key]
		{
			get
			{
				return base [key];
			}

			set
			{
				if (Bloqueado)
				{
					System.Diagnostics.Debug.Write ("Use ListaPesoBloqueable.Add (key, delta) en lugar de éste si es posible.");

					Promesas.Add (new KeyValuePair<TKey, TVal> (key, value));
				}
				else
				{
					base [key] = value; 
				}
			}
		}

		#endregion

		#region ILockable

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public new System.Collections.IEnumerator GetEnumerator ()
		{
			Bloqueado = true;
			var x = new  LockEnumerator<KeyValuePair<TKey, TVal>> (base.GetEnumerator ());
			x.OnTerminate += unblock;
			return x;
		}

		bool IListBloqueable<KeyValuePair<TKey, TVal>>.Bloqueado
		{
			get
			{
				return Bloqueado;
			}
			set
			{
				Bloqueado = value;
			}
		}

		IEnumerator<KeyValuePair<TKey, TVal>> IEnumerable<KeyValuePair<TKey, TVal>>.GetEnumerator ()
		{
			var x = new  LockEnumerator<KeyValuePair<TKey, TVal>> (base.GetEnumerator ());
			x.OnTerminate += unblock;
			Bloqueado = true;
			return x;
		}

		readonly List< object> _enumInstances = new List<object> ();

		void unblock (object s, EventArgs args)
		{
			_enumInstances.Remove (s);
			Bloqueado &= _enumInstances.Count != 0;
		}

		#endregion
	}

	/// <summary>
	/// Lista peso bloqueable.
	/// </summary>
	public class ListaPesoBloqueable<T> : ListaPesoBloqueable<T, float>
	{
		/// <summary>
		/// </summary>
		public ListaPesoBloqueable ()
			: base ((x, y) => x + y, 0)
		{
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Revisa si esta lista contiene como sublista a otra
		/// </summary>
		/// <param name="comparador">Una lista para comparar</param>
		public bool Contains (ListaPeso<T> comparador)
		{
			foreach (var x in comparador)
			{
				if (this [x.Key] < x.Value)
					return false;
			}
			return true;
		}
	}
}