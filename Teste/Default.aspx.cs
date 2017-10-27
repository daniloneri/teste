using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace Teste
{
	
	public partial class Default : System.Web.UI.Page
	{
		
		protected void Page_Load(object sender, EventArgs args)
		{
			atualizarTabela ();

			if (!Page.IsPostBack) {
				listarPessoa ();
				listarCidadeOrigem ();
				listarCidadeDestino ();

			}
		}

		protected void Page_PreRender(object sender, EventArgs e){
			foreach (ListItem it in tabs.Items) {
				if (multiview.ActiveViewIndex.ToString () == it.Value)
					it.Attributes ["class"] = "atual";
				else
					it.Attributes.Remove ("class");
				if (it.Enabled)
					it.Attributes.Remove ("style");
				else
					it.Attributes ["style"] = "display: none;";
			}
			atualizarTabela ();
		}

		protected void tabela_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int idPessoa = Convert.ToInt32 (e.CommandArgument.ToString());
			int id = Convert.ToInt32(tabela.Rows [idPessoa].Cells [0].Text);

			Pessoa p = PessoaDao.recuperar (id);

			if (e.CommandName.Equals("Editar"))
			{
				
				cancelar.Visible = true;
				check.Visible = false;

				this.id.Value = id.ToString();
				nome.Text = p.nome;
				cpf.Text = p.cpf;
				rua.Text = p.endereco.logradouro;
				bairro.Text = p.endereco.bairro;
				numero.Text = p.endereco.numero;
				cidade.Text = p.endereco.municipio.nome;
				cep.Text = p.endereco.cep;
				uf.Text = p.endereco.municipio.uf;

			}

			if (e.CommandName.Equals ("Excluir")) 
			{
				PessoaDao.excluir (p);
				atualizarTabela ();
			}
				
		}

		public void atualizarTabela ()
		{
			tabela.DataSource = PessoaDao.listar ();
			tabela.DataBind ();
			tabelaTransporte.DataSource = TransporteDao.ListarTransportePessoa ();
			tabelaTransporte.DataBind ();
		}


		public void btnCadastrar (object sender, EventArgs args)
		{	
			Pessoa p = new Pessoa ();
			Endereco e = new Endereco ();
			Municipio m = new Municipio ();

			if(string.IsNullOrEmpty(this.id.Value))
			{
				p.nome = nome.Text;
				p.cpf = limparFormatação(cpf.Text);

				e.cep = cep.Text;
				e.bairro = bairro.Text;
				e.logradouro = rua.Text;
				e.numero = numero.Text;

				m.nome = cidade.Text;
				m.uf = uf.Text;

				e.municipio = m;
				p.endereco = e;


				if (PessoaDao.procurarCpf (p.cpf).cpf != null && PessoaDao.procurarCpf (p.cpf).id != p.id) {
					ClientScript.RegisterStartupScript (this.GetType (), "Alerta", "Mensagem()", true);
					return;
				}

				MunicipioDao.inserir (m);
				EnderecoDao.inserir (e);
				PessoaDao.inserir (p);

				limparPagina ();

			} 
			else 
			{

				p = PessoaDao.recuperar (Convert.ToInt32(this.id.Value));
				e = EnderecoDao.recuperar (Convert.ToInt32 (p.endereco.id));
				m = MunicipioDao.recuperar (Convert.ToInt32 (e.municipio.id));
				p.nome = nome.Text;
				p.cpf = limparFormatação(cpf.Text);
				m.nome = cidade.Text;
				m.uf = uf.Text;
				e.cep = cep.Text;
				e.bairro = bairro.Text;
				e.logradouro = rua.Text;
				e.numero = numero.Text;
				e.municipio = m;
				p.endereco = e;

				if (PessoaDao.procurarCpf (p.cpf).cpf != null && PessoaDao.procurarCpf (p.cpf).id != p.id) {
					ClientScript.RegisterStartupScript (this.GetType (), "Alerta", "<script>alert('CPF já cadastrado!');</script>");	
					return;
				}
	
				MunicipioDao.atualizar(m);
				EnderecoDao.atualizar (e);
				PessoaDao.atualizar (p);

				limparPagina ();
			}
				
		}

		public void btnCancelar(object sender, EventArgs args)
		{
			limparPagina ();			
		}

		protected void mudarEstadoOrigem(object sender, EventArgs args)
		{
			listarCidadeOrigem ();
		}

		public void listarCidadeOrigem()
		{
			dropCidOrigem.DataSource = MunicipioDao.listarCidades (dropEstOrigem.SelectedValue);
			dropCidOrigem.DataBind ();
		}

		protected void mudarEstadoDestino(object sender, EventArgs args)
		{
			listarCidadeDestino ();
		}

		public void listarCidadeDestino()
		{
			dropCidDestino.DataSource = MunicipioDao.listarCidades (dropEstDestino.SelectedValue);
			dropCidDestino.DataBind ();

		}

		public void listarPessoa(){
			List<Pessoa> lista = PessoaDao.listar ();
			Pessoa p = new Pessoa ();
			p.id = 0;
			p.nome = "Selecione uma Pessoa";
			lista.Add (p);
			dropPessoa.DataSource = lista;
			dropPessoa.DataBind ();
			dropPessoa.SelectedValue = p.id.ToString();
		}

		public void limparPagina()
		{
			Response.Redirect (Request.Path);
		}

		public string limparFormatação(string cpf){
			cpf = cpf.Replace(".","").Replace("-","");
			return cpf;
		}

		public void btnCadastrarTransporte (object sender, EventArgs args)
		{
			Transporte t;

			Pessoa p = PessoaDao.recuperar (Convert.ToInt32 (dropPessoa.SelectedValue));

			t = Transporte.criarInstancia (listaTransporte.SelectedValue);

			t.distancia = Convert.ToInt32(distancia.Text);
			t.tipo = listaTransporte.SelectedItem.Text;
			t.custo = t.calcularValorGasto ();
			t.origem = dropCidOrigem.Text + "(" + dropEstOrigem.SelectedValue + ")";
			t.destino = dropCidDestino.Text + "(" + dropEstDestino.SelectedValue + ")";
			TransporteDao.inserir (t, p);
		}

		public void btnCancelarTransporte (object sender, EventArgs args)
		{
			limparPagina ();
		}

		protected void BulletedList_Click(object sender, BulletedListEventArgs e)
		{
			multiview.ActiveViewIndex = Convert.ToInt32(tabs.Items[e.Index].Value);
		}

		protected void RadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listaTransporte.SelectedValue == "aviao") {
				lblDistancia.Text = "Milhas";
			} else {
				lblDistancia.Text = "Km";
			}
			panelDistancia.Visible = true;

		}

		protected void tabelaTransporte_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int n = Convert.ToInt32 (e.CommandArgument.ToString());
			string nome = Server.HtmlDecode(tabelaTransporte.Rows [n].Cells [0].Text);

			if(e.CommandName.Equals("Detalhes"))
			{
				panel.Visible = true;
				tabelaDetalhes.DataSource = TransporteDao.ListarDetalhes(nome);
				tabelaDetalhes.DataBind ();
			}
		}


		protected void btnVoltar_Click(object sender, EventArgs e)
		{
			panel.Visible = false;
		}

	}
}

