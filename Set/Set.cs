using System.Collections.Generic;

namespace ListasExtra.Set
{
	[System.Diagnostics.DebuggerDisplayAttribute ("Count = {Count}")]
	/// <summary>
	/// Representa un conjunto de elementos sin un control sobre el orden.
	/// </summary>
	/// <typeparam name="T">Tipo de objetos</typeparam>
	public class Set<T> : ISet<T>
	{
		public Set ()
		{
			_dat = new HashSet<T> ();
		}

		public Set (IEnumerable<T> inicial)
		{
			_dat = new HashSet<T> (inicial);
		}

		#region obj

		readonly HashSet<T> _dat;

		#endregion

		public T Pick ()
		{
			T ret;
			using (var en = _dat.GetEnumerator ())
				ret = en.Current;
			return ret;
		}

		public T PickRemove ()
		{
			var ret = Pick ();
			_dat.Remove (ret);
			return ret;
		}


		#region ICollection implementation

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public void Add (T item)
		{
			if (!_dat.Contains (item))
				_dat.Add (item);
		}

		public bool Remove (T item)
		{
			return _dat.Remove (item);
		}

		public void Clear ()
		{
			_dat.Clear ();
		}

		public bool Contains (T item)
		{
			return _dat.Contains (item);
		}

		public void CopyTo (T[] array, int arrayIndex)
		{
			_dat.CopyTo (array, arrayIndex);
		}

		public int Count {
			get {
				return _dat.Count;
			}
		}

		#endregion

		#region IEnumerable implementation

		public IEnumerator<T> GetEnumerator ()
		{
			return _dat.GetEnumerator ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return _dat.GetEnumerator ();
		}

		#endregion
	}
}