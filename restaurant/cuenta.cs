using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Globalization;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace restaurant
{
    public partial class cuenta : MaterialForm
    {
        int index;
        int indiceImagen;
        bool ban;
        int idPlato;
        int numCuenta;
        float total;
        private const string connMysql = "DataSource=localhost; Database=restaurant; Uid=adan; Pwd=123456;";
        MySqlConnection myconn = new MySqlConnection(connMysql);
        public cuenta(int index, bool ban)
        {
            this.index = index;//numero de mesa o cuenta
            this.ban = ban;
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            mostrarPlatos();
            iniciarCuenta();
        }

        public void iniciarCuenta()
        {
            if (!ban)//ban false es que no se tiene el id exacto de la cuenta, se tiene que buscar
            {
                try
                {
                    myconn.Open();
                    MySqlCommand cmd = new MySqlCommand("select id_cuenta from cuenta where status=1 and id_mesa="+index, myconn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        numCuenta=Convert.ToInt32(reader["id_cuenta"].ToString());
                    }
                    myconn.Close();
                    llenarPedidos(numCuenta);

                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else //se conoce la cuenta por el index
            {
                llenarPedidos(index);
            }
        }

        public void mostrarPlatos()
        {
            try
            {
                platoParaLista platoInfo;
                List<platoParaLista> platos = new List<platoParaLista>();
                byte[] rawData;
                UInt32 tamanio;
                List<byte> array = new List<byte>();
                MemoryStream ms = null;
                Bitmap bm = null;
                string nombre;
                myconn.Open();
                MySqlCommand cmd = new MySqlCommand("select foto,nombre,long_foto  from plato", myconn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("no contiene imagenes aun");
                }
                while (reader.Read())
                {

                    nombre = reader["nombre"].ToString();
                    tamanio = reader.GetUInt32(reader.GetOrdinal("long_foto"));
                    rawData = new byte[tamanio];
                    reader.GetBytes(reader.GetOrdinal("foto"), 0, rawData, 0, (Int32)tamanio);
                    ms = new MemoryStream(rawData);
                    bm = new Bitmap(ms);
                    platos.Add(new platoParaLista(bm, nombre, tamanio));
                }
                ms.Close();
                ms.Dispose();
                reader.Close();
                reader.Dispose();
                myconn.Close();
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.Name = "foto";
                if (platos != null)
                {

                    dataGridView1.Columns.Add("", "");
                    dataGridView1.Columns.Add(imageCol);
                    for (int i = 1; i < platos.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                    }


                    for (int i = 0; i < platos.Count; i++)
                    {
                        platoInfo = platos[i];
                        dataGridView1[0, i].Value = platoInfo.nombre;
                        dataGridView1[1, i].Value = platoInfo.bm;

                    }
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AllowUserToResizeColumns = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        public void llenarPedidos(int numeroDeCuenta)
        {

            string query = "SELECT P.nombre,P.precio FROM plato P INNER JOIN detalle_cuenta DC ON P.id_plato = DC.id_plato where DC.id_cuenta="+numeroDeCuenta;
            myconn.Open();
            DataTable table = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, myconn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            myconn.Close();
        }

        public void precios()
        {
            int numeroDeCuenta;

                if (ban)// depende si se acabo de crear o no la cuenta es donde se almacena el id
                    numeroDeCuenta = index;
                else
                    numeroDeCuenta = numCuenta;

                try
            {
                string query = "SELECT P.precio FROM plato P INNER JOIN detalle_cuenta DC ON P.id_plato = DC.id_plato where DC.id_cuenta=" + numeroDeCuenta;
                myconn.Open();
                MySqlCommand cmd = new MySqlCommand(query, myconn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                  total+= float.Parse(reader["precio"].ToString(),CultureInfo.InvariantCulture.NumberFormat);
                }
                myconn.Close();
                MessageBox.Show(total.ToString());
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel1.Visible = true;
            idPlato=e.RowIndex;
            idPlato++;        
            myconn.Open();
            MySqlCommand cmd = new MySqlCommand("select nombre, descripcion, precio from plato where id_plato="+idPlato, myconn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                labelNombre.Text = reader["nombre"].ToString();
                labelPrecio.Text = reader["precio"].ToString();
                labelDescripcion.Text = reader["descripcion"].ToString();
            }
            myconn.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            int numeroDeCuenta;
            try
            {
                if (ban)// depende si se acabo de crear o no la cuenta es donde se almacena el id
                {
                    numeroDeCuenta = index;
                }
                else
                {
                    numeroDeCuenta = numCuenta;
                }

                myconn.Open();
                String query = "INSERT INTO detalle_cuenta (id_cuenta,id_plato) VALUES (?cuenta, ?plato)";
                MySqlCommand cmd = new MySqlCommand(query, myconn);

                cmd.Parameters.AddWithValue("?cuenta", numeroDeCuenta);
                cmd.Parameters.AddWithValue("?plato", idPlato);
                cmd.ExecuteNonQuery();
                myconn.Close();
                panel1.Visible = false;
                llenarPedidos(numeroDeCuenta);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            precios();

           if (!Directory.Exists(@" C: \Users\Adán\Documents\Visual Studio 2015\Projects\restaurant")) { 
                Directory.CreateDirectory(@"C: \Users\Adán\Documents\Visual Studio 2015\Projects\restaurant");
            }

            TextWriter sw = new StreamWriter(@"C: \Users\Adán\Documents\Visual Studio 2015\Projects\restaurant\cuentaImprimir.txt");
            int rowcount = dataGridView2.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                sw.WriteLine(dataGridView2.Rows[i].Cells[0].Value.ToString() + "\t \t \t"
                             + dataGridView2.Rows[i].Cells[1].Value.ToString() + "\t \t \t");
            }
            sw.WriteLine("\t \t \t el total es: " + total);
            sw.Close();
            cambiarStatus();
            MessageBox.Show("ticket listo");
            Process.Start(@"C:\Users\Adán\Documents\Visual Studio 2015\Projects\restaurant\cuentaImprimir.txt");
            total = 0;
            user u = new user();
            u.Show();
            this.Close();
        }

        public void cambiarStatus()
        {
            int numeroDeCuenta;
            try
            {
                if (ban)// depende si se acabo de crear o no la cuenta es donde se almacena el id
                {
                    numeroDeCuenta = index;
                }
                else
                {
                    numeroDeCuenta = numCuenta;
                }
                myconn.Open();
                String query = "INSERT INTO detalle_cuenta (id_cuenta,id_plato) VALUES (?cuenta, ?plato)";
                query = "update cuenta set total=?total,status=0 where id_cuenta =?numCuenta";
                MySqlCommand cmd = new MySqlCommand(query, myconn);
                cmd.Parameters.AddWithValue("?total", total);
                cmd.Parameters.AddWithValue("?numCuenta", numeroDeCuenta);
                cmd.ExecuteNonQuery();
                myconn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
