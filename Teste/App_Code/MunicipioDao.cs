using System;
using MySql.Data.MySqlClient;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Teste
{
	public class MunicipioDao
	{
		public MunicipioDao ()
		{
		}

		public static void inserir(Municipio m)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlInserir = @"insert into municipio (nome, uf) values(@nome,@uf)";
			MySqlCommand command = new MySqlCommand (sqlInserir, connection);
			command.Parameters.AddWithValue ("@nome", m.nome);
			command.Parameters.AddWithValue ("@uf", m.uf);

			command.ExecuteNonQuery ();

			m.id = command.LastInsertedId;
			conexao.fecharConexao ();
		}

		public static List<Municipio> ListarMunicipio(){
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();

			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from municipio";
			MySqlDataReader dtreader = Query.ExecuteReader();
			List<Municipio> listaDeRetorno = new List<Municipio>();

			while (dtreader.Read())
			{
				Municipio m = new Municipio();
				m.id = Convert.ToInt32(dtreader ["id"].ToString());
				m.nome = dtreader["nome"].ToString();
				m.uf = dtreader ["uf"].ToString ();

				listaDeRetorno.Add(m);
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;
		} 	
			
		public static void atualizar(Municipio m)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlAtualizar = @"update municipio set uf=@uf, nome=@nome where id=@id";
			MySqlCommand command = new MySqlCommand (sqlAtualizar, connection);
			command.Parameters.AddWithValue ("@id", m.id);
			command.Parameters.AddWithValue ("@uf", m.uf);
			command.Parameters.AddWithValue ("@nome", m.nome);

			command.ExecuteNonQuery ();

			conexao.fecharConexao ();
		}

		public static Municipio recuperar(int id){
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();
			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from municipio where id=@id";
			Query.Parameters.AddWithValue ("@id", id);
			MySqlDataReader dtreader = Query.ExecuteReader();

			Municipio m  = new Municipio ();

			while (dtreader.Read ())
			{
				m.id = Convert.ToInt32(dtreader ["id"].ToString());
				m.nome = dtreader["nome"].ToString();
				m.uf = dtreader ["uf"].ToString ();
			}
			conexao.fecharConexao ();
			return m;
		}

		public static List<Municipio> listarCidades(string uf)
		{
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();

			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select id, nome from municipios_ibge where uf=@uf";
			Query.Parameters.AddWithValue ("@uf", uf);
			MySqlDataReader dtreader = Query.ExecuteReader();
			List<Municipio> listaDeRetorno = new List<Municipio>();

			while (dtreader.Read())
			{
				Municipio m = new Municipio();
				m.id = Convert.ToInt32(dtreader ["id"].ToString());
				m.nome = dtreader["nome"].ToString();

				listaDeRetorno.Add(m);
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;
		}

	}
}

