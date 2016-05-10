using System.Collections.Generic;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto de elementos sin un control sobre el orden.
	/// </summary>
	/// <typeparam name="T">Tipo de objetos</typeparam>
	[System.Diagnostics.DebuggerDisplayAttribute ("Count = {Count}")]
	public class Set<T> : HashSet<T>, ISet<T>
	{
		/// <summary>
		/// Initializes a new instance of this class.
		/// </summary>
		public Set ()
		{
			_dat = new HashSet<T> ();
		}

		/// <param name="inicial">Inicial.</param>
		public Set (IEnumerable<T> inicial)
		{
			_dat = new HashSet<T> (inicial);
		}

		readonly HashSet<T> _dat;

		/// <summary>
		/// Toma un elemento
		/// </summary>
		public T Pick ()
		{
			T ret;
			using (var en = _dat.GetEnumerator ())
				ret = en.Current;
			return ret;
		}

		/// <summary>
		/// Toma un elemento y lo remueve
		/// </summary>
		/// <returns>The remove.</returns>
		public T PickRemove ()
		{
			var ret = Pick ();
			_dat.Remove (ret);
			return ret;
		}
	}
}