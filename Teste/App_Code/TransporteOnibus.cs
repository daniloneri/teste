using System;

namespace Teste
{
	public class Onibus : Transporte
	{
		public Onibus ()
		{
		}

		public override double calcularValorGasto (string ori, string dest)
		{
			if (ori != dest) {
				return 70 * 5;
			} else {
				return 70 * 2;				
			}
		}
	}
}

