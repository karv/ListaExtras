using System;

namespace ListasExtra.Set
{
	/// <summary>
	/// Ocurre cuando se intenta accesar a un objeto de una colección vacía.
	/// </summary>
	[Serializable]
	public class EmptySetException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Set.EmptySetException"/> class
		/// </summary>
		public EmptySetException ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Set.EmptySetException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public EmptySetException (string message)
			: base (message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Set.EmptySetException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public EmptySetException (string message, Exception inner)
			: base (message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ListasExtra.Set.EmptySetException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected EmptySetException (System.Runtime.Serialization.SerializationInfo info,
		                             System.Runtime.Serialization.StreamingContext context)
			: base (info,
			        context)
		{
		}
	}
}

