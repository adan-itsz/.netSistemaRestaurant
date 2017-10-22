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
using MySql.Data.MySqlClient;
using System.IO;

namespace restaurant
{
    public partial class infoPlato : MaterialForm
    {
        private const string connMysql = "DataSource=localhost; Database=restaurant; Uid=adan; Pwd=123456;";
        MySqlConnection myconn = new MySqlConnection(connMysql);
        int index;
        public infoPlato(int index)
        {
            this.index = index+1;
            MessageBox.Show(this.index.ToString());
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            cargarInfo();
        }

        public void cargarInfo()
        {
           
            byte[] rawData;
            UInt32 tamanio;
            List<byte> array = new List<byte>();
            MemoryStream ms;
            Bitmap bm = null;
            try
            {
                myconn.Open();
                MySqlCommand cmd = new MySqlCommand("select P.foto,P.long_foto, P.nombre, P.descripcion,P.nivel_dificultad,P.precio, R.pasos, R.ingredientes  from plato P inner join receta R on R.id_receta=P.id_receta where P.id_plato='" + index + "'", myconn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new Exception("no contiene imagenes aun");
                }

                reader.Read();
                tamanio = reader.GetUInt32(reader.GetOrdinal("long_foto"));
                rawData = new byte[tamanio];
                reader.GetBytes(reader.GetOrdinal("foto"), 0, rawData, 0, (Int32)tamanio);
                ms = new MemoryStream(rawData);
                bm = new Bitmap(ms);
                labelPlato.Text = reader["nombre"].ToString();
                labelDescripcion.Text = reader["descripcion"].ToString();
                labelDificultad.Text = reader["nivel_dificultad"].ToString();
                labelPrecio.Text = reader["precio"].ToString();
                string ing= reader["ingredientes"].ToString();
                txtPasos.Text = reader["pasos"].ToString();
                ms.Close();
                ms.Dispose();
                reader.Close();
                reader.Dispose();
                myconn.Close();
                List<string> lis = sacarIngredientes(ing);
                foreach(string elemento in lis)
                {
                    txtIngredientes.Text += elemento + "\r\n";
                }
                pictureBox1.Image = bm;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public List<string> sacarIngredientes(string tira)
        {
            List<string> resultados = new List<string>();
            string value = "";
            int j;
            for(int i = 0; i < tira.Length; i++)
            {
                    if (tira[i] == '~')
                    {
                        for( j = i + 1; j < tira.Length; j++)
                        {
                            if (tira[j] != '~')
                            {
                                value += tira[j];
                            }
                            else if(tira[j]=='~'&& j!=0 || j + 1 == tira.Length)
                            {
                            resultados.Add(value);
                            value = "";
                            }                     
                        }
                    if (j == tira.Length)
                    {
                        break;
                    }
                    }
                
            }
            return resultados;
        }
    }
}
