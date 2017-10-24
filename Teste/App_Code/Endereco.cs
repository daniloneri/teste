using System;

namespace Teste
{
	public class Endereco
	{
		public Endereco ()
		{
			municipio = new Municipio ();
		}

		public long id{ get; set;}

		public string cep{ get; set;}

		public string bairro{ get; set;}

		public string logradouro{ get; set;}

		public string numero{ get; set;}

		public Municipio municipio{ get; set;}

	}
}

