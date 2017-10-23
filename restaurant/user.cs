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
    public partial class user : MaterialForm
    {
        private const string connMysql = "DataSource=localhost; Database=restaurant; Uid=adan; Pwd=123456;";
        MySqlConnection myconn = new MySqlConnection(connMysql);
        List<int> cuentasAbiertas = new List<int>();
        string iconoAbieto= @"C:\Users\Adán\Documents\Visual Studio 2015\Projects\restaurant\presionado.png";
        public user()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            revisarCuentas();
        }

        public string inserts(int mesa, DateTime fecha)
        {
            string index = "#";
            try
            {
                myconn.Open();
                String query = "INSERT INTO cuenta (id_mesa, fecha,total,status) VALUES (?mesa,?fecha,?total,?status)";
                MySqlCommand cmd = new MySqlCommand(query, myconn);
                cmd.Parameters.AddWithValue("?mesa", mesa);
                cmd.Parameters.AddWithValue("?fecha", fecha);
                cmd.Parameters.AddWithValue("?total", 0);
                cmd.Parameters.AddWithValue("?status", 1);
                cmd.ExecuteNonQuery();
                myconn.Close();

                myconn.Open();
                query = "select id_cuenta from cuenta where id_cuenta=(select max(id_cuenta) from cuenta)";
                cmd = new MySqlCommand(query, myconn);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                 index = reader["id_cuenta"].ToString();
                reader.Close();
                myconn.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return index;
        }
        public void revisarCuentas()
        {

            myconn.Open();
            MySqlCommand cmd = new MySqlCommand("select id_mesa from cuenta where status=1", myconn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cuentasAbiertas.Add(Convert.ToInt32(reader["id_mesa"].ToString()));
            }
            myconn.Close();
            if (cuentasAbiertas.Count > 0)
            {
                foreach(int element in cuentasAbiertas)
                {
                    switch (element)
                    {
                        case 1:
                            btn1.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 2:
                            btn2.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 3:
                            btn3.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 4:
                            btn4.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 5:
                            btn5.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 6:
                            btn6.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 7:
                            btn7.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 8:
                            btn8.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;
                        case 9:
                            btn9.BackgroundImage = Image.FromFile(iconoAbieto);
                            break;

                    }
                } 

            }
        }

        public void accederCuenta(int mesa) //o crear
        {
            cuenta c;
            int idCuenta;
            if (cuentasAbiertas.Contains(mesa))
            {
                c = new cuenta(mesa, false);//envio 1 por mesa 1 y false porque no contiene el id exacto de cuenta y hara falta buscarlo
                c.Show();

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("No existe ninguna cuenta abierta en esta mesa, ¿quieres crear una?", "Abrir cuenta", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DateTime thisDay = DateTime.Today;
                    idCuenta = Convert.ToInt32(inserts(mesa, thisDay));
                    btn1.BackgroundImage = Image.FromFile(iconoAbieto);
                    c = new cuenta(idCuenta, true);
                    c.Show();
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            accederCuenta(1);
           
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            accederCuenta(2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            accederCuenta(3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            accederCuenta(4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            accederCuenta(5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            accederCuenta(6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            accederCuenta(7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            accederCuenta(8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            accederCuenta(9);
        }
    }
}
