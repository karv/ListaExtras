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
	public class ListaPesoBloqueable<T, V> : ListaPeso<T, V>, IListBloqueable<KeyValuePair<T, V>>, IEnumerable <KeyValuePair<T, V>>
	{
		public ListaPesoBloqueable (Func<V, V, V> OperSuma, V ObjetoNulo) : base (OperSuma, ObjetoNulo)
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

		public new void Add (T key, V val)
		{
			if (bloqueado)
				Promesas.Add (new KeyValuePair<T, V> (key, val));
			else {
				base [key] = Suma (base [key], val);
			}
		}

		#region Locking

		public event EventHandler OnRelease;

		bool _locked = false;

		public bool bloqueado {
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

		IList<KeyValuePair<T, V>> Promesas = new List<KeyValuePair<T, V>> ();

		public new V this [T key] {
			get {
				return base [key];
			}

			set {
				if (bloqueado) {
					System.Diagnostics.Debug.Write ("Use ListaPesoBloqueable.Add (key, delta) en lugar de éste si es posible.");

					Promesas.Add (new KeyValuePair<T, V> (key, value));
				} else {
					base [key] = value; 
				}
			}
		}

		#endregion


		#region ILockable

		public new System.Collections.IEnumerator GetEnumerator ()
		{
			bloqueado = true;
			LockEnumerator<KeyValuePair<T, V>> x = new  LockEnumerator<KeyValuePair<T, V>> (base.GetEnumerator ());
			x.OnTerminate = unblock;
			return x;
		}

		int IList<KeyValuePair<T, V>>.IndexOf (KeyValuePair<T, V> item)
		{
			throw new NotImplementedException ();
		}

		void IList<KeyValuePair<T, V>>.Insert (int index, KeyValuePair<T, V> item)
		{
			throw new NotImplementedException ();
		}

		void IList<KeyValuePair<T, V>>.RemoveAt (int index)
		{
			throw new NotImplementedException ();
		}

		KeyValuePair<T, V> IList<KeyValuePair<T, V>>.this [int index] {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		bool ListasExtra.Lock.IListBloqueable<KeyValuePair<T, V>>.bloqueado {
			get {
				return bloqueado;
			}
			set {
				bloqueado = value;
			}
		}

		IEnumerator<KeyValuePair<T, V>> IEnumerable<KeyValuePair<T, V>>.GetEnumerator ()
		{
			LockEnumerator<KeyValuePair<T, V>> x = new  LockEnumerator<KeyValuePair<T, V>> (base.GetEnumerator ());
			x.OnTerminate = unblock;
			bloqueado = true;
			return x;
		}

		List< object> _enumInstances = new List<object> ();

		void unblock (object en)
		{
			_enumInstances.Remove (en);
			if (_enumInstances.Count == 0)
				bloqueado = false;
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
