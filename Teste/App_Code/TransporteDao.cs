using System;
using MySql.Data.MySqlClient;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Teste
{
	public class TransporteDao 
	{
		public TransporteDao ()
		{
		}


		public static void inserir(Transporte t, Pessoa p)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlInserir = @"insert into transporte (tipo, origem, destino, valor, id_pessoa) values(@tipo,@origem,@destino,@valor,@id_pessoa)";
			MySqlCommand command = new MySqlCommand (sqlInserir, connection);
			command.Parameters.AddWithValue ("@tipo", t.tipo);
			command.Parameters.AddWithValue ("@origem", t.origem);
			command.Parameters.AddWithValue ("@destino", t.destino);
			command.Parameters.AddWithValue ("@valor", t.custo);
			command.Parameters.AddWithValue ("@id_pessoa", p.id);
			command.ExecuteNonQuery ();

			t.id = command.LastInsertedId;
			conexao.fecharConexao ();
		}

		public static List<Transporte> ListarTransporte(){
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();

			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from transporte";
			MySqlDataReader dtreader = Query.ExecuteReader();
			List<Transporte> listaDeRetorno = new List<Transporte>();

			while (dtreader.Read())
			{
				Transporte t = new Transporte();
				t.id = Convert.ToInt32(dtreader ["id"].ToString());
				t.tipo = dtreader["tipo"].ToString();
				t.origem = dtreader ["origem"].ToString ();
				t.destino = dtreader ["destino"].ToString ();
				t.custo = Convert.ToInt32(dtreader ["valor"].ToString ());
				t.descricaoCliente = PessoaDao.recuperar (Convert.ToInt32 (dtreader ["id_pessoa"].ToString ())).nome;

				listaDeRetorno.Add(t);
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;
		} 	
	}
}

