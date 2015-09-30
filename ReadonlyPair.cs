//
//  ReadonlyPair.cs
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

namespace ListasExtra
{
	public class ReadonlyPair<T1, T2>
	{
		System.Collections.Generic.KeyValuePair<T1, T2> _data;

		public ReadonlyPair (System.Collections.Generic.KeyValuePair<T1, T2> data)
		{
			_data = data;
		}

		public T1 Key {
			get {
				return _data.Key;
			}
		}

		public T2 Value {
			get {
				return _data.Value;
			}
		}
	}
}

