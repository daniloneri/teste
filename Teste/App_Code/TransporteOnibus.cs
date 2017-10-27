using System;

namespace Teste
{
	public class Onibus : Transporte
	{
		public Onibus ()
		{
		}
			
		public override double calcularValorGasto ()
		{
			return distancia * 0.40;

		}
	}
}

