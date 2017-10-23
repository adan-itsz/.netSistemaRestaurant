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

namespace restaurant
{
    public partial class adminPrincipal : MaterialForm
    {
        private const string connMysql = "DataSource=localhost; Database=restaurant; Uid=adan; Pwd=123456;";
        MySqlConnection myconn = new MySqlConnection(connMysql);
        public adminPrincipal()
        {
            InitializeComponent();
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            cargarIngredientes();
            cargarEmpleados();
            llenarComboBox();
            cargarCategorias();
            cargarPlatos();

            pictureBox1.Image = Image.FromFile("c:\\beacon.png");
        }

        private void adminPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                float medidaCantidad;
                String ingrediente = input1.Text;
                String cantidad = input2.Text;
                
                    medidaCantidad = float.Parse(cantidad, CultureInfo.InvariantCulture.NumberFormat);
              
                myconn.Open();
                String query= "INSERT INTO ingrediente (nombre, cantidad) VALUES (?ingrediente, ?medidaCantidad)";
                MySqlCommand cmd = new MySqlCommand(query, myconn);

                cmd.Parameters.AddWithValue("?ingrediente", ingrediente);
                cmd.Parameters.AddWithValue("?medidaCantidad", medidaCantidad);
                cmd.ExecuteNonQuery();
                myconn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
        }

        public void cargarPlatos()
        {
            try
            {
                platoParaLista platoInfo;
                List<platoParaLista> platos = new List<platoParaLista>();
                byte[] rawData;
                UInt32 tamanio;
                List<byte> array = new List<byte>();
                MemoryStream ms=null;
                Bitmap bm = null;
                string nombre;
                myconn.Open();
                MySqlCommand cmd = new MySqlCommand("select foto,nombre,long_foto  from plato", myconn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("no contiene imagenes aun");
                }
                while (reader.Read()) {

                    nombre=reader["nombre"].ToString();
                    tamanio = reader.GetUInt32(reader.GetOrdinal("long_foto"));
                    rawData = new byte[tamanio];
                    reader.GetBytes(reader.GetOrdinal("foto"), 0, rawData, 0, (Int32)tamanio);
                    ms = new MemoryStream(rawData);
                    bm = new Bitmap(ms);
                    platos.Add(new platoParaLista( bm, nombre, tamanio));
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

                    dataGridView4.Columns.Add("", "");
                    dataGridView4.Columns.Add(imageCol);
                    for (int i = 1; i < platos.Count; i++)
                    {
                        dataGridView4.Rows.Add();
                    }


                    for (int i = 0; i < platos.Count; i++)
                    {
                        platoInfo = platos[i];
                         dataGridView4[0, i].Value = platoInfo.nombre;
                         dataGridView4[1, i].Value = platoInfo.bm;
                        
                    }
                    dataGridView4.AutoResizeColumns();
                    dataGridView4.AllowUserToResizeColumns = true;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        public Image regresar(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {

            }
            return bm;

        }

        public void cargarCategorias()
        {
            string query = "SELECT C.nombre,C.descripcion, E.nombre FROM categoria C INNER JOIN encargado E ON C.id_encargado = E.id_encargado";
            myconn.Open();
            DataTable table = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, myconn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            dataGridView3.DataSource = table;
            myconn.Close();

        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }
        private void cargarIngredientes()
        {
            myconn.Open();
            DataTable table = new DataTable();
            MySqlCommand cmd = new MySqlCommand("select * from ingrediente", myconn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            myconn.Close();
            

        }

        private void cargarEmpleados()
        {
            try
            {
                myconn.Open();
                DataTable table = new DataTable();
                table.Clear();
                MySqlCommand cmd = new MySqlCommand("select * from encargado", myconn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
                dataGridView2.DataSource = table;
                myconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error "+ex);
            }
        }

        private void llenarComboBox()
        {
            List<string> empelados = new List<string>();
            myconn.Open();
            MySqlCommand cmd = new MySqlCommand("select nombre from encargado", myconn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                empelados.Add(reader["nombre"].ToString());
                cBoxEncargadoCategoria.Items.Add(reader["nombre"].ToString());
            }
            myconn.Close();

        }

        private void btnAceptarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                string puesto = txtPuestoEmpleado.Text;
                String nombre = txtNombreEmpleado.Text;
                String telefono = txtTelefonoEmpleado.Text;
                myconn.Open();
                String query = "INSERT INTO encargado (nombre, puesto, telefono) VALUES (?nombre, ?puesto,?telefono)";
                MySqlCommand cmd = new MySqlCommand(query, myconn);

                cmd.Parameters.AddWithValue("?nombre", nombre);
                cmd.Parameters.AddWithValue("?puesto",puesto );
                cmd.Parameters.AddWithValue("?telefono", telefono);
                cmd.ExecuteNonQuery();
                myconn.Close();
                cargarEmpleados();
                txtNombreEmpleado.Clear();
                txtPuestoEmpleado.Clear();
                txtTelefonoEmpleado.Clear();
                panel2.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
        }

        private void btnCancelarEmpleado_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e) //btn aceptar categoria
        {
            try
            {
                string nombre = txtNombreCategoria.Text;
                string descripcion = txtDescripcionCategoria.Text;
                int encargado = cBoxEncargadoCategoria.SelectedIndex +1;
                MessageBox.Show("num " + encargado);
                string encargadoBD = encargado.ToString();

                myconn.Open();
                String query = "INSERT INTO categoria (nombre, descripcion, id_encargado) VALUES (?nombre, ?descripcion,?encargado)";
                MySqlCommand cmd = new MySqlCommand(query, myconn);

                cmd.Parameters.AddWithValue("?nombre", nombre);
                cmd.Parameters.AddWithValue("?descripcion", descripcion);
                cmd.Parameters.AddWithValue("?encargado", encargadoBD);
                cmd.ExecuteNonQuery();
                myconn.Close();
                txtDescripcionCategoria.Clear();
                txtDescripcionCategoria.Clear();
                panel3.Visible = false;

            }
            catch(Exception ex)
            {
                MessageBox.Show("llenar correctamente " + ex);
            }
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            addPlatillo a = new addPlatillo();
            a.Show();
        }

        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         
            infoPlato ip = new infoPlato(e.RowIndex);
            ip.Show();
        
        }
    }


    

    
}
