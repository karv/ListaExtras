using System;

namespace ListasExtra.Acotados
{
	/// <summary>
	/// Representa un númerod e punto flotante que se acota de forma automática por dos números fijos
	/// </summary>
	public struct Float
	{
		float _valor;

		/// <summary>
		/// Valor actual
		/// </summary>
		/// <value>The valor.</value>
		public float Valor
		{
			get
			{
				return _valor;
			}
			set
			{
				_valor = Math.Min (Math.Max (Min, value), Max);
			}
		}

		/// <summary>
		/// Valor máximo
		/// </summary>
		/// <value>The max.</value>
		public float Max { get; }

		/// <summary>
		/// Valor mínimo
		/// </summary>
		/// <value>The minimum.</value>
		public float Min { get; }

		/// <param name="v">Valor inicial</param>
		/// <param name="min">Mínimo</param>
		/// <param name="max">Máximo</param>
		public Float (float v,
		              float min = float.NegativeInfinity,
		              float max = float.PositiveInfinity)
		{
			Min = min;
			Max = max;
			_valor = v;
		}

		public static implicit operator float (Float val)
		{
			return val.Valor;
		}
	}
}