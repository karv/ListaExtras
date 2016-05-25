using System.Collections.Generic;
using System.Diagnostics;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto de elementos sin un control sobre el orden.
	/// </summary>
	/// <typeparam name="T">Tipo de objetos</typeparam>
	[DebuggerDisplayAttribute ("Count = {Count}")]
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
			using (var en = GetEnumerator ())
				if (en.MoveNext ())
					return en.Current;
			throw new EmptySetException ("No se puede tomar un elemento de un conjunto vacío.");
		}

		/// <summary>
		/// Toma un elemento y lo remueve
		/// </summary>
		/// <returns>The remove.</returns>
		public T PickRemove ()
		{
			var ret = Pick ();
			Remove (ret);
			return ret;
		}
	}
}