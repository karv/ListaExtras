using System;

namespace ListasExtra
{
	[Serializable]
	public class ReadonlyPair<T1, T2>
	{
		System.Collections.Generic.KeyValuePair<T1, T2> _data;

		public ReadonlyPair (System.Collections.Generic.KeyValuePair<T1, T2> data)
		{
			_data = data;
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
	}
}