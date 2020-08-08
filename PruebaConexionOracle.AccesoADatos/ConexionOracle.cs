using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;



namespace PruebaConexionOracle.AccesoADatos
{
    public class ConexionOracle
    {
        string pruebaQuery = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        OracleConnection connection;

        public ConexionOracle()
        {
            //user id=scott;password=tiger;data source=oracle
            //DATA SOURCE=XE;USER ID=MIGRACION;PASSWORD=123
            /*      connection = new OracleConnection("DATA SOURCE=REYES:1521/xe;USER ID=MIGRACION;PASSWORD=123")*/
            connection = new OracleConnection(pruebaQuery);

        }

        public OracleConnection Conectar()
        {

            try
            {
                Desconectar();
            }
            catch (Exception)
            {

                throw;
            }
            connection.Open();
            return connection;

            //var estado = connection.State;

            //if (estado.ToString() == "Closed")
            //{
            //    try
            //    {
            //        connection.Open();
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }    
        }


        //public OracleConnection Conectar()
        //{
        //    try
        //    {
        //        Desconectar();
        //    }
        //    catch { }

        //    connection.Open();

        //    return connection;
        //}

        public void Desconectar()
        {
            connection.Close();
        }

        public OracleDataReader Query(String query)
        {
            Conectar();

            OracleCommand comando = new OracleCommand(query, connection);

            return comando.ExecuteReader();
        }

        public void Update(string query)
        {
            Conectar();
            OracleCommand sql = new OracleCommand(query, connection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter();
            dataAdapter.InsertCommand = sql;
            dataAdapter.InsertCommand.ExecuteNonQuery();
            Desconectar();

        }

        public DataSet LlenarGrid(String query, DataGridView grid)
        {
            Conectar();

            OracleCommand comando = new OracleCommand(query, connection);

            comando.CommandTimeout = 0;

            OracleDataAdapter adapter = new OracleDataAdapter(comando);
           

            DataSet ds = new DataSet();

            adapter.Fill(ds);

            grid.DataSource = ds.Tables[0];

            Desconectar();

            return ds;
        }
        public DataSet LlenarCombo(String query, ComboBox comboBox, string id, string descripcion, string nombreTabla)
        {
            Conectar();

            OracleCommand comando = new OracleCommand(query, connection);

            comando.CommandTimeout = 0;

            OracleDataAdapter adapter = new OracleDataAdapter(comando);

            DataSet ds = new DataSet();

            adapter.Fill(ds, nombreTabla);

            comboBox.DataSource = ds.Tables[0].DefaultView;
            comboBox.ValueMember = id;
            comboBox.DisplayMember = descripcion;

            Desconectar();

            return ds;
        }






        //public ConexionOracle()
        //{
        //    // este metodo se utiliza para poder hacer la conexion a la base de datos oracle con el usuario sa

        //    connection = new OracleConnection();
        //    connection.ConnectionString = "DATA SOURCE=XE;USER ID=MIGRACION;PASSWORD=123";



        //}



        ////este metodo me abre la conexion
        ////public OracleConnection Conectar()
        ////{
        ////    try
        ////    {
        ////        Desconectar();//me cierra la onexion

        ////    }
        ////    catch {


        ////    }

        ////    connection.Open();// me abre la conexion

        ////    return connection;
        ////}

        ////este metodo me desconecta la conexion
        //public void Desconectar()
        //{
        //    connection.Close();
        //}

        //public OracleDataReader Query(String query)
        //{
        //    connection.Open();

        //    OracleCommand comando = new OracleCommand(query, connection);
        //    OracleDataAdapter adaptador = new OracleDataAdapter();
        //    adaptador.InsertCommand = comando;


        //    return adaptador.InsertCommand.ExecuteReader(); ;
        //}

        //public void Update(String query)
        //{
        //    connection.Open();

        //    OracleCommand comando = new OracleCommand(query, connection);
        //    OracleDataAdapter adaptador = new OracleDataAdapter();
        //    adaptador.InsertCommand = comando;
        //    adaptador.InsertCommand.ExecuteNonQuery();

        //    Desconectar();
        //}

        //public DataSet LlenarGrid(String query, DataGridView grid)
        //{
        //    connection.Open();

        //    OracleCommand comando = new OracleCommand(query, connection);

        //    comando.CommandTimeout = 0;

        //    OracleDataAdapter adapter = new OracleDataAdapter(comando);

        //    DataSet ds = new DataSet();

        //    adapter.Fill(ds);

        //    grid.DataSource = ds.Tables[0];

        //    Desconectar();

        //    return ds;
        //}
        ////public DataSet LlenarCombo(String query, ComboBox comboBox, string id, string descripcion, string nombreTabla)
        ////{
        ////    Conectar();

        ////    SqlCommand comando = new SqlCommand(query, connection);

        ////    comando.CommandTimeout = 0;

        ////    SqlDataAdapter adapter = new SqlDataAdapter(comando);

        ////    DataSet ds = new DataSet();

        ////    adapter.Fill(ds, nombreTabla);

        ////    comboBox.DataSource = ds.Tables[0].DefaultView;
        ////    comboBox.ValueMember = id;
        ////    comboBox.DisplayMember = descripcion;

        ////    Desconectar();

        ////    return ds;
        ////}


    }
}
