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

namespace ListasExtra
{
	public class ListaPesoBloqueable<T, V> : ListaPeso<T, V>, Lock.IListBloqueable<KeyValuePair<T, V>>
	{
		public ListaPesoBloqueable () : base ()
		{
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
					foreach (var x in Promesas) {
						this [x.Key] = x.Value;
					}
					Promesas.Clear ();

					if (OnRelease != null)
						OnRelease.Invoke (this, new EventArgs ());
				}
			}
		}

		IDictionary <T, V> Promesas = new Dictionary<T, V> ();

		public new V this [T Key] {
			set {
				if (bloqueado) {
					Promesas [Key] = value;
				} else {
					base [Key] = value; 
				}
			}
		}

		#endregion


		#region ILockable

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

		#endregion
	}
}

