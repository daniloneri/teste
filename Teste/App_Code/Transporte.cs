using System;

namespace Teste
{
	public  class Transporte
	{
		public Transporte ()
		{
			
		}
		public static Transporte criarInstancia(string tipo){
			Transporte t = null;
			switch (tipo) {
			case "aviao":
				t = new Aviao ();
				break;

			case "onibus":
				t = new Onibus ();
				break;

			case "carro":
				t = new TransporteProprio ();	
				break;

			case "moto":
				t = new TransporteProprio ();
				break;

			case "bicicleta":
				t = new TransporteProprio ();
				break;
			
			
			}
			return t;
		}

		public string descricaoCliente{ get; set;}

		public long id{ get; set;}

		public string tipo { get; set;}

		public double custo { get; set;}

		public double distancia { get; set;}

		public string origem { get; set;}

		public string destino { get; set;}

		public virtual double calcularValorGasto(){
			return 0;
		}
						
	}
}

