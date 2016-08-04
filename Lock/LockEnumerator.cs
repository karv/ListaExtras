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
		/// <summary>
		/// Enumerador base
		/// </summary>
		public readonly IEnumerator<T> BaseEnumerator;

		/// <summary>
		/// </summary>
		/// <param name="baseEnum">Base enum.</param>
		public LockEnumerator (IEnumerator<T> baseEnum)
		{
			BaseEnumerator = baseEnum;
		}

		/// <summary>
		/// Ocurre al terminar de enumerar
		/// </summary>
		public event EventHandler OnTerminate;

		#region IEnumerator implementation

		/// <summary>
		/// Se mueve a la próxima iteración
		/// </summary>
		public bool MoveNext ()
		{
			bool ret = BaseEnumerator.MoveNext ();
			if (!ret)
				OnTerminate?.Invoke (this, EventArgs.Empty);
			return ret;
		}

		/// <Docs>The collection was modified after the enumerator was instantiated.</Docs>
		/// <see cref="M:System.Collections.IEnumerator.MoveNext"></see>
		/// <see cref="M:System.Collections.IEnumerator.Reset"></see>
		/// <see cref="T:System.InvalidOperationException"></see>
		/// <summary>
		/// Reset this instance.
		/// </summary>
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

		/// <summary>
		/// </summary>
		public void Dispose ()
		{
			BaseEnumerator.Dispose ();
			OnTerminate = null;
		}

		#endregion

		#region IEnumerator implementation

		/// <summary>
		/// Devuelve el objeto actual de iteración.
		/// </summary>
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
