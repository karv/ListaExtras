using System.Collections.Generic;

namespace ListasExtra.Enumerable
{
	public class SerialEnumerable<T> : IEnumerable<T>
	{
		protected IEnumerable<IEnumerable<T>> Enum;

		public SerialEnumerable (IEnumerable<IEnumerable<T>> enume)
		{
			Enum = enume;
		}

		public IEnumerator<T> GetEnumerator ()
		{
			foreach (var x in Enum) {
				foreach (var y in x) {
					yield return y;
				}
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}
	}
}