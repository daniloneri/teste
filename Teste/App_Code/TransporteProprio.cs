using System;

namespace Teste
{
	public class TransporteProprio : Transporte
	{
		public TransporteProprio ()
		{
		}

		public override double calcularValorGasto (string ori, string dest)
		{
			if (tipo == "Bicicleta") {
				return 0;
			} else if (tipo == "Carro") {
				if (ori != dest) {
					return 100 * 5;
				} else {
					return 100 * 2;				
				}
			} else {
				if (ori != dest) {
					return 50 * 5;
				} else {
					return 50 * 2;				
				}
			}
		}

	}
}

