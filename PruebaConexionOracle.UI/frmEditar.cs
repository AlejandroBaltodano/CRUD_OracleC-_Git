using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaConexionOracle.UI
{
    public partial class frmEditar : Form
    {
        string idPersonaEditar = string.Empty;
        public frmEditar(string id)
            
        {
            InitializeComponent();
            idPersonaEditar = id;
        }

        public void Llenarcampos()
        {
            AccesoADatos.ConexionOracle con = new AccesoADatos.ConexionOracle();
            string query = "Select * from Persona where Id =" + idPersonaEditar + "";
            OracleDataReader reader = con.Query(query);
            if ((reader.HasRows))
            {
                while (reader.Read())
                {
                    txtNombre.Text = reader.GetString(1);
                    txtCedula.Text = reader.GetString(2);
                    dtpFecha.Value = reader.GetDateTime(3);
                }
            }
            lblIdPersona.Text = idPersonaEditar;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditar_Load(object sender, EventArgs e)
        {
            Llenarcampos();
        }

        public Boolean ValidarCampos() {
            Boolean resultado = true;
            string msj = "Debe de llenar los campos: ";

            if (txtNombre.Text.Trim() == string.Empty)
            {
                resultado = false;
                msj += "Campo nombre";

            }
            if (txtCedula.Text.Trim() == string.Empty)
            {
                resultado = false;
                msj += " Campo cedula";

            }

            if (resultado == false)
            {

                MessageBox.Show(msj);

            }


            return resultado;
         
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (ValidarCampos() == true)
            {
                AccesoADatos.ConexionOracle con = new AccesoADatos.ConexionOracle();
                string query = "Begin EditarPersona("+idPersonaEditar+",'"+txtNombre.Text+"','"+txtCedula.Text+"','"+dtpFecha.Value.ToString("dd/MM/yyyy")+"'); end;";
                con.Update(query);
                MessageBox.Show("se edito bien");
                this.Close();
            }

        }
    }
}
