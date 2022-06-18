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
    public partial class Sotrudnik : MaterialForm
    {
        public MySqlConnection conn = DBUtils.DBConnection();
        public DataSet CinemaCenter;
        public MySqlDataAdapter adapter;
        public MySqlDataAdapter adapter1;
        public MySqlCommand select;
        public MySqlCommand select1;
        public DataTable table;
        public DataTable table1;
        public MySqlDataReader reader;
        public MySqlCommandBuilder builder;

        public string id;

        public Sotrudnik()
        {
            InitializeComponent();

            LoadData_Films();
            LoadData_Seans();
            LoadData_Bilets();
            LoadData_Reiting();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Sotrudnik_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "centercinemaDataSet.bilets_S". При необходимости она может быть перемещена или удалена.
            this.bilets_STableAdapter.Fill(this.centercinemaDataSet.bilets_S);

            label1.Text = Nikname.Text;
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        // Форма Авторизация. 
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 avtoriz = new Form1();
            avtoriz.Show();
            Close();
        }
        // Берет id из таблицы Фильмы. 
        private void Nazv_Film()
        {
            try
            {
                conn.Open();
                select = new MySqlCommand("SELECT id FROM films WHERE nazv = '" + Films_KOD.Text + "'; ", conn);
                reader = select.ExecuteReader();
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

        private void LoadData_Reiting()
        {
            select = new MySqlCommand("CALL reiting_S();", conn);
            conn.Open();

            adapter = new MySqlDataAdapter(select);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView4.DataSource = table;

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

        // Поиск записей.
        private void button2_Click(object sender, EventArgs e)
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
            else {
                table.Clear();
                LoadData_Seans();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nazvFilm.Text != "")
            {
                table.Clear();

                select = new MySqlCommand("select id as 'Номер', nazv as 'Название фильма', dlitelnost as 'Длительность', date_start as 'Начало', date_finish as 'Конец', company_prodakshn as 'Продакшн', reiting as 'Рейтинг' from films WHERE nazv = '" + nazvFilm.Text + "';", conn);
                select1 = new MySqlCommand("select id as 'Номер', film as 'Фильм', reiting as 'Рейтинг', comment as 'Отзыв' from reiting where film = '" + nazvFilm.Text + "';", conn);
                conn.Open();

                adapter = new MySqlDataAdapter(select);
                adapter1 = new MySqlDataAdapter(select1);
                table = new DataTable();
                table1 = new DataTable();
                adapter.Fill(table);
                adapter1.Fill(table1);
                dataGridView1.DataSource = table;
                dataGridView4.DataSource = table1;

                conn.Close();
            }
            else
            {
                table.Clear();
                LoadData_Films();
                LoadData_Reiting();
            }
        }
        // Добавление записи.
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if(Nazv.Text != "" && Dlitelnost.Text != "" && Start.Text != "" && Finish.Text != "" && Product.Text != "") 
            {
                conn.Open();
                label2.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;

                try
                {
                    select = new MySqlCommand("INSERT INTO films(nazv, dlitelnost, date_start, date_finish, company_prodakshn) values('" + Nazv.Text + "', '" + Dlitelnost.Text + "', '" + Start.Text + "', '" + Finish.Text + "', '" + Product.Text + "');", conn);
                    reader = select.ExecuteReader();

                    MessageBox.Show("Запись добавлена!");
                    conn.Close();

                    table.Clear();
                    dataGridView1.DataSource = table;
                    LoadData_Films();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                label2.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;

                label2.Text = "Не заполены поля!";
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            if(Number_Zal.Text != "" && Date.Text != "" && Time.Text != "" && Films_KOD.Text != "")
            {
                label13.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;

                try
                {
                    Nazv_Film();
                    conn.Open();
                    select = new MySqlCommand("INSERT INTO seans (zal, date, time, film) values('" + Number_Zal.Text + "', '" + Date.Text + "', '" + Time.Text + "', '" + id + "');", conn);
                    reader = select.ExecuteReader();

                    MessageBox.Show("Запись добавлена!");
                    conn.Close();

                    table.Clear();
                    dataGridView1.DataSource = table;
                    LoadData_Seans();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                label13.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                label22.Visible = true;
                label23.Visible = true;

                label13.Text = "Не заполены поля!";
            }
        }
        // Обновление записи.
        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            if (Number_Zal.Text != "" && Date.Text != "" && Time.Text != "" && Films_KOD.Text != "")
            {
                Nazv_Film();
                conn.Open();
                label13.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;


                try
                {
                    select = new MySqlCommand("update seans set zal = '" + Number_Zal.Text + "', date = '" + Date.Text + "', time = '" + Time.Text + "' where film = '" + id + "';", conn);

                    if (select.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Запись изменена!");
                        conn.Close();

                        table.Clear();
                        dataGridView2.DataSource = table;
                        LoadData_Seans();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                        conn.Close();
                    }
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
                label13.Text = "Не заполены поля!";

                label13.Visible = true;
                label20.Visible = true;
                label21.Visible = true;
                label22.Visible = true;
                label23.Visible = true;

            }
        }
        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            if (Nazv.Text != "" && Dlitelnost.Text != "" && Start.Text != "" && Finish.Text != "" && Product.Text != "" && Reiting.Text != "")
            {
                conn.Open();
                label2.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;

                try
                {
                    select = new MySqlCommand("update films set dlitelnost = '" + Dlitelnost.Text + "', date_start = '" + Start.Text + "', date_finish = '" + Finish.Text + "', company_prodakshn = '" + Product.Text + "', reiting = '" + Reiting.Text + "' where nazv = '" + Nazv.Text + "';", conn);
                    reader = select.ExecuteReader();

                    MessageBox.Show("Запись изменена!");
                    conn.Close();

                    table.Clear();
                    dataGridView1.DataSource = table;
                    LoadData_Films();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                label2.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;

                label2.Text = "Не заполены поля!";
            }
        }

        // Оформление билета
        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Sotrud_Bilet part1 = new Sotrud_Bilet();
            part1.Show();
            Close();
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            try
            {
                
                select = new MySqlCommand("select (sum(reiting)/count(id)) as 'Средний рейтинг' from reiting where film = '"+ nazvFilm.Text +"';", conn);
                conn.Open();

                adapter = new MySqlDataAdapter(select);
                table = new DataTable();
                adapter.Fill(table);
                dataGridView4.DataSource = table;

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }
    }
}