﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Correios.Net;
using DevExpress.XtraEditors;
using PetApp.View;
using PetApp.View.UTL;
using PetApp.Control;
using SQLite;

namespace PetApp
{
    class F
    {
        public static string IMGPATH = "IMAGES";
        public static string TEMPFOLDER= "TEMPFOLDER";

        public static SQLiteConnection conn()
        {
            Connection connection = new Connection();
            return connection.conn;            
        }
        public static bool cleanTempFolder()
        {
            try
            {
                System.IO.Directory.Delete(TEMPFOLDER);
            }
            catch (Exception ex)
            {
                F.WriteLOG("----------------------\nLOG WSCorreios - " + DateTime.Now.ToString() + " - Erro: " + ex.Message);
                return false;
            }
            return true;
        }
        public static string copyToTempPath(string filePath, string nameFile = "")
        {
            if (nameFile == "")
            {
                nameFile = System.IO.Path.GetFileName(filePath);
            }

            if (!System.IO.Directory.Exists(TEMPFOLDER))
            {
                System.IO.Directory.CreateDirectory(TEMPFOLDER);
            }
            else
            {
                System.IO.Directory.Delete(TEMPFOLDER);
                System.IO.Directory.CreateDirectory(TEMPFOLDER);
            }
            try
            {
                System.IO.File.Copy(filePath, System.IO.Path.Combine(TEMPFOLDER, nameFile), true);
            }
            catch (Exception ex)
            {
                Aviso("Erro ao Salva/Alterar Imagem\n" + ex.Message);
                return "";
            }
            return System.IO.Path.Combine(TEMPFOLDER, nameFile);

        }
        public static string copyToImagePath(string imgPath, string nameImg = "")
        {
            if (nameImg == "")
            {
                nameImg = System.IO.Path.GetFileName(imgPath);
            }
            
            if (!System.IO.Directory.Exists(IMGPATH))
            {
                System.IO.Directory.CreateDirectory(IMGPATH);
            }

            try
            {
                System.IO.File.Copy(imgPath, System.IO.Path.Combine(IMGPATH, nameImg), true);
            }
            catch (Exception ex)
            {
                Aviso("Erro ao Salva/Alterar Imagem\n" + ex.Message);
                return "";
            }
            return System.IO.Path.Combine(IMGPATH, nameImg);

        }
        public static void WriteLOG(string log)
        {
            if (!File.Exists("LOG.txt"))
            {
                File.Create("LOG.txt");
            }

            StreamWriter sw = new StreamWriter("LOG.txt");
            sw.NewLine = log;
            sw.Close();
        }

        public static string toString(object Value, string Default = "")
        {
            string result = Default;
            if ((Value != DBNull.Value) || (Value != null))
            {
                try
                {
                    result = Convert.ToString(Value).Trim();
                }
                catch
                {
                    result = "";
                }
            }
            return result;
        }

        public static int toInt(object Value, int Default = 0)
        {
            try
            {
                if ((Value == DBNull.Value) || (Value == null) || (toString(Value) == ""))
                {
                    return Default;
                }
                return Convert.ToInt32(Value);
            }
            catch
            {
                return Default;
            }
        }

        public static double toDouble(object Value, double Default = 0.0)
        {
            try
            {
                if ((Value == DBNull.Value) || (Value == null) || (toString(Value) == ""))
                {
                    return Default;
                }
                return Convert.ToDouble(Value);
            }
            catch
            {
                return Default;
            }
        }

        public static void Aviso(string aviso)
        {
            MessageBox.Show(aviso);
        }

        public static void LocalizarCEP(string CEP, TextEdit edEstado, TextEdit edCidade, TextEdit edBairro, TextEdit edRua)
        {
            try
            {
                var ws = new WSCorreios.AtendeClienteClient();
                var resposta = ws.consultaCEP(F.toString(CEP));
                edCidade.EditValue = resposta.cidade;
                edRua.EditValue = resposta.end;
                edBairro.EditValue = resposta.bairro;
                edEstado.EditValue = resposta.uf;
            }
            catch (Exception ex)
            {
                Aviso(F.toString(ex.Message));
                F.WriteLOG("----------------------\nLOG WSCorreios - " + DateTime.Now.ToString() + " - Erro: " + ex.Message);
            }
        }

        public static int idade(DateTime data)
        {
            int anoNow = DateTime.Now.Year;
            int anoNas = data.Year;
            int age = anoNow - anoNas;
            return age;
        }

        public static bool YesNo(string Titulo, string Conteudo, int Foco = 2)
        {
            FrmYN objFrmYN = new FrmYN(Titulo, Conteudo, Foco);
            if (objFrmYN.ShowDialog() == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string SearchProds(string oldValue = "")
        {
            FrmSelectPro obj = new FrmSelectPro();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                return obj.PRO_REFERENCIA;
            }
            else
            {
                return oldValue;
            }
        }
        public static string SearchCli(string oldValue = "")
        {
            FrmSelectCli obj = new FrmSelectCli() { GetInLoad = true };

            if (obj.ShowDialog() == DialogResult.OK)
            {
                return obj.Result;
            }
            else
            {
                return oldValue;
            }
        }
        public static string SearchPet(string oldValue = "")
        {
            FrmSelectPet obj = new FrmSelectPet() { GetInLoad = true };

            if (obj.ShowDialog() == DialogResult.OK)
            {
                return obj.Result;
            }
            else
            {
                return oldValue;
            }
        }
    }
}
