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

namespace PetApp.View.Servico
{
    public partial class frmTipoServico : DevExpress.XtraEditors.XtraForm
    {
        int id = 0;
        public frmTipoServico()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                TipoServico tipo = new TipoServico
                {
                    TIPO_SER_NOME = F.toString(edTIPO_SER_NOME.EditValue),
                    TIPO_SER_VALOR = F.toDouble(edTIPO_SER_VALOR.EditValue)
                };
                TipoServico.Insert(tipo);
                edTIPO_SER_NOME.EditValue = "";
                edTIPO_SER_VALOR.EditValue = "";
                edTIPO_SER_NOME.Focus();
                frmTipoServico_Load(null, null);
            }
            else
            {
                TipoServico tipo = new TipoServico
                {
                    TIPO_SER_ID = F.toInt(id),
                    TIPO_SER_NOME = F.toString(edTIPO_SER_NOME.EditValue),
                    TIPO_SER_VALOR = F.toDouble(edTIPO_SER_VALOR.EditValue)
                };
                TipoServico.Update(tipo);
                btnCadastrar.Text = "Adicionar";
                edTIPO_SER_NOME.EditValue = "";
                edTIPO_SER_VALOR.EditValue = "";
                edTIPO_SER_NOME.Focus();
                frmTipoServico_Load(null, null);
                id = 0;
            }
        }

        private void frmTipoServico_Load(object sender, EventArgs e)
        {
            edTIPO_SER_NOME.EditValue = null;
            edTIPO_SER_VALOR.EditValue = null;
            edTIPO_SER_NOME.Focus();
            gridControl1.DataSource = TipoServico.Get();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int TIPO_SER_ID = F.toInt(gridView1.GetFocusedRowCellValue("TIPO_SER_ID"));
            TipoServico tipoAltera = TipoServico.Get(TIPO_SER_ID);
            edTIPO_SER_NOME.EditValue = F.toString(tipoAltera.TIPO_SER_NOME);
            edTIPO_SER_VALOR.EditValue = F.toDouble(tipoAltera.TIPO_SER_VALOR);
            id = F.toInt(tipoAltera.TIPO_SER_ID);
            btnCadastrar.Text = "Salvar";
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            int TIPO_SER_ID = F.toInt(gridView1.GetFocusedRowCellValue("TIPO_SER_ID"));
            if (F.YesNo("Deletar","Você deseja realmente remover este tipo de serviço", 1))
            {
                if (TipoServico.Delete(TipoServico.Get(TIPO_SER_ID)))
                {
                    F.Aviso("Tipo de serviço deletado com sucesso");
                    frmTipoServico_Load(null, null);
                }
                else
                {
                    F.Aviso("Erro ao deletar tipo de serviço!");
                }
            }
            
        }
    }
}
