using System;
using System.Collections.Generic;

namespace ListasExtra.Treelike
{
	public interface ITreeListBackward<T> : ITreeList<T>
	{
		ITreeList<T> pred { get; }

		new IEnumerable<ITreeListBackward<T>> succ { get; }
	}
}

