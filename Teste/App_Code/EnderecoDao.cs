using System;
using MySql.Data.MySqlClient;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Teste
{
	public class EnderecoDao
	{
		public EnderecoDao ()
		{
		}

		public static void inserir(Endereco e)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlInserir = @"insert into endereco (cep,bairro,logradouro,numero,id_municipio) values(@cep,@bairro,@logradouro,@numero,@id_municipio)";
			MySqlCommand command = new MySqlCommand (sqlInserir, connection);
			command.Parameters.AddWithValue ("@cep", e.cep);
			command.Parameters.AddWithValue ("@bairro", e.bairro);
			command.Parameters.AddWithValue ("@logradouro", e.logradouro);
			command.Parameters.AddWithValue ("@numero", e.numero);
			command.Parameters.AddWithValue ("@id_municipio", e.municipio.id);

			command.ExecuteNonQuery ();

			e.id = command.LastInsertedId;
			conexao.fecharConexao ();
		}

		public static void atualizar(Endereco e)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlAtualizar = @"update endereco set cep=@cep,bairro=@bairro,logradouro=@logradouro,numero=@numero where id=@id";
			MySqlCommand command = new MySqlCommand (sqlAtualizar, connection);
			command.Parameters.AddWithValue ("@id", e.id);
			command.Parameters.AddWithValue ("@cep", e.cep);
			command.Parameters.AddWithValue ("@bairro", e.bairro);
			command.Parameters.AddWithValue ("@logradouro", e.logradouro);
			command.Parameters.AddWithValue ("@numero", e.numero);		

			command.ExecuteNonQuery ();

			conexao.fecharConexao ();
		}

		public static Endereco recuperar(int id)
		{
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();
			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from endereco where id = @id";
			Query.Parameters.AddWithValue ("@id", id);
			MySqlDataReader dtreader = Query.ExecuteReader();

			Endereco e = new Endereco ();

			while (dtreader.Read ())
			{
				e.id = Convert.ToInt32(dtreader ["id"].ToString());
				e.cep = dtreader["cep"].ToString();
				e.bairro = dtreader ["bairro"].ToString ();
				e.logradouro = dtreader["logradouro"].ToString();
				e.numero = dtreader ["numero"].ToString ();
				e.municipio = MunicipioDao.recuperar (Convert.ToInt32(dtreader ["id_municipio"].ToString()));

			}
			conexao.fecharConexao ();
			return e ;
		}

	}

}

