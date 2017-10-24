using System;

namespace Teste
{
	public  class Transporte
	{
		public Transporte ()
		{
			
		}

		public string descricaoCliente{ get; set;}

		public long id{ get; set;}

		public string tipo { get; set;}

		public double custo { get; set;}

		public string origem { get; set;}

		public string destino { get; set;}

		public virtual double calcularValorGasto(string origem, string destino){
			return 0;
		}
			
	
				
	}
}

