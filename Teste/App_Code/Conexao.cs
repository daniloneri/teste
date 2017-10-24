using System;
using MySql.Data.MySqlClient;


namespace Teste
{
	public class Conexao
	{
		
		private readonly string USER="root";
		private readonly string PASSWORD="logisk";
		private readonly string HOST="localhost";
		private readonly string DATABASE="db_teste";
		private MySqlConnection connection;


		public Conexao()
		{
			
		}

		public MySqlConnection abrirConexao() {

			string conexao = string.Format("Server={0};Database={1};Uid={2};Pwd={3}",HOST,DATABASE,USER,PASSWORD);
			connection = new MySqlConnection(conexao);
			connection.Open ();

			return connection;
		}


		public void fecharConexao() {

			connection.Close();
		}
	}
}

	
