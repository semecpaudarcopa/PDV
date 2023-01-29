using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using DevComponents.AdvTree;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.ConstrainedExecution;
using System.Collections;
using DevComponents.DotNetBar;


namespace PDV
{
    public partial class frmFuncionarios : Form
    {

        public frmFuncionarios()
        {
            InitializeComponent();

        }
        

        // Conectando com o banco de dados
        string conexDb = "SERVER=localhost;DATABASE=c#pdv;UID=root;PWD=";
        MySqlConnection conexao = null;
        MySqlCommand comando;
        MySqlDataAdapter Adaptador;
        DataTable datTabela;

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            showDvg();
            desabilitaCampos();
        }

        //Método para exibir informações do banco no dataGrid
        public void showDvg()
        {
            conexao = new MySqlConnection(conexDb);
            comando = new MySqlCommand("SELECT * FROM cad_funcionarios", connection:
            conexao);
            MySqlDataAdapter Da = new MySqlDataAdapter();
            Da.SelectCommand = comando;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            dgvFuncionario.DataSource = Dt;
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
            txtBairro.Enabled = false;
            txtEndereco.Enabled = false;
            txtNumero.Enabled = false;
            txtComplemento.Enabled = false;
            
            
           // btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            cbCargo.Enabled = false;
            txtEmail.Enabled = false;
            txtObs.Enabled = false;

        }

        private void habilitaCampos()
        {
            txtId.Visible = false;
            //lbl_Cod.Visible = false;
            txtNome.Enabled = true;
            txtCpf.Enabled = true;
            txtRg.Enabled = true;
            txtTelefone.Enabled = true;
            txtBairro.Enabled = true;
            txtEndereco.Enabled = true;
            txtNumero.Enabled = true;
            txtComplemento.Enabled = true;
            cbCargo.Enabled = true;
            txtEmail.Enabled = true;
            txtObs.Enabled = true;

            // btnCancelar.Enabled = false;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            dgvFuncionario.DataSource = null;

        }

        private void LimparCampos()
        {
            //label1.Visible = false;
            // lbl_Cod.Visible = false;
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtTelefone.Text = "";
            txtBairro.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            cbCargo.SelectedIndex = -1;
            txtEmail.Text = "";
            txtObs.Text = "";
            dgvFuncionario.DataSource = null;
            txtNome.Focus();

        }
        private void dgvFuncionario_MouseClick(object sender, MouseEventArgs e)
        {
            
            // Preenche os campos com as informações contidas na linha selecionada do dataGrid
            txtId.Text = dgvFuncionario.CurrentRow.Cells[0].Value.ToString();

            if (txtId.Text != String.Empty)
            {
                txtNome.Text = dgvFuncionario.CurrentRow.Cells[1].Value.ToString();
                txtCpf.Text = dgvFuncionario.CurrentRow.Cells[2].Value.ToString();
                txtRg.Text = dgvFuncionario.CurrentRow.Cells[3].Value.ToString();
                txtTelefone.Text = dgvFuncionario.CurrentRow.Cells[4].Value.ToString();
                txtBairro.Text = dgvFuncionario.CurrentRow.Cells[5].Value.ToString();
                txtEndereco.Text = dgvFuncionario.CurrentRow.Cells[6].Value.ToString();
                txtNumero.Text = dgvFuncionario.CurrentRow.Cells[7].Value.ToString();
                txtComplemento.Text = dgvFuncionario.CurrentRow.Cells[8].Value.ToString();
                Data_nascimento.Text = dgvFuncionario.CurrentRow.Cells[9].Value.ToString(); 
                dateAdmissao.Text = dgvFuncionario.CurrentRow.Cells[10].Value.ToString();
                cbCargo.Text = dgvFuncionario.CurrentRow.Cells[11].Value.ToString();
                txtEmail.Text = dgvFuncionario.CurrentRow.Cells[12].Value.ToString();
                txtObs.Text = dgvFuncionario.CurrentRow.Cells[13].Value.ToString();
                imgFuncionario.Load(dgvFuncionario.CurrentRow.Cells[14].Value.ToString());
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
            comando.CommandText = "INSERT INTO cad_funcionarios (nome,cpf, rg, telefone, bairro, endereco, numero, complemento, data_nascimento, data_admissao, cargo, email, observacoes, foto) VALUES(@NOME, @CPF, @RG, @TELEFONE, @BAIRRO, @ENDERECO, @NUMERO, @COMPLEMENTO, @DATA_NASCIMENTO, @DATA_ADMISSAO,@CARGO, @EMAIL, @OBSERVACOES, @FOTO)";
            
            comando.Parameters.AddWithValue("NOME", txtNome.Text);
            comando.Parameters.AddWithValue("CPF", txtCpf.Text);
            comando.Parameters.AddWithValue("RG", txtRg.Text);
            comando.Parameters.AddWithValue("TELEFONE", txtTelefone.Text);
            comando.Parameters.AddWithValue("BAIRRO", txtBairro.Text);
            comando.Parameters.AddWithValue("ENDERECO", txtEndereco.Text);
            comando.Parameters.AddWithValue("NUMERO", txtNumero.Text);
            comando.Parameters.AddWithValue("COMPLEMENTO", txtComplemento.Text);
            
            comando.Parameters.AddWithValue("DATA_NASCIMENTO", Data_nascimento.Text);
            
            comando.Parameters.AddWithValue("DATA_ADMISSAO", dateAdmissao.Text);
            
            comando.Parameters.AddWithValue("EMAIL", txtEmail.Text);
            comando.Parameters.AddWithValue("OBSERVACOES", txtObs.Text);
            comando.Parameters.AddWithValue("FOTO", ofdFotoF.FileName);

            comando.CommandText = "INSERT INTO cad_cargos (cargo)";
            comando.Parameters.AddWithValue("CARGO", cbCargo.Text);
            
            CarregaComboCargos();


            abrirConexao();
            comando.ExecuteNonQuery();
            MessageBox.Show("Cadastro realizado com sucesso!");
            showDvg();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string alterar = "UPDATE cad_funcionarios SET nome='" + txtNome.Text + "',cpf='" + txtCpf.Text + "',rg='" + txtRg.Text
                + "', telefone='" + txtTelefone.Text + "',  bairro='" + txtBairro.Text + "'endereco='" + txtEndereco.Text 
                + "'numero='" + txtNumero.Text + "'complemento='" + txtComplemento.Text + "' data_nascimento='" + Data_nascimento.Text
                + "', data_admissao = '" + dateAdmissao.Text + "', cargo = '" + cbCargo.Text + "', email = '" + txtEmail.Text
                + "', observacao='" + txtObs.Text 
                + "'WHERE id ="
                + int.Parse(txtId.Text);
                executar(alterar);

                string altera = "UPDATE cad_cargos SET cargo='" + cbCargo.Text 
                + "'WHERE id ="
                + int.Parse(txtId.Text);
                executar(alterar);
                showDvg();
                LimparCampos();
                
            }
            else
            {
                MessageBox.Show("Só é possível alterar cadastros existentes");
            }
        }
        void CarregaComboCargos()
        {
            Adaptador = new MySqlDataAdapter("SELECT * FROM cad_cargos order by nome", conexao);

            Adaptador.Fill(datTabela = new DataTable());

            cbCargo.DataSource = datTabela;

            cbCargo.DisplayMember = "nome";

            cbCargo.ValueMember = "id";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string excluir = "DELETE FROM cad_funcionarios WHERE id = " +
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

        private void btnFoto_Click(object sender, EventArgs e)
        {
            if (ofdFotoF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imgFuncionario.Load(ofdFotoF.FileName);
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

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
