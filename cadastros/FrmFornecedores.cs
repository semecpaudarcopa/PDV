using DevComponents.AdvTree;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV.cadastros
{
    public partial class FrmFornecedores : Form
    {
        // Conectando com o banco de dados
        string conexDb = "SERVER=localhost;DATABASE=c#pdv;UID=root;PWD=";
        MySqlConnection conexao = null;
        MySqlCommand comando;
       
        
        public FrmFornecedores()
        {
            InitializeComponent();
            showDvg();
            desabilitaCampos();
        }

        //Método para exibir informações do banco no dataGrid
        public void showDvg()
        {
            conexao = new MySqlConnection(conexDb);
            comando = new MySqlCommand("SELECT * FROM cad_fornecedores", connection:
            conexao);
            MySqlDataAdapter Da = new MySqlDataAdapter();
            Da.SelectCommand = comando;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            dgvFornecedores.DataSource = Dt;
        }

        //Método para abrir conexão
        public void abrirConexao()
        {
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
        }
        //Método para fechar conexão
        public void fecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        //Método para testar conexão
        public void executar(string banco)
        {
            try
            {
                abrirConexao();
                comando = new MySqlCommand(banco, conexao);
                if (comando.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Operação Realizada com Sucesso!");
                }
                else
                {
                    MessageBox.Show("Não foi possivel completar esta operação, por favor tente novamente!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }


        }
        private void desabilitaCampos()
        {
            txtId.Visible = false;
            //lbl_Cod.Visible = false;
            txtNome.Enabled = false;
            txtCpf.Enabled = false;
            txtRg.Enabled = false;
            txtTelefone.Enabled = false;
            txtEndereco.Enabled = false;
            txtNumero.Enabled = false;
            txtComplemento.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtCep.Enabled = false;
            cbUf.Enabled = false;
            txtEmail.Enabled = false;
            txtObs.Enabled = false;


            // btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            
            

        }
        private void habilitaCampos()
        {
            txtId.Visible = false;
            //lbl_Cod.Visible = false;
            txtNome.Enabled = true;
            txtCpf.Enabled = true;
            txtRg.Enabled = true;
            txtTelefone.Enabled = true;
            txtEndereco.Enabled = true;
            txtNumero.Enabled = true;
            txtComplemento.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtCep.Enabled = true;

            cbUf.Enabled = true;
            txtEmail.Enabled = true;
            txtObs.Enabled = true;

            // btnCancelar.Enabled = false;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            dgvFornecedores.DataSource = null;

        }
        private void LimparCampos()
        {
            //label1.Visible = false;
            // lbl_Cod.Visible = false;
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtCep.Text = "";
            cbUf.SelectedIndex = -1;
            dtData.Text = "";
            txtEmail.Text = "";
            txtObs.Text = "";
            dgvFornecedores.DataSource = null;
            txtNome.Focus();

        }

        private void dgvFornecedores_MouseClick(object sender, MouseEventArgs e)
        {
            // Preenche os campos com as informações contidas na linha selecionada do dataGrid
            txtId.Text = dgvFornecedores.CurrentRow.Cells[0].Value.ToString();

            if (txtId.Text != String.Empty)
            {
                txtNome.Text = dgvFornecedores.CurrentRow.Cells[1].Value.ToString();
                txtCpf.Text = dgvFornecedores.CurrentRow.Cells[2].Value.ToString();
                txtRg.Text = dgvFornecedores.CurrentRow.Cells[3].Value.ToString();
                txtTelefone.Text = dgvFornecedores.CurrentRow.Cells[4].Value.ToString();
                txtEndereco.Text = dgvFornecedores.CurrentRow.Cells[5].Value.ToString();
                txtNumero.Text = dgvFornecedores.CurrentRow.Cells[6].Value.ToString();
                txtComplemento.Text = dgvFornecedores.CurrentRow.Cells[7].Value.ToString();
                txtBairro.Text = dgvFornecedores.CurrentRow.Cells[8].Value.ToString();
                txtCidade.Text = dgvFornecedores.CurrentRow.Cells[9].Value.ToString();
                txtCep.Text = dgvFornecedores.CurrentRow.Cells[10].Value.ToString();
                cbUf.Text = dgvFornecedores.CurrentRow.Cells[11].Value.ToString();
                dtData.Text = dgvFornecedores.CurrentRow.Cells[12].Value.ToString();
                //dateAdmissao.Text = dgvFornecedores.CurrentRow.Cells[13].Value.ToString();
                txtEmail.Text = dgvFornecedores.CurrentRow.Cells[14].Value.ToString();
                txtObs.Text = dgvFornecedores.CurrentRow.Cells[15].Value.ToString();
                string valor = dgvFornecedores.SelectedRows[0].Cells[13].Value.ToString();

                if (valor == "True")
                {
                    rdAtivo.Checked = true;
                }
                else
                {
                    rdInativo.Checked = true;
                }
                habilitaCampos();
                btnSalvar.Enabled = false;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
            }
            else
            {

                LimparCampos();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            comando.Parameters.Clear();
            comando.CommandText = "INSERT INTO cad_fornecedores(nome,cpf, rg, telefone, endereco,  numero, complemento, bairro, cidade, cep, uf, data, status, email, observacoes) VALUES(@NOME, @CPF, @RG, @TELEFONE, @ENDERECO, @NUMERO, @COMPLEMENTO,  @BAIRRO,  @CIDADE, @CEP, @UF  @DATA, @STATUS  @EMAIL, @OBSERVACOES)";

            comando.Parameters.AddWithValue("NOME", txtNome.Text);
            comando.Parameters.AddWithValue("CPF", txtCpf.Text);
            comando.Parameters.AddWithValue("RG", txtRg.Text);
            comando.Parameters.AddWithValue("TELEFONE", txtTelefone.Text);
            comando.Parameters.AddWithValue("ENDERECO", txtEndereco.Text);
            comando.Parameters.AddWithValue("NUMERO", txtNumero.Text);
            comando.Parameters.AddWithValue("COMPLEMENTO", txtComplemento.Text);
            comando.Parameters.AddWithValue("BAIRRO", txtBairro.Text);
            comando.Parameters.AddWithValue("CIDADE", txtCidade.Text);
            comando.Parameters.AddWithValue("CEP", txtCep.Text);
            comando.Parameters.AddWithValue("UF", cbUf.Text);


            comando.Parameters.Add("@data", MySqlDbType.Date).Value = dtData.Value;
            comando.Parameters.AddWithValue("STATUS", rdAtivo.Text);
            comando.Parameters.AddWithValue("EMAIL", txtEmail.Text);
            comando.Parameters.AddWithValue("OBSERVACOES", txtObs.Text);
           

            abrirConexao();
            comando.ExecuteNonQuery();
            MessageBox.Show("Cadastro realizado com sucesso!");
            showDvg();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string alterar = "UPDATE cad_fornecedores SET nome='" + txtNome.Text + "', cpf='" + txtCpf.Text + "', rg='" + txtRg.Text + "'," +
                    "telefone='" + txtTelefone.Text + "', endereco='" + txtEndereco.Text + "', numero='" + txtNumero.Text + "'," +
                    "complemento='" + txtComplemento.Text + "', bairro='" + txtBairro.Text + "', cidade='" + txtCidade.Text + "'," +
                    "cep='" + txtCep.Text + "', uf='" + cbUf.Text + "', data='" + dtData.Text + "', email='" + txtEmail.Text + "', observacoes='" + txtObs.Text 
                + "'WHERE id ="
                + int.Parse(txtId.Text);
                executar(alterar);

                executar(alterar);
                showDvg();
                LimparCampos();

            }
            else
            {
                MessageBox.Show("Só é possível alterar cadastros existentes");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string excluir = "DELETE FROM cad_fornecedores WHERE id = " +
                int.Parse(txtId.Text);
                executar(excluir);
                showDvg();
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Só é possível excluir cadastros existentes");
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            // btnNovo = Enabled;
            //btnEditar = false;

            LimparCampos();
            habilitaCampos();
            this.txtNome.Focus();
            this.txtId.Enabled = false;
        }
    }
}
