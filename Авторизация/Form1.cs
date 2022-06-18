using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Авторизация
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue800, Primary.LightBlue900, Accent.Blue700, TextShade.WHITE);
        }

        Form2 registr = new Form2();

        Admin admin = new Admin();
        Sotrudnik sotrudnik = new Sotrudnik();
        Client client = new Client();
        MySqlConnection conn = DBUtils.DBConnection();


        private void button1_Click(object sender, EventArgs e)
        {
            registr.Show();
            Hide();
        }

        // Хеширование.
        public static string SHA1_Hash(string text)
        {
            var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(text));
            string n_text = Convert.ToBase64String(hash);
            return n_text;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (login.Text != "" && password.Text != "")
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                conn.Open();
                MySqlCommand select = new MySqlCommand("SELECT * FROM users WHERE login = '" + SHA1_Hash(login.Text) + "' AND password = '" + SHA1_Hash(password.Text) + "'", conn);
                MySqlDataReader reader = select.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object Role = reader.GetValue(5);
                        if (Convert.ToString(Role) == "101")
                        {
                            Hide();
                            Nikname.Text = login.Text;
                            admin.Show();
                        }
                        else if (Convert.ToString(Role) == "102")
                        {
                            Hide();
                            Nikname.Text = login.Text;
                            sotrudnik.Show();
                        }
                        else
                        {
                            Hide();
                            Nikname.Text = login.Text;
                            client.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                reader.Close();
                conn.Close();
            }
            else 
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                conn.Close();
            }
        }
    }
}
