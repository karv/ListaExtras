using System;
using System.Collections;

namespace ListasExtra
{
	[Serializable]
	public struct ReadonlyPair<T1, T2> : IStructuralEquatable
	{
		System.Collections.Generic.KeyValuePair<T1, T2> _data;

		public ReadonlyPair (System.Collections.Generic.KeyValuePair<T1, T2> data)
		{
			_data = data;
		}

		public ReadonlyPair (T1 key, T2 val)
		{
			_data = new System.Collections.Generic.KeyValuePair<T1, T2> (key, val);
		}

		public T1 Key
		{
			get
			{
				return _data.Key;
			}
		}

		public T2 Value
		{
			get
			{
				return _data.Value;
			}
		}

		public bool Equals (object other, IEqualityComparer comparer)
		{
			if (other is ReadonlyPair<object, object>)
			{
				var otro = (ReadonlyPair<object, object>)other;
				if (comparer.GetHashCode (this) == comparer.GetHashCode (otro))
					return comparer.Equals (Key, otro.Key) && comparer.Equals (
						Value,
						otro.Value);
				return false;
			}
			return false;
		}

		public int GetHashCode (IEqualityComparer comparer)
		{
			unchecked
			{
				return comparer.GetHashCode (Key) + 23 * comparer.GetHashCode (Value);
			}
		}
	}
}