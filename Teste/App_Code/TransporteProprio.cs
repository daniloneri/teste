using System;

namespace Teste
{
	public class TransporteProprio : Transporte
	{
		public TransporteProprio ()
		{
		}

		public override double calcularValorGasto ()
		{
			double valor = 0;
			
			switch (tipo) {
			case "Bicicleta":
				valor = 0;
				break;
			case "Onibus":
				valor = 0.40 * distancia;
				break;
			case "Carro":
				valor = 0.30 * distancia;
				break;
			case "Moto":
				valor = 0.15 * distancia;
				break;
			}
				
			return valor;
		}

	}
}

