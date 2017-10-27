using System;
using MySql.Data.MySqlClient;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Teste
{
	public class PessoaDao
	{
		public PessoaDao()
		{
			
		}

		public static void inserir(Pessoa p)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlInserir = @"insert into pessoa (cpf,nome,id_endereco) values(@cpf,@nome,@id_endereco)";
			MySqlCommand command = new MySqlCommand (sqlInserir, connection);
			command.Parameters.AddWithValue ("@nome", p.nome);
			command.Parameters.AddWithValue ("@cpf", p.cpf);
			command.Parameters.AddWithValue ("@id_endereco", p.endereco.id);

			command.ExecuteNonQuery ();

			p.id = command.LastInsertedId;
			conexao.fecharConexao ();
		}

		public static List<Pessoa> listar()
		{
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();
			//string sqlRecuperar =  "select * from pessoa";
			//MySqlCommand command = new MySqlCommand (sqlRecuperar, connection);

			//string CONFIG = ("Persist Security Info=False;server=localhost;database=bdcdc;uid=root;server=localhost;database=bdcdc;uid=root;pwd=''");
			//MySqlConnection Conexao = new MySqlConnection(CONFIG);
			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from pessoa order by nome";
			//Conexao.Open();//Abre conexão
			MySqlDataReader dtreader = Query.ExecuteReader();//Crie um objeto do tipo reader para ler os dados do banco
			List<Pessoa> listaDeRetorno = new List<Pessoa>();//Crie uma lista de pessoa
			//Estancia objeto do tipo cliente
		
			while (dtreader.Read())//Enquanto existir dados no select
			{
				Pessoa p = new Pessoa();
				p.id = Convert.ToInt32(dtreader ["id"].ToString());
				p.nome = dtreader["nome"].ToString();//Preencha objeto do tipo pessoa com dados vindo do banco de dados
				p.cpf = dtreader["cpf"].ToString();
				p.endereco = EnderecoDao.recuperar (Convert.ToInt32 (dtreader ["id_endereco"].ToString ()));

				listaDeRetorno.Add(p);//Adiciona na lista um objeto do tipo pessoa
			}
			conexao.fecharConexao ();//Fecha Conexao
			return listaDeRetorno;

			} 			

		public static Pessoa recuperar(int id)
		{
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();
			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from pessoa where id = @id";
			Query.Parameters.AddWithValue ("@id", id);
			MySqlDataReader dtreader = Query.ExecuteReader();

			Pessoa p = new Pessoa ();

			while (dtreader.Read ())
			{
				p.id = Convert.ToInt32(dtreader ["id"].ToString());
				p.nome = dtreader["nome"].ToString();
				p.cpf = dtreader["cpf"].ToString();
				p.endereco = EnderecoDao.recuperar (Convert.ToInt32 (dtreader ["id_endereco"].ToString ()));

			}
			conexao.fecharConexao ();
			return p ;
		}

		public static void atualizar(Pessoa p)
		{
			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlAtualizar = @"update pessoa set cpf=@cpf, nome=@nome where id=@id";
			MySqlCommand command = new MySqlCommand (sqlAtualizar, connection);
			command.Parameters.AddWithValue ("@id", p.id);
			command.Parameters.AddWithValue ("@nome", p.nome);
			command.Parameters.AddWithValue ("@cpf", p.cpf);
	
			command.ExecuteNonQuery ();

			conexao.fecharConexao ();
		}

		public static void excluir(Pessoa p){

			Conexao conexao = new Conexao();
			MySqlConnection connection = conexao.abrirConexao();
			string sqlExcluir = @"delete from pessoa where id=@id";
			MySqlCommand command = new MySqlCommand (sqlExcluir, connection);
			command.Parameters.AddWithValue ("@id", p.id);

			command.ExecuteNonQuery ();

			conexao.fecharConexao ();
		}

		public static Pessoa procurarCpf(string cpf)
		{
			Conexao conexao = new Conexao ();
			MySqlConnection connection = conexao.abrirConexao ();
			MySqlCommand Query = new MySqlCommand();
			Query.Connection = connection;
			Query.CommandText = @"select * from pessoa where cpf = @cpf";
			Query.Parameters.AddWithValue ("@cpf", cpf);
			MySqlDataReader dtreader = Query.ExecuteReader();

			Pessoa p = new Pessoa ();

			while (dtreader.Read ())
			{
				p.id = Convert.ToInt32(dtreader ["id"].ToString());
				p.nome = dtreader["nome"].ToString();
				p.cpf = dtreader["cpf"].ToString();
				p.endereco = EnderecoDao.recuperar (Convert.ToInt32 (dtreader ["id_endereco"].ToString ()));

			}
			conexao.fecharConexao ();
			return p ;
		}


	}

}		



