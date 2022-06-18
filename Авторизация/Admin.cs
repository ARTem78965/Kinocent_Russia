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

namespace Авторизация
{
    public partial class Admin : MaterialForm
    {
        private const string users = "users";
        private const string role = "role";
        private const string dviz_bilet = "dviz_bilets";
        private const string bilet = "bilets";
        private const string seans = "seans";
        private const string mesta = "mesta";
        private const string zal = "zals";
        private const string films = "films";

        public MySqlConnection conn = DBUtils.DBConnection();
        public DataSet CinemaCenter;
        public MySqlDataAdapter adapter;
        public MySqlCommand command;
        public DataTable table;
        


        public Admin()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            label1.Text = Nikname.Text;

            CinemaCenter = new DataSet();
            adapter = new MySqlDataAdapter();
            CinemaCenter.Tables.Add(users);
            CinemaCenter.Tables.Add(role);
            CinemaCenter.Tables.Add(dviz_bilet);
            CinemaCenter.Tables.Add(bilet);
            CinemaCenter.Tables.Add(seans);
            CinemaCenter.Tables.Add(mesta);
            CinemaCenter.Tables.Add(zal);
            CinemaCenter.Tables.Add(films);
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand();
            command.Connection = conn;                        
            command.CommandType = CommandType.StoredProcedure;

            treeView1.Nodes.Add("Киноцентр", "Киноцентр", 0, 0);
            treeView1.Nodes[0].Nodes.Add(users, "Пользователи", 1, 1);
            treeView1.Nodes[0].Nodes.Add(role, "Привелегии", 2, 1);
            treeView1.Nodes[0].Nodes.Add(dviz_bilet, "Движение билетов", 3, 1);
            treeView1.Nodes[0].Nodes.Add(bilet, "Билеты", 4, 1);
            treeView1.Nodes[0].Nodes.Add(seans, "Сеансы", 5, 1);
            treeView1.Nodes[0].Nodes.Add(mesta, "Места", 6, 1);
            treeView1.Nodes[0].Nodes.Add(zal, "Залы", 7, 1);
            treeView1.Nodes[0].Nodes.Add(films, "Фильмы", 8, 1);
            treeView1.ExpandAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 avtoriz = new Form1();
            avtoriz.Show();
            Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try {
                if (treeView1.SelectedNode.Parent != null)
                {

                    string enityName = treeView1.SelectedNode.Name;
                    table = CinemaCenter.Tables[enityName];
                    command.CommandText = enityName + "_S";
                    adapter.SelectCommand = command;
                    table.Rows.Clear();
                    adapter.Fill(table);                                        
                    conn.Open();
                    MySqlCommandBuilder cd = new MySqlCommandBuilder(adapter);
                    dataGridView1.DataSource = table;
                    enityName = treeView1.SelectedNode.Name;
                    bindingSource1.DataSource = CinemaCenter;
                    bindingSource1.DataMember = enityName;
                    dataGridView1.DataSource = bindingSource1;
                    table = new DataTable();
                    switch (enityName)
                    {
                        case users:
                            table = this.CinemaCenter.Tables[users];
                            break;
                        case role:
                            table = this.CinemaCenter.Tables[role];
                            break;
                        case dviz_bilet:
                            table = this.CinemaCenter.Tables[dviz_bilet];
                            break;
                        case bilet:
                            table = this.CinemaCenter.Tables[bilet];
                            break;
                        case seans:
                            table = this.CinemaCenter.Tables[seans];
                            break;
                        case mesta:
                            table = this.CinemaCenter.Tables[mesta];
                            break;
                        case zal:
                            table = this.CinemaCenter.Tables[zal];
                            break;
                        case films:
                            table = this.CinemaCenter.Tables[films];
                            break;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (script.Text != "")
                {
                    conn.Open();
                    command = new MySqlCommand(script.Text, conn);
                    adapter = new MySqlDataAdapter(command);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Нет запроса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }
    }
}
