using DevComponents.DotNetBar;
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
    public partial class FrmCargos : Form
    {
        public FrmCargos()
        {
            InitializeComponent();
        }
        // Conectando com o banco de dados
        string conexDb = "SERVER=localhost;DATABASE=c#pdv;UID=root;PWD=";
        MySqlConnection conexao = null;
        MySqlCommand comando;
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtNome.Enabled = true;
            this.txtNome.Focus();
            this.txtId.Enabled = false;
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;

        }

        private void FrmCargos_Load(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            txtNome.Enabled = false;
            showDvg();
        }
        public void showDvg()
        {
            conexao = new MySqlConnection(conexDb);
            comando = new MySqlCommand("SELECT * FROM cad_cargos", connection:
            conexao);
            MySqlDataAdapter Da = new MySqlDataAdapter();
            Da.SelectCommand = comando;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            dgvCargos.DataSource = Dt;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string alterar = "UPDATE cad_cargos SET cargo='" + txtNome.Text 
                + "'WHERE id ="
                + int.Parse(txtId.Text);
                executar(alterar);
                showDvg();
                dgvCargos.DataSource = null;
                btnNovo.Enabled = true;
                btnExcluir.Enabled = false;
                btnEditar.Enabled = false;

            }
            else
            {
                MessageBox.Show("Só é possível alterar cadastros existentes");
            }
        }

        private void dgvCargos_MouseClick(object sender, MouseEventArgs e)
        {
            // Preenche os campos com as informações contidas na linha selecionada do dataGrid
            txtId.Text = dgvCargos.CurrentRow.Cells[0].Value.ToString();

            if (txtId.Text != String.Empty)
            {
                txtNome.Text = dgvCargos.CurrentRow.Cells[1].Value.ToString();
                txtNome.Enabled = true;
                btnSalvar.Enabled = false;
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                btnNovo.Enabled = false;
            }
            else
            {

                txtNome.Text = "";
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text != String.Empty)
            {
                string excluir = "DELETE FROM cad_cargos WHERE id = " +
                int.Parse(txtId.Text);
                executar(excluir);
                showDvg();
                txtNome.Text = "";
            }
            else
            {
                MessageBox.Show("Só é possível excluir cadastros existentes");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            comando.Parameters.Clear();
            comando.CommandText = "INSERT INTO cad_cargos (cargo) VALUES(@CARGO )";
            comando.Parameters.AddWithValue("CARGO", txtNome.Text);

            abrirConexao();
            comando.ExecuteNonQuery();
            MessageBox.Show("Cadastro realizado com sucesso!");
            showDvg();
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
        }
    }
}
