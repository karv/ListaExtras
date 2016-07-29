using System;
using System.Collections;

namespace ListasExtra
{
	/// <summary>
	/// Pareja ordenada lectura
	/// </summary>
	[Serializable]
	public struct ReadonlyPair<T1, T2> : IStructuralEquatable
	{
		System.Collections.Generic.KeyValuePair<T1, T2> _data;

		/// <summary>
		/// </summary>
		/// <param name="data">Data.</param>
		public ReadonlyPair (System.Collections.Generic.KeyValuePair<T1, T2> data)
		{
			_data = data;
		}

		/// <summary>
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="val">Value.</param>
		public ReadonlyPair (T1 key, T2 val)
		{
			_data = new System.Collections.Generic.KeyValuePair<T1, T2> (key, val);
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public T1 Key
		{
			get
			{
				return _data.Key;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public T2 Value
		{
			get
			{
				return _data.Value;
			}
		}

		/// <summary>
		/// Equals the specified other and comparer.
		/// </summary>
		/// <param name="other">Other.</param>
		/// <param name="comparer">Comparer.</param>
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

		/// <summary>
		/// Gets the hash code.
		/// </summary>
		/// <returns>The hash code.</returns>
		/// <param name="comparer">Comparer.</param>
		public int GetHashCode (IEqualityComparer comparer)
		{
			unchecked
			{
				return comparer.GetHashCode (Key) + 23 * comparer.GetHashCode (Value);
			}
		}
	}
}