using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Авторизация
{
    public partial class Form2 : MaterialForm
    {

        public Form2()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue800, Primary.LightBlue900, Accent.Blue700, TextShade.WHITE);
        }

        MySqlConnection conn = DBUtils.DBConnection();

        // Хеширование.
        public static string SHA1_Hash(string value)
        {
            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(value));
            string n_text = Convert.ToBase64String(hash);
            return n_text;
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (fio.Text != "" && phone.Text != "" && login.Text != "" && password.Text != "" && role.Text != "")
            {
                conn.Open();
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;

                try
                {
                    if (role.Text == "Администратор")
                    {
                        if (password.Text == "Admin" || password.Text == "Администратор")
                        {
                            string index = "101";
                            MySqlCommand insert = new MySqlCommand("INSERT INTO users(fio, phone, login, role, password) values('" + fio.Text + "', '" + phone.Text + "', '" + SHA1_Hash(login.Text) + "', '" + index + "', '" + SHA1_Hash(password.Text) + "');", conn);
                            string number = insert.ExecuteNonQuery().ToString();
                            MessageBox.Show("Администратор добавлен!");
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("У вас недостаточно прав для администратора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (role.Text == "Сотрудник")
                    {
                        if (password.Text == "Сотрудник")
                        {
                            string index = "102";
                            MySqlCommand insert = new MySqlCommand("INSERT INTO users(fio, phone, login, role, password) values('" + fio.Text + "', '" + phone.Text + "', '" + SHA1_Hash(login.Text) + "', '" + index + "', '" + SHA1_Hash(password.Text) + "');", conn);
                            string number = insert.ExecuteNonQuery().ToString();
                            MessageBox.Show("Сотрудник добавлен!");
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("У вас недостаточно прав для сотрудника!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (role.Text == "Клиент")
                    {
                        string index = "103";
                        MySqlCommand insert = new MySqlCommand("INSERT INTO users(fio, phone, login, role, password) values('" + fio.Text + "', '" + phone.Text + "', '" + SHA1_Hash(login.Text) + "', '" + index + "', '" + SHA1_Hash(password.Text) + "');", conn);
                        string number = insert.ExecuteNonQuery().ToString();
                        MessageBox.Show("Пользователь добавлен!");
                        conn.Close();
                    }
                    Form1 avtoriz = new Form1();
                    avtoriz.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fio.Clear();
                    phone.Clear();
                    login.Clear();
                    password.Clear();
                }
                conn.Close();
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                conn.Close();
            }
        }
    }
}