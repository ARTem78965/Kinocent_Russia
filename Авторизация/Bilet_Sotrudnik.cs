
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Авторизация
{
    public partial class Bilet_1 : MaterialForm
    {
        public MySqlConnection conn = DBUtils.DBConnection();
        public MySqlDataAdapter adapter;
        public MySqlCommand cmd;
        public MySqlCommand select;
        public DataTable table;
        public MySqlDataReader reader;

        public string zal;
        public string seans;
        public string fio;
        public string datetime;
        public string film;
        public string mesto;
        public string pay;
        public string id;
        public string sum;

        Random rnd = new Random();

        public Bilet_1()
        {
            InitializeComponent();

            LoadData_Films();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }


        // Вывод фильмов.
        private void LoadData_Films()
        {
            select = new MySqlCommand("CALL films_S();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(select);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            conn.Close();
        }

        public void Seans()
        {
            
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT seans.zal, seans.id FROM seans INNER JOIN films ON seans.film = films.id INNER JOIN zals ON seans.zal = zals.id WHERE films.nazv = '" + Films.Text + "'; ", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    zal = reader.GetValue(0).ToString();
                    seans = reader.GetValue(1).ToString();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public void Filmes()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT seans.id FROM seans INNER JOIN WHERE nazv = '" + Films.Text + "' AND date_start = '" + DateTime.Text + "'; ", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seans = reader.GetValue(0).ToString();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public void Dviz_bilets()
        {
            if (Pay.Text == "Наличные")
            {
                try
                {
                    conn.Open();
                    cmd = new MySqlCommand("INSERT INTO dviz_bilets(number, date, operacia, users) VALUES('" + id + "', now(), 1, '" + fio + "');", conn);
                    reader = cmd.ExecuteReader();

                    MessageBox.Show("Запись добавлена!");

                    reader.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }else if (Pay.Text == "Безналичные")
            {
                try
                {
                    conn.Open();
                    cmd = new MySqlCommand("INSERT INTO dviz_bilets(number, date, operacia, users) VALUES('" + id + "', now(), 2, '" + fio + "');", conn);
                    reader = cmd.ExecuteReader();

                    reader.Close();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else 
            {
                MessageBox.Show("Вы не ввели способ оплаты", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public void Bilets()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT INTO bilets(datetime, seans, zal, mesta) VALUES('" + datetime + "', '" + seans + "', '" + zal + "' , '" + mesto + "'); ", conn);
                reader = cmd.ExecuteReader();

                MessageBox.Show("Запись добавлена!");

                reader.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public void Num_bilets()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT id FROM bilets WHERE mesta = '" + comboBox1.Text + "'; ", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        public void user()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT id FROM users WHERE fio = '" + FIO.Text + "'; ", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fio = reader.GetValue(0).ToString();
                }
                reader.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        // Оформление билетов.
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                datetime = DateTime.Text;
                film = Films.Text;
                mesto = comboBox1.Text;
                pay = Pay.Text;
                sum = Convert.ToString(rnd.Next(100, 500));

                // Поиск на совпадения.
                //Filmes();
                Seans();

                //Добавление записи.
                Bilets();
                Num_bilets();
                user();
                Dviz_bilets();

                MessageBox.Show("Номер билета: " + id + "\n" + "\n" + "\n" + "Название фильма: " + film + "\n" + "Дата и время начало: " + datetime + "\n" + "Клиент: " + fio + "\n" + "Зал: " + zal + "\n" + "Место: " + mesto + "\n" + "Оплата: " + pay + "\n" + "Сумма: " + sum + "\n");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Поиск фильмов.
        private void button3_Click(object sender, EventArgs e)
        {
            if (Film.Text != "")
            {
                conn.Open();


                table.Clear();
                select = new MySqlCommand("SELECT * FROM films WHERE nazv = '" + Film.Text + "';", conn);
                adapter = new MySqlDataAdapter(select);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                conn.Close();
            }
            else
            {
                table.Clear();
                LoadData_Films();
            }
        }
    }
}
