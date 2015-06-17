using System;
using System.Collections;
using System.Collections.Generic;

namespace ListasExtra.Lock
{
	/// <summary>
	/// Modifica un IEnumerator dado para hacer que produzca algo al final de su iteración.
	/// </summary>
	public class LockEnumerator<T> : IEnumerator<T>
	{
		public readonly IEnumerator<T> baseEnumerator;

		public LockEnumerator (IEnumerator<T> baseEnum)
		{
			baseEnumerator = baseEnum;
		}

		public Action OnTerminate;

		#region IEnumerator implementation

		public bool MoveNext ()
		{
			bool ret = baseEnumerator.MoveNext ();
			if (!ret && OnTerminate != null)
				OnTerminate.Invoke ();
			return ret;
		}

		public void Reset ()
		{
			baseEnumerator.Reset ();
		}

		object IEnumerator.Current {
			get {
				return baseEnumerator.Current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			baseEnumerator.Dispose ();
		}

		#endregion

		#region IEnumerator implementation

		public T Current {
			get {
				return baseEnumerator.Current;
			}
		}

		#endregion
	}
}
