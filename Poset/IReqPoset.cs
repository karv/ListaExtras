using System.Collections.Generic;

namespace ListasExtra.Poset
{
	/// <summary>
	/// Representa un objeto que tiene depencencias a otros objetos de tipo dado
	/// </summary>
	public interface IReqPoset<T>
	{
		/// <summary>
		/// Objeto vinculado
		/// </summary>
		/// <value>The objeto.</value>
		T Objeto { get; }

		/// <summary>
		/// Requicitos
		/// </summary>
		/// <value>The reqs.</value>
		IDictionary <IReqPoset<T>, float> Reqs { get; }

		/// <summary>
		/// Revisa si este objeto es satisfacido por un diccionario.
		/// No debe eser recursivo
		/// </summary>
		/// <returns><c>true</c>, if satisface was loed, <c>false</c> otherwise.</returns>
		/// <param name="comparador">Comparador.</param>
		bool LoSatisface (IDictionary <T, float> comparador);

		/// <summary>
		/// Enumera los requerimientos hereditariamente
		/// </summary>
		/// <returns>The requerimientos.</returns>
		IDictionary<T, float> EnumerarRequerimientos ();
	}

}