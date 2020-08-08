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
    public partial class frmPruebaConexionOracle : Form
    {
       
        public frmPruebaConexionOracle()
        {
            InitializeComponent();
        }

        public void LlenarGrid()
        {
            AccesoADatos.ConexionOracle con = new AccesoADatos.ConexionOracle();
            con.Conectar();
            string query = "select * from Persona";
            con.LlenarGrid(query, dgvEmpleado);
            PropiedadesGrip();



        }



        private void frmPruebaConexionOracle_Load(object sender, EventArgs e)
        {
            LlenarGrid();


        }
        
        public void PropiedadesGrip()
        {
            this.dgvEmpleado.Columns[0].Visible = false;
            this.dgvEmpleado.Columns[1].HeaderText = "Nombre";
            this.dgvEmpleado.Columns[2].HeaderText = "Cedula";
            this.dgvEmpleado.Columns[3].HeaderText = "Fecha Nacimiento";
           
        }

        private void btnoracle_Click(object sender, EventArgs e)
        {
            AccesoADatos.ConexionOracle conexionOracle = new AccesoADatos.ConexionOracle();
            try
            {
                conexionOracle.Conectar();
                MessageBox.Show("se conecto a oracle");


            string query = "select * from Persona";
            conexionOracle.LlenarGrid(query, dgvEmpleado);

            }
            catch (Exception)
            {

                MessageBox.Show("Error en oracle");
           }
        }

        private void btnacces_Click(object sender, EventArgs e)
        {

            AccesoADatos.ConexionAcces conexionAcces = new AccesoADatos.ConexionAcces();
            try
            {
                conexionAcces.Conectar();
                MessageBox.Show("se conecto a access");
                string query = "select * from TABLA_EMPLEADO";
                conexionAcces.LlenarGrid(query,dgvEmpleado);
                
                
            }
            catch (Exception)
            {

                MessageBox.Show("Error en acces");
            }


        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            //try
            //{ //esto es una prueba
                AccesoADatos.ConexionAcces conexion = new AccesoADatos.ConexionAcces();
            AccesoADatos.ConexionOracle conexionOracle = new AccesoADatos.ConexionOracle();
                string query = "select * from TABLA_EMPLEADO;";
            string queryEliminar = "DELETE FROM TABLA_EMPLEADO;";

            //aqui obtengo los registros de la tabla empleados y lo meto en el data set
                var lista = conexion.obtenerRegistrosAcces(query,"TABLA_EMPLEADO");
            //prueba dataset
            String columnas = String.Empty;
            String vals = String.Empty;
          
            DataTable dataTabla = lista.Tables[0];
            var listaTabla = dataTabla.Select(null, null, DataViewRowState.CurrentRows);


            if (listaTabla.Count() < 1)
            {
                MessageBox.Show("no hay registros");
            }
            else
            {

                String sql = String.Empty;
                string comilla = "'";
                List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

                foreach (DataRow row in dataTabla.Rows)
                {
                    foreach (DataColumn column in dataTabla.Columns)
                    {
                        
                        values.Add(new KeyValuePair<string, string>(
                            column.ColumnName, 
                            comilla + row[column].ToString() + comilla
                            ));
                        
                    }
                    sql += insertBuilder("TABLA_EMPLEADO", values ) + "\n" ;
                    values.Clear();



                }

                MessageBox.Show(sql);
                try
                {
                    conexionOracle.Update(queryEliminar);
                    MessageBox.Show("se eliminaron registros");
                }
                catch (Exception)
                {
                    MessageBox.Show("error al borrar");

                }
               



            }

            //dgvEmpleado.DataSource = lista.Tables[0];


            //    MessageBox.Show(this.dgvEmpleado.CurrentRow.Cells[1].Value.ToString()+ " "+ this.dgvEmpleado.CurrentRow.Cells[2].Value.ToString());


            //    string querycon = "insert into TABLA_PRUEBA(DNI,NOMBRE) values(" + this.dgvEmpleado.CurrentRow.Cells[1].Value.ToString() + "," + this.dgvEmpleado.CurrentRow.Cells[2].Value.ToString() + ");";
            //    conexion.Update(querycon);
            //    string prueba = "select * from TABLA_PRUEBA";
            //    conexion.LlenarGrid(prueba, dgvEmpleado);


            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("error");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String resultado = String.Empty;

            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("DNI", "'207270606'"),
                new KeyValuePair<string, string>("IDEMPLEADO", "'12'"),
                new KeyValuePair<string, string>("NOMBRE", "'JOSE'"),
            };


            resultado = insertBuilder("TABLA_EMPLEADOS" , values );

            MessageBox.Show(resultado);
        }

        public String insertBuilder( string table , List<KeyValuePair<string, string>> values)
        {
            String sql = String.Empty;

            String columns = String.Empty;
            String vals = String.Empty;

            sql = "INSERT INTO " + table;
            columns += "( ";
            vals += "( ";
            int contador = 0;
            foreach (KeyValuePair<string, string> keyVal in values)
            {
                var coma = (contador == 0) ? " " : ", ";
                columns += coma + keyVal.Key ;
                vals += coma + keyVal.Value ;
                contador += 1;
            }
            columns += " ) VALUES";
            vals += " ); ";

            sql += columns + vals; 
            return sql;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            frmInsertar frmInsert = new frmInsertar();
            frmInsert.ShowDialog();
            LlenarGrid();
          }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvEmpleado.Rows.Count != 0 )
            {
                AccesoADatos.ConexionOracle con = new AccesoADatos.ConexionOracle();
                string idPersona = this.dgvEmpleado.CurrentRow.Cells[0].Value.ToString();

                string query = "Begin ELIMINARPERSONA("+ idPersona +"); end;";
                MessageBox.Show(query);
                try
                {
                    con.Update(query);
                    MessageBox.Show("se elimino", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LlenarGrid();

                }
                catch (Exception)
                {
                    MessageBox.Show("problemas al eliminar", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                

            }
            else
            {
                MessageBox.Show("eliga un registro", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           



        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string id = this.dgvEmpleado.CurrentRow.Cells[0].Value.ToString();
            frmEditar edi = new frmEditar(id);
            edi.ShowDialog();
            LlenarGrid(); 
        }

        //public bool ValidarCampos()
        //{
        //    bool bandera = false;

        //    if (!string.IsNullOrEmpty(txtDNI.Text) && !string.IsNullOrEmpty(txtNombre.Text))
        //    {
        //        bandera = true;
        //    }

        //    return bandera;

        //}
        //public void LimpiarDatos() {
        //    txtDNI.Text = "";
        //    txtNombre.Text = "";

        //}

        //private void btnIngresar_Click(object sender, EventArgs e)
        //{
        //    if (ValidarCampos())
        //    {
        //        try
        //        {

        //            AccesoADatos.ConexionOracle conexion = new AccesoADatos.ConexionOracle();

        //            MessageBox.Show(txtDNI.Text +" "+txtNombre.Text);

        //            string Query = "Insert into TABLA_PERSONA(CEDULA,NOMBRE) values('"+txtDNI.Text+"','"+txtNombre.Text+"');";//"EXECUTE PROCE_INSERTAR_PERSONA ('" + txtDNI.Text + "','" + txtNombre.Text + "');";

        //            conexion.Update(Query);

        //            refrescar();

        //            LimpiarDatos();


        //            MessageBox.Show("Transaccion realizada exitosamente", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Problemas al realizar la transaccion", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Problemas al realizar la transaccion, Verifique que los campos no esten vacios", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //}

        //private void btnLimpiar_Click(object sender, EventArgs e)
        //{
        //    LimpiarDatos();
        //}

        //private void btnRefrescar_Click(object sender, EventArgs e)
        //{
        //    refrescar();
        //}

        //public void refrescar() {
        //    AccesoADatos.ConexionOracle conexion = new AccesoADatos.ConexionOracle();
        //   string query = "select e.IDEMPLEADO,e.DNI,e.NOMBRE,p.NOMBREPUESTO from TABLA_EMPLEADO e, TABLA_PUESTO p where e.IDPUESTOEMPLEADO = p.IDPUESTO; ";
        //   conexion.LlenarGrid(query, dgvEmpleado);




        //}
    }
}
