﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PetApp.Model;

namespace PetApp
{
    public partial class SH_CLIENTES : DevExpress.XtraEditors.XtraForm
    {
        int IdCli = 0;
        public bool pesquisa = false;
        public int CLI_ID = 0;
        public SH_CLIENTES()
        {
            InitializeComponent();

        }

        private void btnAdcCli_Click(object sender, EventArgs e)
        {
            FrmCadCli frmcadcli = new FrmCadCli();
            frmcadcli.ShowDialog();
        }

        private void FrmCli_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = Clientes.Get(" AND CLI_PESTIPO <> 'FOR'");
            btnAltCli.Enabled = !pesquisa;
            btnInserir.Enabled = !pesquisa;
            btnRemCli.Enabled = !pesquisa;
            panelControlBottom.Enabled = pesquisa;
            panelControlBottom.Visible = pesquisa;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnRemCli_Click(object sender, EventArgs e)
        {
            IdCli = F.toInt(gridView1.GetFocusedRowCellValue("CLI_ID"));
            if (F.YesNo("", "Deseja realmente excluir este cliente?", 1))
            {

                Clientes.Delete(Clientes.Get(IdCli));
                List<Pets> listPets = Pets.Get("AND CLI_ID = " + F.toString(IdCli));
                foreach (Pets pet in listPets)
                {
                    Pets.Delete(pet);
                }
                FrmCli_Load(null, null);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnAltCli_Click(object sender, EventArgs e)
        {
            IdCli = F.toInt(gridView1.GetFocusedRowCellValue("CLI_ID"));
            FrmCadCli form = new FrmCadCli();
            form.IdCliAlt = IdCli;
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                FrmCli_Load(null, null);
            }
        }

        private void btnInserir_Click_1(object sender, EventArgs e)
        {
            FrmCadCli form = new FrmCadCli();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                FrmCli_Load(null,null);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            StringBuilder filtro = new StringBuilder();
            if (F.toString(edCLI_FANTASIA.EditValue) != "")
            {
                filtro.Append(" AND CLI_FANTASIA = '" + F.toString(edCLI_FANTASIA.EditValue) + "' ");
            }
            if (F.toString(edCLI_RAZAO.EditValue) != "")
            {
                filtro.Append(" AND CLI_RAZAO = '" + F.toString(edCLI_RAZAO.EditValue) + "' ");
            }
            if (F.toString(edINSCRICAO.EditValue) != "")
            {
                filtro.Append(" AND CLI_INSCRICAO = '" + F.toString(edINSCRICAO.EditValue) + "' ");
            }
            if (F.toString(edCLI_CNPJ.EditValue) != "")
            {
                filtro.Append(" AND CLI_CNPJ = '" + F.toString(edCLI_CNPJ.EditValue) + "' ");
            }
            if (F.toString(edCLI_EMAIL.EditValue) != "")
            {
                filtro.Append(" AND CLI_EMAIL = '" + F.toString(edCLI_EMAIL.EditValue) + "' ");
            }
            if (F.toString(edCLI_NASC.EditValue) != "")
            {
                filtro.Append(" AND CLI_NASC = '" + F.toString(edCLI_NASC.EditValue) + "' ");
            }
            if (F.toString(edCLI_CEP.EditValue) != "")
            {
                filtro.Append(" AND CLI_CEP = '" + F.toString(edCLI_CEP.EditValue) + "' ");
            }
            if (F.toString(rgCLI_PESTIPO.EditValue) != "")
            {
                filtro.Append(" AND CLI_PESTIPO = '" + F.toString(rgCLI_PESTIPO.EditValue) + "' ");
            }

            List<Clientes> listCliente = new List<Clientes>();
            listCliente = Clientes.Get(filtro.ToString());
            gridControl1.DataSource = listCliente;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Clientes cli = new Clientes();
            try
            {
                cli = gridView1.GetFocusedRow() as Clientes;
                CLI_ID = cli.CLI_ID;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                CLI_ID = 0;
            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAltCli_Click(null, null);
        }

        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemCli_Click(null,null);
        }
    }
}
