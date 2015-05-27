using System;
using System.Collections.Generic;

namespace ListasExtra.Set
{
	public interface ISet<T> : ICollection<T>
	{
		T Pick ();
	}

	public static class ISetExt
	{
		public static T PickRemove<T> (this ISet<T> col)
		{
			T ret = col.Pick ();
			col.Remove (ret);
			return ret;
		}
	}
}

