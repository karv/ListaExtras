﻿using System.Collections.Generic;
using System.Collections;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto
	/// </summary>
	public interface ISet<T> : ICollection<T>, IStructuralEquatable
	{
		/// <summary>
		/// Toma un elemento
		/// </summary>
		T Pick ();

		/// <summary>
		/// Toma un elemento y lo remueve
		/// </summary>
		T PickRemove ();
	}
}