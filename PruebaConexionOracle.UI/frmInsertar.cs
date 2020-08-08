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
    public partial class frmInsertar : Form
    {
        public frmInsertar()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Boolean ValidarCampos()
        {
            string msj = "LLene los campos:";
            Boolean resultado = true;

            if (txtNombre.Text.Trim() == string.Empty)
            {
                msj += " Nombre completo ";
                resultado = false;
            }
            if (txtCedula.Text.Trim() == string.Empty)
            {
                msj += " cedula ";
                resultado = false;
            }

            if (resultado == false)
            {
                MessageBox.Show(msj);

            }


            return resultado;

        }


        private void btnInsertar_Click(object sender, EventArgs e)
        {
            AccesoADatos.ConexionOracle con = new AccesoADatos.ConexionOracle();

            if (ValidarCampos())
            {
                DateTime date = new DateTime(2020, 08, 06);
                string query = "Begin INSERTARPERSONA('" + txtNombre.Text + "', '" + txtCedula.Text + "', '" + dtpFecha.Value.ToString("d") + "'); end;";
                MessageBox.Show(query);

                try
                {
                    con.Update(query);

                    MessageBox.Show("insercion exitosa");
                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("error en la insercion");

                }

            }
           
            

        }
    }
}
