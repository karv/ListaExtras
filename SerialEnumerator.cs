using System.Collections.Generic;

namespace ListasExtra.Enumerator
{
	public class SerialEnumerator<T> : IEnumerator<T>
	{
		protected List<IEnumerator<T>> Enum;
		protected int CurrEnumIndex;

		protected IEnumerator<T> CurrEnum {
			get {
				return Enum [CurrEnumIndex];
			}
		}

		public SerialEnumerator (IEnumerable<IEnumerable<T>> enums)
		{
			Enum = new List<IEnumerator<T>> ();
			foreach (var x in enums) {
				Enum.Add (x.GetEnumerator ()); 
			}

		}

		#region IEnumerator implementation

		public bool MoveNext ()
		{
			while (CurrEnumIndex < Enum.Count && !CurrEnum.MoveNext ()) {
				CurrEnumIndex++;
			}
			return CurrEnumIndex != Enum.Count;
		}

		public void Reset ()
		{
			CurrEnumIndex = 0;
			foreach (var x in Enum) {
				x.Reset ();
			}
		}

		public object Current {
			get {
				return CurrEnum.Current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			foreach (var x in Enum) {
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

