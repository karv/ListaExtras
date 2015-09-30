//
//  ILockableEnumerator.cs
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
using System.Collections.Generic;

namespace ListasExtra.Lock
{
	/// <summary>
	/// Es un enumerator que puede bloquear su modificación temporalmente.
	/// útil para multithreading
	/// </summary>
	public interface IListBloqueable<T> : IList<T>
	{
		/// <summary>
		/// Devuelve o establece si el enumerador está bloqueado.
		/// </summary>
		/// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
		bool Bloqueado { get; set; }
	}
}

