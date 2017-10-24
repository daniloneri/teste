using System;

namespace Teste
{
	public class Pessoa
	{
		public long id{ get; set;}

		public string nome{ get; set;}

		public string cpf{ get; set;}

		public Endereco endereco{ get; set;}

		public string descricaoEnderecoBairro{ get{
				return endereco.bairro;
			}}

		public string descricaoEnderecoRua{ get{
				return endereco.logradouro;
			}}
		
		public string descricaoEnderecoNumero{ get{
				return endereco.numero;
			}}
		
		public string descricaoEnderecoCep{ get{
				return endereco.cep;
			}}
		
		public string descricaoMunicipioNome{ get{
				return endereco.municipio.nome;
			}}
		
		public string descricaoMunicipioUf{ get{
				return endereco.municipio.uf;
			}}

		public Pessoa ()
		{
			endereco = new Endereco ();
		}
	}
}

