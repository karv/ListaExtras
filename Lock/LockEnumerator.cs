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
		public readonly IEnumerator<T> BaseEnumerator;

		public LockEnumerator (IEnumerator<T> baseEnum)
		{
			BaseEnumerator = baseEnum;
		}

		/// <summary>
		/// Ocurre al terminar de enumerar
		/// </summary>
		public event Action<object> OnTerminate;

		#region IEnumerator implementation

		public bool MoveNext ()
		{
			bool ret = BaseEnumerator.MoveNext ();
			if (!ret)
				OnTerminate?.Invoke (this);
			return ret;
		}

		public void Reset ()
		{
			BaseEnumerator.Reset ();
		}

		object IEnumerator.Current
		{
			get
			{
				return BaseEnumerator.Current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			BaseEnumerator.Dispose ();
			OnTerminate = null;
		}

		#endregion

		#region IEnumerator implementation

		public T Current
		{
			get
			{
				return BaseEnumerator.Current;
			}
		}

		#endregion
	}
}
