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
    public partial class Sotrud_Bilet : MaterialForm
    {
        public MySqlConnection conn = DBUtils.DBConnection();
        public MySqlDataAdapter adapter;
        public MySqlDataAdapter adapter1;
        public MySqlCommand cmd;
        public MySqlCommand select;
        public DataTable table;
        public DataTable table1;
        public MySqlDataReader reader;

        // Переменные
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
        public Sotrud_Bilet()
        {
            InitializeComponent();

            LoadData_Films();
            LoadData_Bilets();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        // Вывод таблиц.
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
        private void LoadData_Bilets()
        {
            select = new MySqlCommand("CALL bilets_S();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(select);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;

            conn.Close();
        }
        // Берет переменные из вывода
        public void Seans()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT seans.zal, seans.id FROM seans INNER JOIN films ON seans.film = films.id INNER JOIN zals ON seans.zal = zals.id WHERE films.nazv = '" + FILMS.Text + "'; ", conn);
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
                cmd = new MySqlCommand("SELECT seans.id FROM seans INNER JOIN WHERE nazv = '" + FILMS.Text + "' AND date_start = '" + Date.Text + "'; ", conn);
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
            if (paymast.Text == "Наличные")
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
            }
            else if (paymast.Text == "Безналичные")
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
                cmd = new MySqlCommand("INSERT INTO bilets(datetime, seans, zal, mesta, price) VALUES('" + datetime + "', '" + seans + "', '" + zal + "' , '" + mesto + "', '" + sum + "'); ", conn);
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
        }

        public void Num_bilets()
        {
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT id FROM bilets WHERE mesta = '" + mesta.Text + "'; ", conn);
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
                cmd = new MySqlCommand("SELECT id FROM users WHERE fio = '" + name.Text + "'; ", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fio = reader.GetValue(0).ToString();
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

        // Оформление билетов.
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                datetime = Date.Text;
                film = FILMS.Text;
                mesto = mesta.Text;
                pay = paymast.Text;
                sum = Convert.ToString(rnd.Next(100, 500));

                // Поиск на совпадения.
                //Filmes();
                Seans();

                //Добавление записи.
                Bilets();
                Num_bilets();
                user();
                Dviz_bilets();

                MessageBox.Show("Номер билета: " + id + "\n" + "\n" + "\n" + "Название фильма: " + film + "\n" + "Дата и время начало: " + datetime + "\n" + "\n" + "Зал: " + zal + "\n" + "Место: " + mesto + "\n" + "Оплата: " + "Безналичные" + "\n" + "Сумма: " + sum + "\n");

                Client client = new Client();
                client.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Поиск фильмов.
        private void button1_Click(object sender, EventArgs e)
        {
            if (fil.Text != "")
            {
                conn.Open();

                table.Clear();
                select = new MySqlCommand("SELECT id as 'Номер', nazv as 'Название фильма', dlitelnost as 'Длительность', date_start as 'Начало', date_finish as 'Конец', company_prodakshn as 'Продакшн', reiting as 'Рейтинг' FROM films WHERE nazv = '" + fil.Text + "'; ", conn);
                cmd = new MySqlCommand("SELECT bilets.id AS 'Номер', bilets.datetime AS 'Время', films.nazv AS 'Фильм', bilets.zal AS 'Зал', mesta.id AS 'Место', bilets.price AS 'Цена' FROM bilets INNER JOIN seans ON bilets.seans = seans.film INNER JOIN films ON seans.film = films.id INNER JOIN mesta on bilets.mesta = mesta.id WHERE films.nazv ='" + fil.Text + "'; ", conn);

                adapter = new MySqlDataAdapter(select);
                adapter1 = new MySqlDataAdapter(cmd);
                table = new DataTable();
                table1 = new DataTable();
                adapter.Fill(table);
                adapter1.Fill(table1);
                dataGridView1.DataSource = table;
                dataGridView2.DataSource = table1;

                conn.Close();
            }
            else
            {
                table.Clear();
                LoadData_Films();
                LoadData_Bilets();
            }
        }
    }
}
