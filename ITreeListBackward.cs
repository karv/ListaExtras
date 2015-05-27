using System;

namespace ListasExtra.Treelike
{
	public interface ITreeListBackward<T> : ITreeList<T>
	{
		ITreeList<T> pred { get; }
	}
}

