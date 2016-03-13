using System;

namespace ListasExtra.Acotados
{
	public struct Float
	{
		float _valor;

		public float Valor {
			get {
				return _valor;
			}
			set {
				_valor = Math.Min (Math.Max (Min, value), Max);
			}
		}

		public float Max { get; }

		public float Min { get; }

		public Float (float v, float min = float.NegativeInfinity, float max = float.PositiveInfinity)
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