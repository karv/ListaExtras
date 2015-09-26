using System.Collections.Generic;

namespace ListasExtra.Set
{
	/// <summary>
	/// Representa un conjunto
	/// </summary>
	public interface ISet<T> : ICollection<T>
	{
		T Pick ();

		T PickRemove ();
	}
}