using System;

namespace Teste
{
	public class Aviao : Transporte
	{
		public Aviao ()
		{
		}

		public override double calcularValorGasto (string ori, string dest)
		{
			if (ori != dest) {
				return 100 * 5;
			} else {
				return 100 * 2;				
			}
		}


	}
}

