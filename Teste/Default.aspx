	<%@ Page Language="C#" Inherits="Teste.Default" %>

	<!DOCTYPE html>
	<html>
	<head runat="server">
		<meta charset="utf-8"/>
		<title>Cadastro</title>
		<link rel="stylesheet" type="text/css" href="Semantic/semantic.min.css">
		<link rel="stylesheet" type="text/css" href="css/stilo.css">
		<script	src="https://code.jquery.com/jquery-3.1.1.min.js"
	  		integrity="sha256-hVVnYaiADRTO2PzUGmuLJr8BLUSjGIZsDYGmIJLv2b8="
	  		crossorigin="anonymous"></script>
	  		<script src="scripts/dist/jquery.maskedinput.min.js"></script>
	  		<script src="Semantic/semantic.min.js"></script>
	  		<script src="App_Code/Mensagem.js"></script>
	</head>
	<body>
		<div class="ui container" id="abas">
	 		<form class="ui form segment" runat="server">	
				<asp:BulletedList CssClass="ui top attached tabular menu" DisplayMode="LinkButton" OnClick="BulletedList_Click" id="tabs" runat="server">
					<asp:ListItem id="cad" value="0">Cadastro</asp:ListItem>
					<asp:ListItem id="pessoas" value="1">Pessoas Cadastradas</asp:ListItem>
					<asp:ListItem id="transporte" value="2">Transporte</asp:ListItem>
				</asp:BulletedList>

				<asp:MultiView id="multiview" runat="server" ActiveViewIndex="0">

				<div class="ui bottom attached active tab segment">

					<asp:View id="aba1" runat="server">
						 <!-- Cadastro !-->
				 
				  	<div class="field">
						<asp:HiddenField id="id" runat="server"/>
					</div>
					<div class="required two fields">						
	  					<div class="field">	  						
	    					<label for="nome">NOME</label>
	    					<asp:TextBox name="nome" data-validate="nomevazio" id="nome" placeholder="Nome" runat="server" />
	  					</div>
	 					<div class="field">
	    					<label for="cpf">CPF</label>
	    					<asp:TextBox name="cpf" data-validate="cpfvazio" id="cpf" placeholder="CPF" runat="server" />
	  					</div>
	  				</div>	   
    				<div class="required fields">     
						<div class="two wide field">
							<label>CEP</label>
							<asp:TextBox name="cep" data-validate="cepvazio" id="cep" placeholder="CEP" runat="server" />
						</div>
    					<div class="three wide field">
    						<label>CIDADE</label>
       						<asp:TextBox name="cidade" data-validate="cidadevazio" id="cidade" placeholder="Cidade" runat="server" />
       					</div>
      					<div class="three wide field">
       						<label>BAIRRO</label>
        					<asp:TextBox name="bairro" data-validate="bairrovazio" id="bairro" placeholder="Bairro" runat="server" />
      					</div>
      					<div class="four wide field">
       						<label>RUA</label>
        					<asp:TextBox name="rua" data-validate="ruavazio" id="rua" placeholder="Rua" runat="server" />
      					</div>
      					<div class="two wide field">
      						<label>UF</label>
    						<asp:TextBox name="uf" data-validate="ufvazio" id="uf" placeholder="Estado" runat="server"/>
      					</div>
      					<div class="two wide field">  
      						<label>NÚMERO</label>    	
        					<asp:TextBox name="numero" data-validate="numerovazio" id="numero" placeholder="Número" runat="server" />
      					</div>
    				</div> 
    				<asp:Panel id="termos" runat="server" Visible="true">
	  				<div class="required inline field">
	    				<div class="ui checkbox" >
	      					<asp:CheckBox id="check" runat="server"/>
	      					<label>Eu aceito os termos e condições</label>
	    				</div>
	  				</div>
	  				</asp:Panel>

	  				<div class="ui hidden divider"></div>

	  			<asp:Button class="ui primary button" id="cadastrar" Text="Enviar" OnClick="btnCadastrar" runat="server"/>  		
	  			<asp:LinkButton class="ui red button" id="cancelar" Text="Cancelar" OnClick="btnCancelar" runat="server"/>  	
	  			</asp:View>

				<asp:View id="aba2" runat="server">
					  <!-- Tabela !-->
				 
				  <asp:GridView CssClass="ui celled striped table" GridLines="None" id="tabela" runat="server" AutoGenerateColumns="false" OnRowCommand="tabela_RowCommand">
	  				<Columns>   
	  					<asp:BoundField DataField="id" HeaderText="id" Visible="false" />     
            			<asp:BoundField DataField="nome" HeaderText="Nome" />
           				<asp:BoundField DataField="cpf" HeaderText="CPF" />
           				<asp:BoundField DataField="descricaoEnderecoCep" HeaderText="CEP" /> 
           				<asp:BoundField DataField="descricaoMunicipioNome" HeaderText="Cidade" /> 
           				<asp:BoundField DataField="descricaoEnderecoBairro" HeaderText="Bairro" /> 
           				<asp:BoundField DataField="descricaoMunicipioUf" HeaderText="Estado" /> 
           				<asp:BoundField DataField="descricaoEnderecoRua" HeaderText="Rua" /> 
           				<asp:BoundField DataField="descricaoEnderecoNumero" HeaderText="Numero" />
           				 <asp:TemplateField>           				
                			<ItemTemplate>
                    			<asp:LinkButton id="edit" runat="server" data-tooltip="Editar" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Editar"><i class="large edit icon" ></i> </asp:LinkButton> 
                    			<asp:LinkButton OnClientClick="return confirm('Confirmar exclusão?');" data-tooltip="Excluir" id="del" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Excluir"><i class="large trash outline icon"></i> </asp:LinkButton>
                   			</ItemTemplate>
                			<ItemStyle CssClass="ui center aligned"/>
            			</asp:TemplateField>         			
          			</Columns>
    			</asp:GridView> 

				</asp:View>

				<asp:View id="aba3" runat="server">
					<!-- Transporte !-->
			
				<div class="required two fields">
					<div class="field">
						<label>Nome</label>					
						<asp:DropDownList CssClass="ui dropdown search" id="dropPessoa" DataTextField="nome" DataValueField="id" runat="server"/> 
					</div>
					<div class="field">
						<label>Tipo de Transporte</label>
						<asp:RadioButtonList CssClass="radioButtonList" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal" id="listaTransporte" runat="server">
							<asp:ListItem id="aviao" Value="aviao">Avião</asp:ListItem>
							<asp:ListItem id="onibus" Value="onibus">Ônibus</asp:ListItem>
							<asp:ListItem id="carro" Value="carro">Carro</asp:ListItem>
							<asp:ListItem id="moto" Value="moto">Moto</asp:ListItem>
							<asp:ListItem id="bicicleta" Value="bicicleta">Bicicleta</asp:ListItem>
						</asp:RadioButtonList>
					</div> 				
				</div>

				<asp:Panel id="panelDistancia" runat="server" Visible="false">	  
					<div class="two fields">
						<div class="field">
							<asp:TextBox Visible="false"/>
						</div>
						<div class="field">
			    			<asp:Label id="lblDistancia" runat="server"></asp:Label>
			    			<asp:TextBox id="distancia" placeholder="Distância" runat="server" />
		    			</div>
	    			</div>	  			
	    		</asp:Panel>
	    	
				<div class="two fields">
					<div class="field">
						<h3 class="ui header">Origem</h3>
					</div>
					<div class="field">
						<h3 class="ui header">Destino</h3>
					</div>
				</div>			

				<div class="required fields">					
					<div class="four wide field">						
						<label>Estado</label>
						<asp:DropDownList CssClass="ui dropdown search" OnSelectedIndexChanged="mudarEstadoOrigem" AutoPostBack="true" id="dropEstOrigem" runat="server">
							<asp:ListItem Text="Acre(AC)" Value="AC" />
							<asp:ListItem Text="Alagoas(AL)" Value="AL"/>
							<asp:ListItem Text="Amapá(AP)" Value="AP"/>
							<asp:ListItem Text="Amazonas(AM)" Value="AM"/>
							<asp:ListItem Text="Bahia(BA)" Value="BA"/>
							<asp:ListItem Text="Ceará(CE)" Value="CE"/>
							<asp:ListItem Text="Distrito Federal(DF)" Value="DF"/>
							<asp:ListItem Text="Espírito Santo(ES)" Value="ES"/>
							<asp:ListItem Text="Goiás(GO)" Value="GO"/>
							<asp:ListItem Text="Maranhão(MA)" Value="MA"/>
							<asp:ListItem Text="Mato Grosso(MT)" Value="MT"/>
							<asp:ListItem Text="Mato Grosso do Sul(MS)" Value="MS"/>
							<asp:ListItem Text="Minas Gerais(MG)" Value="MG"/>
							<asp:ListItem Text="Pará(PA)" Value="PA"/>
							<asp:ListItem Text="Paraíba(PB)" Value="PB"/>
							<asp:ListItem Text="Paraná(PR)" Value="PR"/>
							<asp:ListItem Text="Pernambuco(PE)" Value="PE"/>
							<asp:ListItem Text="Piauí(PI)" Value="PI"/>
							<asp:ListItem Text="Rio de Janeiro(RJ)" Value="RJ"/>
							<asp:ListItem Text="Rio Grande do Norte(RN)" Value="RN"/>
							<asp:ListItem Text="Rio Grande do Sul(RS)" Value="RS"/>
							<asp:ListItem Text="Rondônia(RO)" Value="RO"/>
							<asp:ListItem Text="Roraima(RR)" Value="RR"/>
							<asp:ListItem Text="Santa Catarina(SC)" Value="SC"/>
							<asp:ListItem Text="São Paulo(SP)" Value="SP"/>
							<asp:ListItem Text="Sergipe(SE)" Value="SE"/>
							<asp:ListItem Text="Tocantins(TO)" Value="TO"/>
						</asp:DropDownList>
					</div>
					<div class="four wide field">
						<label>Cidade</label>
						<asp:DropDownList CssClass="ui dropdown search" id="dropCidOrigem" DataTextField="nome" DataValueField="nome" runat="server"/>
					</div>

					<div class="four wide field">
						<label>Estado</label>
						<asp:DropDownList CssClass="ui dropdown search" id="dropEstDestino" OnSelectedIndexChanged="mudarEstadoDestino" AutoPostBack="true" runat="server">
							<asp:ListItem Text="Acre(AC)" Value="AC"/>
							<asp:ListItem Text="Alagoas(AL)" Value="AL"/>
							<asp:ListItem Text="Amapá(AP)" Value="AP"/>
							<asp:ListItem Text="Amazonas(AM)" Value="AM"/>
							<asp:ListItem Text="Bahia(BA)" Value="BA"/>
							<asp:ListItem Text="Ceará(CE)" Value="CE"/>
							<asp:ListItem Text="Distrito Federal(DF)" Value="DF"/>
							<asp:ListItem Text="Espírito Santo(ES)" Value="ES"/>
							<asp:ListItem Text="Goiás(GO)" Value="GO"/>
							<asp:ListItem Text="Maranhão(MA)" Value="MA"/>
							<asp:ListItem Text="Mato Grosso(MT)" Value="MT"/>
							<asp:ListItem Text="Mato Grosso do Sul(MS)" Value="MS"/>
							<asp:ListItem Text="Minas Gerais(MG)" Value="MG"/>
							<asp:ListItem Text="Pará(PA)" Value="PA"/>
							<asp:ListItem Text="Paraíba(PB)" Value="PB"/>
							<asp:ListItem Text="Paraná(PR)" Value="PR"/>
							<asp:ListItem Text="Pernambuco(PE)" Value="PE"/>
							<asp:ListItem Text="Piauí(PI)" Value="PI"/>
							<asp:ListItem Text="Rio de Janeiro(RJ)" Value="RJ"/>
							<asp:ListItem Text="Rio Grande do Norte(RN)" Value="RN"/>
							<asp:ListItem Text="Rio Grande do Sul(RS)" Value="RS"/>
							<asp:ListItem Text="Rondônia(RO)" Value="RO"/>
							<asp:ListItem Text="Roraima(RR)" Value="RR"/>
							<asp:ListItem Text="Santa Catarina(SC)" Value="SC"/>
							<asp:ListItem Text="São Paulo(SP)" Value="SP"/>
							<asp:ListItem Text="Sergipe(SE)" Value="SE"/>
							<asp:ListItem Text="Tocantins(TO)" Value="TO"/>
						</asp:DropDownList>
					</div>
					<div class="four wide field">
						<label>Cidade</label>
						<asp:DropDownList CssClass="ui dropdown search" id="dropCidDestino" DataTextField="nome" DataValueField="nome" runat="server"/>
					</div>
				</div>
				<asp:LinkButton class="ui primary button" id="cadastrarTransporte" Text="Enviar" OnClick="btnCadastrarTransporte" runat="server"/>
	  			<div class="ui red reset button">Limpar</div>

				<div class="ui hidden divider"></div>

	  			 <asp:GridView CssClass="ui celled striped table" GridLines="None" id="tabelaTransporte" runat="server" AutoGenerateColumns="false" OnRowCommand="tabelaTransporte_RowCommand" >
	  				<Columns> 
	  					<asp:BoundField DataField="id" HeaderText="id" Visible="false"/>
	  					<asp:BoundField DataField="nome" HeaderText="Nome"/>
					    <asp:BoundField DataField="valorTotal" DataFormatString="{0:C2}" HeaderText="Gastos Totais"/>
           				<asp:TemplateField>           				
                			<ItemTemplate>
                    			<asp:LinkButton id="detalheTransporte" runat="server" data-tooltip="Detalhes" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Detalhes"><i class=" large file text outline icon"></i></asp:LinkButton>
                   			</ItemTemplate>
                			<ItemStyle CssClass="ui center aligned"/>
            			</asp:TemplateField>         			
          			</Columns>
    			</asp:GridView>	

    			<asp:ScriptManager runat="server" ID="js1" EnableScriptGlobalization="true" EnableScriptLocalization="true" EnablePartialRendering="true">
                </asp:ScriptManager> 	

    			<asp:UpdatePanel id="upPanel" runat="server">
    				<ContentTemplate>
    					<asp:Panel id="panel" runat="server" Visible="false">
    						<div class="ui dimmer modals page transition visible active" style="display: block !important;">
                                 <div class="ui modal transition visible active scrolling" style="display: block !important; top: 0px;">
                                	<asp:GridView CssClass="ui celled striped table" GridLines="None" id="tabelaDetalhes" runat="server" AutoGenerateColumns="false" >
	  									<Columns>	  					
					           				<asp:BoundField DataField="tipo" HeaderText="Tipo de Transporte"/>
					           				<asp:BoundField DataField="numeroViagens" HeaderText="Número de Viagens"/> 
					           				<asp:BoundField DataField="distanciaTotal" HeaderText="Distância Total"/> 
					           				<asp:BoundField DataField="valorTotal" DataFormatString="{0:C2}" HeaderText="Valor Total"/>
					          			</Columns>
    								</asp:GridView>
    								<div class="ui hidden divider"></div>		   
    								<asp:Button Text="Voltar" CssClass="ui inverted green button" id="btnVoltar" OnClick="btnVoltar_Click" runat="server"/>
    							</div>
    						</div>
    					</asp:Panel>
    			</ContentTemplate>

    			</asp:UpdatePanel>
    					    					    					    					    			
				</asp:View>
				</div>
				</asp:MultiView>

			</form>

		</div>		
	
	</body>

	<script>$('.ui.checkbox').checkbox();</script>

	<script>
	//Controle de abas
    $(".tabs").addClass('ui top attached tabular stackable menu');
    $("#abas li[class!='atual'] a").addClass('item');
    $("#abas li[class='atual'] a").addClass('item azul active');
    </script>

	<script>
	$('.ui.form')
	.form({
	inline: 'true',
    on: 'blur',
    fields: {
      nomevazio: {
        identifier  : 'nomevazio',
        rules: [
          {
            type   : 'empty',
            prompt : 'Informe o nome'
          }
        ]
      },
	  cpfvazio: {
	       identifier  : 'cpfvazio',
	       rules: [
	         {
	           type   : 'empty',
	           prompt : 'Informe o CPF'
	         }
	       ]
	  },
	  dropdown: {
        identifier  : 'dropdown',
        rules: [
          {
            type   : 'empty',
            prompt : 'Please select a dropdown value'
          }
        ]
      },
	  bairrovazio: {
	       identifier  : 'bairrovazio',
	       rules: [
	         {
	           type   : 'empty',
	           prompt : 'Informe o bairro'
	         }
	       ]
	  },
	 
	  ruavazio: {
	        identifier  : 'ruavazio',
	        rules: [
	          {
	            type   : 'empty',
	            prompt : 'Informe a rua'
	          }
	        ]
	      },
	  numero: {
        	identifier  : 'numerovazio',
       		rules: [
          		{
            		type   : 'empty',
            		prompt : 'Informe o número'
          		}
        	]
     	 },
      cidadevazio: {
        identifier  : 'cidadevazio',
        rules: [
          {
            type   : 'empty',
            prompt : 'Informe a Cidade'
          }
        ]
      },
      ufvazio: {
        identifier  : 'ufvazio',
        rules: [
          {
            type   : 'empty',
            prompt : 'Informe a UF'
          }
        ]
      },
      cepvazio: {
        identifier  : 'cepvazio',
        rules: [
          {
            type   : 'empty',
            prompt : 'Informe o CEP'
          }
        ]
      },
	 checkbox: {
	        identifier  : 'check',
	        rules: [
	          {
	            type   : 'checked',
	            prompt : 'Aceite os termos'
	          }
	        ]
	      }
      }
  });
	</script>

	<script>$('.tabular.menu .item').tab();</script>

	 <script type="text/javascript">
         jQuery(function ($) {
             $("#cep").mask("99999-999");
             $("#cpf").mask("999.999.999-99");
            
         });
    </script> 

  
    <script>
    $('.ui.dropdown').dropdown();
    </script>

     
    <!-- Adicionando Javascript -->
    <script type="text/javascript" >

		$(document).ready(function() {

            function limpa_formulário_cep() {
                // Limpa valores do formulário de cep.
                $("#rua").val("");
                $("#bairro").val("");
                $("#cidade").val("");
                $("#uf").val("");
            }
            
            //Quando o campo cep perde o foco.
          $("#cep").blur(function() {

                //Nova variável "cep" somente com dígitos.
                var cep = $(this).val().replace(/\D/g, '');

                //Verifica se campo cep possui valor informado.
                if (cep != "") {

                    //Expressão regular para validar o CEP.
                    var validacep = /^[0-9]{8}$/;

                    //Valida o formato do CEP.
                    if(validacep.test(cep)) {

                        //Preenche os campos com "..." enquanto consulta webservice.
                        $("#rua").val("...");
                        $("#bairro").val("...");
                        $("#cidade").val("...");
                        $("#uf").val("...");
                        $("#ibge").val("...");

                        //Consulta o webservice viacep.com.br/
                        $.getJSON("//viacep.com.br/ws/"+ cep +"/json/?callback=?", function(dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#rua").val(dados.logradouro);
                                $("#bairro").val(dados.bairro);
                                $("#cidade").val(dados.localidade);
                                $("#uf").val(dados.uf);
                                $("#ibge").val(dados.ibge);
                                                              
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                    } //end if.
                    else {
                        //cep é inválido.
                        limpa_formulário_cep();
                        alert("Formato de CEP inválido.");
                    }
                } //end if.
                else {
                    //cep sem valor, limpa formulário.
                    limpa_formulário_cep();
                }
           });
        });

    </script>

</html>

