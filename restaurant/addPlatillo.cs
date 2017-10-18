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
using System.Drawing;
using System.IO;

namespace restaurant
{
    public partial class addPlatillo : MaterialForm
    {
        private const string connMysql = "DataSource=localhost; Database=restaurant; Uid=adan; Pwd=123456;";
        MySqlConnection myconn = new MySqlConnection(connMysql);
        int imgLength;
        MySqlConnection myconn2 = new MySqlConnection(connMysql);
        Image imagen1;
        string imagen;
        List<string> listaIngredientes = new List<string>();
        string totalidadIngredientes= "~";
        public addPlatillo()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            cargarComboBoxs();
        }

        private void btnAddImagen_Click(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    imagen = openFileDialog1.FileName;
                    MessageBox.Show(imagen);
                    pictureBox1.Image = Image.FromFile(imagen);
                    imagen1= Image.FromFile(imagen); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
            }
        }

        public void cargarComboBoxs()
        {
            
            myconn.Open();
            MySqlCommand cmd = new MySqlCommand("select nombre from categoria", myconn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cBoxCategoria.Items.Add(reader["nombre"].ToString());
            }
            myconn.Close();
            myconn.Open();

            cmd = new MySqlCommand("select nombre from ingrediente", myconn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cBoxIngredientesReceta.Items.Add(reader["nombre"].ToString());
            }

            reader.Close();
            myconn.Close();


        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            
            String ing = cBoxIngredientesReceta.SelectedItem.ToString();
            listView1.Items.Add(ing);
            listaIngredientes.Add(ing);

           
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            string nombre = txtNombrePlatillo.Text;
            MessageBox.Show(nombre);
            string descripcion = txtDescripcionPlatillo.Text;
            string dificultad = cBoxDificultad.Text;
            string pasosReceta = txtPasosReceta.Text;
            int categoria = cBoxCategoria.SelectedIndex +1;
            float precio = float.Parse(txtPrecio.Text, CultureInfo.InvariantCulture.NumberFormat);
            //falta imagen
           foreach(string elemento in listaIngredientes)
            {
                totalidadIngredientes += elemento + "~";
            }

            byte[] imagen = deImagenAByte(imagen1);



            try {
                //receta
                myconn2.Open();
                String query = "INSERT INTO receta (nombre, descripcion,ingredientes) VALUES (?nombre, ?descripcion,?ingredientes)";
                MySqlCommand cmd = new MySqlCommand(query, myconn2);
               

                cmd.Parameters.AddWithValue("?nombre", nombre);
                cmd.Parameters.AddWithValue("?descripcion", pasosReceta);
                cmd.Parameters.AddWithValue("?ingredientes", totalidadIngredientes);
                cmd.ExecuteNonQuery();
                myconn2.Close();
           
                myconn.Open();
                query = "select max(id_receta) from receta";
                cmd = new MySqlCommand(query, myconn);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                //int index = Convert.ToInt32( reader.ToString());
                int index = 1;
                reader.Close();
                myconn.Close();
                //plato
                myconn.Open();
                 query = "INSERT INTO plato (nombre, descripcion,nivel_dificultad,foto,precio, id_categoria,id_receta,long_foto ) VALUES (?nombre, ?descripcion,?dificultad,?foto,?precio,?categoria,?receta,?long)";
                 cmd = new MySqlCommand(query, myconn);

                cmd.Parameters.AddWithValue("?nombre", nombre);
                cmd.Parameters.AddWithValue("?descripcion", descripcion);
                cmd.Parameters.AddWithValue("?dificultad", dificultad);
                cmd.Parameters.AddWithValue("?foto", imagen);
                cmd.Parameters.AddWithValue("?precio", precio);
                cmd.Parameters.AddWithValue("?categoria", categoria);
                cmd.Parameters.AddWithValue("?receta", index);
                cmd.Parameters.AddWithValue("?long", imgLength);
                cmd.ExecuteNonQuery();
                myconn.Close();
                this.Close();


            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public byte[] deImagenAByte(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Position = 0;

            imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;

        }

   
    }


    
}
