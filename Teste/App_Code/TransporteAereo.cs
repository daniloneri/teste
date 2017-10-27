using System;

namespace Teste
{
	public class Aviao : Transporte
	{
		public Aviao ()
		{
		}

		public override double calcularValorGasto ()
		{
			return distancia * 0.50;
		
		}


	}
}

