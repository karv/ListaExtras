//
//  ListaPesoLock.cs
//
//  Author:
//       Edgar Carballo <karvayoEdgar@gmail.com>
//
//  Copyright (c) 2015 edgar
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using ListasExtra;

namespace ListasExtra.Lock
{
	/// <summary>
	/// Es una listapeso en el que se puede editar mientras se realiza una iteración 'foreach'.
	/// </summary>
	public class ListaPesoBloqueable<TKey, TVal> : ListaPeso<TKey, TVal>, IListBloqueable<KeyValuePair<TKey, TVal>>, IEnumerable <KeyValuePair<TKey, TVal>>
	{
		public ListaPesoBloqueable (Func<TVal, TVal, TVal> operSuma, TVal objetoNulo) : base (operSuma, objetoNulo)
		{
		}

		/// <summary>
		/// Incorpora las promesas a la lista.
		/// </summary>
		public void Commit ()
		{
			foreach (var x in Promesas) {
				this [x.Key] = Suma (base [x.Key], x.Value);
			}
			Promesas.Clear ();
		}

		public new void Add (TKey key, TVal val)
		{
			if (Bloqueado)
				Promesas.Add (new KeyValuePair<TKey, TVal> (key, val));
			else {
				base [key] = Suma (base [key], val);
			}
		}

		#region Locking

		public event EventHandler OnRelease;

		bool _locked;

		public bool Bloqueado {
			get {
				return _locked;
			}
			set {
				bool released = _locked && !value;
				_locked = value;

				if (released) {
					Commit ();

					if (OnRelease != null)
						OnRelease.Invoke (this, new EventArgs ());
				}
			}
		}

		IList<KeyValuePair<TKey, TVal>> Promesas = new List<KeyValuePair<TKey, TVal>> ();

		public new TVal this [TKey key] {
			get {
				return base [key];
			}

			set {
				if (Bloqueado) {
					System.Diagnostics.Debug.Write ("Use ListaPesoBloqueable.Add (key, delta) en lugar de éste si es posible.");

					Promesas.Add (new KeyValuePair<TKey, TVal> (key, value));
				} else {
					base [key] = value; 
				}
			}
		}

		#endregion


		#region ILockable

		public new System.Collections.IEnumerator GetEnumerator ()
		{
			Bloqueado = true;
			var x = new  LockEnumerator<KeyValuePair<TKey, TVal>> (base.GetEnumerator ());
			x.OnTerminate = unblock;
			return x;
		}

		int IList<KeyValuePair<TKey, TVal>>.IndexOf (KeyValuePair<TKey, TVal> item)
		{
			throw new NotImplementedException ();
		}

		void IList<KeyValuePair<TKey, TVal>>.Insert (int index, KeyValuePair<TKey, TVal> item)
		{
			throw new NotImplementedException ();
		}

		void IList<KeyValuePair<TKey, TVal>>.RemoveAt (int index)
		{
			throw new NotImplementedException ();
		}

		KeyValuePair<TKey, TVal> IList<KeyValuePair<TKey, TVal>>.this [int index] {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		bool IListBloqueable<KeyValuePair<TKey, TVal>>.Bloqueado {
			get {
				return Bloqueado;
			}
			set {
				Bloqueado = value;
			}
		}

		IEnumerator<KeyValuePair<TKey, TVal>> IEnumerable<KeyValuePair<TKey, TVal>>.GetEnumerator ()
		{
			var x = new  LockEnumerator<KeyValuePair<TKey, TVal>> (base.GetEnumerator ());
			x.OnTerminate = unblock;
			Bloqueado = true;
			return x;
		}

		readonly List< object> _enumInstances = new List<object> ();

		void unblock (object en)
		{
			_enumInstances.Remove (en);
			Bloqueado &= _enumInstances.Count != 0;
		}

		#endregion
	}

	/// <summary>
	/// Lista peso bloqueable.
	/// </summary>
	public class ListaPesoBloqueable<T> : ListaPesoBloqueable<T, float>
	{
		public ListaPesoBloqueable () : base ((x, y) => x + y, 0)
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
			foreach (var x in comparador) {
				if (this [x.Key] < x.Value)
					return false;
			}
			return true;
		}


	}
}
