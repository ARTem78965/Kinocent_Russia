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
    public partial class Client : MaterialForm
    {
        public MySqlConnection conn = DBUtils.DBConnection();
        public DataSet CinemaCenter;
        public MySqlDataAdapter adapter;
        public MySqlCommand select;
        public DataTable table;
        public MySqlDataReader reader;
        public Client()
        {
            InitializeComponent();

            LoadData_Films();
            LoadData_Seans();
            LoadData_Bilets();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            label1.Text = Nikname.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 avtoriz = new Form1();
            avtoriz.Show();
            Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MySqlCommand reiting = new MySqlCommand("CALL avg_reiting();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(reiting);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            conn.Close();
        }

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

        private void LoadData_Seans()
        {
            select = new MySqlCommand("CALL seans_S();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(select);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;

            conn.Close();
        }

        private void LoadData_Bilets()
        {
            select = new MySqlCommand("CALL bilets_S();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(select);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;

            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nazvFilm.Text != "")
            {
                table.Clear();

                select = new MySqlCommand("select id as 'Номер', nazv as 'Название фильма', dlitelnost as 'Длительность', date_start as 'Начало', date_finish as 'Конец', company_prodakshn as 'Продакшн', reiting as 'Рейтинг' from films WHERE nazv = '" + nazvFilm.Text + "';", conn);
                conn.Open();

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (Film.Text != "")
            {
                table.Clear();

                select = new MySqlCommand("SELECT seans.id AS 'Номер', seans.zal AS 'Зал', seans.date AS 'Дата', seans.time AS 'Время', films.nazv AS 'Фильм' FROM seans  INNER JOIN films ON seans.film = films.id WHERE films.nazv = '" + Film.Text + "';", conn);
                conn.Open();

                adapter = new MySqlDataAdapter(select);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;

                conn.Close();
            }
            else
            {
                table.Clear();
                LoadData_Seans();
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Bilet_Client part2 = new Bilet_Client();
            part2.Show();
            Close();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            Reiting reiting = new Reiting();
            reiting.Show();
            Hide();
        }
    }
}
