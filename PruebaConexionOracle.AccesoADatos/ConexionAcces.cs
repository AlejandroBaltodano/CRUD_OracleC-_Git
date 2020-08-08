using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace PruebaConexionOracle.AccesoADatos
{
   public class ConexionAcces
    {
        OleDbConnection connection;

        public ConexionAcces()
        {

            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Creyes/Documents/PruebaMigracion.accdb");

        }

        public OleDbConnection Conectar()
        {
            try
            {
                Desconectar();
            }
            catch { }

            connection.Open();

            return connection;
        }

        public void Desconectar()
        {
            connection.Close();
        }

        public OleDbDataReader Query(String query)
        {
            Conectar();

            OleDbCommand comando = new OleDbCommand(query, connection);

            return comando.ExecuteReader();
        }

        public void Update(String query)
        {
            Conectar();

            OleDbCommand comando = new OleDbCommand(query, connection);

            comando.ExecuteNonQuery();

            Desconectar();
        }

        public DataSet LlenarGrid(String query, DataGridView grid)
        {
            Conectar();

            OleDbCommand comando = new OleDbCommand(query, connection);

            comando.CommandTimeout = 0;

            OleDbDataAdapter adapter = new OleDbDataAdapter(comando);

            DataSet ds = new DataSet();

            adapter.Fill(ds);

            grid.DataSource = ds.Tables[0];

            Desconectar();

            return ds;
        }
        public DataSet LlenarCombo(String query, ComboBox comboBox, string id, string descripcion, string nombreTabla)
        {
            Conectar();

            OleDbCommand comando = new OleDbCommand(query, connection);

            comando.CommandTimeout = 0;

            OleDbDataAdapter adapter = new OleDbDataAdapter(comando);

            DataSet ds = new DataSet();

            adapter.Fill(ds, nombreTabla);

            comboBox.DataSource = ds.Tables[0].DefaultView;
            comboBox.ValueMember = id;
            comboBox.DisplayMember = descripcion;

            Desconectar();

            return ds;
        }


        public DataSet obtenerRegistrosAcces(String query, string tabla)
        {
            Conectar();

            OleDbCommand comando = new OleDbCommand(query, connection);

            comando.CommandTimeout = 0;

            OleDbDataAdapter adapter = new OleDbDataAdapter(comando);

            DataSet ds = new DataSet();

            adapter.Fill(ds,tabla);

            return ds;
           
        }







    }
}
