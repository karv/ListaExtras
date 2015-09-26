using System.Collections.Generic;

namespace ListasExtra.Enumerator
{
	public class SerialEnumerator<T> : IEnumerator<T>
	{
		protected List<IEnumerator<T>> _enum;
		protected int currEnumIndex;

		protected IEnumerator<T> currEnum {
			get {
				return _enum [currEnumIndex];
			}
		}

		public SerialEnumerator (IEnumerable<IEnumerable<T>> enums)
		{
			_enum = new List<IEnumerator<T>> ();
			foreach (var x in enums) {
				_enum.Add (x.GetEnumerator ()); 
			}

		}

		#region IEnumerator implementation

		public bool MoveNext ()
		{
			while (currEnumIndex < _enum.Count && !currEnum.MoveNext ()) {
				currEnumIndex++;
			}
			return currEnumIndex != _enum.Count;
		}

		public void Reset ()
		{
			currEnumIndex = 0;
			foreach (var x in _enum) {
				x.Reset ();
			}
		}

		public object Current {
			get {
				return currEnum.Current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			foreach (var x in _enum) {
				x.Dispose ();
			}
		}

		#endregion

		#region IEnumerator implementation

		T IEnumerator<T>.Current {
			get {
				return (T)Current;
			}
		}

		#endregion
	}
}

