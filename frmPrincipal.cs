using PDV.cadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
           frmFuncionarios funcionarios = new frmFuncionarios();
            funcionarios.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo sair?", "formulario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHoras.Text = DateTime.Now.ToString("HH:mm:ss");
            lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnCargos_Click(object sender, EventArgs e)
        {
            FrmCargos cargos = new FrmCargos();
            cargos.ShowDialog();
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            FrmFornecedores fornecedores = new FrmFornecedores();
            fornecedores.ShowDialog();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            FrmUsuarios usuarios = new FrmUsuarios();
            usuarios.ShowDialog();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {

        }

        private void btnsProdutos_Click(object sender, EventArgs e)
        {
            FrmProdutos produtos = new FrmProdutos();
            produtos.ShowDialog();
        }
    }
}
