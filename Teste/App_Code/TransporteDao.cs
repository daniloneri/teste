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
			string sqlInserir = @"insert into transporte (tipo, origem, destino, valor, id_pessoa,distancia) values(@tipo,@origem,@destino,@valor,@id_pessoa,@distancia)";
			MySqlCommand command = new MySqlCommand (sqlInserir, connection);
			command.Parameters.AddWithValue ("@tipo", t.tipo);
			command.Parameters.AddWithValue ("@origem", t.origem);
			command.Parameters.AddWithValue ("@destino", t.destino);
			command.Parameters.AddWithValue ("@valor", t.custo);
			command.Parameters.AddWithValue ("@id_pessoa", p.id);
			command.Parameters.AddWithValue ("@distancia", t.distancia);
			command.ExecuteNonQuery ();

			t.id = command.LastInsertedId;
			conexao.fecharConexao ();
		}

		public static List<TransportePessoa> ListarTransportePessoa(){
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();

			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select distinct p.nome, t.id, sum(t.valor) as Valor_Total from transporte as t
								inner join pessoa as p on p.id=t.id_pessoa
								group by p.nome";
			MySqlDataReader dtreader = Query.ExecuteReader();
			List<TransportePessoa> listaDeRetorno = new List<TransportePessoa>();

			while (dtreader.Read())
			{
				TransportePessoa t = new TransportePessoa();
				t.id = Convert.ToInt32(dtreader ["id"].ToString());
				t.nome = dtreader ["nome"].ToString ();
				t.valorTotal = Convert.ToDouble(dtreader ["Valor_Total"].ToString ());

				listaDeRetorno.Add(t);
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;
		} 	
			
		public static List<TransportePessoa> ListarDetalhes(int id){
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();

			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = 
				@"select p.nome, t.tipo as Transporte, count(t.tipo) as Num_Viagens, sum(t.distancia) as Distancia_Total, sum(t.valor) as Valor_Total from transporte as t
				inner join pessoa as p on p.id=t.id_pessoa
				where p.id = @id_pessoa
				group by t.tipo";
			Query.Parameters.AddWithValue ("@id_pessoa",id);
			MySqlDataReader dtreader = Query.ExecuteReader();
			List<TransportePessoa> listaDeRetorno = new List<TransportePessoa>();

			while (dtreader.Read())
			{
				TransportePessoa t = new TransportePessoa();
				t.nome = dtreader ["nome"].ToString();
				t.tipo = dtreader["Transporte"].ToString();
				t.numeroViagens = dtreader ["Num_Viagens"].ToString ();
				t.distanciaTotal = Convert.ToDouble(dtreader ["Distancia_Total"].ToString ());
				t.valorTotal = Convert.ToDouble(dtreader ["Valor_Total"].ToString ());

				listaDeRetorno.Add(t);
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;
		} 	
	}
}

